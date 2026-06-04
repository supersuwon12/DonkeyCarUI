using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer; // WinForms Timer 명시적 사용

namespace DonkeyCarUI
{
    public partial class Form1 : Form
    {
        private List<FrameData> _records = new List<FrameData>();
        private string _baseDirectory = string.Empty;

        // 재생 관련 변수
        private Timer _playbackTimer = new Timer();
        private bool _isPlaying = false;
        private int _playbackSpeed = 1;

        // 필터 및 선택 지점 관련 변수
        private int _startIndex = -1;
        private int _endIndex = -1;
        private List<FrameData> _originalRecords = new List<FrameData>();
        private const int MaxHistory = 10;
        private readonly Stack<HistoryState> _undoStack = new Stack<HistoryState>();
        private readonly Stack<HistoryState> _redoStack = new Stack<HistoryState>();
        private string _trashDirectory = string.Empty;
        private string _catalogPath = string.Empty;
        private bool _isMultiJsonFormat = false;
        private string[] _multiJsonFiles = Array.Empty<string>();
        private double _brightness = 0;
        private double _blurAmount = 0;
        private bool _invertColors = false;
        private readonly object _imageLock = new object();

        // 학습 관련 변수
        private Process? _trainProcess;
        private string _donkeyProjectPath = string.Empty;
        private string _modelSaveDirectory = string.Empty;
        private string _transferModelPath = string.Empty;
        private string _wslProjectPath = "/home/geonho0927/mysim";
        private string _condaEnvName = "e2e_env";

        public Form1()
        {
            InitializeComponent();

            // Setup Event Handlers
            btnLoadData.Click += BtnLoadData_Click;
            tbFrameSlider.Scroll += TbFrameSlider_Scroll;
            tbFrameSlider.ValueChanged += TbFrameSlider_ValueChanged;

            // 재생 컨트롤 이벤트 연결
            btnPlay.Click += BtnPlay_Click;
            button2.Click += BtnPlay_Click;           // button2도 재생/일시정지
            btnPrevFrame.Click += BtnPrevFrame_Click;
            btnNextFrame.Click += BtnNextFrame_Click;
            cmbSpeed.SelectedIndexChanged += CmbSpeed_SelectedIndexChanged;
            cmbSpeed.SelectedIndex = 0; // 기본 1.0x

            // 지점 설정, 필터, 삭제, 학습 이벤트 연결
            btnSetPoint1.Click += BtnSetPoint1_Click;
            btnSetPoint2.Click += BtnSetPoint2_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRestore.Click += BtnRestore_Click;
            btnFilter.Click += BtnFilter_Click;
            btnTestModel.Click += BtnTestModel_Click;
            button1.Click += (_, __) => ResetSelection(); // 선택 취소 버튼

            // 데이터 리스트 클릭 → 해당 프레임으로 이동
            lstDataList.SelectedIndexChanged += LstDataList_SelectedIndexChanged;
            lstDataList.DrawMode = DrawMode.OwnerDrawFixed;
            lstDataList.DrawItem += LstDataList_DrawItem;
            lstDataList.ItemHeight = 18;

            // textBox1 기본값 설정
            textBox1.Text = "1";

            // 차트 초기화 설정
            InitializeChart();
            if (chartData != null)
            {
                chartData.MouseClick += ChartData_MouseClick;
            }

            _playbackTimer.Interval = 33;
            _playbackTimer.Tick += PlaybackTimer_Tick;

            // 고급 기능 버튼 추가 (정지 데이터 제거, 데이터 스무딩)
            Button btnRemoveStopped = new Button { Text = "정지 데이터 제거", Left = 12, Top = 570, Width = 120, Height = 25, ForeColor = Color.White, BackColor = Color.DarkSlateGray, FlatStyle = FlatStyle.Flat };
            btnRemoveStopped.Click += BtnRemoveStopped_Click;
            btnRemoveStopped.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.Controls.Add(btnRemoveStopped);

            Button btnSmoothData = new Button { Text = "조향 스무딩(MA)", Left = 137, Top = 570, Width = 120, Height = 25, ForeColor = Color.White, BackColor = Color.DarkSlateBlue, FlatStyle = FlatStyle.Flat };
            btnSmoothData.Click += BtnSmoothData_Click;
            btnSmoothData.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.Controls.Add(btnSmoothData);

            // 키보드 단축키 지원 활성화
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            ConfigureUiMappings();
            InitializeTrainingTab();
        }

        private void DeleteSelectedRange()
        {
            if (_startIndex == -1 || _endIndex == -1) return;
            BtnDelete_Click(this, EventArgs.Empty);
        }

        private async void UndoLastAction()
        {
            if (_undoStack.Count <= 1) return;
            var current = _undoStack.Pop();
            _redoStack.Push(current);
            var previous = _undoStack.Peek();

            _records = previous.Records.Select(r => r.Clone()).ToList();
            await RestoreDeletedFilesAsync(current.DeletedFiles);
            await SyncCatalogAsync(_records);

            tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
            tbFrameSlider.Value = 0;
            UpdateDataListText();
            ResetSelection();
            if (_records.Count > 0) UpdateUIForFrame(tbFrameSlider.Value);
            AddLog($"Undo: {current.Reason}", Color.DarkOrange);
        }

        private async void RedoLastAction()
        {
            if (_redoStack.Count == 0) return;
            var redo = _redoStack.Pop();

            _records = redo.Records.Select(r => r.Clone()).ToList();
            await SyncCatalogAsync(_records);
            _undoStack.Push(redo);

            tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
            tbFrameSlider.Value = 0;
            UpdateDataListText();
            ResetSelection();
            if (_records.Count > 0) UpdateUIForFrame(tbFrameSlider.Value);
            AddLog($"Redo: {redo.Reason}", Color.SlateBlue);
        }

        private List<FrameData> LoadRecords(string baseDir, bool isMultiJsonFormat, string[] multiJsonFiles)
        {
            var loaded = new List<FrameData>();

            if (!isMultiJsonFormat)
            {
                // ★ 핵심: baseDir 안의 모든 .catalog 파일을 번호 순으로 전부 읽음
                var allCatalogs = Directory.GetFiles(baseDir, "*.catalog")
                    .OrderBy(f =>
                    {
                        string digits = new string(
                            Path.GetFileNameWithoutExtension(f).Where(char.IsDigit).ToArray());
                        return int.TryParse(digits, out int num) ? num : 999999;
                    }).ToList();

                foreach (var catalogFile in allCatalogs)
                {
                    foreach (var line in File.ReadAllLines(catalogFile))
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        if (!line.Contains("cam/image_array")) continue; // 메타라인 skip
                        try
                        {
                            var record = JsonSerializer.Deserialize<FrameData>(line);
                            if (record != null && !string.IsNullOrEmpty(record.ImagePath))
                                loaded.Add(record);
                        }
                        catch { }
                    }
                }
            }
            else
            {
                // 멀티 JSON 포맷: 숫자가 포함된 파일만, 번호 순 정렬
                var sortedFiles = multiJsonFiles
                    .Where(f => Path.GetFileNameWithoutExtension(f).Any(char.IsDigit))
                    .OrderBy(f =>
                    {
                        string digits = new string(
                            Path.GetFileNameWithoutExtension(f).Where(char.IsDigit).ToArray());
                        return int.TryParse(digits, out int n) ? n : int.MaxValue;
                    }).ToList();

                foreach (var file in sortedFiles)
                {
                    try
                    {
                        string content = File.ReadAllText(file);
                        if (!content.Contains("cam/image_array")) continue;
                        var record = JsonSerializer.Deserialize<FrameData>(content);
                        if (record != null && !string.IsNullOrEmpty(record.ImagePath))
                        {
                            record.SourceFileName = Path.GetFileName(file);
                            loaded.Add(record);
                        }
                    }
                    catch { }
                }
            }

