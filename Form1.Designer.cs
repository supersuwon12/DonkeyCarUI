namespace DonkeyCarUI
{
    partial class Form1
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
            btnLoadData = new Button();
            lblPath = new Label();
            pbCameraView = new PictureBox();
            tbFrameSlider = new TrackBar();
            groupBox1 = new GroupBox();
            btnPlay = new Button();
            btnFastForward = new Button();
            btnRewind = new Button();
            btnPrevFrame = new Button();
            btnNextFrame = new Button();
            btnSpeed = new Button();
            lblFrameIndex = new Label();
            lblTitle = new Label();
            groupBox2 = new GroupBox();
            pbThrottle = new ProgressBar();
            label7 = new Label();
            lblThrottleValue = new Label();
            pbSteering = new ProgressBar();
            lblSteeringValue = new Label();
            label5 = new Label();
            btnTrain = new Button();
            txtLog = new TextBox();
            btnSetPoint1 = new Button();
            btnSetPoint2 = new Button();
            btnDelete = new Button();
            btnRestore = new Button();
            btnRefresh = new Button();
            lblRange = new Label();
            btnFilter = new Button();
            txtFilter = new TextBox();
            groupBox3 = new GroupBox();
            txtDataList = new TextBox();
            btnTestModel = new Button();
            btnRenderGraph = new Button();
            chartData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)pbCameraView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbFrameSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartData).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoadData
            // 
            btnLoadData.ImageAlign = ContentAlignment.MiddleRight;
            btnLoadData.Location = new Point(173, 11);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(135, 27);
            btnLoadData.TabIndex = 6;
            btnLoadData.Text = "주행 데이터 불러오기";
            btnLoadData.UseVisualStyleBackColor = true;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(319, 17);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(31, 15);
            lblPath.TabIndex = 7;
            lblPath.Text = "경로";
            // 
            // pbCameraView
            // 
            pbCameraView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbCameraView.Location = new Point(275, 45);
            pbCameraView.Name = "pbCameraView";
            pbCameraView.Size = new Size(515, 385);
            pbCameraView.SizeMode = PictureBoxSizeMode.Zoom;
            pbCameraView.TabIndex = 8;
            pbCameraView.TabStop = false;
            // 
            // tbFrameSlider
            // 
            tbFrameSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbFrameSlider.Location = new Point(275, 440);
            tbFrameSlider.Name = "tbFrameSlider";
            tbFrameSlider.Size = new Size(515, 45);
            tbFrameSlider.TabIndex = 9;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(btnPlay);
            groupBox1.Controls.Add(btnFastForward);
            groupBox1.Controls.Add(btnRewind);
            groupBox1.Controls.Add(btnPrevFrame);
            groupBox1.Controls.Add(btnNextFrame);
            groupBox1.Controls.Add(btnSpeed);
            groupBox1.Controls.Add(lblFrameIndex);
            groupBox1.Location = new Point(594, 45);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(208, 240);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "재생 설정";
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnPlay.Location = new Point(8, 175);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(186, 45);
            btnPlay.TabIndex = 11;
            btnPlay.Text = "▶";
            btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnFastForward
            // 
            btnFastForward.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnFastForward.Location = new Point(104, 124);
            btnFastForward.Name = "btnFastForward";
            btnFastForward.Size = new Size(90, 45);
            btnFastForward.TabIndex = 14;
            btnFastForward.Text = ">>";
            btnFastForward.UseVisualStyleBackColor = true;
            // 
            // btnRewind
            // 
            btnRewind.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRewind.Location = new Point(8, 123);
            btnRewind.Name = "btnRewind";
            btnRewind.Size = new Size(90, 45);
            btnRewind.TabIndex = 13;
            btnRewind.Text = "<<";
            btnRewind.UseVisualStyleBackColor = true;
            // 
            // btnPrevFrame
            // 
            btnPrevFrame.Location = new Point(8, 72);
            btnPrevFrame.Name = "btnPrevFrame";
            btnPrevFrame.Size = new Size(90, 45);
            btnPrevFrame.TabIndex = 11;
            btnPrevFrame.Text = "<";
            btnPrevFrame.UseVisualStyleBackColor = true;
            // 
            // btnNextFrame
            // 
            btnNextFrame.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNextFrame.Location = new Point(104, 73);
            btnNextFrame.Name = "btnNextFrame";
            btnNextFrame.Size = new Size(90, 45);
            btnNextFrame.TabIndex = 12;
            btnNextFrame.Text = ">";
            btnNextFrame.UseVisualStyleBackColor = true;
            // 
            // btnSpeed
            // 
            btnSpeed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSpeed.Location = new Point(104, 22);
            btnSpeed.Name = "btnSpeed";
            btnSpeed.Size = new Size(90, 45);
            btnSpeed.TabIndex = 1;
            btnSpeed.Text = "1.0";
            btnSpeed.UseVisualStyleBackColor = true;
            // 
            // lblFrameIndex
            // 
            lblFrameIndex.AutoSize = true;
            lblFrameIndex.Location = new Point(23, 37);
            lblFrameIndex.Name = "lblFrameIndex";
            lblFrameIndex.Size = new Size(49, 15);
            lblFrameIndex.TabIndex = 0;
            lblFrameIndex.Text = "000000";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTitle.Location = new Point(8, 11);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(159, 25);
            lblTitle.TabIndex = 11;
            lblTitle.Text = "주행 데이터 관리";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(pbThrottle);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(lblThrottleValue);
            groupBox2.Controls.Add(pbSteering);
            groupBox2.Controls.Add(lblSteeringValue);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(12, 45);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(244, 132);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "주행 데이터";
            // 
            // pbThrottle
            // 
            pbThrottle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pbThrottle.Location = new Point(142, 90);
            pbThrottle.Name = "pbThrottle";
            pbThrottle.Size = new Size(97, 10);
            pbThrottle.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 88);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 13;
            label7.Text = "속도";
            // 
            // lblThrottleValue
            // 
            lblThrottleValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblThrottleValue.AutoSize = true;
            lblThrottleValue.Location = new Point(97, 87);
            lblThrottleValue.Name = "lblThrottleValue";
            lblThrottleValue.Size = new Size(39, 15);
            lblThrottleValue.TabIndex = 14;
            lblThrottleValue.Text = "label8";
            // 
            // pbSteering
            // 
            pbSteering.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pbSteering.Location = new Point(141, 42);
            pbSteering.Name = "pbSteering";
            pbSteering.Size = new Size(97, 10);
            pbSteering.TabIndex = 2;
            // 
            // lblSteeringValue
            // 
            lblSteeringValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSteeringValue.AutoSize = true;
            lblSteeringValue.Location = new Point(96, 38);
            lblSteeringValue.Name = "lblSteeringValue";
            lblSteeringValue.Size = new Size(39, 15);
            lblSteeringValue.TabIndex = 1;
            lblSteeringValue.Text = "label6";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 38);
            label5.Name = "label5";
            label5.Size = new Size(31, 15);
            label5.TabIndex = 0;
            label5.Text = "방향";
            // 
            // btnTrain
            // 
            btnTrain.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnTrain.Location = new Point(12, 183);
            btnTrain.Name = "btnTrain";
            btnTrain.Size = new Size(90, 31);
            btnTrain.TabIndex = 13;
            btnTrain.Text = "학습";
            btnTrain.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtLog.Location = new Point(12, 220);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(245, 140);
            txtLog.TabIndex = 14;
            // 
            // btnSetPoint1
            // 
            btnSetPoint1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSetPoint1.Location = new Point(12, 380);
            btnSetPoint1.Name = "btnSetPoint1";
            btnSetPoint1.Size = new Size(120, 35);
            btnSetPoint1.TabIndex = 15;
            btnSetPoint1.Text = "지점 설정 1";
            btnSetPoint1.UseVisualStyleBackColor = true;
            // 
            // btnSetPoint2
            // 
            btnSetPoint2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSetPoint2.Location = new Point(137, 380);
            btnSetPoint2.Name = "btnSetPoint2";
            btnSetPoint2.Size = new Size(120, 35);
            btnSetPoint2.TabIndex = 16;
            btnSetPoint2.Text = "지점 설정 2";
            btnSetPoint2.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(12, 450);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 35);
            btnDelete.TabIndex = 18;
            btnDelete.Text = "삭제";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnRestore
            // 
            btnRestore.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRestore.Location = new Point(137, 450);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(120, 35);
            btnRestore.TabIndex = 19;
            btnRestore.Text = "복원";
            btnRestore.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRefresh.Location = new Point(12, 490);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(245, 35);
            btnRefresh.TabIndex = 20;
            btnRefresh.Text = "새로고침";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblRange
            // 
            lblRange.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblRange.AutoSize = true;
            lblRange.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lblRange.Location = new Point(12, 420);
            lblRange.Name = "lblRange";
            lblRange.Size = new Size(42, 21);
            lblRange.TabIndex = 21;
            lblRange.Text = "[0,0)";
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFilter.Location = new Point(12, 535);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(120, 24);
            btnFilter.TabIndex = 22;
            btnFilter.Text = "임계값 필터 적용";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtFilter.Location = new Point(137, 536);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(120, 23);
            txtFilter.TabIndex = 23;
            txtFilter.Text = "0.1";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox3.Controls.Add(txtDataList);
            groupBox3.Location = new Point(808, 17);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(176, 378);
            groupBox3.TabIndex = 24;
            groupBox3.TabStop = false;
            groupBox3.Text = "데이터 리스트";
            // 
            // txtDataList
            // 
            txtDataList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDataList.Location = new Point(8, 22);
            txtDataList.Multiline = true;
            txtDataList.Name = "txtDataList";
            txtDataList.Size = new Size(160, 350);
            txtDataList.TabIndex = 0;
            // 
            // Form1
            // 
            // btnTestModel
            // 
            btnTestModel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnTestModel.Location = new Point(108, 183);
            btnTestModel.Name = "btnTestModel";
            btnTestModel.Size = new Size(90, 31);
            btnTestModel.TabIndex = 25;
            btnTestModel.Text = "모델 테스트";
            btnTestModel.UseVisualStyleBackColor = true;
            // 
            // btnRenderGraph
            // 
            btnRenderGraph.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRenderGraph.Location = new Point(660, 560);
            btnRenderGraph.Name = "btnRenderGraph";
            btnRenderGraph.Size = new Size(130, 26);
            btnRenderGraph.TabIndex = 27;
            btnRenderGraph.Text = "그래프 렌더링";
            btnRenderGraph.UseVisualStyleBackColor = true;
            // 
            // chartData
            // 
            chartData.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartData.Location = new Point(275, 485);
            chartData.Name = "chartData";
            chartData.Size = new Size(515, 70);
            chartData.TabIndex = 26;
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 601);
            Controls.Add(btnRenderGraph);
            Controls.Add(chartData);
            Controls.Add(btnTestModel);
            Controls.Add(groupBox3);
            Controls.Add(txtFilter);
            Controls.Add(btnFilter);
            Controls.Add(lblRange);
            Controls.Add(btnRefresh);
            Controls.Add(btnRestore);
            Controls.Add(btnDelete);
            Controls.Add(btnSetPoint2);
            Controls.Add(btnSetPoint1);
            Controls.Add(txtLog);
            Controls.Add(btnTrain);
            Controls.Add(groupBox2);
            Controls.Add(lblTitle);
            Controls.Add(groupBox1);
            Controls.Add(tbFrameSlider);
            Controls.Add(pbCameraView);
            Controls.Add(lblPath);
            Controls.Add(btnLoadData);
            Name = "Form1";
            Text = "Donkeycar UI";
            ((System.ComponentModel.ISupportInitialize)pbCameraView).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbFrameSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartData).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnLoadData;
        private Label lblPath;
        private PictureBox pbCameraView;
        private TrackBar tbFrameSlider;
        private GroupBox groupBox1;
        private Button btnFastForward;
        private Button btnRewind;
        private Button btnPrevFrame;
        private Button btnNextFrame;
        private Button btnSpeed;
        private Label lblFrameIndex;
        private Button btnPlay;
        private Label lblTitle;
        private GroupBox groupBox2;
        private ProgressBar pbThrottle;
        private Label label7;
        private Label lblThrottleValue;
        private ProgressBar pbSteering;
        private Label lblSteeringValue;
        private Label label5;
        private Button btnTrain;
        private TextBox txtLog;
        private Button btnSetPoint1;
        private Button btnSetPoint2;
        private Button btnDelete;
        private Button btnRestore;
        private Button btnRefresh;
        private Label lblRange;
        private Button btnFilter;
        private TextBox txtFilter;
        private GroupBox groupBox3;
        private TextBox txtDataList;
        private Button btnTestModel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
        private Button btnRenderGraph;
    }
}
