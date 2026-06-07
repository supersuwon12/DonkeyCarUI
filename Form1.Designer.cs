using System.Windows.Forms.DataVisualization.Charting;

namespace DonkeyCarUI
{
    partial class DataManager
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            panelTimeline = new Panel();
            grbDataCorrection = new GroupBox();
            chkActBW = new CheckBox();
            lbBW = new Label();
            lbBlur = new Label();
            lbBright = new Label();
            tbBright = new TrackBar();
            tbBlur = new TrackBar();
            grbEditRange = new GroupBox();
            btnSetPoint1 = new Button();
            btnCancel = new Button();
            btnDelete = new Button();
            btnSetPoint2 = new Button();
            btnRestore = new Button();
            lblRange = new Label();
            lstProcess = new ListView();
            grbFiltering = new GroupBox();
            tbCriteria = new TextBox();
            cmbRange = new ComboBox();
            cmbDirSpeed = new ComboBox();
            btnFilter = new Button();
            txtFilter = new TextBox();
            grbDataList = new GroupBox();
            lstDataList = new ListBox();
            grbRunningData = new GroupBox();
            pbThrottle = new ProgressBar();
            lbDataSpeed = new Label();
            lblThrottleValue = new Label();
            pbSteering = new ProgressBar();
            lblSteeringValue = new Label();
            lbDataDir = new Label();
            lblTitle = new Label();
            grbPlayOption1 = new GroupBox();
            btnRun1 = new Button();
            lbSpeed1 = new Label();
            lbFrmMvm1 = new Label();
            txtFrmMvm1 = new TextBox();
            cmbSpeed1 = new ComboBox();
            btnPlay = new Button();
            btnPrevFrame1 = new Button();
            btnNextFrame1 = new Button();
            lblFrmInx1 = new Label();
            tbFrameSlider1 = new TrackBar();
            pbCameraView = new PictureBox();
            lblPath = new Label();
            btnLoadData = new Button();
            tabPage2 = new TabPage();
            pnModelManage = new Panel();
            pnModelScore = new Panel();
            lbLoss = new Label();
            lbModelScore = new Label();
            lbModelManage = new Label();
            lstvModelManage = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            pnDataLearning = new Panel();
            grbExtraOption = new GroupBox();
            lbExtraExpl = new Label();
            txtExtraExpl = new TextBox();
            lbCaution = new Label();
            txtExtraModel = new TextBox();
            lbExtraModelName = new Label();
            lbExtraModelPath = new Label();
            btnExtraModel = new Button();
            lbLearningRate = new Label();
            btnLearningStop = new Button();
            pbLearning = new ProgressBar();
            btnLearningStart = new Button();
            grbModelOption = new GroupBox();
            lbDonkeyPath = new Label();
            lbSavePath = new Label();
            btnDonkeyPath = new Button();
            btnSavePath = new Button();
            cmbMulti = new ComboBox();
            lbMulti = new Label();
            label32 = new Label();
            lbExpl = new Label();
            txtModelName = new TextBox();
            lbModelName = new Label();
            txtEpoch = new TextBox();
            lbEpoch = new Label();
            cmbModelSelect = new ComboBox();
            lbModelSelect = new Label();
            txtExpl = new TextBox();
            lbDataLearning = new Label();
            tabPage3 = new TabPage();
            tbFrameSlider2 = new TrackBar();
            pnOption = new Panel();
            grbRawData = new GroupBox();
            pbRawDataSpeed = new ProgressBar();
            lbRawDataSpeed = new Label();
            lbRawDataSpeed2 = new Label();
            pbRawDataDir = new ProgressBar();
            lbRawDataDir2 = new Label();
            lbRawDataDir = new Label();
            grbImgCorrection = new GroupBox();
            chkImgActBW = new CheckBox();
            lbImgBW = new Label();
            lbImgBlur = new Label();
            lbImgBright = new Label();
            tbImgBright = new TrackBar();
            tbImgBlur = new TrackBar();
            grbPlayOption2 = new GroupBox();
            btnRun2 = new Button();
            lbSpeed2 = new Label();
            lbFrmMvm2 = new Label();
            txtFrmMvm2 = new TextBox();
            cmbSpeed2 = new ComboBox();
            button8 = new Button();
            btnPrevFrame2 = new Button();
            btnNextFrame2 = new Button();
            lblFrmInx2 = new Label();
            splitContainer1 = new SplitContainer();
            panel4 = new Panel();
            pictureBox2 = new PictureBox();
            panel5 = new Panel();
            grbModelData = new GroupBox();
            pbAISpeed = new ProgressBar();
            lbAISpeed = new Label();
            lbAISpeed2 = new Label();
            pbAIDir = new ProgressBar();
            lbAIDir2 = new Label();
            lbAIDir1 = new Label();
            lbRawDataPath = new Label();
            btnRawData = new Button();
            lbCmp = new Label();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            groupBox10 = new GroupBox();
            progressBar4 = new ProgressBar();
            label20 = new Label();
            label21 = new Label();
            progressBar5 = new ProgressBar();
            label22 = new Label();
            label23 = new Label();
            label9 = new Label();
            btn = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            grbDataCorrection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbBright).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbBlur).BeginInit();
            grbEditRange.SuspendLayout();
            grbFiltering.SuspendLayout();
            grbDataList.SuspendLayout();
            grbRunningData.SuspendLayout();
            grbPlayOption1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbFrameSlider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCameraView).BeginInit();
            tabPage2.SuspendLayout();
            pnModelManage.SuspendLayout();
            pnModelScore.SuspendLayout();
            pnDataLearning.SuspendLayout();
            grbExtraOption.SuspendLayout();
            grbModelOption.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbFrameSlider2).BeginInit();
            pnOption.SuspendLayout();
            grbRawData.SuspendLayout();
            grbImgCorrection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbImgBright).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbImgBlur).BeginInit();
            grbPlayOption2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel5.SuspendLayout();
            grbModelData.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            groupBox10.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tabControl1.ItemSize = new Size(113, 40);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(4, 5, 4, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1983, 1118);
            tabControl1.TabIndex = 32;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(panelTimeline);
            tabPage1.Controls.Add(grbDataCorrection);
            tabPage1.Controls.Add(grbEditRange);
            tabPage1.Controls.Add(lstProcess);
            tabPage1.Controls.Add(grbFiltering);
            tabPage1.Controls.Add(grbDataList);
            tabPage1.Controls.Add(grbRunningData);
            tabPage1.Controls.Add(lblTitle);
            tabPage1.Controls.Add(grbPlayOption1);
            tabPage1.Controls.Add(tbFrameSlider1);
            tabPage1.Controls.Add(pbCameraView);
            tabPage1.Controls.Add(lblPath);
            tabPage1.Controls.Add(btnLoadData);
            tabPage1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            tabPage1.Location = new Point(4, 44);
            tabPage1.Margin = new Padding(4, 5, 4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 5, 4, 5);
            tabPage1.Size = new Size(1975, 1070);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "주행 데이터 관리";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelTimeline
            // 
            panelTimeline.BackColor = Color.Black;
            panelTimeline.BorderStyle = BorderStyle.FixedSingle;
            panelTimeline.Location = new Point(399, 835);
            panelTimeline.Margin = new Padding(4, 5, 4, 5);
            panelTimeline.Name = "panelTimeline";
            panelTimeline.Size = new Size(1301, 169);
            panelTimeline.TabIndex = 53;
            // 
            // grbDataCorrection
            // 
            grbDataCorrection.Controls.Add(chkActBW);
            grbDataCorrection.Controls.Add(lbBW);
            grbDataCorrection.Controls.Add(lbBlur);
            grbDataCorrection.Controls.Add(lbBright);
            grbDataCorrection.Controls.Add(tbBright);
            grbDataCorrection.Controls.Add(tbBlur);
            grbDataCorrection.Location = new Point(24, 723);
            grbDataCorrection.Margin = new Padding(4, 5, 4, 5);
            grbDataCorrection.Name = "grbDataCorrection";
            grbDataCorrection.Padding = new Padding(4, 5, 4, 5);
            grbDataCorrection.Size = new Size(349, 282);
            grbDataCorrection.TabIndex = 52;
            grbDataCorrection.TabStop = false;
            grbDataCorrection.Text = "데이터 보정";
            // 
            // chkActBW
            // 
            chkActBW.AutoSize = true;
            chkActBW.Location = new Point(144, 227);
            chkActBW.Margin = new Padding(4, 5, 4, 5);
            chkActBW.Name = "chkActBW";
            chkActBW.Size = new Size(92, 29);
            chkActBW.TabIndex = 5;
            chkActBW.Text = "활성화";
            chkActBW.UseVisualStyleBackColor = true;
            // 
            // lbBW
            // 
            lbBW.AutoSize = true;
            lbBW.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbBW.Location = new Point(21, 227);
            lbBW.Margin = new Padding(4, 0, 4, 0);
            lbBW.Name = "lbBW";
            lbBW.Size = new Size(99, 28);
            lbBW.TabIndex = 4;
            lbBW.Text = "흑백 반전";
            // 
            // lbBlur
            // 
            lbBlur.AutoSize = true;
            lbBlur.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbBlur.Location = new Point(7, 147);
            lbBlur.Margin = new Padding(4, 0, 4, 0);
            lbBlur.Name = "lbBlur";
            lbBlur.Size = new Size(146, 28);
            lbBlur.TabIndex = 3;
            lbBlur.Text = "흐림 효과 조절";
            // 
            // lbBright
            // 
            lbBright.AutoSize = true;
            lbBright.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbBright.Location = new Point(21, 55);
            lbBright.Margin = new Padding(4, 0, 4, 0);
            lbBright.Name = "lbBright";
            lbBright.Size = new Size(99, 28);
            lbBright.TabIndex = 2;
            lbBright.Text = "밝기 조절";
            // 
            // tbBright
            // 
            tbBright.Location = new Point(144, 33);
            tbBright.Margin = new Padding(4, 5, 4, 5);
            tbBright.Name = "tbBright";
            tbBright.Size = new Size(196, 69);
            tbBright.TabIndex = 0;
            // 
            // tbBlur
            // 
            tbBlur.Location = new Point(144, 127);
            tbBlur.Margin = new Padding(4, 5, 4, 5);
            tbBlur.Name = "tbBlur";
            tbBlur.Size = new Size(196, 69);
            tbBlur.TabIndex = 1;
            // 
            // grbEditRange
            // 
            grbEditRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grbEditRange.Controls.Add(btnSetPoint1);
            grbEditRange.Controls.Add(btnCancel);
            grbEditRange.Controls.Add(btnDelete);
            grbEditRange.Controls.Add(btnSetPoint2);
            grbEditRange.Controls.Add(btnRestore);
            grbEditRange.Controls.Add(lblRange);
            grbEditRange.Location = new Point(1396, 425);
            grbEditRange.Margin = new Padding(4, 5, 4, 5);
            grbEditRange.Name = "grbEditRange";
            grbEditRange.Padding = new Padding(4, 5, 4, 5);
            grbEditRange.Size = new Size(307, 308);
            grbEditRange.TabIndex = 51;
            grbEditRange.TabStop = false;
            grbEditRange.Text = "선택 범위 편집";
            // 
            // btnSetPoint1
            // 
            btnSetPoint1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSetPoint1.Location = new Point(6, 37);
            btnSetPoint1.Margin = new Padding(4, 5, 4, 5);
            btnSetPoint1.Name = "btnSetPoint1";
            btnSetPoint1.Size = new Size(140, 58);
            btnSetPoint1.TabIndex = 41;
            btnSetPoint1.Text = "시작 지점 선택";
            btnSetPoint1.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(1, 235);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(297, 63);
            btnCancel.TabIndex = 50;
            btnCancel.Text = "선택 취소";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(1, 167);
            btnDelete.Margin = new Padding(4, 5, 4, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(140, 58);
            btnDelete.TabIndex = 43;
            btnDelete.Text = "선택 삭제";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSetPoint2
            // 
            btnSetPoint2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSetPoint2.Location = new Point(154, 37);
            btnSetPoint2.Margin = new Padding(4, 5, 4, 5);
            btnSetPoint2.Name = "btnSetPoint2";
            btnSetPoint2.Size = new Size(149, 58);
            btnSetPoint2.TabIndex = 42;
            btnSetPoint2.Text = "끝 지점 선택";
            btnSetPoint2.UseVisualStyleBackColor = true;
            // 
            // btnRestore
            // 
            btnRestore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRestore.Location = new Point(150, 167);
            btnRestore.Margin = new Padding(4, 5, 4, 5);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(149, 58);
            btnRestore.TabIndex = 44;
            btnRestore.Text = "삭제 복원";
            btnRestore.UseVisualStyleBackColor = true;
            // 
            // lblRange
            // 
            lblRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRange.AutoSize = true;
            lblRange.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lblRange.Location = new Point(121, 118);
            lblRange.Margin = new Padding(4, 0, 4, 0);
            lblRange.Name = "lblRange";
            lblRange.Size = new Size(59, 32);
            lblRange.TabIndex = 45;
            lblRange.Text = "[0,0)";
            // 
            // lstProcess
            // 
            lstProcess.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstProcess.Location = new Point(24, 512);
            lstProcess.Margin = new Padding(4, 5, 4, 5);
            lstProcess.Name = "lstProcess";
            lstProcess.Size = new Size(347, 199);
            lstProcess.TabIndex = 49;
            lstProcess.UseCompatibleStateImageBehavior = false;
            // 
            // grbFiltering
            // 
            grbFiltering.Controls.Add(tbCriteria);
            grbFiltering.Controls.Add(cmbRange);
            grbFiltering.Controls.Add(cmbDirSpeed);
            grbFiltering.Controls.Add(btnFilter);
            grbFiltering.Controls.Add(txtFilter);
            grbFiltering.Location = new Point(24, 268);
            grbFiltering.Margin = new Padding(4, 5, 4, 5);
            grbFiltering.Name = "grbFiltering";
            grbFiltering.Padding = new Padding(4, 5, 4, 5);
            grbFiltering.Size = new Size(349, 233);
            grbFiltering.TabIndex = 48;
            grbFiltering.TabStop = false;
            grbFiltering.Text = "범위 필터링";
            // 
            // tbCriteria
            // 
            tbCriteria.Location = new Point(176, 168);
            tbCriteria.Margin = new Padding(4, 5, 4, 5);
            tbCriteria.Name = "tbCriteria";
            tbCriteria.Size = new Size(148, 31);
            tbCriteria.TabIndex = 26;
            tbCriteria.Text = "0.0";
            // 
            // cmbRange
            // 
            cmbRange.FormattingEnabled = true;
            cmbRange.Items.AddRange(new object[] { ">", "<", "≥", "≤" });
            cmbRange.Location = new Point(176, 105);
            cmbRange.Margin = new Padding(4, 5, 4, 5);
            cmbRange.Name = "cmbRange";
            cmbRange.Size = new Size(148, 33);
            cmbRange.TabIndex = 25;
            cmbRange.Text = "범위";
            // 
            // cmbDirSpeed
            // 
            cmbDirSpeed.FormattingEnabled = true;
            cmbDirSpeed.Items.AddRange(new object[] { "방향", "속도" });
            cmbDirSpeed.Location = new Point(176, 42);
            cmbDirSpeed.Margin = new Padding(4, 5, 4, 5);
            cmbDirSpeed.Name = "cmbDirSpeed";
            cmbDirSpeed.Size = new Size(148, 33);
            cmbDirSpeed.TabIndex = 24;
            cmbDirSpeed.Text = "방향/속도";
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFilter.Location = new Point(10, 40);
            btnFilter.Margin = new Padding(4, 5, 4, 5);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(149, 52);
            btnFilter.TabIndex = 22;
            btnFilter.Text = "범위 필터링";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtFilter.Location = new Point(176, 232);
            txtFilter.Margin = new Padding(4, 5, 4, 5);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(148, 31);
            txtFilter.TabIndex = 23;
            txtFilter.Text = "0.1";
            // 
            // grbDataList
            // 
            grbDataList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grbDataList.Controls.Add(lstDataList);
            grbDataList.Location = new Point(1711, 10);
            grbDataList.Margin = new Padding(4, 5, 4, 5);
            grbDataList.Name = "grbDataList";
            grbDataList.Padding = new Padding(4, 5, 4, 5);
            grbDataList.Size = new Size(251, 1013);
            grbDataList.TabIndex = 46;
            grbDataList.TabStop = false;
            grbDataList.Text = "데이터 리스트";
            // 
            // lstDataList
            // 
            lstDataList.Dock = DockStyle.Fill;
            lstDataList.FormattingEnabled = true;
            lstDataList.Location = new Point(4, 29);
            lstDataList.Margin = new Padding(4, 5, 4, 5);
            lstDataList.Name = "lstDataList";
            lstDataList.Size = new Size(243, 979);
            lstDataList.TabIndex = 29;
            // 
            // grbRunningData
            // 
            grbRunningData.Controls.Add(pbThrottle);
            grbRunningData.Controls.Add(lbDataSpeed);
            grbRunningData.Controls.Add(lblThrottleValue);
            grbRunningData.Controls.Add(pbSteering);
            grbRunningData.Controls.Add(lblSteeringValue);
            grbRunningData.Controls.Add(lbDataDir);
            grbRunningData.Location = new Point(24, 65);
            grbRunningData.Margin = new Padding(4, 5, 4, 5);
            grbRunningData.Name = "grbRunningData";
            grbRunningData.Padding = new Padding(4, 5, 4, 5);
            grbRunningData.Size = new Size(349, 193);
            grbRunningData.TabIndex = 38;
            grbRunningData.TabStop = false;
            grbRunningData.Text = "주행 데이터";
            // 
            // pbThrottle
            // 
            pbThrottle.Location = new Point(164, 150);
            pbThrottle.Margin = new Padding(4, 5, 4, 5);
            pbThrottle.Name = "pbThrottle";
            pbThrottle.Size = new Size(139, 17);
            pbThrottle.TabIndex = 13;
            // 
            // lbDataSpeed
            // 
            lbDataSpeed.AutoSize = true;
            lbDataSpeed.Location = new Point(9, 147);
            lbDataSpeed.Margin = new Padding(4, 0, 4, 0);
            lbDataSpeed.Name = "lbDataSpeed";
            lbDataSpeed.Size = new Size(48, 25);
            lbDataSpeed.TabIndex = 13;
            lbDataSpeed.Text = "속도";
            // 
            // lblThrottleValue
            // 
            lblThrottleValue.AutoSize = true;
            lblThrottleValue.Location = new Point(100, 145);
            lblThrottleValue.Margin = new Padding(4, 0, 4, 0);
            lblThrottleValue.Name = "lblThrottleValue";
            lblThrottleValue.Size = new Size(60, 25);
            lblThrottleValue.TabIndex = 14;
            lblThrottleValue.Text = "label8";
            // 
            // pbSteering
            // 
            pbSteering.Location = new Point(163, 70);
            pbSteering.Margin = new Padding(4, 5, 4, 5);
            pbSteering.Name = "pbSteering";
            pbSteering.Size = new Size(139, 17);
            pbSteering.TabIndex = 2;
            // 
            // lblSteeringValue
            // 
            lblSteeringValue.AutoSize = true;
            lblSteeringValue.Location = new Point(99, 63);
            lblSteeringValue.Margin = new Padding(4, 0, 4, 0);
            lblSteeringValue.Name = "lblSteeringValue";
            lblSteeringValue.Size = new Size(60, 25);
            lblSteeringValue.TabIndex = 1;
            lblSteeringValue.Text = "label6";
            // 
            // lbDataDir
            // 
            lbDataDir.AutoSize = true;
            lbDataDir.Location = new Point(10, 63);
            lbDataDir.Margin = new Padding(4, 0, 4, 0);
            lbDataDir.Name = "lbDataDir";
            lbDataDir.Size = new Size(48, 25);
            lbDataDir.TabIndex = 0;
            lbDataDir.Text = "방향";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTitle.Location = new Point(9, 10);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(240, 40);
            lblTitle.TabIndex = 37;
            lblTitle.Text = "주행 데이터 관리";
            // 
            // grbPlayOption1
            // 
            grbPlayOption1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grbPlayOption1.Controls.Add(btnRun1);
            grbPlayOption1.Controls.Add(lbSpeed1);
            grbPlayOption1.Controls.Add(lbFrmMvm1);
            grbPlayOption1.Controls.Add(txtFrmMvm1);
            grbPlayOption1.Controls.Add(cmbSpeed1);
            grbPlayOption1.Controls.Add(btnPlay);
            grbPlayOption1.Controls.Add(btnPrevFrame1);
            grbPlayOption1.Controls.Add(btnNextFrame1);
            grbPlayOption1.Controls.Add(lblFrmInx1);
            grbPlayOption1.Location = new Point(1396, 42);
            grbPlayOption1.Margin = new Padding(4, 5, 4, 5);
            grbPlayOption1.Name = "grbPlayOption1";
            grbPlayOption1.Padding = new Padding(4, 5, 4, 5);
            grbPlayOption1.Size = new Size(307, 370);
            grbPlayOption1.TabIndex = 36;
            grbPlayOption1.TabStop = false;
            grbPlayOption1.Text = "재생 설정";
            // 
            // btnRun1
            // 
            btnRun1.Location = new Point(14, 280);
            btnRun1.Margin = new Padding(4, 5, 4, 5);
            btnRun1.Name = "btnRun1";
            btnRun1.Size = new Size(281, 75);
            btnRun1.TabIndex = 33;
            btnRun1.Text = "▶";
            btnRun1.UseVisualStyleBackColor = true;
            // 
            // lbSpeed1
            // 
            lbSpeed1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbSpeed1.AutoSize = true;
            lbSpeed1.Location = new Point(41, 153);
            lbSpeed1.Margin = new Padding(4, 0, 4, 0);
            lbSpeed1.Name = "lbSpeed1";
            lbSpeed1.Size = new Size(48, 25);
            lbSpeed1.TabIndex = 32;
            lbSpeed1.Text = "배속";
            // 
            // lbFrmMvm1
            // 
            lbFrmMvm1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbFrmMvm1.AutoSize = true;
            lbFrmMvm1.Location = new Point(23, 93);
            lbFrmMvm1.Margin = new Padding(4, 0, 4, 0);
            lbFrmMvm1.Name = "lbFrmMvm1";
            lbFrmMvm1.Size = new Size(108, 25);
            lbFrmMvm1.TabIndex = 30;
            lbFrmMvm1.Text = "프레임 이동";
            // 
            // txtFrmMvm1
            // 
            txtFrmMvm1.Location = new Point(144, 87);
            txtFrmMvm1.Margin = new Padding(4, 5, 4, 5);
            txtFrmMvm1.Name = "txtFrmMvm1";
            txtFrmMvm1.Size = new Size(145, 31);
            txtFrmMvm1.TabIndex = 29;
            // 
            // cmbSpeed1
            // 
            cmbSpeed1.FormattingEnabled = true;
            cmbSpeed1.Items.AddRange(new object[] { "1.0", "1.5", "2.0", "2.5", "3.0" });
            cmbSpeed1.Location = new Point(144, 147);
            cmbSpeed1.Margin = new Padding(4, 5, 4, 5);
            cmbSpeed1.Name = "cmbSpeed1";
            cmbSpeed1.Size = new Size(145, 33);
            cmbSpeed1.TabIndex = 28;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnPlay.Location = new Point(17, 485);
            btnPlay.Margin = new Padding(4, 5, 4, 5);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(287, 75);
            btnPlay.TabIndex = 11;
            btnPlay.Text = "▶";
            btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnPrevFrame1
            // 
            btnPrevFrame1.Location = new Point(13, 195);
            btnPrevFrame1.Margin = new Padding(4, 5, 4, 5);
            btnPrevFrame1.Name = "btnPrevFrame1";
            btnPrevFrame1.Size = new Size(140, 75);
            btnPrevFrame1.TabIndex = 11;
            btnPrevFrame1.Text = "<";
            btnPrevFrame1.UseVisualStyleBackColor = true;
            // 
            // btnNextFrame1
            // 
            btnNextFrame1.Location = new Point(153, 195);
            btnNextFrame1.Margin = new Padding(4, 5, 4, 5);
            btnNextFrame1.Name = "btnNextFrame1";
            btnNextFrame1.Size = new Size(144, 75);
            btnNextFrame1.TabIndex = 12;
            btnNextFrame1.Text = ">";
            btnNextFrame1.UseVisualStyleBackColor = true;
            // 
            // lblFrmInx1
            // 
            lblFrmInx1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblFrmInx1.AutoSize = true;
            lblFrmInx1.Location = new Point(23, 45);
            lblFrmInx1.Margin = new Padding(4, 0, 4, 0);
            lblFrmInx1.Name = "lblFrmInx1";
            lblFrmInx1.Size = new Size(234, 25);
            lblFrmInx1.TabIndex = 0;
            lblFrmInx1.Text = "해당 프레임    :        00000";
            // 
            // tbFrameSlider1
            // 
            tbFrameSlider1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbFrameSlider1.Location = new Point(396, 750);
            tbFrameSlider1.Margin = new Padding(4, 5, 4, 5);
            tbFrameSlider1.Name = "tbFrameSlider1";
            tbFrameSlider1.Size = new Size(1307, 69);
            tbFrameSlider1.TabIndex = 35;
            // 
            // pbCameraView
            // 
            pbCameraView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbCameraView.Location = new Point(399, 63);
            pbCameraView.Margin = new Padding(4, 5, 4, 5);
            pbCameraView.Name = "pbCameraView";
            pbCameraView.Size = new Size(1001, 670);
            pbCameraView.SizeMode = PictureBoxSizeMode.Zoom;
            pbCameraView.TabIndex = 34;
            pbCameraView.TabStop = false;
            // 
            // lblPath
            // 
            lblPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPath.AutoSize = true;
            lblPath.Location = new Point(453, 20);
            lblPath.Margin = new Padding(4, 0, 4, 0);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(48, 25);
            lblPath.TabIndex = 33;
            lblPath.Text = "경로";
            // 
            // btnLoadData
            // 
            btnLoadData.ImageAlign = ContentAlignment.MiddleRight;
            btnLoadData.Location = new Point(248, 10);
            btnLoadData.Margin = new Padding(4, 5, 4, 5);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(193, 45);
            btnLoadData.TabIndex = 32;
            btnLoadData.Text = "주행 데이터 불러오기";
            btnLoadData.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(pnModelManage);
            tabPage2.Controls.Add(pnDataLearning);
            tabPage2.ForeColor = SystemColors.ControlText;
            tabPage2.Location = new Point(4, 44);
            tabPage2.Margin = new Padding(4, 5, 4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 5, 4, 5);
            tabPage2.Size = new Size(1975, 1070);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "데이터 학습";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnModelManage
            // 
            pnModelManage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnModelManage.Controls.Add(pnModelScore);
            pnModelManage.Controls.Add(lbModelManage);
            pnModelManage.Controls.Add(lstvModelManage);
            pnModelManage.Location = new Point(4, 493);
            pnModelManage.Margin = new Padding(4, 5, 4, 5);
            pnModelManage.Name = "pnModelManage";
            pnModelManage.Size = new Size(1963, 540);
            pnModelManage.TabIndex = 49;
            // 
            // pnModelScore
            // 
            pnModelScore.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnModelScore.Controls.Add(lbLoss);
            pnModelScore.Controls.Add(lbModelScore);
            pnModelScore.Location = new Point(1111, 0);
            pnModelScore.Margin = new Padding(4, 5, 4, 5);
            pnModelScore.Name = "pnModelScore";
            pnModelScore.Size = new Size(851, 540);
            pnModelScore.TabIndex = 48;
            // 
            // lbLoss
            // 
            lbLoss.AutoSize = true;
            lbLoss.Font = new Font("함초롬돋움", 12F);
            lbLoss.Location = new Point(27, 67);
            lbLoss.Margin = new Padding(4, 0, 4, 0);
            lbLoss.Name = "lbLoss";
            lbLoss.Size = new Size(117, 32);
            lbLoss.TabIndex = 1;
            lbLoss.Text = "손실값 : 0";
            // 
            // lbModelScore
            // 
            lbModelScore.AutoSize = true;
            lbModelScore.Font = new Font("함초롬돋움", 12F);
            lbModelScore.Location = new Point(27, 22);
            lbModelScore.Margin = new Padding(4, 0, 4, 0);
            lbModelScore.Name = "lbModelScore";
            lbModelScore.Size = new Size(173, 32);
            lbModelScore.TabIndex = 0;
            lbModelScore.Text = "모델 점수 : 100";
            // 
            // lbModelManage
            // 
            lbModelManage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbModelManage.AutoSize = true;
            lbModelManage.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbModelManage.Location = new Point(26, 22);
            lbModelManage.Margin = new Padding(4, 0, 4, 0);
            lbModelManage.Name = "lbModelManage";
            lbModelManage.Size = new Size(211, 40);
            lbModelManage.TabIndex = 47;
            lbModelManage.Text = "학습 모델 관리";
            // 
            // lstvModelManage
            // 
            lstvModelManage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstvModelManage.BorderStyle = BorderStyle.None;
            lstvModelManage.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
            lstvModelManage.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lstvModelManage.FullRowSelect = true;
            lstvModelManage.GridLines = true;
            lstvModelManage.Location = new Point(44, 100);
            lstvModelManage.Margin = new Padding(4, 5, 4, 5);
            lstvModelManage.Name = "lstvModelManage";
            lstvModelManage.Size = new Size(1027, 407);
            lstvModelManage.TabIndex = 46;
            lstvModelManage.UseCompatibleStateImageBehavior = false;
            lstvModelManage.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "모델 이름";
            columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "모델 종류";
            columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "사용한 데이터";
            columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "수정한 날짜";
            columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "주석";
            columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "전이학습";
            columnHeader6.Width = 120;
            // 
            // pnDataLearning
            // 
            pnDataLearning.Controls.Add(grbExtraOption);
            pnDataLearning.Controls.Add(lbLearningRate);
            pnDataLearning.Controls.Add(btnLearningStop);
            pnDataLearning.Controls.Add(pbLearning);
            pnDataLearning.Controls.Add(btnLearningStart);
            pnDataLearning.Controls.Add(grbModelOption);
            pnDataLearning.Controls.Add(lbDataLearning);
            pnDataLearning.Dock = DockStyle.Top;
            pnDataLearning.Location = new Point(4, 5);
            pnDataLearning.Margin = new Padding(4, 5, 4, 5);
            pnDataLearning.Name = "pnDataLearning";
            pnDataLearning.Size = new Size(1967, 482);
            pnDataLearning.TabIndex = 48;
            // 
            // grbExtraOption
            // 
            grbExtraOption.Controls.Add(lbExtraExpl);
            grbExtraOption.Controls.Add(txtExtraExpl);
            grbExtraOption.Controls.Add(lbCaution);
            grbExtraOption.Controls.Add(txtExtraModel);
            grbExtraOption.Controls.Add(lbExtraModelName);
            grbExtraOption.Controls.Add(lbExtraModelPath);
            grbExtraOption.Controls.Add(btnExtraModel);
            grbExtraOption.Location = new Point(1323, 85);
            grbExtraOption.Margin = new Padding(4, 5, 4, 5);
            grbExtraOption.Name = "grbExtraOption";
            grbExtraOption.Padding = new Padding(4, 5, 4, 5);
            grbExtraOption.Size = new Size(633, 252);
            grbExtraOption.TabIndex = 54;
            grbExtraOption.TabStop = false;
            grbExtraOption.Text = "추가 학습 설정";
            // 
            // lbExtraExpl
            // 
            lbExtraExpl.AutoSize = true;
            lbExtraExpl.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbExtraExpl.Location = new Point(13, 168);
            lbExtraExpl.Margin = new Padding(4, 0, 4, 0);
            lbExtraExpl.Name = "lbExtraExpl";
            lbExtraExpl.Size = new Size(60, 31);
            lbExtraExpl.TabIndex = 51;
            lbExtraExpl.Text = "설명";
            // 
            // txtExtraExpl
            // 
            txtExtraExpl.Location = new Point(93, 163);
            txtExtraExpl.Margin = new Padding(4, 5, 4, 5);
            txtExtraExpl.Name = "txtExtraExpl";
            txtExtraExpl.Size = new Size(474, 33);
            txtExtraExpl.TabIndex = 50;
            // 
            // lbCaution
            // 
            lbCaution.AutoSize = true;
            lbCaution.Location = new Point(13, 210);
            lbCaution.Margin = new Padding(4, 0, 4, 0);
            lbCaution.Name = "lbCaution";
            lbCaution.Size = new Size(548, 28);
            lbCaution.TabIndex = 52;
            lbCaution.Text = "※ 기존 모델의 추가 학습은 추가 학습 설정에서 진행하세요.";
            // 
            // txtExtraModel
            // 
            txtExtraModel.Location = new Point(127, 112);
            txtExtraModel.Margin = new Padding(4, 5, 4, 5);
            txtExtraModel.Name = "txtExtraModel";
            txtExtraModel.Size = new Size(440, 33);
            txtExtraModel.TabIndex = 49;
            // 
            // lbExtraModelName
            // 
            lbExtraModelName.AutoSize = true;
            lbExtraModelName.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbExtraModelName.Location = new Point(13, 110);
            lbExtraModelName.Margin = new Padding(4, 0, 4, 0);
            lbExtraModelName.Name = "lbExtraModelName";
            lbExtraModelName.Size = new Size(114, 31);
            lbExtraModelName.TabIndex = 48;
            lbExtraModelName.Text = "모델 이름";
            // 
            // lbExtraModelPath
            // 
            lbExtraModelPath.AutoSize = true;
            lbExtraModelPath.Location = new Point(217, 52);
            lbExtraModelPath.Margin = new Padding(4, 0, 4, 0);
            lbExtraModelPath.Name = "lbExtraModelPath";
            lbExtraModelPath.Size = new Size(52, 28);
            lbExtraModelPath.TabIndex = 1;
            lbExtraModelPath.Text = "경로";
            // 
            // btnExtraModel
            // 
            btnExtraModel.Location = new Point(9, 40);
            btnExtraModel.Margin = new Padding(4, 5, 4, 5);
            btnExtraModel.Name = "btnExtraModel";
            btnExtraModel.Size = new Size(187, 50);
            btnExtraModel.TabIndex = 0;
            btnExtraModel.Text = "학습 모델 불러오기";
            btnExtraModel.UseVisualStyleBackColor = true;
            // 
            // lbLearningRate
            // 
            lbLearningRate.AutoSize = true;
            lbLearningRate.Location = new Point(209, 415);
            lbLearningRate.Margin = new Padding(4, 0, 4, 0);
            lbLearningRate.Name = "lbLearningRate";
            lbLearningRate.Size = new Size(118, 28);
            lbLearningRate.TabIndex = 53;
            lbLearningRate.Text = "학습률 : 0%";
            // 
            // btnLearningStop
            // 
            btnLearningStop.Location = new Point(44, 415);
            btnLearningStop.Margin = new Padding(4, 5, 4, 5);
            btnLearningStop.Name = "btnLearningStop";
            btnLearningStop.Size = new Size(156, 47);
            btnLearningStop.TabIndex = 52;
            btnLearningStop.Text = "학습 중지";
            btnLearningStop.UseVisualStyleBackColor = true;
            // 
            // pbLearning
            // 
            pbLearning.Location = new Point(209, 350);
            pbLearning.Margin = new Padding(4, 5, 4, 5);
            pbLearning.Name = "pbLearning";
            pbLearning.Size = new Size(1641, 53);
            pbLearning.TabIndex = 51;
            // 
            // btnLearningStart
            // 
            btnLearningStart.Location = new Point(44, 347);
            btnLearningStart.Margin = new Padding(4, 5, 4, 5);
            btnLearningStart.Name = "btnLearningStart";
            btnLearningStart.Size = new Size(156, 58);
            btnLearningStart.TabIndex = 50;
            btnLearningStart.Text = "학습 시작";
            btnLearningStart.UseVisualStyleBackColor = true;
            // 
            // grbModelOption
            // 
            grbModelOption.Controls.Add(lbDonkeyPath);
            grbModelOption.Controls.Add(lbSavePath);
            grbModelOption.Controls.Add(btnDonkeyPath);
            grbModelOption.Controls.Add(btnSavePath);
            grbModelOption.Controls.Add(cmbMulti);
            grbModelOption.Controls.Add(lbMulti);
            grbModelOption.Controls.Add(label32);
            grbModelOption.Controls.Add(lbExpl);
            grbModelOption.Controls.Add(txtModelName);
            grbModelOption.Controls.Add(lbModelName);
            grbModelOption.Controls.Add(txtEpoch);
            grbModelOption.Controls.Add(lbEpoch);
            grbModelOption.Controls.Add(cmbModelSelect);
            grbModelOption.Controls.Add(lbModelSelect);
            grbModelOption.Controls.Add(txtExpl);
            grbModelOption.Location = new Point(44, 85);
            grbModelOption.Margin = new Padding(4, 5, 4, 5);
            grbModelOption.Name = "grbModelOption";
            grbModelOption.Padding = new Padding(4, 5, 4, 5);
            grbModelOption.Size = new Size(1270, 252);
            grbModelOption.TabIndex = 49;
            grbModelOption.TabStop = false;
            grbModelOption.Text = "모델 설정";
            // 
            // lbDonkeyPath
            // 
            lbDonkeyPath.AutoSize = true;
            lbDonkeyPath.Location = new Point(679, 210);
            lbDonkeyPath.Margin = new Padding(4, 0, 4, 0);
            lbDonkeyPath.Name = "lbDonkeyPath";
            lbDonkeyPath.Size = new Size(52, 28);
            lbDonkeyPath.TabIndex = 54;
            lbDonkeyPath.Text = "경로";
            // 
            // lbSavePath
            // 
            lbSavePath.AutoSize = true;
            lbSavePath.Location = new Point(571, 162);
            lbSavePath.Margin = new Padding(4, 0, 4, 0);
            lbSavePath.Name = "lbSavePath";
            lbSavePath.Size = new Size(52, 28);
            lbSavePath.TabIndex = 54;
            lbSavePath.Text = "경로";
            // 
            // btnDonkeyPath
            // 
            btnDonkeyPath.Location = new Point(456, 202);
            btnDonkeyPath.Margin = new Padding(4, 5, 4, 5);
            btnDonkeyPath.Name = "btnDonkeyPath";
            btnDonkeyPath.Size = new Size(214, 45);
            btnDonkeyPath.TabIndex = 53;
            btnDonkeyPath.Text = "동키카 프로젝트 경로";
            btnDonkeyPath.UseVisualStyleBackColor = true;
            // 
            // btnSavePath
            // 
            btnSavePath.Location = new Point(456, 153);
            btnSavePath.Margin = new Padding(4, 5, 4, 5);
            btnSavePath.Name = "btnSavePath";
            btnSavePath.Size = new Size(107, 45);
            btnSavePath.TabIndex = 53;
            btnSavePath.Text = "저장 경로";
            btnSavePath.UseVisualStyleBackColor = true;
            // 
            // cmbMulti
            // 
            cmbMulti.FormattingEnabled = true;
            cmbMulti.Items.AddRange(new object[] { "1", "16", "32", "64", "128" });
            cmbMulti.Location = new Point(234, 173);
            cmbMulti.Margin = new Padding(4, 5, 4, 5);
            cmbMulti.Name = "cmbMulti";
            cmbMulti.Size = new Size(140, 36);
            cmbMulti.TabIndex = 51;
            // 
            // lbMulti
            // 
            lbMulti.AutoSize = true;
            lbMulti.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbMulti.Location = new Point(20, 173);
            lbMulti.Margin = new Padding(4, 0, 4, 0);
            lbMulti.Name = "lbMulti";
            lbMulti.Size = new Size(222, 31);
            lbMulti.TabIndex = 50;
            lbMulti.Text = "동시 처리 데이터 수";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(411, 33);
            label32.Margin = new Padding(4, 0, 4, 0);
            label32.Name = "label32";
            label32.Size = new Size(17, 224);
            label32.TabIndex = 49;
            label32.Text = "l\r\nl\r\nl\r\nl\r\nl\r\nl\r\nl\r\nl\r\n";
            // 
            // lbExpl
            // 
            lbExpl.AutoSize = true;
            lbExpl.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbExpl.Location = new Point(456, 108);
            lbExpl.Margin = new Padding(4, 0, 4, 0);
            lbExpl.Name = "lbExpl";
            lbExpl.Size = new Size(60, 31);
            lbExpl.TabIndex = 48;
            lbExpl.Text = "설명";
            // 
            // txtModelName
            // 
            txtModelName.Location = new Point(570, 48);
            txtModelName.Margin = new Padding(4, 5, 4, 5);
            txtModelName.Name = "txtModelName";
            txtModelName.Size = new Size(670, 33);
            txtModelName.TabIndex = 47;
            // 
            // lbModelName
            // 
            lbModelName.AutoSize = true;
            lbModelName.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbModelName.Location = new Point(456, 47);
            lbModelName.Margin = new Padding(4, 0, 4, 0);
            lbModelName.Name = "lbModelName";
            lbModelName.Size = new Size(114, 31);
            lbModelName.TabIndex = 46;
            lbModelName.Text = "모델 이름";
            // 
            // txtEpoch
            // 
            txtEpoch.Location = new Point(203, 105);
            txtEpoch.Margin = new Padding(4, 5, 4, 5);
            txtEpoch.Name = "txtEpoch";
            txtEpoch.Size = new Size(171, 33);
            txtEpoch.TabIndex = 45;
            // 
            // lbEpoch
            // 
            lbEpoch.AutoSize = true;
            lbEpoch.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbEpoch.Location = new Point(20, 110);
            lbEpoch.Margin = new Padding(4, 0, 4, 0);
            lbEpoch.Name = "lbEpoch";
            lbEpoch.Size = new Size(168, 31);
            lbEpoch.TabIndex = 44;
            lbEpoch.Text = "반복 학습 횟수";
            // 
            // cmbModelSelect
            // 
            cmbModelSelect.FormattingEnabled = true;
            cmbModelSelect.Items.AddRange(new object[] { "Linear", "Behavioral" });
            cmbModelSelect.Location = new Point(203, 47);
            cmbModelSelect.Margin = new Padding(4, 5, 4, 5);
            cmbModelSelect.Name = "cmbModelSelect";
            cmbModelSelect.Size = new Size(171, 36);
            cmbModelSelect.TabIndex = 1;
            // 
            // lbModelSelect
            // 
            lbModelSelect.AutoSize = true;
            lbModelSelect.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbModelSelect.Location = new Point(20, 47);
            lbModelSelect.Margin = new Padding(4, 0, 4, 0);
            lbModelSelect.Name = "lbModelSelect";
            lbModelSelect.Size = new Size(168, 31);
            lbModelSelect.TabIndex = 0;
            lbModelSelect.Text = "모델 종류 선택";
            // 
            // txtExpl
            // 
            txtExpl.Location = new Point(533, 105);
            txtExpl.Margin = new Padding(4, 5, 4, 5);
            txtExpl.Name = "txtExpl";
            txtExpl.Size = new Size(707, 33);
            txtExpl.TabIndex = 42;
            // 
            // lbDataLearning
            // 
            lbDataLearning.AutoSize = true;
            lbDataLearning.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbDataLearning.Location = new Point(26, 23);
            lbDataLearning.Margin = new Padding(4, 0, 4, 0);
            lbDataLearning.Name = "lbDataLearning";
            lbDataLearning.Size = new Size(172, 40);
            lbDataLearning.TabIndex = 48;
            lbDataLearning.Text = "데이터 학습";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(tbFrameSlider2);
            tabPage3.Controls.Add(pnOption);
            tabPage3.Controls.Add(splitContainer1);
            tabPage3.Location = new Point(4, 44);
            tabPage3.Margin = new Padding(4, 5, 4, 5);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(4, 5, 4, 5);
            tabPage3.Size = new Size(1975, 1070);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "학습 미리보기";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbFrameSlider2
            // 
            tbFrameSlider2.AutoSize = false;
            tbFrameSlider2.Location = new Point(11, 662);
            tbFrameSlider2.Margin = new Padding(4, 5, 4, 5);
            tbFrameSlider2.Name = "tbFrameSlider2";
            tbFrameSlider2.Size = new Size(1949, 68);
            tbFrameSlider2.TabIndex = 44;
            // 
            // pnOption
            // 
            pnOption.Controls.Add(grbRawData);
            pnOption.Controls.Add(grbImgCorrection);
            pnOption.Controls.Add(grbPlayOption2);
            pnOption.Location = new Point(4, 740);
            pnOption.Margin = new Padding(4, 5, 4, 5);
            pnOption.Name = "pnOption";
            pnOption.Size = new Size(1963, 292);
            pnOption.TabIndex = 43;
            // 
            // grbRawData
            // 
            grbRawData.Controls.Add(pbRawDataSpeed);
            grbRawData.Controls.Add(lbRawDataSpeed);
            grbRawData.Controls.Add(lbRawDataSpeed2);
            grbRawData.Controls.Add(pbRawDataDir);
            grbRawData.Controls.Add(lbRawDataDir2);
            grbRawData.Controls.Add(lbRawDataDir);
            grbRawData.Location = new Point(1069, 5);
            grbRawData.Margin = new Padding(4, 5, 4, 5);
            grbRawData.Name = "grbRawData";
            grbRawData.Padding = new Padding(4, 5, 4, 5);
            grbRawData.Size = new Size(349, 202);
            grbRawData.TabIndex = 40;
            grbRawData.TabStop = false;
            grbRawData.Text = "원본 주행 데이터";
            // 
            // pbRawDataSpeed
            // 
            pbRawDataSpeed.Location = new Point(139, 150);
            pbRawDataSpeed.Margin = new Padding(4, 5, 4, 5);
            pbRawDataSpeed.Name = "pbRawDataSpeed";
            pbRawDataSpeed.Size = new Size(164, 17);
            pbRawDataSpeed.TabIndex = 13;
            // 
            // lbRawDataSpeed
            // 
            lbRawDataSpeed.AutoSize = true;
            lbRawDataSpeed.Location = new Point(9, 147);
            lbRawDataSpeed.Margin = new Padding(4, 0, 4, 0);
            lbRawDataSpeed.Name = "lbRawDataSpeed";
            lbRawDataSpeed.Size = new Size(52, 28);
            lbRawDataSpeed.TabIndex = 13;
            lbRawDataSpeed.Text = "속도";
            // 
            // lbRawDataSpeed2
            // 
            lbRawDataSpeed2.AutoSize = true;
            lbRawDataSpeed2.Location = new Point(67, 142);
            lbRawDataSpeed2.Margin = new Padding(4, 0, 4, 0);
            lbRawDataSpeed2.Name = "lbRawDataSpeed2";
            lbRawDataSpeed2.Size = new Size(66, 28);
            lbRawDataSpeed2.TabIndex = 14;
            lbRawDataSpeed2.Text = "label8";
            // 
            // pbRawDataDir
            // 
            pbRawDataDir.Location = new Point(137, 70);
            pbRawDataDir.Margin = new Padding(4, 5, 4, 5);
            pbRawDataDir.Name = "pbRawDataDir";
            pbRawDataDir.Size = new Size(164, 17);
            pbRawDataDir.TabIndex = 2;
            // 
            // lbRawDataDir2
            // 
            lbRawDataDir2.AutoSize = true;
            lbRawDataDir2.Location = new Point(67, 63);
            lbRawDataDir2.Margin = new Padding(4, 0, 4, 0);
            lbRawDataDir2.Name = "lbRawDataDir2";
            lbRawDataDir2.Size = new Size(66, 28);
            lbRawDataDir2.TabIndex = 1;
            lbRawDataDir2.Text = "label6";
            // 
            // lbRawDataDir
            // 
            lbRawDataDir.AutoSize = true;
            lbRawDataDir.Location = new Point(10, 63);
            lbRawDataDir.Margin = new Padding(4, 0, 4, 0);
            lbRawDataDir.Name = "lbRawDataDir";
            lbRawDataDir.Size = new Size(52, 28);
            lbRawDataDir.TabIndex = 0;
            lbRawDataDir.Text = "방향";
            // 
            // grbImgCorrection
            // 
            grbImgCorrection.Controls.Add(chkImgActBW);
            grbImgCorrection.Controls.Add(lbImgBW);
            grbImgCorrection.Controls.Add(lbImgBlur);
            grbImgCorrection.Controls.Add(lbImgBright);
            grbImgCorrection.Controls.Add(tbImgBright);
            grbImgCorrection.Controls.Add(tbImgBlur);
            grbImgCorrection.Location = new Point(7, 10);
            grbImgCorrection.Margin = new Padding(4, 5, 4, 5);
            grbImgCorrection.Name = "grbImgCorrection";
            grbImgCorrection.Padding = new Padding(4, 5, 4, 5);
            grbImgCorrection.Size = new Size(407, 275);
            grbImgCorrection.TabIndex = 2;
            grbImgCorrection.TabStop = false;
            grbImgCorrection.Text = "이미지 보정";
            // 
            // chkImgActBW
            // 
            chkImgActBW.AutoSize = true;
            chkImgActBW.Location = new Point(144, 227);
            chkImgActBW.Margin = new Padding(4, 5, 4, 5);
            chkImgActBW.Name = "chkImgActBW";
            chkImgActBW.Size = new Size(98, 32);
            chkImgActBW.TabIndex = 5;
            chkImgActBW.Text = "활성화";
            chkImgActBW.UseVisualStyleBackColor = true;
            // 
            // lbImgBW
            // 
            lbImgBW.AutoSize = true;
            lbImgBW.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbImgBW.Location = new Point(21, 227);
            lbImgBW.Margin = new Padding(4, 0, 4, 0);
            lbImgBW.Name = "lbImgBW";
            lbImgBW.Size = new Size(99, 28);
            lbImgBW.TabIndex = 4;
            lbImgBW.Text = "흑백 반전";
            // 
            // lbImgBlur
            // 
            lbImgBlur.AutoSize = true;
            lbImgBlur.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbImgBlur.Location = new Point(7, 147);
            lbImgBlur.Margin = new Padding(4, 0, 4, 0);
            lbImgBlur.Name = "lbImgBlur";
            lbImgBlur.Size = new Size(146, 28);
            lbImgBlur.TabIndex = 3;
            lbImgBlur.Text = "흐림 효과 조절";
            // 
            // lbImgBright
            // 
            lbImgBright.AutoSize = true;
            lbImgBright.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbImgBright.Location = new Point(21, 55);
            lbImgBright.Margin = new Padding(4, 0, 4, 0);
            lbImgBright.Name = "lbImgBright";
            lbImgBright.Size = new Size(99, 28);
            lbImgBright.TabIndex = 2;
            lbImgBright.Text = "밝기 조절";
            // 
            // tbImgBright
            // 
            tbImgBright.Location = new Point(144, 33);
            tbImgBright.Margin = new Padding(4, 5, 4, 5);
            tbImgBright.Name = "tbImgBright";
            tbImgBright.Size = new Size(254, 69);
            tbImgBright.TabIndex = 0;
            // 
            // tbImgBlur
            // 
            tbImgBlur.Location = new Point(144, 127);
            tbImgBlur.Margin = new Padding(4, 5, 4, 5);
            tbImgBlur.Name = "tbImgBlur";
            tbImgBlur.Size = new Size(254, 69);
            tbImgBlur.TabIndex = 1;
            // 
            // grbPlayOption2
            // 
            grbPlayOption2.Controls.Add(btnRun2);
            grbPlayOption2.Controls.Add(lbSpeed2);
            grbPlayOption2.Controls.Add(lbFrmMvm2);
            grbPlayOption2.Controls.Add(txtFrmMvm2);
            grbPlayOption2.Controls.Add(cmbSpeed2);
            grbPlayOption2.Controls.Add(button8);
            grbPlayOption2.Controls.Add(btnPrevFrame2);
            grbPlayOption2.Controls.Add(btnNextFrame2);
            grbPlayOption2.Controls.Add(lblFrmInx2);
            grbPlayOption2.Location = new Point(423, 10);
            grbPlayOption2.Margin = new Padding(4, 5, 4, 5);
            grbPlayOption2.Name = "grbPlayOption2";
            grbPlayOption2.Padding = new Padding(4, 5, 4, 5);
            grbPlayOption2.Size = new Size(637, 255);
            grbPlayOption2.TabIndex = 37;
            grbPlayOption2.TabStop = false;
            grbPlayOption2.Text = "재생 설정";
            // 
            // btnRun2
            // 
            btnRun2.Location = new Point(311, 147);
            btnRun2.Margin = new Padding(4, 5, 4, 5);
            btnRun2.Name = "btnRun2";
            btnRun2.Size = new Size(293, 58);
            btnRun2.TabIndex = 33;
            btnRun2.Text = "▶";
            btnRun2.UseVisualStyleBackColor = true;
            // 
            // lbSpeed2
            // 
            lbSpeed2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbSpeed2.AutoSize = true;
            lbSpeed2.Location = new Point(41, 143);
            lbSpeed2.Margin = new Padding(4, 0, 4, 0);
            lbSpeed2.Name = "lbSpeed2";
            lbSpeed2.Size = new Size(52, 28);
            lbSpeed2.TabIndex = 32;
            lbSpeed2.Text = "배속";
            // 
            // lbFrmMvm2
            // 
            lbFrmMvm2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbFrmMvm2.AutoSize = true;
            lbFrmMvm2.Location = new Point(23, 90);
            lbFrmMvm2.Margin = new Padding(4, 0, 4, 0);
            lbFrmMvm2.Name = "lbFrmMvm2";
            lbFrmMvm2.Size = new Size(119, 28);
            lbFrmMvm2.TabIndex = 30;
            lbFrmMvm2.Text = "프레임 이동";
            // 
            // txtFrmMvm2
            // 
            txtFrmMvm2.Location = new Point(144, 83);
            txtFrmMvm2.Margin = new Padding(4, 5, 4, 5);
            txtFrmMvm2.Name = "txtFrmMvm2";
            txtFrmMvm2.Size = new Size(145, 33);
            txtFrmMvm2.TabIndex = 29;
            // 
            // cmbSpeed2
            // 
            cmbSpeed2.FormattingEnabled = true;
            cmbSpeed2.Items.AddRange(new object[] { "1.0", "1.5", "2.0", "2.5", "3.0" });
            cmbSpeed2.Location = new Point(144, 137);
            cmbSpeed2.Margin = new Padding(4, 5, 4, 5);
            cmbSpeed2.Name = "cmbSpeed2";
            cmbSpeed2.Size = new Size(145, 36);
            cmbSpeed2.TabIndex = 28;
            // 
            // button8
            // 
            button8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button8.Location = new Point(17, 735);
            button8.Margin = new Padding(4, 5, 4, 5);
            button8.Name = "button8";
            button8.Size = new Size(660, 75);
            button8.TabIndex = 11;
            button8.Text = "▶";
            button8.UseVisualStyleBackColor = true;
            // 
            // btnPrevFrame2
            // 
            btnPrevFrame2.Location = new Point(310, 65);
            btnPrevFrame2.Margin = new Padding(4, 5, 4, 5);
            btnPrevFrame2.Name = "btnPrevFrame2";
            btnPrevFrame2.Size = new Size(140, 50);
            btnPrevFrame2.TabIndex = 11;
            btnPrevFrame2.Text = "<";
            btnPrevFrame2.UseVisualStyleBackColor = true;
            // 
            // btnNextFrame2
            // 
            btnNextFrame2.Location = new Point(460, 65);
            btnNextFrame2.Margin = new Padding(4, 5, 4, 5);
            btnNextFrame2.Name = "btnNextFrame2";
            btnNextFrame2.Size = new Size(144, 50);
            btnNextFrame2.TabIndex = 12;
            btnNextFrame2.Text = ">";
            btnNextFrame2.UseVisualStyleBackColor = true;
            // 
            // lblFrmInx2
            // 
            lblFrmInx2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblFrmInx2.AutoSize = true;
            lblFrmInx2.Location = new Point(23, 47);
            lblFrmInx2.Margin = new Padding(4, 0, 4, 0);
            lblFrmInx2.Name = "lblFrmInx2";
            lblFrmInx2.Size = new Size(262, 28);
            lblFrmInx2.TabIndex = 0;
            lblFrmInx2.Text = "해당 프레임    :        00000";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Top;
            splitContainer1.Location = new Point(4, 5);
            splitContainer1.Margin = new Padding(4, 5, 4, 5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel4);
            splitContainer1.Panel1.Controls.Add(panel5);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel3);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new Size(1967, 647);
            splitContainer1.SplitterDistance = 983;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 40;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel4.Controls.Add(pictureBox2);
            panel4.Location = new Point(0, 143);
            panel4.Margin = new Padding(4, 5, 4, 5);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(14, 17, 14, 17);
            panel4.Size = new Size(983, 503);
            panel4.TabIndex = 43;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox2.BackColor = Color.Linen;
            pictureBox2.Location = new Point(14, 17);
            pictureBox2.Margin = new Padding(4, 5, 4, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(955, 470);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // panel5
            // 
            panel5.Controls.Add(grbModelData);
            panel5.Controls.Add(lbRawDataPath);
            panel5.Controls.Add(btnRawData);
            panel5.Controls.Add(lbCmp);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(4, 5, 4, 5);
            panel5.Name = "panel5";
            panel5.Size = new Size(983, 143);
            panel5.TabIndex = 41;
            // 
            // grbModelData
            // 
            grbModelData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grbModelData.Controls.Add(pbAISpeed);
            grbModelData.Controls.Add(lbAISpeed);
            grbModelData.Controls.Add(lbAISpeed2);
            grbModelData.Controls.Add(pbAIDir);
            grbModelData.Controls.Add(lbAIDir2);
            grbModelData.Controls.Add(lbAIDir1);
            grbModelData.Location = new Point(586, 12);
            grbModelData.Margin = new Padding(4, 5, 4, 5);
            grbModelData.Name = "grbModelData";
            grbModelData.Padding = new Padding(4, 5, 4, 5);
            grbModelData.Size = new Size(362, 120);
            grbModelData.TabIndex = 39;
            grbModelData.TabStop = false;
            grbModelData.Text = "AI모델 주행 데이터";
            // 
            // pbAISpeed
            // 
            pbAISpeed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbAISpeed.Location = new Point(139, 85);
            pbAISpeed.Margin = new Padding(4, 5, 4, 5);
            pbAISpeed.Name = "pbAISpeed";
            pbAISpeed.Size = new Size(215, 17);
            pbAISpeed.TabIndex = 13;
            // 
            // lbAISpeed
            // 
            lbAISpeed.AutoSize = true;
            lbAISpeed.Location = new Point(9, 80);
            lbAISpeed.Margin = new Padding(4, 0, 4, 0);
            lbAISpeed.Name = "lbAISpeed";
            lbAISpeed.Size = new Size(52, 28);
            lbAISpeed.TabIndex = 13;
            lbAISpeed.Text = "속도";
            // 
            // lbAISpeed2
            // 
            lbAISpeed2.AutoSize = true;
            lbAISpeed2.Location = new Point(67, 80);
            lbAISpeed2.Margin = new Padding(4, 0, 4, 0);
            lbAISpeed2.Name = "lbAISpeed2";
            lbAISpeed2.Size = new Size(66, 28);
            lbAISpeed2.TabIndex = 14;
            lbAISpeed2.Text = "label8";
            // 
            // pbAIDir
            // 
            pbAIDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pbAIDir.Location = new Point(137, 43);
            pbAIDir.Margin = new Padding(4, 5, 4, 5);
            pbAIDir.Name = "pbAIDir";
            pbAIDir.Size = new Size(215, 17);
            pbAIDir.TabIndex = 2;
            // 
            // lbAIDir2
            // 
            lbAIDir2.AutoSize = true;
            lbAIDir2.Location = new Point(67, 35);
            lbAIDir2.Margin = new Padding(4, 0, 4, 0);
            lbAIDir2.Name = "lbAIDir2";
            lbAIDir2.Size = new Size(66, 28);
            lbAIDir2.TabIndex = 1;
            lbAIDir2.Text = "label6";
            // 
            // lbAIDir1
            // 
            lbAIDir1.AutoSize = true;
            lbAIDir1.Location = new Point(9, 37);
            lbAIDir1.Margin = new Padding(4, 0, 4, 0);
            lbAIDir1.Name = "lbAIDir1";
            lbAIDir1.Size = new Size(52, 28);
            lbAIDir1.TabIndex = 0;
            lbAIDir1.Text = "방향";
            // 
            // lbRawDataPath
            // 
            lbRawDataPath.AutoSize = true;
            lbRawDataPath.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbRawDataPath.Location = new Point(293, 80);
            lbRawDataPath.Margin = new Padding(4, 0, 4, 0);
            lbRawDataPath.Name = "lbRawDataPath";
            lbRawDataPath.Size = new Size(52, 28);
            lbRawDataPath.TabIndex = 41;
            lbRawDataPath.Text = "경로";
            // 
            // btnRawData
            // 
            btnRawData.Location = new Point(7, 70);
            btnRawData.Margin = new Padding(4, 5, 4, 5);
            btnRawData.Name = "btnRawData";
            btnRawData.Size = new Size(263, 48);
            btnRawData.TabIndex = 40;
            btnRawData.Text = "원본 주행 데이터 불러오기";
            btnRawData.UseVisualStyleBackColor = true;
            // 
            // lbCmp
            // 
            lbCmp.AutoSize = true;
            lbCmp.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lbCmp.Location = new Point(7, 18);
            lbCmp.Margin = new Padding(4, 0, 4, 0);
            lbCmp.Name = "lbCmp";
            lbCmp.Size = new Size(201, 40);
            lbCmp.TabIndex = 39;
            lbCmp.Text = "학습 비교보기";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(pictureBox1);
            panel3.Location = new Point(0, 143);
            panel3.Margin = new Padding(4, 5, 4, 5);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(14, 17, 14, 17);
            panel3.Size = new Size(978, 503);
            panel3.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.Linen;
            pictureBox1.Location = new Point(14, 17);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(949, 470);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(groupBox10);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(btn);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(978, 143);
            panel1.TabIndex = 0;
            // 
            // groupBox10
            // 
            groupBox10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox10.Controls.Add(progressBar4);
            groupBox10.Controls.Add(label20);
            groupBox10.Controls.Add(label21);
            groupBox10.Controls.Add(progressBar5);
            groupBox10.Controls.Add(label22);
            groupBox10.Controls.Add(label23);
            groupBox10.Location = new Point(587, 12);
            groupBox10.Margin = new Padding(4, 5, 4, 5);
            groupBox10.Name = "groupBox10";
            groupBox10.Padding = new Padding(4, 5, 4, 5);
            groupBox10.Size = new Size(362, 120);
            groupBox10.TabIndex = 44;
            groupBox10.TabStop = false;
            groupBox10.Text = "AI모델 주행 데이터";
            // 
            // progressBar4
            // 
            progressBar4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar4.Location = new Point(137, 85);
            progressBar4.Margin = new Padding(4, 5, 4, 5);
            progressBar4.Name = "progressBar4";
            progressBar4.Size = new Size(216, 17);
            progressBar4.TabIndex = 13;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(9, 80);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(52, 28);
            label20.TabIndex = 13;
            label20.Text = "속도";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(66, 80);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(66, 28);
            label21.TabIndex = 14;
            label21.Text = "label8";
            // 
            // progressBar5
            // 
            progressBar5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar5.Location = new Point(136, 43);
            progressBar5.Margin = new Padding(4, 5, 4, 5);
            progressBar5.Name = "progressBar5";
            progressBar5.Size = new Size(216, 17);
            progressBar5.TabIndex = 2;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(66, 37);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(66, 28);
            label22.TabIndex = 1;
            label22.Text = "label6";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(9, 37);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(52, 28);
            label23.TabIndex = 0;
            label23.Text = "방향";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label9.Location = new Point(250, 80);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(52, 28);
            label9.TabIndex = 43;
            label9.Text = "경로";
            // 
            // btn
            // 
            btn.Location = new Point(19, 70);
            btn.Margin = new Padding(4, 5, 4, 5);
            btn.Name = "btn";
            btn.Size = new Size(223, 48);
            btn.TabIndex = 42;
            btn.Text = "학습 모델 불러오기";
            btn.UseVisualStyleBackColor = true;
            // 
            // DataManager
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1983, 1118);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "DataManager";
            Text = "데이터 관리 및 모델 학습 프로그램";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            grbDataCorrection.ResumeLayout(false);
            grbDataCorrection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbBright).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbBlur).EndInit();
            grbEditRange.ResumeLayout(false);
            grbEditRange.PerformLayout();
            grbFiltering.ResumeLayout(false);
            grbFiltering.PerformLayout();
            grbDataList.ResumeLayout(false);
            grbRunningData.ResumeLayout(false);
            grbRunningData.PerformLayout();
            grbPlayOption1.ResumeLayout(false);
            grbPlayOption1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbFrameSlider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCameraView).EndInit();
            tabPage2.ResumeLayout(false);
            pnModelManage.ResumeLayout(false);
            pnModelManage.PerformLayout();
            pnModelScore.ResumeLayout(false);
            pnModelScore.PerformLayout();
            pnDataLearning.ResumeLayout(false);
            pnDataLearning.PerformLayout();
            grbExtraOption.ResumeLayout(false);
            grbExtraOption.PerformLayout();
            grbModelOption.ResumeLayout(false);
            grbModelOption.PerformLayout();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tbFrameSlider2).EndInit();
            pnOption.ResumeLayout(false);
            grbRawData.ResumeLayout(false);
            grbRawData.PerformLayout();
            grbImgCorrection.ResumeLayout(false);
            grbImgCorrection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbImgBright).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbImgBlur).EndInit();
            grbPlayOption2.ResumeLayout(false);
            grbPlayOption2.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            grbModelData.ResumeLayout(false);
            grbModelData.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btnCancel;
        private ListView lstProcess;
        private GroupBox grbFiltering;
        private ComboBox cmbRange;
        private ComboBox cmbDirSpeed;
        private Button btnFilter;
        private TextBox txtFilter;
        private GroupBox grbDataList;
        private ListBox lstDataList;
        private Label lblRange;
        private Button btnRestore;
        private Button btnSetPoint2;
        private Button btnDelete;
        private Button btnSetPoint1;
        private GroupBox grbRunningData;
        private ProgressBar pbThrottle;
        private Label lbDataSpeed;
        private Label lblThrottleValue;
        private ProgressBar pbSteering;
        private Label lblSteeringValue;
        private Label lbDataDir;
        private Label lblTitle;
        private GroupBox grbPlayOption1;
        private Label lbSpeed1;
        private Label lbFrmMvm1;
        private TextBox txtFrmMvm1;
        private ComboBox cmbSpeed1;
        private Button btnPlay;
        private Button btnPrevFrame1;
        private Button btnNextFrame1;
        private Label lblFrmInx1;
        private TrackBar tbFrameSlider1;
        private PictureBox pbCameraView;
        private Label lblPath;
        private Button btnLoadData;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private GroupBox grbEditRange;
        private Button btnRun1;
        private TextBox tbCriteria;
        private Label lbCmp;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private Panel panel5;
        private Panel panel4;
        private PictureBox pictureBox2;
        private Label lbRawDataPath;
        private Button btnRawData;
        private Label label9;
        private Button btn;
        private GroupBox grbModelData;
        private ProgressBar pbAISpeed;
        private Label lbAISpeed;
        private Label lbAISpeed2;
        private ProgressBar pbAIDir;
        private Label lbAIDir2;
        private Label lbAIDir1;
        private GroupBox groupBox10;
        private ProgressBar progressBar4;
        private Label label20;
        private Label label21;
        private ProgressBar progressBar5;
        private Label label22;
        private Label label23;
        private Panel pnModelManage;
        private Label lbModelManage;
        private ListView lstvModelManage;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private Panel pnDataLearning;
        private GroupBox grbExtraOption;
        private Label lbExtraModelPath;
        private Button btnExtraModel;
        private Label lbLearningRate;
        private Button btnLearningStop;
        private ProgressBar pbLearning;
        private Button btnLearningStart;
        private GroupBox grbModelOption;
        private ComboBox cmbMulti;
        private Label lbMulti;
        private Label label32;
        private Label lbExpl;
        private TextBox txtModelName;
        private Label lbModelName;
        private TextBox txtEpoch;
        private Label lbEpoch;
        private ComboBox cmbModelSelect;
        private Label lbModelSelect;
        private TextBox txtExpl;
        private Label lbDataLearning;
        private Label lbExtraExpl;
        private TextBox txtExtraExpl;
        private TextBox txtExtraModel;
        private Label lbExtraModelName;
        private Label lbCaution;
        private Panel pnOption;
        private GroupBox grbRawData;
        private ProgressBar pbRawDataSpeed;
        private Label lbRawDataSpeed;
        private Label lbRawDataSpeed2;
        private ProgressBar pbRawDataDir;
        private Label lbRawDataDir2;
        private Label lbRawDataDir;
        private GroupBox grbImgCorrection;
        private CheckBox chkImgActBW;
        private Label lbImgBW;
        private Label lbImgBlur;
        private Label lbImgBright;
        private TrackBar tbImgBright;
        private TrackBar tbImgBlur;
        private GroupBox grbPlayOption2;
        private Button btnRun2;
        private Label lbSpeed2;
        private Label lbFrmMvm2;
        private TextBox txtFrmMvm2;
        private ComboBox cmbSpeed2;
        private Button button8;
        private Button btnPrevFrame2;
        private Button btnNextFrame2;
        private Label lblFrmInx2;
        private Panel panel3;
        private PictureBox pictureBox1;
        private TrackBar tbFrameSlider2;
        private GroupBox grbDataCorrection;
        private CheckBox chkActBW;
        private Label lbBW;
        private Label lbBlur;
        private Label lbBright;
        private TrackBar tbBright;
        private TrackBar tbBlur;
        private Button btnSavePath;
        private Label lbDonkeyPath;
        private Label lbSavePath;
        private Button btnDonkeyPath;
        private Panel pnModelScore;
        private Label lbLoss;
        private Label lbModelScore;
        private Panel panelTimeline;
    }
}