            return loaded;
        }

        // 타이머 설정 (약 30 FPS 기준 = 33ms)


        private void ConfigureUiMappings()
        {
            _trashDirectory = string.IsNullOrEmpty(_baseDirectory)
                ? string.Empty
                : Path.Combine(_baseDirectory, ".trash");

            if (listView1 != null)
            {
                listView1.View = View.Details;
                listView1.Columns.Clear();
                listView1.Columns.Add("타임라인", -2, HorizontalAlignment.Left);
                listView1.FullRowSelect = true;
            }

            if (trackBar4 != null)
            {
                trackBar4.Minimum = -100;
                trackBar4.Maximum = 100;
                trackBar4.Value = 0;
                trackBar4.ValueChanged += (_, __) =>
                {
                    _brightness = trackBar4.Value / 100.0;
                    UpdateUIForFrame(tbFrameSlider.Value);
                };
            }

            if (trackBar5 != null)
            {
                trackBar5.Minimum = 0;
                trackBar5.Maximum = 100;
                trackBar5.Value = 0;
                trackBar5.ValueChanged += (_, __) =>
                {
                    _blurAmount = trackBar5.Value / 100.0;
                    UpdateUIForFrame(tbFrameSlider.Value);
                };
            }

            if (checkBox2 != null)
            {
                checkBox2.CheckedChanged += (_, __) =>
                {
                    _invertColors = checkBox2.Checked;
                    UpdateUIForFrame(tbFrameSlider.Value);
                };
            }
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (_records.Count == 0) return;

            // 스페이스바: 재생/일시정지
            if (e.KeyCode == Keys.Space)
            {
                BtnPlay_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // 방향키 왼쪽: 이전 프레임
            else if (e.KeyCode == Keys.Left)
            {
                BtnPrevFrame_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // 방향키 오른쪽: 다음 프레임
            else if (e.KeyCode == Keys.Right)
            {
                BtnNextFrame_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // Page Up/Down: 10 프레임 이동
            else if (e.KeyCode == Keys.PageUp)
            {
                tbFrameSlider.Value = Math.Max(tbFrameSlider.Minimum, tbFrameSlider.Value - 10);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                tbFrameSlider.Value = Math.Min(tbFrameSlider.Maximum, tbFrameSlider.Value + 10);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // Delete 키: 현재 선택 범위 데이터 삭제
            else if (e.KeyCode == Keys.Delete && _startIndex != -1 && _endIndex != -1)
            {
                BtnDelete_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                UndoLastAction();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                RedoLastAction();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // A 키: 이상치(Anomaly) 데이터 자동 강조
            else if (e.KeyCode == Keys.A)
            {
                DetectAndHighlightAnomalies();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void BtnLoadData_Click(object? sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Donkeycar 데이터 폴더를 선택하세요.";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    _baseDirectory = fbd.SelectedPath;
                    lblPath.Text = _baseDirectory;
                    LoadCatalogData();
                    AddLog($"경로 선택: {_baseDirectory}", Color.DimGray);
                }
            }
        }

        private async void LoadCatalogData()
        {
            _records.Clear();
            _isMultiJsonFormat = false;
            _multiJsonFiles = Array.Empty<string>();

            _trashDirectory = Path.Combine(_baseDirectory, ".trash");
            Directory.CreateDirectory(_trashDirectory);

            // catalog_0.catalog 우선 탐색, 없으면 .catalog 파일 전체 수집
            string defaultCatalog = Path.Combine(_baseDirectory, "catalog_0.catalog");
            var allCatalogFiles = Directory.GetFiles(_baseDirectory, "*.catalog")
                .OrderBy(f =>
                {
                    string n = new string(
                        Path.GetFileNameWithoutExtension(f).Where(char.IsDigit).ToArray());
                    return int.TryParse(n, out int num) ? num : 0;
                }).ToArray();

            if (allCatalogFiles.Length > 0)
            {
                // catalog_0.catalog 을 기준 경로로 지정하되,
                // LoadRecords 내에서 모든 .catalog 파일을 순서대로 읽음
                _catalogPath = File.Exists(defaultCatalog) ? defaultCatalog : allCatalogFiles[0];
                _isMultiJsonFormat = false;
            }
            else
            {
                // Tub v2 포맷: 개별 JSON 파일 다수
                _multiJsonFiles = Directory.GetFiles(_baseDirectory, "*.json");
                if (_multiJsonFiles.Length > 0)
                {
                    _catalogPath = string.Empty;
                    _isMultiJsonFormat = true;
                }
                else
                {
                    MessageBox.Show(
                        "데이터 파일을 찾을 수 없습니다.\n(.catalog 파일 또는 다수의 .json 파일이 필요합니다)",
                        "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                var loaded = await Task.Run(() => LoadRecords(_baseDirectory, _isMultiJsonFormat, _multiJsonFiles));
                _records = loaded;
                _originalRecords = _records.Select(r => r.Clone()).ToList();
                ResetHistory();

                if (_records.Count > 0)
                {
                    tbFrameSlider.Minimum = 0;
                    tbFrameSlider.Maximum = _records.Count - 1;
                    tbFrameSlider.Value = 0;
                    UpdateUIForFrame(0);
                }

                UpdateDataListText();
                ResetSelection();
                UpdateListBox();
                AddLog($"데이터 로드 완료: {_records.Count}장 (catalog {allCatalogFiles.Length}개)", Color.ForestGreen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로딩 중 오류 발생: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog($"데이터 로딩 실패: {ex.Message}", Color.OrangeRed);
            }
        }

        private void TbFrameSlider_Scroll(object? sender, EventArgs e)
        {
            UpdateUIForFrame(tbFrameSlider.Value);
        }

        private void TbFrameSlider_ValueChanged(object? sender, EventArgs e)
        {
            UpdateUIForFrame(tbFrameSlider.Value);
        }

        private void UpdateUIForFrame(int index)
        {
            if (index < 0 || index >= _records.Count) return;

            var record = _records[index];
            lblFrameIndex.Text = $"{index + 1} / {_records.Count}";

            // Update Labels
            lblSteeringValue.Text = record.Angle.ToString("F2");
            lblThrottleValue.Text = record.Throttle.ToString("F2");

            // Update ProgressBars
            pbSteering.Value = Math.Max(0, Math.Min(100, (int)((record.Angle + 1) * 50)));
            pbThrottle.Value = Math.Max(0, Math.Min(100, (int)((record.Throttle + 1) * 50)));

            // 리스트 선택 동기화 (재생 중에는 스킵 - 성능)
            if (!_isPlaying) SyncListSelectionToSlider(index);

            // Load Image
            if (!string.IsNullOrEmpty(record.ImagePath))
            {
                string imgRelPath = record.ImagePath;
                // Donkeycar sometimes saves "cam/image_array" as just the filename or "images/xxx.jpg"
                if (imgRelPath.StartsWith("images/") || imgRelPath.StartsWith("images\\"))
                {
                    imgRelPath = imgRelPath.Substring(7);
                }

                string imgPath = Path.Combine(_baseDirectory, "images", imgRelPath);

                // Fallback root dir
                if (!File.Exists(imgPath))
                {
                    imgPath = Path.Combine(_baseDirectory, record.ImagePath);
                }

                if (File.Exists(imgPath))
                {
                    try
                    {
                        var oldImg = pbCameraView.Image;
                        using (var fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            var img = Image.FromStream(fs);
                            pbCameraView.Image = ApplyImageAdjustments(new Bitmap(img));
                        }
                        oldImg?.Dispose();
                    }
                    catch { /* Handle image load error silently for smooth sliding */ }
                }
            }
        }

        private Bitmap ApplyImageAdjustments(Bitmap source)
        {
            lock (_imageLock)
            {
                double brightness = _brightness;
                double blurAmount = _blurAmount;
                bool invert = _invertColors;

                var adjusted = new Bitmap(source.Width, source.Height);
                using (var g = Graphics.FromImage(adjusted))
                {
                    float b = (float)brightness;
                    float[][] matrixItems =
                    {
                        new float[] {1, 0, 0, 0, 0},
                        new float[] {0, 1, 0, 0, 0},
                        new float[] {0, 0, 1, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {b, b, b, 0, 1}
                    };

                    if (invert)
                    {
                        matrixItems = new float[][]
                        {
                            new float[] {-1, 0, 0, 0, 0},
                            new float[] {0, -1, 0, 0, 0},
                            new float[] {0, 0, -1, 0, 0},
                            new float[] {0, 0, 0, 1, 0},
                            new float[] {1 + b, 1 + b, 1 + b, 0, 1}
                        };
                    }

                    var cm = new System.Drawing.Imaging.ColorMatrix(matrixItems);
                    using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                    {
                        attributes.SetColorMatrix(cm);
                        g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height), 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
                    }
                }

                source.Dispose();

                if (blurAmount <= 0.01)
                {
                    return adjusted;
                }

                return ApplyBoxBlur(adjusted, (int)Math.Max(1, blurAmount * 3));
            }
        }

        private Bitmap ApplyBoxBlur(Bitmap image, int radius)
        {
            var blurred = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    int r = 0, g = 0, b = 0, count = 0;
                    for (int ix = Math.Max(0, x - radius); ix <= Math.Min(image.Width - 1, x + radius); ix++)
                    {
                        for (int iy = Math.Max(0, y - radius); iy <= Math.Min(image.Height - 1, y + radius); iy++)
                        {
                            var color = image.GetPixel(ix, iy);
                            r += color.R;
                            g += color.G;
                            b += color.B;
                            count++;
                        }
                    }
                    blurred.SetPixel(x, y, Color.FromArgb(r / count, g / count, b / count));
                }
            }
            image.Dispose();
            return blurred;
        }

        #region Playback Controls
        private void PlaybackTimer_Tick(object? sender, EventArgs e)
        {
            if (tbFrameSlider.Value < tbFrameSlider.Maximum)
            {
                // 배속에 맞춰 프레임 인덱스 증가
                int nextFrame = tbFrameSlider.Value + _playbackSpeed;
                if (nextFrame > tbFrameSlider.Maximum)
                    nextFrame = tbFrameSlider.Maximum;

                tbFrameSlider.Value = nextFrame;

                if (tbFrameSlider.Value == tbFrameSlider.Maximum)
                {
                    StopPlayback();
                }
            }
            else
            {
                StopPlayback();
            }
        }

        private int GetFrameStep()
        {
            if (int.TryParse(textBox1.Text.Trim(), out int step) && step > 0)
                return step;
            return 1;
        }

        private void BtnPlay_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;

            if (_isPlaying)
            {
                StopPlayback();
            }
            else
            {
                if (tbFrameSlider.Value == tbFrameSlider.Maximum)
                    tbFrameSlider.Value = 0;

                _isPlaying = true;
                btnPlay.Text = "⏸";
                button2.Text = "⏸";
                _playbackTimer.Start();
            }
        }

        private void StopPlayback()
        {
            _isPlaying = false;
            btnPlay.Text = "▶";
            button2.Text = "▶";
            _playbackTimer.Stop();
        }

        private void BtnPrevFrame_Click(object? sender, EventArgs e)
        {
            int step = GetFrameStep();
            tbFrameSlider.Value = Math.Max(tbFrameSlider.Minimum, tbFrameSlider.Value - step);
        }

        private void BtnNextFrame_Click(object? sender, EventArgs e)
        {
            int step = GetFrameStep();
            tbFrameSlider.Value = Math.Min(tbFrameSlider.Maximum, tbFrameSlider.Value + step);
        }

        private void CmbSpeed_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // cmbSpeed items: "1.0", "1.5", "2.0", "2.5", "3.0"
            string text = cmbSpeed.SelectedItem?.ToString() ?? "1.0";
            if (double.TryParse(text, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double speed))
            {
                // _playbackSpeed는 int(프레임 건너뛰기 수)로 사용.
                // 1.0x=1프레임, 1.5x=2프레임, 2.0x=2, 2.5x=3, 3.0x=3 건너뛰기
                _playbackSpeed = Math.Max(1, (int)Math.Round(speed));
                // 타이머 간격도 배속에 맞게 조정 (기본 33ms / speed)
                _playbackTimer.Interval = Math.Max(8, (int)(33.0 / speed));
            }
        }

        private void BtnSpeed_Click(object? sender, EventArgs e)
        {
            // 기존 토글 방식 유지 (직접 호출될 경우 대비)
            _playbackSpeed = _playbackSpeed == 1 ? 2 : (_playbackSpeed == 2 ? 4 : 1);
            cmbSpeed.Text = $"{_playbackSpeed}.0x";
        }

        private void BtnRewind_Click(object? sender, EventArgs e)
        {
            tbFrameSlider.Value = tbFrameSlider.Minimum;
        }

        private void BtnFastForward_Click(object? sender, EventArgs e)
        {
            tbFrameSlider.Value = tbFrameSlider.Maximum;
        }
        #endregion

        #region Edit and Filter Controls
        private void UpdateDataListText()
        {
            UpdateListBox();
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            if (_records.Count == 0) return;

            double avgAngle = _records.Average(r => r.Angle);
            double avgThrottle = _records.Average(r => r.Throttle);

            lblSteeringValue.Text = avgAngle.ToString("F2");
            lblThrottleValue.Text = avgThrottle.ToString("F2");
        }

        // ── lstDataList 관련 ──────────────────────────────────────────────────────

        // 슬라이더 → 리스트 동기화 중 무한루프 방지 플래그
        private bool _listSyncInProgress = false;

        /// <summary>_records 전체를 lstDataList에 다시 그립니다. 삭제/로드 후 호출.</summary>
        private void UpdateListBox()
        {
            if (lstDataList == null) return;

            _listSyncInProgress = true;
            lstDataList.BeginUpdate();
            lstDataList.Items.Clear();

            for (int i = 0; i < _records.Count; i++)
            {
                var r = _records[i];
                string fileName = Path.GetFileName(r.ImagePath);

                string imageNumber = new string(
                    Path.GetFileNameWithoutExtension(r.ImagePath)
                        .TakeWhile(char.IsDigit)
                        .ToArray());

                if (string.IsNullOrEmpty(imageNumber))
                    imageNumber = Path.GetFileNameWithoutExtension(r.ImagePath);

                lstDataList.Items.Add($"{imageNumber}  A:{r.Angle:+0.00;-0.00;0.00}  T:{r.Throttle:+0.00;-0.00;0.00}  {fileName}");
            }

            lstDataList.EndUpdate();
            _listSyncInProgress = false;

            // 현재 슬라이더 위치로 선택 동기화
            SyncListSelectionToSlider(tbFrameSlider.Value);
        }

        /// <summary>슬라이더 값에 맞춰 리스트 선택 항목을 스크롤 없이 부드럽게 이동.</summary>
        private void SyncListSelectionToSlider(int frameIndex)
        {
            if (lstDataList == null || lstDataList.Items.Count == 0) return;
            if (frameIndex < 0 || frameIndex >= lstDataList.Items.Count) return;

            _listSyncInProgress = true;
            lstDataList.SelectedIndex = frameIndex;
            lstDataList.TopIndex = Math.Max(0, frameIndex - 5); // 선택 항목이 화면 중간쯤 오도록
            _listSyncInProgress = false;
        }

        /// <summary>리스트 항목 클릭 → 슬라이더 + 이미지 이동</summary>
        private void LstDataList_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_listSyncInProgress) return; // 코드에서 바꾼 경우는 무시
            int idx = lstDataList.SelectedIndex;
            if (idx < 0 || idx >= _records.Count) return;

            _listSyncInProgress = true;
            tbFrameSlider.Value = idx;
            _listSyncInProgress = false;

            UpdateUIForFrame(idx);
        }

        /// <summary>현재 선택 행 파란색, 선택 범위(start~end) 노란색, 나머지 기본색으로 그림.</summary>
        private void LstDataList_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lstDataList.Items.Count) return;

            bool isSelected = (e.State & DrawItemState.Selected) != 0;
            bool inRange = _startIndex != -1 && _endIndex != -1
                               && e.Index >= Math.Min(_startIndex, _endIndex)
                               && e.Index <= Math.Max(_startIndex, _endIndex);

            Color backColor;
            Color foreColor;

            if (isSelected)
            {
                backColor = Color.FromArgb(51, 153, 255);   // 밝은 파랑 - 현재 프레임
                foreColor = Color.White;
            }
            else if (inRange)
            {
                backColor = Color.FromArgb(255, 230, 100);  // 노랑 - 선택 범위
                foreColor = Color.Black;
            }
            else
            {
                backColor = e.Index % 2 == 0
                    ? Color.FromArgb(30, 30, 30)
                    : Color.FromArgb(40, 40, 40);            // 짝수/홀수 줄 구분
                foreColor = Color.FromArgb(200, 200, 200);
            }

            using var bgBrush = new SolidBrush(backColor);
            e.Graphics.FillRectangle(bgBrush, e.Bounds);

            using var font = new Font("Consolas", 8.5f);
            string text = lstDataList.Items[e.Index]?.ToString() ?? string.Empty;
            e.Graphics.DrawString(text, font, new SolidBrush(foreColor),
                new PointF(e.Bounds.X + 4, e.Bounds.Y + 2));
        }

