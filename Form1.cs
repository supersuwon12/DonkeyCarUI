using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer; // WinForms Timer 명시적 사용

namespace DonkeyCarUI
{
    public partial class Form1 : Form
    {
        private List<DonkeyRecord> _records = new List<DonkeyRecord>();
        private string _baseDirectory = string.Empty;

        // 재생 관련 변수
        private Timer _playbackTimer = new Timer();
        private bool _isPlaying = false;
        private int _playbackSpeed = 1;

        // 필터 및 선택 지점 관련 변수
        private int _startIndex = -1;
        private int _endIndex = -1;
        private List<DonkeyRecord> _originalRecords = new List<DonkeyRecord>();

        public Form1()
        {
            InitializeComponent();

            // Setup Event Handlers
            btnLoadData.Click += BtnLoadData_Click;
            tbFrameSlider.Scroll += TbFrameSlider_Scroll;
            tbFrameSlider.ValueChanged += TbFrameSlider_ValueChanged;

            // 재생 컨트롤 이벤트 연결
            btnPlay.Click += BtnPlay_Click;
            btnPrevFrame.Click += BtnPrevFrame_Click;
            btnNextFrame.Click += BtnNextFrame_Click;
            cmbSpeed.Click += BtnSpeed_Click;
            btnRewind.Click += BtnRewind_Click;
            btnFastForward.Click += BtnFastForward_Click;

            // 지점 설정, 필터, 삭제, 학습 이벤트 연결
            btnSetPoint1.Click += BtnSetPoint1_Click;
            btnSetPoint2.Click += BtnSetPoint2_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRestore.Click += BtnRestore_Click;
            btnFilter.Click += BtnFilter_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnTrain.Click += BtnTrain_Click;
            btnTestModel.Click += BtnTestModel_Click;
            btnRenderGraph.Click += BtnRenderGraph_Click;

            // 차트 초기화 설정
            InitializeChart();
            chartData.MouseClick += ChartData_MouseClick;

            // 타이머 설정 (약 30 FPS 기준 = 33ms)
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
            // Delete 키: 현재 선택 범위 데이터 삭제
            else if (e.KeyCode == Keys.Delete && _startIndex != -1 && _endIndex != -1)
            {
                BtnDelete_Click(this, EventArgs.Empty);
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
                }
            }
        }

