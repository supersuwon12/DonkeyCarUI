using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;
using Timer = System.Windows.Forms.Timer; // WinForms Timer 명시적 사용

namespace DonkeyCarUI
{
    public partial class DataManager : Form
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

        // 학습 결과 그래프/지표
        private Chart? chartTrainingLoss;
        private int _currentEpoch = 0;
        private int _totalEpochs = 0;
        private double _lastLoss = double.NaN;
        private double _lastValLoss = double.NaN;
        private double _bestLoss = double.MaxValue;
        private int _bestEpoch = 0;

        // 학습 미리보기 탭
        private readonly List<FrameData> _previewRecords = new List<FrameData>();
        private string _previewBaseDirectory = string.Empty;
        private string _previewModelPath = string.Empty;
        private readonly Timer _previewTimer = new Timer();
        private bool _isPreviewPlaying = false;
        private int _previewPlaybackSpeed = 1;
        private double _previewBrightness = 0;
        private double _previewBlurAmount = 0;
        private bool _previewInvertColors = false;

        private readonly Dictionary<int, Image> _timelineThumbCache = new Dictionary<int, Image>();

        private string _catalogBackupDirectory = string.Empty;

        private double _actualSteering = 0;
        private double _actualThrottle = 0;
        private double _predictedSteering = 0;
        private double _predictedThrottle = 0;

        private string _leftModelPath = string.Empty;
        private string _rightModelPath = string.Empty;

        private double _originalSteering = 0;
        private double _originalThrottle = 0;

        private double _leftPredictedSteering = 0;
        private double _leftPredictedThrottle = 0;

        private double _rightPredictedSteering = 0;
        private double _rightPredictedThrottle = 0;

        private bool _leftPredictionReady = false;
        private bool _rightPredictionReady = false;

        private readonly Stack<List<int>> _deleteMarkHistory = new();

        private readonly Dictionary<int, (double steering, double throttle)> _leftPredictionCache = new();
        private readonly Dictionary<int, (double steering, double throttle)> _rightPredictionCache = new();

        private readonly HashSet<int> _deletedIndices = new();

        private string _trainingDataDirectory = string.Empty;

        public DataManager()
        {
            InitializeComponent();

            try
            {
                string logPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "training_log.txt");

                File.WriteAllText(logPath, "");
            }
            catch
            {
            }
            // 탭 헤더 색상
            tabControl1.DrawItem += (sender, e) =>
            {
                // [1단계] 첫 번째 탭을 그리는 순간, 상단 헤더 영역 전체(우측 여백 포함)를 Lavender로 싹 청소합니다.
                if (e.Index == 0)
                {
                    Rectangle headerArea = tabControl1.ClientRectangle;
                    headerArea.Height = tabControl1.DisplayRectangle.Top;
                    e.Graphics.FillRectangle(Brushes.Lavender, headerArea);
                }

                // [2단계] 핵심 솔루션: 현재 인덱스 하나만 그리지 말고, 
                // 어떤 탭이 호출되었든 간에 '모든 탭(0번, 1번, 2번)'을 동시에 강제로 다 그려버립니다.
                for (int i = 0; i < tabControl1.TabCount; i++)
                {
                    Rectangle rect = tabControl1.GetTabRect(i);

                    // 탭 버튼 배경을 Lavender로 도색
                    e.Graphics.FillRectangle(Brushes.Lavender, rect);

                    // 탭 버튼 텍스트를 MidnightBlue로 그리기
                    string tabText = tabControl1.TabPages[i].Text;
                    using (Brush textBrush = new SolidBrush(Color.MidnightBlue))
                    {
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        e.Graphics.DrawString(tabText, e.Font, textBrush, rect, sf);
                    }
                }
            };
            //탭 헤더 색상

            // Setup Event Handlers
            btnLoadData.Click += BtnLoadData_Click;
            tbFrameSlider1.Scroll += TbFrameSlider_Scroll;
            tbFrameSlider1.ValueChanged += TbFrameSlider_ValueChanged;

            // 재생 컨트롤 이벤트 연결
            btnPlay.Click += BtnPlay_Click;
            btnRun1.Click += BtnPlay_Click;           // button2도 재생/일시정지
            btnPrevFrame1.Click += BtnPrevFrame_Click;
            btnNextFrame1.Click += BtnNextFrame_Click;
            cmbSpeed1.SelectedIndexChanged += CmbSpeed_SelectedIndexChanged;
            cmbSpeed1.SelectedIndex = 0; // 기본 1.0x

            // 지점 설정, 필터, 삭제, 학습 이벤트 연결
            btnSetPoint1.Click += BtnSetPoint1_Click;
            btnSetPoint2.Click += BtnSetPoint2_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRestore.Click += BtnRestore_Click;
            btnFilter.Click += BtnFilter_Click;
            btnCancel.Click += (_, __) => ResetSelection(); // 선택 취소 버튼

            // 데이터 리스트 클릭 → 해당 프레임으로 이동
            lstDataList.SelectedIndexChanged += LstDataList_SelectedIndexChanged;
            lstDataList.DrawMode = DrawMode.OwnerDrawFixed;
            lstDataList.DrawItem += LstDataList_DrawItem;
            lstDataList.ItemHeight = 18;

            // textBox1 기본값 설정
            txtFrmMvm1.Text = "1";

            _playbackTimer.Interval = 33;
            _playbackTimer.Tick += PlaybackTimer_Tick;

            btnRemoveStopped.Click += BtnRemoveStopped_Click;
            btnSmoothData.Click += BtnSmoothData_Click;

            // 키보드 단축키 지원 활성화
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            pictureBox2.Paint += PictureBox2_Paint;
            pictureBox1.Paint += PictureBox1_Paint;
            button1.Click += BtnLoadPreviewData_Click;
            btnRawData.Click += BtnLoadLeftModel_Click;
            btn.Click += BtnLoadRightModel_Click;

            btnSave.Click += BtnSave_Click;

            btnLoadData2.Click += BtnLoadTrainingData_Click;