        private void AddLog(string message, Color color)
        {
            if (listView1 == null) return;

            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new Action(() => AddLog(message, color)));
                return;
            }

            var item = new ListViewItem($"[{DateTime.Now:HH:mm:ss}] {message}")
            {
                ForeColor = color
            };
            listView1.Items.Insert(0, item);
        }

        private void ResetHistory()
        {
            _undoStack.Clear();
            _redoStack.Clear();
            SaveHistorySnapshot("초기 로드");
        }

        private void SaveHistorySnapshot(string reason)
        {
            var snapshot = new HistoryState
            {
                Records = _records.Select(r => r.Clone()).ToList(),
                DeletedFiles = new List<DeletedFile>(),
                Reason = reason
            };

            _undoStack.Push(snapshot);
            // Stack<T>에는 Remove/Last가 없으므로 초과 시 배열로 재구성해 가장 오래된 항목 제거
            if (_undoStack.Count > MaxHistory)
            {
                var items = _undoStack.ToArray(); // [newest ... oldest]
                _undoStack.Clear();
                for (int i = Math.Min(items.Length, MaxHistory) - 1; i >= 0; i--)
                    _undoStack.Push(items[i]);
            }
            _redoStack.Clear();
        }

        private sealed class HistoryState
        {
            public List<FrameData> Records { get; set; } = new List<FrameData>();
            public List<DeletedFile> DeletedFiles { get; set; } = new List<DeletedFile>();
            public string Reason { get; set; } = string.Empty;
        }

        private sealed class DeletedFile
        {
            public string SourcePath { get; set; } = string.Empty;
            public string TrashPath { get; set; } = string.Empty;
        }

        private void ResetSelection()
        {
            _startIndex = -1;
            _endIndex = -1;
            UpdateRangeLabel();
        }

        private void UpdateRangeLabel()
        {
            string s = _startIndex == -1 ? "-" : _startIndex.ToString();
            string e = _endIndex == -1 ? "-" : _endIndex.ToString();
            lblRange.Text = $"[{s} , {e}]";
        }

        // ── 파일 I/O ──────────────────────────────────────────────────────────────

        /// <summary>records의 이미지를 .trash 폴더로 이동합니다. 세마포어 없이 직접 실행.</summary>
        private async Task<List<DeletedFile>> MoveFilesToTrashAsync(IEnumerable<FrameData> records)
        {
            var deleted = new List<DeletedFile>();
            if (string.IsNullOrEmpty(_baseDirectory) || string.IsNullOrEmpty(_trashDirectory))
                return deleted;

            Directory.CreateDirectory(_trashDirectory);

            await Task.Run(() =>
            {
                foreach (var record in records)
                {
                    if (string.IsNullOrEmpty(record.ImagePath)) continue;

                    // 이미지 경로 후보 계산
                    string rel = record.ImagePath;
                    if (rel.StartsWith("images/", StringComparison.OrdinalIgnoreCase) ||
                        rel.StartsWith("images\\", StringComparison.OrdinalIgnoreCase))
                        rel = rel.Substring(7);

                    string srcPath = Path.Combine(_baseDirectory, "images", rel);
                    if (!File.Exists(srcPath))
                        srcPath = Path.Combine(_baseDirectory, record.ImagePath);
                    if (!File.Exists(srcPath))
                        continue;

                    // 충돌 없는 대상 경로 생성
                    string dstPath = Path.Combine(_trashDirectory, Path.GetFileName(srcPath));
                    int attempt = 0;
                    while (File.Exists(dstPath))
                    {
                        attempt++;
                        string baseName = Path.GetFileNameWithoutExtension(srcPath);
                        string ext = Path.GetExtension(srcPath);
                        dstPath = Path.Combine(_trashDirectory, $"{baseName}_{attempt}{ext}");
                    }

                    try
                    {
                        File.Move(srcPath, dstPath);
                        deleted.Add(new DeletedFile { SourcePath = srcPath, TrashPath = dstPath });
                    }
                    catch (Exception ex)
                    {
                        // 개별 파일 실패는 무시하고 계속
                        System.Diagnostics.Debug.WriteLine($"[Trash] {srcPath} → 실패: {ex.Message}");
                    }
                }
            });

            return deleted;
        }

        /// <summary>.trash → images 복원</summary>
        private async Task RestoreDeletedFilesAsync(IEnumerable<DeletedFile> deletedFiles)
        {
            await Task.Run(() =>
            {
                foreach (var file in deletedFiles)
                {
                    if (!File.Exists(file.TrashPath)) continue;
                    try
                    {
                        if (File.Exists(file.SourcePath))
                            File.Delete(file.SourcePath);
                        File.Move(file.TrashPath, file.SourcePath);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[Restore] {file.TrashPath} → 실패: {ex.Message}");
                    }
                }
            });
        }

        /// <summary>현재 _records 상태를 catalog 파일에 동기화합니다.</summary>
        private async Task SyncCatalogAsync(List<FrameData> data)
        {
            if (string.IsNullOrEmpty(_baseDirectory)) return;

            await Task.Run(async () =>
            {
                if (_isMultiJsonFormat)
                {
                    foreach (var record in data)
                    {
                        if (string.IsNullOrEmpty(record.SourceFileName)) continue;
                        string jsonPath = Path.Combine(_baseDirectory, record.SourceFileName);
                        string json = JsonSerializer.Serialize(record);
                        await File.WriteAllTextAsync(jsonPath, json);
                    }
                }
                else
                {
                    // 모든 .catalog 파일을 번호 순으로 수집
                    var catalogFiles = Directory.GetFiles(_baseDirectory, "*.catalog")
                        .OrderBy(f =>
                        {
                            string digits = new string(
                                Path.GetFileNameWithoutExtension(f).Where(char.IsDigit).ToArray());
                            return int.TryParse(digits, out int num) ? num : 999999;
                        }).ToList();

                    if (catalogFiles.Count == 0) return;

                    // 각 catalog 파일의 원래 레코드 수 파악 (비율 계산용)
                    var origCounts = new List<int>();
                    foreach (var cf in catalogFiles)
                    {
                        int cnt = File.ReadAllLines(cf)
                            .Count(l => !string.IsNullOrWhiteSpace(l) && l.Contains("cam/image_array"));
                        origCounts.Add(Math.Max(1, cnt));
                    }
                    int totalOrig = origCounts.Sum();

                    // 비율대로 data를 각 파일에 분배
                    int idx = 0;
                    for (int fi = 0; fi < catalogFiles.Count; fi++)
                    {
                        int quota = (fi == catalogFiles.Count - 1)
                            ? data.Count - idx
                            : Math.Min(
                                (int)Math.Round((double)origCounts[fi] / totalOrig * data.Count),
                                data.Count - idx);
                        quota = Math.Max(0, quota);

                        var slice = data.Skip(idx).Take(quota).Select(r => JsonSerializer.Serialize(r)).ToArray();
                        idx += quota;

                        await File.WriteAllLinesAsync(catalogFiles[fi], slice);
                    }
                }
            });
        }

        private void BtnSetPoint1_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;
            _startIndex = tbFrameSlider.Value;
            UpdateRangeLabel();
            lstDataList?.Invalidate(); // 범위 강조 즉시 반영
            AddLog($"시작 지점 선택: {_startIndex + 1}", Color.Gray);
        }

        private void BtnSetPoint2_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;
            _endIndex = tbFrameSlider.Value;
            UpdateRangeLabel();
            lstDataList?.Invalidate(); // 범위 강조 즉시 반영
            AddLog($"끝 지점 선택: {_endIndex + 1}", Color.Gray);
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_startIndex == -1 || _endIndex == -1)
            {
                MessageBox.Show("시작 지점과 끝 지점을 먼저 설정하세요.");
                return;
            }

            int start = Math.Min(_startIndex, _endIndex);
            int end = Math.Max(_startIndex, _endIndex);
            int count = end - start + 1;

            var confirm = MessageBox.Show(
                $"프레임 {start + 1} ~ {end + 1} ({count}개)를 삭제하시겠습니까?\n이미지는 .trash 폴더로 이동됩니다.",
                "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            // UI 잠금 (중복 클릭 방지)
            btnDelete.Enabled = false;

            var toRemove = _records.Skip(start).Take(count).ToList();
            var beforeDelete = _records
                .Select(r => r.Clone())
                .ToList();
            var afterDelete = _records
                .Where((_, idx) => idx < start || idx > end)
                .Select(r => r.Clone())
                .ToList();
            _ = Task.Run(async () =>
            {
                try
                {
                    // 1) 이미지 → .trash 이동
                    var deletedFiles = await MoveFilesToTrashAsync(toRemove);

                    // 2) 메모리에서 제거 (UI 스레드에서 리스트 수정)
                    BeginInvoke(new Action(() =>
                    {
                        _records = afterDelete;

                        // 3) 히스토리 저장
                        var history = new HistoryState
                        {
                            Records = beforeDelete,
                            DeletedFiles = deletedFiles,
                            Reason = $"삭제: {start + 1}~{end + 1}"
                        };
                        _undoStack.Push(history);
                        _redoStack.Clear();

                        // 4) UI 갱신
                        tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
                        tbFrameSlider.Value = Math.Min(tbFrameSlider.Value, tbFrameSlider.Maximum);
                        UpdateDataListText();
                        ResetSelection();
                        if (_records.Count > 0) UpdateUIForFrame(tbFrameSlider.Value);
                        btnDelete.Enabled = true;
                        AddLog($"✅ 삭제 완료: {count}개 제거 → 남은 프레임 {_records.Count}장", Color.IndianRed);
                    }));

                    // 5) catalog 동기화 (백그라운드)
                    await SyncCatalogAsync(afterDelete);
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        btnDelete.Enabled = true;
                        MessageBox.Show($"삭제 중 오류 발생:\n{ex.Message}", "오류",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AddLog($"❌ 삭제 실패: {ex.Message}", Color.OrangeRed);
                    }));
                }
            });
        }

        private void BtnRestore_Click(object? sender, EventArgs e)
        {
            if (_undoStack.Count == 0)
            {
                MessageBox.Show("복원할 기록이 없습니다.");
                return;
            }

            var confirm = MessageBox.Show(
                "가장 최근 삭제를 복원하시겠습니까?",
                "복원 확인",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            var restoreState = _undoStack.Pop();

            _ = Task.Run(async () =>
            {
                try
                {
                    // 이미지 복원
                    await RestoreDeletedFilesAsync(restoreState.DeletedFiles);

                    // catalog / records 복원
                    _records = restoreState.Records
                        .Select(r => r.Clone())
                        .ToList();

                    await SyncCatalogAsync(_records);

                    BeginInvoke(new Action(() =>
                    {
                        tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
                        tbFrameSlider.Value = 0;

                        UpdateDataListText();
                        ResetSelection();

                        if (_records.Count > 0)
                            UpdateUIForFrame(0);

                        AddLog("✅ 최근 삭제 복원 완료", Color.ForestGreen);
                    }));
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show(
                            $"복원 실패:\n{ex.Message}",
                            "오류",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }));
                }
            });
        }

        private void BtnFilter_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;

            if (!double.TryParse(textBox2.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double threshold))
            {
                MessageBox.Show("올바른 숫자(예: 0.1)를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var field = comboBox1.SelectedItem?.ToString() ?? comboBox1.Text;
            var op = comboBox2.SelectedItem?.ToString() ?? comboBox2.Text;
            Func<FrameData, double> selector = field.Contains("속도") ? r => r.Throttle : r => r.Angle;

            bool Keep(FrameData r)
            {
                double v = selector(r);
                return op switch
                {
                    ">" => v > threshold,
                    "<" => v < threshold,
                    "≥" => v >= threshold,
                    "≤" => v <= threshold,
                    _ => Math.Abs(v) >= threshold
                };
            }

            var kept = _records.Where(Keep).ToList();
            var removed = _records.Where(r => !Keep(r)).ToList();

            if (kept.Count == 0)
            {
                MessageBox.Show("조건에 맞는 데이터가 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"조건 미달 {removed.Count}개를 .trash로 이동하겠습니까?\n남은 프레임: {kept.Count}개",
                "필터 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            btnFilter.Enabled = false;

            _ = Task.Run(async () =>
            {
                try
                {
                    var deletedFiles = await MoveFilesToTrashAsync(removed);

                    BeginInvoke(new Action(() =>
                    {
                        _records = kept;

                        var history = new HistoryState
                        {
                            Records = _records.Select(r => r.Clone()).ToList(),
                            DeletedFiles = deletedFiles,
                            Reason = $"필터: {field} {op} {threshold}"
                        };
                        _undoStack.Push(history);
                        _redoStack.Clear();

                        tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
                        tbFrameSlider.Value = 0;
                        UpdateDataListText();
                        UpdateUIForFrame(0);
                        btnFilter.Enabled = true;
                        AddLog($"✅ 필터 완료: {kept.Count}장 유지, {removed.Count}장 제거", Color.SteelBlue);
                    }));

                    await SyncCatalogAsync(kept);
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        btnFilter.Enabled = true;
                        MessageBox.Show($"필터 중 오류:\n{ex.Message}", "오류",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            });
        }

        // 특별 기능 1: 주행 속도(Throttle)가 완전히 0(정지 상태)인 불필요한 데이터 일괄 제거
        private void BtnRemoveStopped_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;

            double epsilon = 0.01;
            var kept = _records.Where(r => Math.Abs(r.Throttle) > epsilon).ToList();
            var removed = _records.Where(r => Math.Abs(r.Throttle) <= epsilon).ToList();

            if (removed.Count == 0)
            {
                MessageBox.Show("정지 상태 데이터가 없습니다.", "알림");
                return;
            }

            var confirm = MessageBox.Show(
                $"정지 상태 {removed.Count}개를 .trash로 이동하겠습니까?\n남은 프레임: {kept.Count}개",
                "정지 데이터 제거", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            _ = Task.Run(async () =>
            {
                try
                {
                    var deletedFiles = await MoveFilesToTrashAsync(removed);

                    BeginInvoke(new Action(() =>
                    {
                        _records = kept;

                        var history = new HistoryState
                        {
                            Records = _records.Select(r => r.Clone()).ToList(),
                            DeletedFiles = deletedFiles,
                            Reason = $"정지 제거 ({removed.Count}개)"
                        };
                        _undoStack.Push(history);
                        _redoStack.Clear();

                        tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
                        tbFrameSlider.Value = 0;
                        UpdateDataListText();
                        UpdateUIForFrame(0);
                        AddLog($"✅ 정지 데이터 {removed.Count}개 제거 완료", Color.IndianRed);
                        MessageBox.Show(
                            $"정지 데이터 {removed.Count}개 제거 완료\n남은 프레임: {kept.Count}장",
                            "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));

                    await SyncCatalogAsync(kept);
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                        MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                }
            });
        }

        // 특별 기능 2: 조향각(Steering) 데이터 스무딩(Moving Average) - 극단적으로 떨리는 손떨림 보정
        private void BtnSmoothData_Click(object? sender, EventArgs e)
        {
            if (_records.Count < 5) return;

            int windowSize = 5; // 5프레임 이동 평균
            var smoothedRecords = _records.Select(r => r.Clone()).ToList();

            for (int i = 0; i < _records.Count; i++)
            {
                double sumAngle = 0;
                int count = 0;

                // 앞뒤로 windowSize/2 만큼 검사하여 평균을 냄
                for (int j = i - (windowSize / 2); j <= i + (windowSize / 2); j++)
                {
                    if (j >= 0 && j < _records.Count)
                    {
                        sumAngle += _records[j].Angle;
                        count++;
                    }
                }

                smoothedRecords[i].Angle = sumAngle / count; // 평균값으로 덮어씌움 (부드러운 주행 곡선)
            }

            _records = smoothedRecords;
            UpdateUIForFrame(tbFrameSlider.Value);
            BtnRenderGraph_Click(null, EventArgs.Empty); // 그래프 반영
            MessageBox.Show("조향각 데이터 스무딩(이동 평균 필터)이 적용되었습니다.\n그래프를 확인해보세요. 센서 노이즈가 제거되어 훨씬 더 매끄러운 커브를 그립니다.", "데이터 최적화", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_baseDirectory))
            {
                LoadCatalogData();
            }
        }
        #endregion

        #region Train (Python Interop)
        private string ConvertWindowsPathToWslPath(string windowsPath)
        {
            if (string.IsNullOrWhiteSpace(windowsPath))
                return string.Empty;

            string fullPath = Path.GetFullPath(windowsPath);
            string driveLetter = fullPath.Substring(0, 1).ToLower();
            string pathWithoutDrive = fullPath.Substring(2).Replace("\\", "/");

            return $"/mnt/{driveLetter}{pathWithoutDrive}";
        }
        private void InitializeTrainingTab()
        {
            // 모델 종류
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(new object[] { "Linear", "Behavioral" });
            comboBox3.SelectedIndex = 0;

            // 동시처리데이터수
            comboBox5.Items.Clear();
            comboBox5.Items.AddRange(new object[] { "1", "16", "32", "64", "128" });
            comboBox5.SelectedIndex = 2; // 기본 32

            // 반복학습횟수 기본값
            if (string.IsNullOrWhiteSpace(textBox5.Text))
                textBox5.Text = "10";

            // 모델 이름 기본값
            if (string.IsNullOrWhiteSpace(textBox6.Text))
                textBox6.Text = $"model_{DateTime.Now:yyyyMMdd_HHmm}";

            // 진행도 초기화
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            label33.Text = "대기 중";

            // 경로 표시 초기화
            label42.Text = "모델 저장 경로 미선택";
            label43.Text = "DonkeyCar 프로젝트 경로 미선택";

            // 버튼 이벤트 연결
            button3.Click += BtnStartTraining_Click;              // 학습 시작
            button11.Click += BtnStopTraining_Click;              // 학습 중지
            button12.Click += BtnSelectModelSavePath_Click;       // 저장 경로 선택
            button13.Click += BtnSelectDonkeyProjectPath_Click;   // 프로젝트 경로 선택
            button4.Click += BtnLoadTransferModel_Click;          // 전이학습 모델 불러오기

            button11.Enabled = false;

            SetupModelListView();
        }

        private void SetupModelListView()
        {
            if (listView2 == null) return;

            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.GridLines = true;
            listView2.Columns.Clear();
            listView2.Items.Clear();

            listView2.Columns.Add("모델이름", 130);
            listView2.Columns.Add("모델종류", 90);
            listView2.Columns.Add("사용한 데이터", 180);
            listView2.Columns.Add("수정한 날짜", 140);
            listView2.Columns.Add("주석", 220);
            listView2.Columns.Add("전이학습", 150);
        }

        private void BtnSelectDonkeyProjectPath_Click(object? sender, EventArgs e)
        {
            _wslProjectPath = "/home/geonho0927/mysim";
            _donkeyProjectPath = _wslProjectPath;

            label43.Text = _wslProjectPath;
            AddLog($"WSL DonkeyCar 프로젝트 경로 설정: {_wslProjectPath}", Color.SteelBlue);

            MessageBox.Show(
                $"WSL DonkeyCar 프로젝트 경로가 설정되었습니다.\n{_wslProjectPath}",
                "프로젝트 경로 설정",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnSelectModelSavePath_Click(object? sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            fbd.Description = "학습된 모델을 저장할 폴더를 선택하세요.";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _modelSaveDirectory = fbd.SelectedPath;
                label42.Text = _modelSaveDirectory;
                AddLog($"모델 저장 경로 설정: {_modelSaveDirectory}", Color.SteelBlue);
            }
        }

        private void BtnLoadTransferModel_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Title = "전이학습에 사용할 기존 모델을 선택하세요.";
            ofd.Filter = "Model Files (*.h5;*.keras)|*.h5;*.keras|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _transferModelPath = ofd.FileName;

                textBox7.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
                textBox8.Text = ofd.FileName;

                AddLog($"전이학습 모델 선택: {_transferModelPath}", Color.SteelBlue);
            }
        }

        private void BtnStartTraining_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_baseDirectory))
            {
                MessageBox.Show("먼저 학습에 사용할 데이터 폴더를 불러오세요.");
                return;
            }

            if(string.IsNullOrEmpty(_wslProjectPath))
{
                MessageBox.Show("DonkeyCar 프로젝트 경로를 먼저 설정하세요.");
                return;
            }

            if (string.IsNullOrEmpty(_modelSaveDirectory))
            {
                MessageBox.Show("모델 저장 경로를 먼저 선택하세요.");
                return;
            }

            string modelName = textBox6.Text.Trim();
            if (string.IsNullOrEmpty(modelName))
                modelName = $"model_{DateTime.Now:yyyyMMdd_HHmmss}";

            string modelKind = comboBox3.SelectedItem?.ToString() ?? "Linear";

            // DonkeyCar 학습 타입 변환
            string donkeyType = modelKind == "Behavioral" ? "categorical" : "linear";

            if (!int.TryParse(textBox5.Text.Trim(), out int epochs) || epochs <= 0)
                epochs = 10;

            if (!int.TryParse(comboBox5.SelectedItem?.ToString() ?? "32", out int batchSize))
                batchSize = 32;

            Directory.CreateDirectory(_modelSaveDirectory);

            string modelPath = Path.Combine(_modelSaveDirectory, modelName + ".h5");

            string wslTubPath = ConvertWindowsPathToWslPath(_baseDirectory);
            string wslModelPath = ConvertWindowsPathToWslPath(modelPath);

            // DonkeyCar v5.3.0 기준: train.py + --tubs 사용
            string wslCommand =
                $"source ~/miniconda3/etc/profile.d/conda.sh && " +
                $"conda activate {_condaEnvName} && " +
                $"cd \"{_wslProjectPath}\" && " +
                $"python train.py " +
                $"--tubs \"{wslTubPath}\" " +
                $"--model \"{wslModelPath}\" " +
                $"--type linear";

            // 전이학습은 현재 DonkeyCar v5.3.0 train.py 옵션 확인 후 연결 필요
            if (!string.IsNullOrEmpty(_transferModelPath))
            {
                AddLog("전이학습 모델은 선택되었지만 현재 train.py 명령에는 자동 반영하지 않습니다.", Color.DarkOrange);
            }

            progressBar1.Value = 0;
            label33.Text = "학습 준비 중...";

            button3.Enabled = false;
            button11.Enabled = true;

            var psi = new ProcessStartInfo
            {
                FileName = "wsl",
                Arguments = $"bash -lc \"{wslCommand}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            _trainProcess = new Process();
            _trainProcess.StartInfo = psi;
            _trainProcess.EnableRaisingEvents = true;

            _trainProcess.OutputDataReceived += (s, ev) =>
            {
                if (!string.IsNullOrEmpty(ev.Data))
                    HandleTrainingOutput(ev.Data);
            };

            _trainProcess.ErrorDataReceived += (s, ev) =>
            {
                if (!string.IsNullOrEmpty(ev.Data))
                    HandleTrainingOutput(ev.Data);
            };

            _trainProcess.Exited += (s, ev) =>
            {
                BeginInvoke(new Action(() =>
                {
                    button3.Enabled = true;
                    button11.Enabled = false;

                    if (_trainProcess != null && _trainProcess.ExitCode == 0)
                    {
                        progressBar1.Value = 100;
                        label33.Text = "학습 완료";

                        AddTrainedModelToList(
                            modelName,
                            modelKind,
                            _baseDirectory,
                            textBox3.Text.Trim(),
                            string.IsNullOrEmpty(_transferModelPath)
                                ? "없음"
                                : Path.GetFileName(_transferModelPath));

                        AddLog($"✅ 학습 완료: {modelName}", Color.ForestGreen);
                        MessageBox.Show("학습이 완료되었습니다.");
                    }
                    else
                    {
                        int exitCode = _trainProcess?.ExitCode ?? -999;
                        label33.Text = $"학습 종료됨 (ExitCode: {exitCode})";
                        AddLog($"❌ 학습 비정상 종료 - ExitCode: {exitCode}", Color.OrangeRed);
                        AddLog("명령어, DonkeyCar 경로, Python 환경, 옵션 인식 여부를 확인하세요.", Color.OrangeRed);
                    }
                }));
            };

            try
            {
                AddLog($"학습 시작(WSL): {wslCommand}", Color.SteelBlue);

                _trainProcess.Start();
                _trainProcess.BeginOutputReadLine();
                _trainProcess.BeginErrorReadLine();

                label33.Text = "학습 중...";
            }
            catch (Exception ex)
            {
                button3.Enabled = true;
                button11.Enabled = false;
                label33.Text = "실행 실패";

                MessageBox.Show(
                    $"학습 실행 실패:\n{ex.Message}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnStopTraining_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_trainProcess != null && !_trainProcess.HasExited)
                {
                    _trainProcess.Kill(true);
                    label33.Text = "학습 중지됨";
                    AddLog("학습 프로세스 중지", Color.OrangeRed);
                }

                button3.Enabled = true;
                button11.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"학습 중지 실패:\n{ex.Message}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void HandleTrainingOutput(string line)
        {
            BeginInvoke(new Action(() =>
            {
                AddLog(line, Color.DimGray);

                // 예: Epoch 3/10 형태 감지
                if (line.Contains("Epoch") && line.Contains("/"))
                {
                    string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var part in parts)
                    {
                        if (!part.Contains("/")) continue;

                        var nums = part.Split('/');
                        if (nums.Length != 2) continue;

                        if (int.TryParse(nums[0], out int current) &&
                            int.TryParse(nums[1], out int total) &&
                            total > 0)
                        {
                            int percent = Math.Max(0, Math.Min(100, current * 100 / total));
                            progressBar1.Value = percent;
                            label33.Text = $"학습 중... {percent}% ({current}/{total})";
                            return;
                        }
                    }
                }

                if (progressBar1.Value == 0)
                {
                    progressBar1.Value = 5;
                    label33.Text = "학습 중...";
                }
            }));
        }

        private void AddTrainedModelToList(
            string modelName,
            string modelKind,
            string dataPath,
            string memo,
            string transferModel)
        {
            if (listView2 == null) return;

            var item = new ListViewItem(modelName);
            item.SubItems.Add(modelKind);
            item.SubItems.Add(Path.GetFileName(dataPath));
            item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            item.SubItems.Add(memo);
            item.SubItems.Add(transferModel);

            listView2.Items.Add(item);
        }

        private void AppendLog(string message)
        {
            AddLog(message, Color.DimGray);
        }

        #endregion


        #region Extended Features (Graph & Test)
        private void InitializeChart()
        {
            if (chartData == null)
            {
                return;
            }
            chartData.Series.Clear();
            var seriesSteering = new System.Windows.Forms.DataVisualization.Charting.Series("Steering");
            seriesSteering.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            seriesSteering.Color = Color.Blue;

            var seriesThrottle = new System.Windows.Forms.DataVisualization.Charting.Series("Throttle");
            seriesThrottle.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            seriesThrottle.Color = Color.Red;

            chartData.Series.Add(seriesSteering);
            chartData.Series.Add(seriesThrottle);
        }

        private void BtnRenderGraph_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0 || chartData == null) return;

            chartData.Series["Steering"].Points.Clear();
            chartData.Series["Throttle"].Points.Clear();

            // 너무 많은 데이터가 있으면 차트가 멈추므로 샘플링 처리 (최대 1000개 정도만)
            int step = Math.Max(1, _records.Count / 1000);

            for (int i = 0; i < _records.Count; i += step)
            {
                chartData.Series["Steering"].Points.AddXY(i, _records[i].Angle);
                chartData.Series["Throttle"].Points.AddXY(i, _records[i].Throttle);
            }
        }

        private void BtnTestModel_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("모델 테스트 기능 (5단계) - Python 연동 (예: drive.py 실행 등)\n향후 환경에 맞게 명령어 연동이 필요합니다.");
            // Example process call:
            // Process.Start("python", "manage.py drive --model models/mypilot.h5");
        }

        private void ChartData_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_records.Count == 0 || chartData == null) return;

            var hit = chartData.HitTest(e.X, e.Y);
            if (hit.ChartElementType == System.Windows.Forms.DataVisualization.Charting.ChartElementType.DataPoint)
            {
                var dp = hit.Series.Points[hit.PointIndex];
                int frameIndex = (int)dp.XValue;

                if (frameIndex >= 0 && frameIndex <= tbFrameSlider.Maximum)
                {
                    tbFrameSlider.Value = frameIndex;
                    UpdateUIForFrame(frameIndex);
                }
            }
        }

        private void DetectAndHighlightAnomalies()
        {
            if (_records.Count == 0 || chartData == null) return;

            // A simple threshold for anomalies (e.g. extreme values where Steering is > 0.8 or Throttle is negative/erratic suddenly)
            int anomalyCount = 0;
            foreach (var pt in chartData.Series["Steering"].Points)
            {
                pt.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
            }

            for (int i = 0; i < chartData.Series["Steering"].Points.Count; i++)
            {
                var dp = chartData.Series["Steering"].Points[i];
                if (Math.Abs(dp.YValues[0]) >= 0.8)
                {
                    dp.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                    dp.MarkerColor = Color.Magenta;
                    dp.MarkerSize = 8;
                    anomalyCount++;
                }
            }

            if (anomalyCount > 0)
            {
                MessageBox.Show($"이상데이터(조향각 0.8 이상) {anomalyCount}개가 차트에 하이라이트 되었습니다!\n해당 지점을 클릭하여 바로 확인할 수 있습니다.", "이상 감지", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("감지된 이상치 데이터가 없습니다.", "이상 감지", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion




    }
}