        private void LoadCatalogData()
        {
            _records.Clear();
            string catalogPath = Path.Combine(_baseDirectory, "catalog_0.catalog");
            bool isMultiJsonFormat = false;
            string[] multiJsonFiles = new string[0];

            // Look for any catalog file if catalog_0 doesn't exist
            if (File.Exists(catalogPath))
            {
                // Single catalog file found
                isMultiJsonFormat = false;
            }
            else
            {
                var catalogFiles = Directory.GetFiles(_baseDirectory, "*.catalog");
                if (catalogFiles.Length > 0)
                {
                    catalogPath = catalogFiles[0];
                    isMultiJsonFormat = false;
                }
                else
                {
                    // If no catalog files, check if there are multiple JSON files (Tub v2 format)
                    multiJsonFiles = Directory.GetFiles(_baseDirectory, "*.json");
                    if (multiJsonFiles.Length > 0)
                    {
                        isMultiJsonFormat = true;
                    }
                    else
                    {
                        MessageBox.Show("데이터 파일을 찾을 수 없습니다. (catalog 또는 수천 개의 json 파일이 필요합니다)", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            try
            {
                if (!isMultiJsonFormat)
                {
                    // Parse single large catalog file
                    var lines = File.ReadAllLines(catalogPath);
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var record = JsonSerializer.Deserialize<DonkeyRecord>(line);
                        if (record != null)
                        {
                            _records.Add(record);
                        }
                    }
                }
                else
                {
                    // Parse thousands of individual JSON files
                    // Sort them numerically since they are usually named record_1.json, record_2.json
                    var sortedFiles = multiJsonFiles.OrderBy(f =>
                    {
                        string name = Path.GetFileNameWithoutExtension(f);
                        string numberOnly = new string(name.Where(char.IsDigit).ToArray());
                        return int.TryParse(numberOnly, out int n) ? n : 0;
                    }).ToList();

                    foreach (var file in sortedFiles)
                    {
                        string content = File.ReadAllText(file);
                        var record = JsonSerializer.Deserialize<DonkeyRecord>(content);
                        if (record != null)
                        {
                            // Some older formats have "cam/image_array" as just the filename in a different property
                            _records.Add(record);
                        }
                    }
                }

                if (_records.Count > 0)
                {
                    // 원본 데이터 백업 (복원용)
                    _originalRecords = new List<DonkeyRecord>(_records);

                    tbFrameSlider.Minimum = 0;
                    tbFrameSlider.Maximum = _records.Count - 1;
                    tbFrameSlider.Value = 0;
                    UpdateUIForFrame(0);

                    UpdateDataListText();
                    ResetSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로딩 중 오류 발생: {ex.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Update ProgressBars (Handling negative and positive values appropriately based on expected ranges)
            // Assuming typical range -1 to 1 for steering and throttle
            pbSteering.Value = Math.Max(0, Math.Min(100, (int)((record.Angle + 1) * 50)));
            pbThrottle.Value = Math.Max(0, Math.Min(100, (int)((record.Throttle + 1) * 50)));

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
                        using (var fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                        {
                            var img = Image.FromStream(fs);
                            pbCameraView.Image = new Bitmap(img);
                        }
                        oldImg?.Dispose();
                    }
                    catch { /* Handle image load error silently for smooth sliding */ }
                }
            }
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
                    tbFrameSlider.Value = 0; // 끝에 있으면 처음으로

                _isPlaying = true;
                btnPlay.Text = "⏸"; // 일시정지 아이콘
                _playbackTimer.Start();
            }
        }

        private void StopPlayback()
        {
            _isPlaying = false;
            btnPlay.Text = "▶";
            _playbackTimer.Stop();
        }

        private void BtnPrevFrame_Click(object? sender, EventArgs e)
        {
            if (tbFrameSlider.Value > tbFrameSlider.Minimum)
                tbFrameSlider.Value--;
        }

        private void BtnNextFrame_Click(object? sender, EventArgs e)
        {
            if (tbFrameSlider.Value < tbFrameSlider.Maximum)
                tbFrameSlider.Value++;
        }

        private void BtnSpeed_Click(object? sender, EventArgs e)
        {
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
            txtDataList.Text = $"현재 표시 중: {_records.Count} 프레임\r\n" +
                               $"원본: {_originalRecords.Count} 프레임";
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

        private void BtnSetPoint1_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;
            _startIndex = tbFrameSlider.Value;
            UpdateRangeLabel();
        }

        private void BtnSetPoint2_Click(object? sender, EventArgs e)
        {
            if (_records.Count == 0) return;
            _endIndex = tbFrameSlider.Value;
            UpdateRangeLabel();
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_startIndex == -1 || _endIndex == -1)
            {
                MessageBox.Show("시작 지점과 끝 지점을 먼저 설정하세요.");
                return;
            }

            int start = Math.Min(_startIndex, _endIndex);
            int count = Math.Abs(_endIndex - _startIndex) + 1;

            _records.RemoveRange(start, count);

            tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
            if (tbFrameSlider.Value > tbFrameSlider.Maximum)
                tbFrameSlider.Value = tbFrameSlider.Maximum;

            UpdateDataListText();
            ResetSelection();
            if (_records.Count > 0) UpdateUIForFrame(tbFrameSlider.Value);
        }

        private void BtnRestore_Click(object? sender, EventArgs e)
        {
            _records = new List<DonkeyRecord>(_originalRecords);

            tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
            tbFrameSlider.Value = 0;

            UpdateDataListText();
            ResetSelection();
            if (_records.Count > 0) UpdateUIForFrame(tbFrameSlider.Value);
        }

        private void BtnFilter_Click(object? sender, EventArgs e)
        {
            if (double.TryParse(txtFilter.Text, out double threshold))
            {
                // 조향각이나 스로틀 값이 임계값 이상인(의미있는 움직임이 있는) 데이터만 필터링
                _records = _originalRecords.Where(r => Math.Abs(r.Throttle) >= threshold || Math.Abs(r.Angle) >= threshold).ToList();

                if (_records.Count > 0)
                {
                    tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
                    tbFrameSlider.Value = 0;
                    UpdateDataListText();
                    UpdateUIForFrame(tbFrameSlider.Value);
                    MessageBox.Show($"필터 적용 완료: {_records.Count}개의 유효 프레임이 남았습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("설정한 임계값 조건에 맞는 데이터가 없습니다. 원본으로 복구합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BtnRestore_Click(null, EventArgs.Empty);
                }
            }
            else
            {
                MessageBox.Show("올바른 숫자(예: 0.1)를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 특별 기능 1: 주행 속도(Throttle)가 완전히 0(정지 상태)인 불필요한 데이터 일괄 제거
        private void BtnRemoveStopped_Click(object? sender, EventArgs e)
        {
            if (_originalRecords.Count == 0) return;

            // Tolerance for floating point 0
            double epsilon = 0.01;
            int beforeCount = _records.Count;
            _records = _records.Where(r => Math.Abs(r.Throttle) > epsilon).ToList();

            int removed = beforeCount - _records.Count;
            if (_records.Count > 0)
            {
                tbFrameSlider.Maximum = Math.Max(0, _records.Count - 1);
                tbFrameSlider.Value = 0;
                UpdateDataListText();
                UpdateUIForFrame(tbFrameSlider.Value);
                BtnRenderGraph_Click(null, EventArgs.Empty); // 그래프 자동 업데이트
                MessageBox.Show($"정지된 주행 데이터 {removed}개가 완벽히 제거되었습니다.\n이제 더 깨끗한 데이터로 학습할 수 있습니다.", "데이터 최적화", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("모든 데이터가 정지 상태입니다.", "알림");
                BtnRestore_Click(null, EventArgs.Empty);
            }
        }

        // 특별 기능 2: 조향각(Steering) 데이터 스무딩(Moving Average) - 극단적으로 떨리는 손떨림 보정
        private void BtnSmoothData_Click(object? sender, EventArgs e)
        {
            if (_records.Count < 5) return;

            int windowSize = 5; // 5프레임 이동 평균
            var smoothedRecords = new List<DonkeyRecord>(_records);

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
        private void BtnTrain_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_baseDirectory))
            {
                MessageBox.Show("먼저 데이터 폴더를 불러오세요.");
                return;
            }

            txtLog.Text = "학습 중...\r\n";
            btnTrain.Enabled = false;

            // Python 프로세스 비동기 실행
            Task.Run(() =>
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = "python",
                        // 실제 환경에 맞게 manage.py 경로 지정 필요 (여기선 가정)
                        Arguments = $"manage.py train --tub \"{_baseDirectory}\" --model models/mypilot.h5",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        // 작업 폴더를 baseDirectory의 상위 등 적절한 곳으로 지정해야 함 (manage.py 위치)
                        // WorkingDirectory = Path.GetDirectoryName(_baseDirectory) 
                    };

                    using (Process process = Process.Start(psi)!)
                    {
                        process.OutputDataReceived += (s, ev) =>
                        {
                            if (!string.IsNullOrEmpty(ev.Data))
                                AppendLog(ev.Data);
                        };
                        process.ErrorDataReceived += (s, ev) =>
                        {
                            if (!string.IsNullOrEmpty(ev.Data))
                                AppendLog($"ERROR: {ev.Data}");
                        };

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        process.WaitForExit();

                        AppendLog($"\r\n학습 완료 (Exit Code: {process.ExitCode})");
                    }
                }
                catch (Exception ex)
                {
                    AppendLog($"\r\n파이썬 실행 중 오류 발생:\r\n{ex.Message}");
                    AppendLog("Python이 시스템 경로(PATH)에 등록되어 있는지 확인하세요.");
                }
                finally
                {
                    this.Invoke((MethodInvoker)delegate { btnTrain.Enabled = true; });
                }
            });
        }

        private void AppendLog(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendLog), message);
                return;
            }

            txtLog.AppendText(message + "\r\n");
            // 텍스트 박스 맨 아래로 스크롤
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
        #endregion

        #region Extended Features (Graph & Test)
        private void InitializeChart()
        {
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
            if (_records.Count == 0) return;

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
            if (_records.Count == 0) return;

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
            if (_records.Count == 0) return;

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

        private void btnPlay_Click_1(object sender, EventArgs e)
        {

        }
    }
}