            tbFrameSlider2.PreviewKeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                    e.IsInputKey = true;
            };

            ConfigureUiMappings();
            SetupTimelinePanel();
            InitializeTrainingTab();
            SetupPreviewTab();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (tabControl1.SelectedTab == tabPage3)
            {
                if (keyData == Keys.Space)
                {
                    btnRun2.PerformClick();
                    return true;
                }

                if (keyData == Keys.Left)
                {
                    btnPrevFrame2.PerformClick();
                    return true;
                }

                if (keyData == Keys.Right)
                {
                    btnNextFrame2.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void BtnLoadTrainingData_Click(object? sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            fbd.Description = "학습에 사용할 DonkeyCar 데이터 폴더를 선택하세요.";

            if (fbd.ShowDialog() != DialogResult.OK) return;

            _trainingDataDirectory = fbd.SelectedPath;
            lblDataPath.Text = _trainingDataDirectory;

            AddLog($"학습 데이터 경로 선택: {_trainingDataDirectory}", Color.SteelBlue);
        }
        private void EnableDoubleBuffer(Control control)
        {
            typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance
            )?.SetValue(control, true, null);
        }
        private void SetupTimelinePanel()
        {
            if (panelTimeline == null) return;

            EnableDoubleBuffer(panelTimeline);

            panelTimeline.Paint += PanelTimeline_Paint;
            panelTimeline.MouseClick += PanelTimeline_MouseClick;
            panelTimeline.Resize += (_, __) => panelTimeline.Invalidate();
        }
        private void PanelTimeline_Paint(object? sender, PaintEventArgs e)
        {
            if (_records.Count == 0) return;

            int width = panelTimeline.Width;
            int height = panelTimeline.Height;

            if (width <= 0 || height <= 0) return;

            int thumbCount = Math.Max(1, width / 80);
            int thumbWidth = width / thumbCount;
            int thumbHeight = height;

            for (int slot = 0; slot < thumbCount; slot++)
            {
                int recordIndex = (int)Math.Round((double)slot / Math.Max(1, thumbCount - 1) * Math.Max(0, _records.Count - 1));
                recordIndex = Math.Clamp(recordIndex, 0, _records.Count - 1);

                Rectangle rect = new Rectangle(slot * thumbWidth, 0, thumbWidth, thumbHeight);

                Image? img = GetTimelineThumbnail(recordIndex, thumbWidth, thumbHeight);
                if (img != null)
                {
                    e.Graphics.DrawImage(img, rect);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.DimGray, rect);
                }
            }
            foreach (int deletedIndex in _deletedIndices)
            {
                if (deletedIndex < 0 || deletedIndex >= _records.Count)
                    continue;

                int startX = (int)Math.Round(
                    (double)deletedIndex /
                    Math.Max(1, _records.Count - 1) *
                    (width - 1));

                int endX = (int)Math.Round(
                    (double)(deletedIndex + 1) /
                    Math.Max(1, _records.Count - 1) *
                    (width - 1));

                using var redBrush = new SolidBrush(Color.FromArgb(30, Color.Red));

                e.Graphics.FillRectangle(
                    redBrush,
                    startX,
                    0,
                    Math.Max(2, endX - startX),
                    height);
            }
            if (_startIndex != -1 && _endIndex != -1)
            {
                int start = Math.Min(_startIndex, _endIndex);
                int end = Math.Max(_startIndex, _endIndex);

                int startX = (int)Math.Round((double)start / Math.Max(1, _records.Count - 1) * (width - 1));
                int endX = (int)Math.Round((double)end / Math.Max(1, _records.Count - 1) * (width - 1));

                using var brush = new SolidBrush(Color.FromArgb(90, Color.Yellow));
                e.Graphics.FillRectangle(
                    brush,
                    startX,
                    0,
                    Math.Max(1, endX - startX),
                    height
                );
            }
            int currentX = (int)Math.Round((double)tbFrameSlider1.Value / Math.Max(1, _records.Count - 1) * (width - 1));

            using (var pen = new Pen(Color.ForestGreen, 3))
            {
                e.Graphics.DrawLine(pen, currentX, 0, currentX, height);
            }
        }
        private Image? GetTimelineThumbnail(int recordIndex, int width, int height)
        {
            if (_timelineThumbCache.TryGetValue(recordIndex, out var cached))
                return cached;

            string imgPath = GetImageFullPath(_records[recordIndex]);
            if (!File.Exists(imgPath)) return null;

            try
            {
                using var fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var original = Image.FromStream(fs);

                Bitmap thumb = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(thumb))
                {
                    g.DrawImage(original, new Rectangle(0, 0, width, height));
                }

                _timelineThumbCache[recordIndex] = thumb;
                return thumb;
            }
            catch
            {
                return null;
            }
        }
        private void PanelTimeline_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_records.Count == 0) return;

            double ratio = (double)e.X / panelTimeline.Width;
            int index = (int)(ratio * _records.Count);
            index = Math.Clamp(index, 0, _records.Count - 1);

            tbFrameSlider1.Value = index;
            UpdateUIForFrame(index);
            panelTimeline.Invalidate();
        }
        private string GetImageFullPath(FrameData record)
        {
            return GetImageFullPath(record, _baseDirectory);
        }

        private string GetImageFullPath(FrameData record, string baseDirectory)
        {
            if (string.IsNullOrEmpty(record.ImagePath) || string.IsNullOrEmpty(baseDirectory)) return string.Empty;

            string imgRelPath = record.ImagePath;

            if (imgRelPath.StartsWith("images/") || imgRelPath.StartsWith("images\\"))
                imgRelPath = imgRelPath.Substring(7);

            string imgPath = Path.Combine(baseDirectory, "images", imgRelPath);

            if (!File.Exists(imgPath))
                imgPath = Path.Combine(baseDirectory, record.ImagePath);

            return imgPath;
        }
        private string GetImageNumberText(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return "-";

            string fileNameOnly = Path.GetFileNameWithoutExtension(imagePath);

            string digits = new string(fileNameOnly.Where(char.IsDigit).ToArray());

            return string.IsNullOrEmpty(digits) ? fileNameOnly : digits;
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

            tbFrameSlider1.Maximum = Math.Max(0, _records.Count - 1);
            tbFrameSlider1.Value = 0;
            UpdateDataListText();
            ResetSelection();
            if (_records.Count > 0) UpdateUIForFrame(tbFrameSlider1.Value);
            AddLog($"Undo: {current.Reason}", Color.DarkOrange);
        }

        private async void RedoLastAction()
        {
            if (_redoStack.Count == 0) return;
            var redo = _redoStack.Pop();

            _records = redo.Records.Select(r => r.Clone()).ToList();
            await SyncCatalogAsync(_records);
            _undoStack.Push(redo);

            tbFrameSlider1.Maximum = Math.Max(0, _records.Count - 1);
            tbFrameSlider1.Value = 0;
            UpdateDataListText();
            ResetSelection();
            if (_records.Count > 0) UpdateUIForFrame(tbFrameSlider1.Value);
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

            if (lstProcess != null)
            {
                lstProcess.View = View.Details;
                lstProcess.Columns.Clear();
                lstProcess.Columns.Add("타임라인", 3000, HorizontalAlignment.Left);
                lstProcess.FullRowSelect = true;
                lstProcess.GridLines = false;
                lstProcess.Scrollable = true;
            }

            if (tbBright != null)
            {
                tbBright.Minimum = -100;
                tbBright.Maximum = 100;
                tbBright.Value = 0;
                tbBright.ValueChanged += (_, __) =>
                {
                    _brightness = tbBright.Value / 100.0;
                    UpdateUIForFrame(tbFrameSlider1.Value);
                };
            }

            if (tbBlur != null)
            {
                tbBlur.Minimum = 0;
                tbBlur.Maximum = 100;
                tbBlur.Value = 0;
                tbBlur.ValueChanged += (_, __) =>
                {
                    _blurAmount = tbBlur.Value / 100.0;
                    UpdateUIForFrame(tbFrameSlider1.Value);
                };
            }

            if (chkActBW != null)
            {
                chkActBW.CheckedChanged += (_, __) =>
                {
                    _invertColors = chkActBW.Checked;
                    UpdateUIForFrame(tbFrameSlider1.Value);
                };
            }
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (_records.Count == 0) return;
            // 비교보기 탭일 때만
            if (tabControl1.SelectedTab == tabPage3)
            {
                if (e.KeyCode == Keys.Left)
                {
                    btnPrevFrame2.PerformClick();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    btnNextFrame2.PerformClick();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Space)
                {
                    btnRun2.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true; // 띵 소리 방지
                }

                return;
            }
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
                tbFrameSlider1.Value = Math.Max(tbFrameSlider1.Minimum, tbFrameSlider1.Value - 10);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                tbFrameSlider1.Value = Math.Min(tbFrameSlider1.Maximum, tbFrameSlider1.Value + 10);
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
                BtnRestore_Click(this, EventArgs.Empty);
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
            _deletedIndices.Clear();
            _deleteMarkHistory.Clear();
            _timelineThumbCache.Clear();
            _isMultiJsonFormat = false;
            _multiJsonFiles = Array.Empty<string>();

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
                    // 주행 데이터 관리 탭
                    tbFrameSlider1.Minimum = 0;
                    tbFrameSlider1.Maximum = _records.Count - 1;
                    tbFrameSlider1.Value = 0;
                    UpdateUIForFrame(0);
                }

                UpdateDataListText();
                ResetSelection();
                UpdateListBox();
                _timelineThumbCache.Clear();
                panelTimeline.Invalidate();
                AddLog($"데이터 로드 완료: {_records.Count}장 (catalog {allCatalogFiles.Length}개)", Color.ForestGreen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로딩 중 오류 발생: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AddLog($"데이터 로딩 실패: {ex.Message}", Color.OrangeRed);
            }
        }
        private async Task BackupCatalogFilesAsync(string reason)
        {
            if (string.IsNullOrEmpty(_baseDirectory)) return;

            await Task.Run(() =>
            {
                Directory.CreateDirectory(_catalogBackupDirectory);

                string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupDir = Path.Combine(_catalogBackupDirectory, $"{stamp}_{reason}");
                Directory.CreateDirectory(backupDir);

                foreach (var file in Directory.GetFiles(_baseDirectory, "*.catalog"))
                {
                    string dest = Path.Combine(backupDir, Path.GetFileName(file));
                    File.Copy(file, dest, true);
                }

                foreach (var file in Directory.GetFiles(_baseDirectory, "*.manifest"))
                {
                    string dest = Path.Combine(backupDir, Path.GetFileName(file));
                    File.Copy(file, dest, true);
                }
            });
        }
        private void TbFrameSlider_Scroll(object? sender, EventArgs e)
        {
            UpdateUIForFrame(tbFrameSlider1.Value);
        }

        private void TbFrameSlider_ValueChanged(object? sender, EventArgs e)
        {
            UpdateUIForFrame(tbFrameSlider1.Value);
        }

        private void UpdateUIForFrame(int index)
        {
            if (index < 0 || index >= _records.Count) return;

            var record = _records[index];
            lblFrmInx1.Text = $"{index + 1} / {_records.Count}";

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
            panelTimeline?.Invalidate();
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
            if (tbFrameSlider1.Value < tbFrameSlider1.Maximum)
            {
                // 배속에 맞춰 프레임 인덱스 증가
                int nextFrame = tbFrameSlider1.Value + _playbackSpeed;
                if (nextFrame > tbFrameSlider1.Maximum)
                    nextFrame = tbFrameSlider1.Maximum;

                tbFrameSlider1.Value = nextFrame;

                if (tbFrameSlider1.Value == tbFrameSlider1.Maximum)
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
            if (int.TryParse(txtFrmMvm1.Text.Trim(), out int step) && step > 0)
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
                if (tbFrameSlider1.Value == tbFrameSlider1.Maximum)
                    tbFrameSlider1.Value = 0;

                _isPlaying = true;
                btnPlay.Text = "⏸";
                btnRun1.Text = "⏸";
                _playbackTimer.Start();
            }
        }

        private void StopPlayback()
        {
            _isPlaying = false;
            btnPlay.Text = "▶";
            btnRun1.Text = "▶";
            _playbackTimer.Stop();
        }

        private void BtnPrevFrame_Click(object? sender, EventArgs e)
        {
            int step = GetFrameStep();
            tbFrameSlider1.Value = Math.Max(tbFrameSlider1.Minimum, tbFrameSlider1.Value - step);
        }

        private void BtnNextFrame_Click(object? sender, EventArgs e)
        {
            int step = GetFrameStep();
            tbFrameSlider1.Value = Math.Min(tbFrameSlider1.Maximum, tbFrameSlider1.Value + step);
        }

        private void CmbSpeed_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // cmbSpeed items: "1.0", "1.5", "2.0", "2.5", "3.0"
            string text = cmbSpeed1.SelectedItem?.ToString() ?? "1.0";
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
            cmbSpeed1.Text = $"{_playbackSpeed}.0x";
        }

        private void BtnRewind_Click(object? sender, EventArgs e)
        {
            tbFrameSlider1.Value = tbFrameSlider1.Minimum;
        }

        private void BtnFastForward_Click(object? sender, EventArgs e)
        {
            tbFrameSlider1.Value = tbFrameSlider1.Maximum;
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

                string imageNumber = GetImageNumberText(r.ImagePath);

                lstDataList.Items.Add($"{imageNumber}  A:{r.Angle:+0.00;-0.00;0.00}  T:{r.Throttle:+0.00;-0.00;0.00}  {fileName}");
            }

            lstDataList.EndUpdate();
            _listSyncInProgress = false;

            // 현재 슬라이더 위치로 선택 동기화
            SyncListSelectionToSlider(tbFrameSlider1.Value);
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
            tbFrameSlider1.Value = idx;
            _listSyncInProgress = false;

            UpdateUIForFrame(idx);
        }

        /// <summary>현재 선택 행 파란색, 선택 범위(start~end) 노란색, 나머지 기본색으로 그림.</summary>
        private void LstDataList_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lstDataList.Items.Count) return;

            bool isDeleted = _deletedIndices.Contains(e.Index);
            bool isSelected = (e.State & DrawItemState.Selected) != 0;
            bool inRange = _startIndex != -1 && _endIndex != -1
                               && e.Index >= Math.Min(_startIndex, _endIndex)
                               && e.Index <= Math.Max(_startIndex, _endIndex);

            Color backColor;
            Color foreColor;

            if (isSelected)
            {
                backColor = Color.FromArgb(51, 153, 255);
                foreColor = Color.White;
            }
            else if (isDeleted)
            {
                backColor = Color.FromArgb(180, 60, 60);
                foreColor = Color.White;
            }
            else if (inRange)
            {
                backColor = Color.FromArgb(255, 230, 100);
                foreColor = Color.Black;
            }
            else
            {
                backColor = e.Index % 2 == 0
                    ? Color.FromArgb(30, 30, 30)
                    : Color.FromArgb(40, 40, 40);
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
            if (lstProcess == null) return;

            if (lstProcess.InvokeRequired)
            {
                lstProcess.Invoke(new Action(() => AddLog(message, color)));
                return;
            }

            var item = new ListViewItem($"[{DateTime.Now:HH:mm:ss}] {message}")
            {
                ForeColor = color
            };
            lstProcess.Items.Insert(0, item);

            // 로그 파일 저장
            try
            {
                string logPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "training_log.txt");

                File.AppendAllText(
                    logPath,
                    $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            }
            catch
            {
            }
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
            panelTimeline?.Invalidate();
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
            _startIndex = tbFrameSlider1.Value;
            UpdateRangeLabel();
            lstDataList?.Invalidate(); // 범위 강조 즉시 반영
            AddLog($"시작 지점 선택: {_startIndex + 1}", Color.Gray);
            panelTimeline?.Invalidate();
        }

        private void BtnSetPoint2_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;
            _endIndex = tbFrameSlider1.Value;
            UpdateRangeLabel();
            lstDataList?.Invalidate(); // 범위 강조 즉시 반영
            AddLog($"끝 지점 선택: {_endIndex + 1}", Color.Gray);
            panelTimeline?.Invalidate();
        }
        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0)
            {
                MessageBox.Show("저장할 데이터가 없습니다.");
                return;
            }

            using var fbd = new FolderBrowserDialog();
            fbd.Description = "가공된 데이터를 저장할 폴더를 선택하세요.";

            if (fbd.ShowDialog() != DialogResult.OK) return;

            string saveDir = fbd.SelectedPath;
            string imagesDir = Path.Combine(saveDir, "images");

            btnSave.Enabled = false;

            try
            {
                await Task.Run(() =>
                {
                    // 기존 저장 폴더에 남아있는 옛 catalog/manifest 제거
                    Directory.CreateDirectory(saveDir);

                    foreach (var file in Directory.GetFiles(saveDir, "catalog_*.catalog"))
                        File.Delete(file);

                    foreach (var file in Directory.GetFiles(saveDir, "catalog_*.catalog_manifest"))
                        File.Delete(file);

                    string oldManifest = Path.Combine(saveDir, "manifest.json");
                    if (File.Exists(oldManifest))
                        File.Delete(oldManifest);

                    if (Directory.Exists(imagesDir))
                        Directory.Delete(imagesDir, true);

                    Directory.CreateDirectory(imagesDir);

                    var savedRecords = new List<FrameData>();

                    for (int i = 0; i < _records.Count; i++)
                    {
                        if (_deletedIndices.Contains(i))
                            continue;

                        var record = _records[i].Clone();
                        string srcImgPath = GetImageFullPath(record, _baseDirectory);

                        if (!File.Exists(srcImgPath))
                            continue;

                        string fileName = Path.GetFileName(srcImgPath);
                        string dstImgPath = Path.Combine(imagesDir, fileName);

                        using (var fs = new FileStream(srcImgPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var img = Image.FromStream(fs))
                        {
                            using Bitmap bitmap = new Bitmap(img);
                            using Bitmap adjusted = ApplyImageAdjustments(new Bitmap(bitmap));

                            adjusted.Save(dstImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }

                        record.ImagePath = fileName;
                        savedRecords.Add(record);
                    }

                    string catalogPath = Path.Combine(saveDir, "catalog_0.catalog");
                    string catalogManifestPath = Path.Combine(saveDir, "catalog_0.catalog_manifest");
                    string manifestPath = Path.Combine(saveDir, "manifest.json");

                    // catalog_0.catalog 생성
                    var catalogLines = savedRecords
                        .Select(r => JsonSerializer.Serialize(r))
                        .ToArray();

                    File.WriteAllLines(catalogPath, catalogLines);

                    // catalog_0.catalog_manifest 생성
                    // catalog 각 줄의 길이를 기록해야 DonkeyCar가 원하는 프레임을 찾아 읽을 수 있음
                    var lineLengths = catalogLines
                        .Select(line => System.Text.Encoding.UTF8.GetByteCount(line + Environment.NewLine))
                        .ToArray();

                    var catalogManifest = new
                    {
                        line_lengths = lineLengths
                    };

                    File.WriteAllText(
                        catalogManifestPath,
                        JsonSerializer.Serialize(catalogManifest)
                    );

                    // manifest.json 생성
                    // DonkeyCar v5는 이 파일을 3줄로 읽음:
                    // 1줄: inputs, 2줄: types, 3줄: metadata
                    long now = DateTimeOffset.Now.ToUnixTimeSeconds();

                    var manifestLines = new[]
                    {
                        JsonSerializer.Serialize(new[]
                        {
                            "cam/image_array",
                            "user/angle",
                            "user/throttle",
                            "user/mode"
                        }),

                        JsonSerializer.Serialize(new[]
                        {
                            "image_array",
                            "float",
                            "float",
                            "str"
                        }),

                        JsonSerializer.Serialize(new { }),

                        JsonSerializer.Serialize(new
                        {
                            created_at = now,
                            sessions = new
                            {
                                all_full_ids = new[]
                                {
                                    DateTime.Now.ToString("yy-MM-dd_0")
                                },
                                last_id = 0,
                                last_full_id = DateTime.Now.ToString("yy-MM-dd_0")
                            }
                        }),

                        JsonSerializer.Serialize(new
                        {
                            paths = new[]
                            {
                                "catalog_0.catalog"
                            },
                            current_index = savedRecords.Count,
                            max_len = 1000,
                            deleted_indexes = Array.Empty<int>()
                        })
                    };

                    File.WriteAllLines(manifestPath, manifestLines);
                });

                MessageBox.Show(
                    $"저장 완료!\n저장 위치: {saveDir}",
                    "완료",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                AddLog($"저장 완료: {saveDir}", Color.ForestGreen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"저장 중 오류 발생:\n{ex.Message}");
                AddLog($"저장 실패: {ex.Message}", Color.OrangeRed);
            }
            finally
            {
                btnSave.Enabled = true;
            }
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

            var deletedNow = new List<int>();

            for (int i = start; i <= end; i++)
            {
                if (_deletedIndices.Add(i))
                    deletedNow.Add(i);
            }

            if (deletedNow.Count > 0)
                _deleteMarkHistory.Push(deletedNow);

            lstDataList?.Invalidate();
            panelTimeline?.Invalidate();

            AddLog($"삭제 표시: {start + 1}~{end + 1}", Color.IndianRed);
            ResetSelection();
        }

        private void BtnRestore_Click(object? sender, EventArgs e)
        {
            if (_deleteMarkHistory.Count == 0)
            {
                MessageBox.Show("되돌릴 삭제 표시가 없습니다.");
                return;
            }

            var lastDeleted = _deleteMarkHistory.Pop();

            foreach (int index in lastDeleted)
            {
                _deletedIndices.Remove(index);
            }

            lstDataList?.Invalidate();
            panelTimeline?.Invalidate();

            AddLog($"삭제 표시 복원: {lastDeleted.Count}개", Color.ForestGreen);
        }
        private void BtnFilter_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;

            if (!double.TryParse(tbCriteria.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double threshold))
            {
                MessageBox.Show("올바른 숫자(예: 0.01)를 입력해주세요.");
                return;
            }

            var field = cmbDirSpeed.SelectedItem?.ToString() ?? cmbDirSpeed.Text;
            var op = cmbRange.SelectedItem?.ToString() ?? cmbRange.Text;

            Func<FrameData, double> selector =
                field.Contains("속도") ? r => r.Throttle : r => r.Angle;

            bool Match(FrameData r)
            {
                double v = selector(r);

                return op switch
                {
                    ">" => v > threshold,
                    "<" => v < threshold,
                    "≥" or ">=" => v >= threshold,
                    "≤" or "<=" => v <= threshold,
                    _ => Math.Abs(v) >= threshold
                };
            }

            var filteredNow = new List<int>();

            for (int i = 0; i < _records.Count; i++)
            {
                if (_deletedIndices.Contains(i))
                    continue;

                // 조건에 맞는 데이터를 저장 제외 표시
                if (Match(_records[i]))
                {
                    _deletedIndices.Add(i);
                    filteredNow.Add(i);
                }
            }

            if (filteredNow.Count == 0)
            {
                MessageBox.Show("새롭게 제외 표시할 데이터가 없습니다.");
                return;
            }

            _deleteMarkHistory.Push(filteredNow);

            lstDataList?.Invalidate();
            panelTimeline?.Invalidate();

            AddLog($"필터 적용: {filteredNow.Count}개 저장 제외 표시", Color.SteelBlue);
        }
        // 특별 기능 1: 주행 속도(Throttle)가 완전히 0(정지 상태)인 불필요한 데이터 일괄 제거
        private void BtnRemoveStopped_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;

            double epsilon = 0.01;
            var markedNow = new List<int>();

            for (int i = 0; i < _records.Count; i++)
            {
                if (Math.Abs(_records[i].Throttle) <= epsilon)
                {
                    if (_deletedIndices.Add(i))
                        markedNow.Add(i);
                }
            }

            if (markedNow.Count == 0)
            {
                MessageBox.Show("새롭게 제외 표시할 정지 상태 데이터가 없습니다.", "알림");
                return;
            }

            _deleteMarkHistory.Push(markedNow);

            lstDataList?.Invalidate();
            panelTimeline?.Invalidate();

            AddLog($"정지 데이터 제외 표시: {markedNow.Count}개", Color.IndianRed);

            MessageBox.Show(
                $"정지 상태 데이터 {markedNow.Count}개를 저장 제외로 표시했습니다.\n원본 파일은 수정되지 않습니다.",
                "완료",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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
            UpdateUIForFrame(tbFrameSlider1.Value);
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
            cmbModelSelect.Items.Clear();
            cmbModelSelect.Items.AddRange(new object[] { "Linear", "Behavioral" });
            cmbModelSelect.SelectedIndex = 0;

            // 동시처리데이터수
            cmbMulti.Items.Clear();
            cmbMulti.Items.AddRange(new object[] { "1", "16", "32", "64", "128" });
            cmbMulti.SelectedIndex = 2; // 기본 32

            // 반복학습횟수 기본값
            if (string.IsNullOrWhiteSpace(txtEpoch.Text))
                txtEpoch.Text = "10";

            // 모델 이름 기본값
            if (string.IsNullOrWhiteSpace(txtModelName.Text))
                txtModelName.Text = $"model_{DateTime.Now:yyyyMMdd_HHmm}";

            // 진행도 초기화
            pbLearning.Minimum = 0;
            pbLearning.Maximum = 100;
            pbLearning.Value = 0;
            lbLearningRate.Text = "대기 중";

            // 경로 표시 초기화
            lbSavePath.Text = "모델 저장 경로 미선택";
            lbDonkeyPath.Text = "DonkeyCar 프로젝트 경로 미선택";

            // 버튼 이벤트 연결
            btnLearningStart.Click += BtnStartTraining_Click;              // 학습 시작
            btnLearningStop.Click += BtnStopTraining_Click;              // 학습 중지
            btnSavePath.Click += BtnSelectModelSavePath_Click;       // 저장 경로 선택
            btnDonkeyPath.Click += BtnSelectDonkeyProjectPath_Click;   // 프로젝트 경로 선택
            btnExtraModel.Click += BtnLoadTransferModel_Click;          // 전이학습 모델 불러오기

            btnLearningStop.Enabled = false;

            SetupModelListView();
            SetupTrainingLossChart();
            ResetTrainingMetrics();
        }

        private void SetupTrainingLossChart()
        {
            if (pnModelScore == null) return;

            if (chartTrainingLoss != null)
            {
                pnModelScore.Controls.Remove(chartTrainingLoss);
                chartTrainingLoss.Dispose();
            }

            chartTrainingLoss = new Chart
            {
                Name = "chartTrainingLoss",
                Location = new Point(12, 72),
                Size = new Size(Math.Max(250, pnModelScore.Width - 24), Math.Max(180, pnModelScore.Height - 84)),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White
            };

            var chartArea = new ChartArea("LossArea");
            chartArea.AxisX.Title = "Epoch";
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisY.Title = "Loss";
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chartArea.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chartTrainingLoss.ChartAreas.Add(chartArea);

            var lossSeries = new Series("loss")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                ChartArea = "LossArea",
                XValueType = ChartValueType.Int32,
                YValueType = ChartValueType.Double
            };

            var valLossSeries = new Series("val_loss")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                ChartArea = "LossArea",
                XValueType = ChartValueType.Int32,
                YValueType = ChartValueType.Double
            };

            chartTrainingLoss.Series.Add(lossSeries);
            chartTrainingLoss.Series.Add(valLossSeries);
            chartTrainingLoss.Legends.Add(new Legend("Legend")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center
            });

            pnModelScore.Controls.Add(chartTrainingLoss);
            chartTrainingLoss.BringToFront();
        }

        private void ResetTrainingMetrics()
        {
            _currentEpoch = 0;
            _totalEpochs = 0;
            _lastLoss = double.NaN;
            _lastValLoss = double.NaN;
            _bestLoss = double.MaxValue;
            _bestEpoch = 0;

            lbModelScore.Text = "모델 점수 : -";
            lbLoss.Text = "손실값 : -";

            if (chartTrainingLoss != null)
            {
                chartTrainingLoss.Series["loss"].Points.Clear();
                chartTrainingLoss.Series["val_loss"].Points.Clear();
            }
        }

        private void UpdateTrainingChartPoint(int epoch, double? loss, double? valLoss)
        {
            if (chartTrainingLoss == null || epoch <= 0) return;

            if (loss.HasValue && !double.IsNaN(loss.Value))
            {
                AddOrUpdateChartPoint(chartTrainingLoss.Series["loss"], epoch, loss.Value);
            }

            if (valLoss.HasValue && !double.IsNaN(valLoss.Value))
            {
                AddOrUpdateChartPoint(chartTrainingLoss.Series["val_loss"], epoch, valLoss.Value);
            }

            chartTrainingLoss.ChartAreas["LossArea"].RecalculateAxesScale();
        }

        private void AddOrUpdateChartPoint(Series series, int epoch, double value)
        {
            foreach (var point in series.Points)
            {
                if ((int)point.XValue == epoch)
                {
                    point.YValues[0] = value;
                    return;
                }
            }

            series.Points.AddXY(epoch, value);
        }

        private void UpdateTrainingMetricLabels()
        {
            double scoreSource = !double.IsNaN(_lastValLoss) ? _lastValLoss : _lastLoss;
            if (!double.IsNaN(scoreSource))
            {
                double score = Math.Max(0, Math.Min(100, (1.0 - scoreSource) * 100.0));
                lbModelScore.Text = $"모델 점수 : {score:F1}%";
            }
            else
            {
                lbModelScore.Text = "모델 점수 : -";
            }

            if (!double.IsNaN(_lastLoss))
                lbLoss.Text = $"손실값 : {_lastLoss:F4}";
            else
                lbLoss.Text = "손실값 : -";
        }

        private double? ExtractMetric(string line, string metricName)
        {
            var match = Regex.Match(line, $@"(?:^|\s){Regex.Escape(metricName)}:\s*([-+]?\d*\.?\d+(?:[eE][-+]?\d+)?)");
            if (!match.Success) return null;

            if (double.TryParse(match.Groups[1].Value,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double value))
            {
                return value;
            }

            return null;
        }

        private (int current, int total)? ExtractEpochInfo(string line)
        {
            var match = Regex.Match(line, @"Epoch\s+(\d+)\s*/\s*(\d+)");
            if (!match.Success) return null;

            if (int.TryParse(match.Groups[1].Value, out int current) &&
                int.TryParse(match.Groups[2].Value, out int total))
            {
                return (current, total);
            }

            return null;
        }

        private void SetupModelListView()
        {
            if (lstvModelManage == null) return;

            lstvModelManage.View = View.Details;
            lstvModelManage.FullRowSelect = true;
            lstvModelManage.GridLines = true;
            lstvModelManage.Columns.Clear();
            lstvModelManage.Items.Clear();

            lstvModelManage.Columns.Add("모델이름", 130);
            lstvModelManage.Columns.Add("모델종류", 90);
            lstvModelManage.Columns.Add("사용한 데이터", 180);
            lstvModelManage.Columns.Add("수정한 날짜", 140);
            lstvModelManage.Columns.Add("주석", 220);
            lstvModelManage.Columns.Add("전이학습", 150);
        }

        private async void BtnSelectDonkeyProjectPath_Click(object? sender, EventArgs e)
        {
            string[] candidates =
            {
                "~/mysim",
                "~/mycar",
                "~/donkeycar",
                "~/projects/mysim",
                "~/Desktop/mysim"
            };

            foreach (string path in candidates)
            {
                string found = await RunWslCommandAsync(
                    $"if [ -f {path}/train.py ]; then cd {path} && pwd; fi");

                if (!string.IsNullOrWhiteSpace(found))
                {
                    _wslProjectPath = found.Trim();
                    _donkeyProjectPath = _wslProjectPath;
                    lbDonkeyPath.Text = _wslProjectPath;
                    AddLog($"WSL DonkeyCar 프로젝트 자동 설정: {_wslProjectPath}", Color.SteelBlue);
                    return;
                }
            }

            string searched = await RunWslCommandAsync(
                "find ~ -maxdepth 4 -name train.py -type f 2>/dev/null | head -n 1");

            if (!string.IsNullOrWhiteSpace(searched))
            {
                _wslProjectPath = Path.GetDirectoryName(searched.Trim())!.Replace("\\", "/");
                _donkeyProjectPath = _wslProjectPath;
                lbDonkeyPath.Text = _wslProjectPath;
                AddLog($"WSL DonkeyCar 프로젝트 자동 검색 성공: {_wslProjectPath}", Color.SteelBlue);
                return;
            }

            MessageBox.Show("DonkeyCar 프로젝트 경로를 자동으로 찾지 못했습니다.");
        }
        private async Task<string> RunWslCommandAsync(string command)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "wsl",
                Arguments = $"bash -lc \"{command.Replace("\"", "\\\"")}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null) return "";

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                AddLog($"WSL 명령 실패: {error}", Color.OrangeRed);
                throw new Exception(error);
            }

            return output.Trim();
        }
        private async Task UpdateMyConfigTrainingSettingsAsync(int epochs, int batchSize)
{
    string command =
        $"cd \"{_wslProjectPath}\" && " +
        $"python3 - <<'PY'\n" +
        $"from pathlib import Path\n" +
        $"import re\n" +
        $"p = Path('myconfig.py')\n" +
        $"text = p.read_text()\n" +
        $"def set_value(text, key, value):\n" +
        $"    line = f'{{key}} = {{value}}'\n" +
        $"    pattern = rf'^\\s*{{key}}\\s*=.*$'\n" +
        $"    if re.search(pattern, text, flags=re.M):\n" +
        $"        return re.sub(pattern, line, text, flags=re.M)\n" +
        $"    return text + '\\n' + line + '\\n'\n" +
        $"text = set_value(text, 'MAX_EPOCHS', {epochs})\n" +
        $"text = set_value(text, 'BATCH_SIZE', {batchSize})\n" +
        $"p.write_text(text)\n" +
        $"PY";

    await RunWslCommandAsync(command);

    AddLog($"myconfig.py 수정 완료: MAX_EPOCHS={epochs}, BATCH_SIZE={batchSize}", Color.ForestGreen);
}
        private void BtnSelectModelSavePath_Click(object? sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            fbd.Description = "학습된 모델을 저장할 폴더를 선택하세요.";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _modelSaveDirectory = fbd.SelectedPath;
                lbSavePath.Text = _modelSaveDirectory;
                AddLog($"모델 저장 경로 설정: {_modelSaveDirectory}", Color.SteelBlue);
            }
        }

        private void BtnLoadTransferModel_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Title = "추가학습에 사용할 기존 모델을 선택하세요.";
            ofd.Filter = "Model Files (*.h5;*.keras)|*.h5;*.keras|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _transferModelPath = ofd.FileName;

                txtExtraModel.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
                txtExtraExpl.Text = $"기존 모델 기반 추가학습: {ofd.FileName}";

                AddLog($"추가학습 모델 선택: {_transferModelPath}", Color.SteelBlue);
            }
        }

        private async void BtnStartTraining_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_trainingDataDirectory))
            {
                MessageBox.Show("먼저 학습 데이터 경로를 선택하세요.");
                return;
            }

            if (string.IsNullOrEmpty(_wslProjectPath))
            {
                MessageBox.Show("DonkeyCar 프로젝트 경로를 먼저 설정하세요.");
                return;
            }

            if (string.IsNullOrEmpty(_modelSaveDirectory))
            {
                MessageBox.Show("모델 저장 경로를 먼저 선택하세요.");
                return;
            }

            string modelName = txtModelName.Text.Trim();
            if (string.IsNullOrEmpty(modelName))
                modelName = $"model_{DateTime.Now:yyyyMMdd_HHmmss}";

            string modelKind = cmbModelSelect.SelectedItem?.ToString() ?? "Linear";

            // DonkeyCar 학습 타입 변환
            string donkeyType = modelKind == "Behavioral" ? "categorical" : "linear";

            if (!int.TryParse(txtEpoch.Text.Trim(), out int epochs) || epochs <= 0)
                epochs = 10;

            if (!int.TryParse(cmbMulti.SelectedItem?.ToString() ?? "32", out int batchSize))
                batchSize = 32;
            try
            {
                AddLog("myconfig.py 수정 시작", Color.SteelBlue);
                await UpdateMyConfigTrainingSettingsAsync(epochs, batchSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"myconfig.py 설정 변경 실패:\n{ex.Message}");
                return;
            }
            Directory.CreateDirectory(_modelSaveDirectory);

            string modelPath = Path.Combine(_modelSaveDirectory, modelName + ".h5");

            if (!string.IsNullOrEmpty(_transferModelPath) && File.Exists(_transferModelPath))
            {
                File.Copy(_transferModelPath, modelPath, true);
                AddLog($"추가학습 준비: 기존 모델을 새 모델 경로로 복사", Color.SteelBlue);
                AddLog($"기존 모델: {_transferModelPath}", Color.Gray);
                AddLog($"새 모델: {modelPath}", Color.Gray);
            }

            string wslTubPath = ConvertWindowsPathToWslPath(_trainingDataDirectory);
            string wslModelPath = ConvertWindowsPathToWslPath(modelPath);

            // DonkeyCar v5.3.0 기준: train.py + --tubs 사용
            string wslCommand =
                $"source ~/miniconda3/etc/profile.d/conda.sh && " +
                $"conda activate {_condaEnvName} && " +
                $"cd \"{_wslProjectPath}\" && " +
                $"python train.py " +
                $"--tubs \"{wslTubPath}\" " +
                $"--model \"{wslModelPath}\" " +
                $"--type {donkeyType} ";

            AddLog($"학습 데이터 경로: {_trainingDataDirectory}", Color.SteelBlue);
            AddLog($"WSL 데이터 경로: {wslTubPath}", Color.SteelBlue);
            AddLog($"WSL 프로젝트 경로: {_wslProjectPath}", Color.SteelBlue);
            AddLog($"학습 명령어: {wslCommand}", Color.SteelBlue);


            pbLearning.Value = 0;
            lbLearningRate.Text = "학습 준비 중...";
            ResetTrainingMetrics();

            btnLearningStart.Enabled = false;
            btnLearningStop.Enabled = true;

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
                    btnLearningStart.Enabled = true;
                    btnLearningStop.Enabled = false;

                    if (_trainProcess != null && _trainProcess.ExitCode == 0)
                    {
                        pbLearning.Value = 100;
                        lbLearningRate.Text = _bestEpoch > 0
                            ? $"학습 완료 (Best Epoch: {_bestEpoch}, Best Loss: {_bestLoss:F4})"
                            : "학습 완료";

                        AddTrainedModelToList(
                            modelName,
                            modelKind,
                            _trainingDataDirectory,
                            string.IsNullOrWhiteSpace(txtExpl.Text)
                                ? (_bestEpoch > 0 ? $"Best Epoch {_bestEpoch}, Best Loss {_bestLoss:F4}" : string.Empty)
                                : txtExpl.Text.Trim(),
                            string.IsNullOrEmpty(_transferModelPath)
                                ? "없음"
                                : Path.GetFileName(_transferModelPath));

                        AddLog($"✅ 학습 완료: {modelName}", Color.ForestGreen);
                        MessageBox.Show("학습이 완료되었습니다.");
                    }
                    else
                    {
                        int exitCode = _trainProcess?.ExitCode ?? -999;
                        lbLearningRate.Text = $"학습 종료됨 (ExitCode: {exitCode})";
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

                lbLearningRate.Text = "학습 중...";
            }
            catch (Exception ex)
            {
                btnLearningStart.Enabled = true;
                btnLearningStop.Enabled = false;
                lbLearningRate.Text = "실행 실패";

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
                    lbLearningRate.Text = "학습 중지됨";
                    AddLog("학습 프로세스 중지", Color.OrangeRed);
                }

                btnLearningStart.Enabled = true;
                btnLearningStop.Enabled = false;
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

                var epochInfo = ExtractEpochInfo(line);
                if (epochInfo.HasValue)
                {
                    _currentEpoch = epochInfo.Value.current;
                    _totalEpochs = epochInfo.Value.total;

                    int percent = Math.Max(0, Math.Min(100, _currentEpoch * 100 / Math.Max(1, _totalEpochs)));
                    pbLearning.Value = percent;
                    lbLearningRate.Text = $"학습 중... {percent}% ({_currentEpoch}/{_totalEpochs})";
                }

                double? loss = ExtractMetric(line, "loss");
                double? valLoss = ExtractMetric(line, "val_loss");

                if (loss.HasValue)
                    _lastLoss = loss.Value;

                if (valLoss.HasValue)
                    _lastValLoss = valLoss.Value;

                double? bestCandidate = valLoss ?? loss;
                if (bestCandidate.HasValue && bestCandidate.Value < _bestLoss)
                {
                    _bestLoss = bestCandidate.Value;
                    _bestEpoch = _currentEpoch;
                }

                if ((loss.HasValue || valLoss.HasValue) && _currentEpoch > 0)
                    UpdateTrainingChartPoint(_currentEpoch, loss, valLoss);

                UpdateTrainingMetricLabels();

                if (pbLearning.Value == 0)
                {
                    pbLearning.Value = 5;
                    lbLearningRate.Text = "학습 중...";
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
            if (lstvModelManage == null) return;

            var item = new ListViewItem(modelName);
            item.SubItems.Add(modelKind);
            item.SubItems.Add(Path.GetFileName(dataPath));
            item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            item.SubItems.Add(memo);
            item.SubItems.Add(transferModel);

            lstvModelManage.Items.Add(item);
        }

        private void AppendLog(string message)
        {
            AddLog(message, Color.DimGray);
        }


        private void SetupPreviewTab()
        {
            // 학습 미리보기 탭: 좌측은 실제 데이터, 우측은 선택한 모델 비교 영역으로 사용
            btnRun2.Click += BtnPreviewPlay_Click;
            btnPrevFrame2.Click += BtnPreviewPrev_Click;
            btnNextFrame2.Click += BtnPreviewNext_Click;
            tbFrameSlider2.ValueChanged += TrackBar3_ValueChanged;

            tbImgBright.Minimum = -100;
            tbImgBright.Maximum = 100;
            tbImgBright.Value = 0;
            tbImgBright.ValueChanged += (_, __) =>
            {
                _previewBrightness = tbImgBright.Value / 100.0;
                UpdatePreviewFrame(tbFrameSlider2.Value);
            };

            tbImgBlur.Minimum = 0;
            tbImgBlur.Maximum = 100;
            tbImgBlur.Value = 0;
            tbImgBlur.ValueChanged += (_, __) =>
            {
                _previewBlurAmount = tbImgBlur.Value / 100.0;
                UpdatePreviewFrame(tbFrameSlider2.Value);
            };

            chkImgActBW.CheckedChanged += (_, __) =>
            {
                _previewInvertColors = chkImgActBW.Checked;
                UpdatePreviewFrame(tbFrameSlider2.Value);
            };

            cmbSpeed2.Items.Clear();
            cmbSpeed2.Items.AddRange(new object[] { "0.5", "1.0", "1.5", "2.0", "3.0" });
            cmbSpeed2.SelectedIndex = 1;
            cmbSpeed2.SelectedIndexChanged += (_, __) => UpdatePreviewSpeed();

            if (string.IsNullOrWhiteSpace(txtFrmMvm2.Text))
                txtFrmMvm2.Text = "1";

            _previewTimer.Interval = 33;
            _previewTimer.Tick += PreviewTimer_Tick;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            lbRawDataPath.Text = "경로";
            label9.Text = "경로";
            lblFrmInx2.Text = "해당 프레임    :        00000";
            lbAISpeed2.Text = "0.00";
            lbAIDir2.Text = "0.00";
            lbAISpeed4.Text = "-";
            lbAIDir4.Text = "-";
        }

        private void BtnLoadPreviewData_Click(object? sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            fbd.Description = "미리보기할 Donkeycar 데이터 폴더를 선택하세요.";

            if (fbd.ShowDialog() != DialogResult.OK) return;

            _previewBaseDirectory = fbd.SelectedPath;
            label1.Text = _previewBaseDirectory;

            bool isMultiJsonFormat = false;
            string[] multiJsonFiles = Array.Empty<string>();

            if (!Directory.GetFiles(_previewBaseDirectory, "*.catalog").Any())
            {
                multiJsonFiles = Directory.GetFiles(_previewBaseDirectory, "*.json");
                isMultiJsonFormat = multiJsonFiles.Length > 0;
            }

            _previewRecords.Clear();
            _previewRecords.AddRange(LoadRecords(_previewBaseDirectory, isMultiJsonFormat, multiJsonFiles));

            if (_previewRecords.Count == 0)
            {
                MessageBox.Show("미리보기용 데이터를 찾을 수 없습니다.");
                return;
            }

            tbFrameSlider2.Minimum = 0;
            tbFrameSlider2.Maximum = _previewRecords.Count - 1;
            tbFrameSlider2.Value = 0;

            _leftPredictionCache.Clear();
            _rightPredictionCache.Clear();
            _leftPredictionReady = false;
            _rightPredictionReady = false;

            UpdatePreviewFrame(0);

            AddLog($"미리보기 데이터 로드: {_previewRecords.Count}장", Color.SteelBlue);
        }

        private void BtnLoadPreviewModel_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Title = "비교할 학습 모델을 선택하세요.";
            ofd.Filter = "Model Files (*.h5;*.keras;*.tflite)|*.h5;*.keras;*.tflite|All Files (*.*)|*.*";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            _previewModelPath = ofd.FileName;
            label9.Text = _previewModelPath;
            AddLog($"미리보기 모델 선택: {_previewModelPath}", Color.SteelBlue);
            UpdatePreviewFrame(tbFrameSlider2.Value);
        }

        private void TrackBar3_ValueChanged(object? sender, EventArgs e)
        {
            if (_previewRecords.Count == 0) return;

            UpdatePreviewFrame(tbFrameSlider2.Value);
        }

        private async void UpdatePreviewFrame(int index)
        {
            if (_previewRecords.Count == 0)
            {
                lblFrmInx2.Text = "주행 데이터 없음";
                return;
            }

            if (index < 0 || index >= _previewRecords.Count) return;

            var record = _previewRecords[index];

            lblFrmInx2.Text = $"해당 프레임    :        {index + 1} / {_previewRecords.Count}";

            _originalSteering = record.Angle;
            _originalThrottle = record.Throttle;

            lbRawDataDir2.Text = _originalSteering.ToString("F2");
            lbRawDataSpeed2.Text = _originalThrottle.ToString("F2");
            pbRawDataDir.Value = Math.Max(0, Math.Min(100, (int)((_originalSteering + 1) * 50)));
            pbRawDataSpeed.Value = Math.Max(0, Math.Min(100, (int)((_originalThrottle + 1) * 50)));

            string imgPath = GetImageFullPath(record, _previewBaseDirectory);

            // 1. 이미지 먼저 표시
            if (File.Exists(imgPath))
            {
                try
                {
                    using var fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using var img = Image.FromStream(fs);
                    using var previewImage = ApplyPreviewImageAdjustments(new Bitmap(img));

                    var oldLeft = pictureBox2.Image;
                    pictureBox2.Image = new Bitmap(previewImage);
                    oldLeft?.Dispose();

                    var oldRight = pictureBox1.Image;
                    pictureBox1.Image = new Bitmap(previewImage);
                    oldRight?.Dispose();
                }
                catch
                {
                }
            }

            pictureBox2.Invalidate();
            pictureBox1.Invalidate();

            // 왼쪽 모델 예측값은 캐시에서 가져오기
            if (_leftPredictionCache.TryGetValue(index, out var leftPred))
            {
                _leftPredictedSteering = leftPred.steering;
                _leftPredictedThrottle = leftPred.throttle;
                _leftPredictionReady = true;
            }
            else
            {
                _leftPredictionReady = false;
            }

            // 오른쪽 모델 예측값은 캐시에서 가져오기
            if (_rightPredictionCache.TryGetValue(index, out var rightPred))
            {
                _rightPredictedSteering = rightPred.steering;
                _rightPredictedThrottle = rightPred.throttle;
                _rightPredictionReady = true;
            }
            else
            {
                _rightPredictionReady = false;
            }

            // 5. 예측값 표시
            lbAIDir2.Text = _leftPredictionReady ? _leftPredictedSteering.ToString("F2") : "-";
            lbAISpeed2.Text = _leftPredictionReady ? _leftPredictedThrottle.ToString("F2") : "-";
            pbAIDir.Value = Math.Max(0, Math.Min(100, (int)((_leftPredictedSteering + 1) * 50)));
            pbAISpeed.Value = Math.Max(0, Math.Min(100, (int)((_leftPredictedThrottle + 1) * 50)));

            lbAIDir4.Text = _rightPredictionReady ? _rightPredictedSteering.ToString("F2") : "-";
            lbAISpeed4.Text = _rightPredictionReady ? _rightPredictedThrottle.ToString("F2") : "-";
            pbAIDir2.Value = Math.Max(0, Math.Min(100, (int)((_rightPredictedSteering + 1) * 50)));
            pbAISpeed2.Value = Math.Max(0, Math.Min(100, (int)((_rightPredictedThrottle + 1) * 50)));

            pictureBox2.Invalidate();
            pictureBox1.Invalidate();
        }

        private Bitmap ApplyPreviewImageAdjustments(Bitmap source)
        {
            double oldBrightness = _brightness;
            double oldBlur = _blurAmount;
            bool oldInvert = _invertColors;

            try
            {
                _brightness = _previewBrightness;
                _blurAmount = _previewBlurAmount;
                _invertColors = _previewInvertColors;
                return ApplyImageAdjustments(source);
            }
            finally
            {
                _brightness = oldBrightness;
                _blurAmount = oldBlur;
                _invertColors = oldInvert;
            }
        }

        private int GetPreviewFrameStep()
        {
            if (int.TryParse(txtFrmMvm2.Text.Trim(), out int step) && step > 0)
                return step;
            return 1;
        }

        private void UpdatePreviewSpeed()
        {
            string text = cmbSpeed2.SelectedItem?.ToString() ?? "1.0";
            if (double.TryParse(text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double speed))
            {
                _previewPlaybackSpeed = Math.Max(1, (int)Math.Round(speed));
                _previewTimer.Interval = Math.Max(8, (int)(33.0 / Math.Max(0.1, speed)));
            }
        }

        private void BtnPreviewPlay_Click(object? sender, EventArgs e)
        {
            if (_previewRecords.Count == 0) return;

            if (_isPreviewPlaying)
            {
                StopPreviewPlayback();
                return;
            }

            if (tbFrameSlider2.Value == tbFrameSlider2.Maximum)
                tbFrameSlider2.Value = 0;

            _isPreviewPlaying = true;
            btnRun2.Text = "⏸";
            _previewTimer.Start();
        }

        private void StopPreviewPlayback()
        {
            _isPreviewPlaying = false;
            btnRun2.Text = "▶";
            _previewTimer.Stop();
        }

        private void PreviewTimer_Tick(object? sender, EventArgs e)
        {
            if (_previewRecords.Count == 0)
            {
                StopPreviewPlayback();
                return;
            }

            if (tbFrameSlider2.Value >= tbFrameSlider2.Maximum)
            {
                StopPreviewPlayback();
                return;
            }

            int next = Math.Min(tbFrameSlider2.Maximum, tbFrameSlider2.Value + _previewPlaybackSpeed);
            tbFrameSlider2.Value = next;
        }

        private void BtnPreviewPrev_Click(object? sender, EventArgs e)
        {
            if (_previewRecords.Count == 0) return;
            tbFrameSlider2.Value = Math.Max(tbFrameSlider2.Minimum, tbFrameSlider2.Value - GetPreviewFrameStep());
        }

        private void BtnPreviewNext_Click(object? sender, EventArgs e)
        {
            if (_previewRecords.Count == 0) return;
            tbFrameSlider2.Value = Math.Min(tbFrameSlider2.Maximum, tbFrameSlider2.Value + GetPreviewFrameStep());
        }

        #endregion


        #region Extended Features (Graph & Test)

        private void BtnRenderGraph_Click(object? sender, EventArgs e)
        {
            // chartData 컨트롤이 현재 UI 브랜치에 없어서 비워둠.
        }

        private void DetectAndHighlightAnomalies()
        {
            // chartData 컨트롤이 현재 UI 브랜치에 없어서 비워둠.
        }

        #endregion

        private void PictureBox2_Paint(object? sender, PaintEventArgs e)
        {
            DrawSteeringArrow(e.Graphics, pictureBox2.ClientRectangle, _originalSteering, Color.Black);

            if (_leftPredictionReady)
                DrawSteeringArrow(e.Graphics, pictureBox2.ClientRectangle, _leftPredictedSteering, Color.Blue);
        }

        private void PictureBox1_Paint(object? sender, PaintEventArgs e)
        {
            DrawSteeringArrow(e.Graphics, pictureBox1.ClientRectangle, _originalSteering, Color.Black);

            if (_rightPredictionReady)
                DrawSteeringArrow(e.Graphics, pictureBox1.ClientRectangle, _rightPredictedSteering, Color.Red);
        }

        private void DrawSteeringArrow(Graphics g, Rectangle area, double steering, Color color)
        {
            int centerX = area.Width / 2;
            int centerY = area.Height - 40;
            int length = 120;

            double angle = -90 + steering * 45;
            double rad = angle * Math.PI / 180.0;

            int endX = centerX + (int)(Math.Cos(rad) * length);
            int endY = centerY + (int)(Math.Sin(rad) * length);

            using Pen pen = new Pen(color, 4);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            g.DrawLine(pen, centerX, centerY, endX, endY);
        }
        private async void BtnLoadLeftModel_Click(object? sender, EventArgs e)
        {
            if (_previewRecords.Count == 0)
            {
                MessageBox.Show("먼저 주행 데이터 불러오기로 미리보기 데이터 폴더를 선택하세요.");
                return;
            }
            using var ofd = new OpenFileDialog();
            ofd.Title = "왼쪽 비교 모델을 선택하세요.";
            ofd.Filter = "Keras Model (*.h5;*.keras)|*.h5;*.keras|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _leftModelPath = ofd.FileName;
                lbRawDataPath.Text = _leftModelPath;

                lbAIDir2.Text = "전체 예측 중";
                lbAISpeed2.Text = "전체 예측 중";

                btnRawData.Enabled = false;
                await PredictAllFramesAsync(_leftModelPath, _leftPredictionCache);
                btnRawData.Enabled = true;

                UpdatePreviewFrame(tbFrameSlider2.Value);
            }
        }

        private async void BtnLoadRightModel_Click(object? sender, EventArgs e)
        {
            if (_previewRecords.Count == 0)
            {
                MessageBox.Show("먼저 주행 데이터 불러오기로 미리보기 데이터 폴더를 선택하세요.");
                return;
            }
            using var ofd = new OpenFileDialog();
            ofd.Title = "오른쪽 비교 모델을 선택하세요.";
            ofd.Filter = "Keras Model (*.h5;*.keras)|*.h5;*.keras|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _rightModelPath = ofd.FileName;
                label9.Text = _rightModelPath;

                lbAIDir4.Text = "전체 예측 중";
                lbAISpeed4.Text = "전체 예측 중";

                btn.Enabled = false;
                await PredictAllFramesAsync(_rightModelPath, _rightPredictionCache);
                btn.Enabled = true;

                UpdatePreviewFrame(tbFrameSlider2.Value);
            }
        }
        private sealed class PredictionResult
        {
            public double steering { get; set; }
            public double throttle { get; set; }
        }
        private sealed class PredictionItem
        {
            public double steering { get; set; }
            public double throttle { get; set; }
        }
        private async Task<(double steering, double throttle)?> PredictWithModelAsync(string modelPath, string imagePath)
        {
            if (string.IsNullOrEmpty(modelPath) || !File.Exists(modelPath)) return null;
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath)) return null;

            string wslModelPath = ConvertWindowsPathToWslPath(modelPath);
            string wslImagePath = ConvertWindowsPathToWslPath(imagePath);

            string command =
                $"source ~/miniconda3/etc/profile.d/conda.sh && " +
                $"conda activate {_condaEnvName} && " +
                $"cd \"{_wslProjectPath}\" && " +
                $"python predict_one.py " +
                $"--model \"{wslModelPath}\" " +
                $"--image \"{wslImagePath}\"";

            var psi = new ProcessStartInfo
            {
                FileName = "wsl",
                Arguments = $"bash -lc \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null) return null;

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                AddLog($"예측 실패: {error}", Color.OrangeRed);
                return null;
            }

            var result = JsonSerializer.Deserialize<PredictionResult>(output.Trim());

            if (result == null) return null;

            return (result.steering, result.throttle);
        }
        private async Task PredictAllFramesAsync(
    string modelPath,
    Dictionary<int, (double steering, double throttle)> cache)
        {
            if (_previewRecords.Count == 0) return;
            if (string.IsNullOrEmpty(modelPath) || !File.Exists(modelPath)) return;

            cache.Clear();

            string tempJsonPath = Path.Combine(
                Path.GetTempPath(),
                $"donkey_preview_images_{Guid.NewGuid():N}.json");

            var imageList = new List<object>();

            for (int i = 0; i < _previewRecords.Count; i++)
            {
                string imgPath = GetImageFullPath(_previewRecords[i], _previewBaseDirectory);

                if (File.Exists(imgPath))
                {
                    imageList.Add(new
                    {
                        index = i,
                        path = ConvertWindowsPathToWslPath(imgPath)
                    });
                }
            }

            await File.WriteAllTextAsync(
                tempJsonPath,
                JsonSerializer.Serialize(imageList));

            string wslModelPath = ConvertWindowsPathToWslPath(modelPath);
            string wslImagesJsonPath = ConvertWindowsPathToWslPath(tempJsonPath);

            string command =
                $"source ~/miniconda3/etc/profile.d/conda.sh && " +
                $"conda activate {_condaEnvName} && " +
                $"cd \"{_wslProjectPath}\" && " +
                $"python predict_all.py " +
                $"--model \"{wslModelPath}\" " +
                $"--images \"{wslImagesJsonPath}\"";

            var psi = new ProcessStartInfo
            {
                FileName = "wsl",
                Arguments = $"bash -lc \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            AddLog("전체 예측 시작...", Color.SteelBlue);

            using var process = Process.Start(psi);
            if (process == null) return;

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            try
            {
                File.Delete(tempJsonPath);
            }
            catch
            {
            }

            if (process.ExitCode != 0)
            {
                AddLog($"전체 예측 실패: {error}", Color.OrangeRed);
                return;
            }

            var result = JsonSerializer.Deserialize<Dictionary<string, PredictionItem>>(output.Trim());

            if (result == null)
            {
                AddLog("전체 예측 결과 파싱 실패", Color.OrangeRed);
                return;
            }

            foreach (var pair in result)
            {
                if (int.TryParse(pair.Key, out int index))
                {
                    cache[index] = (pair.Value.steering, pair.Value.throttle);
                }
            }

            AddLog($"전체 예측 완료: {cache.Count}/{_previewRecords.Count}", Color.ForestGreen);
        }
    }
}
