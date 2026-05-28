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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            button1 = new Button();
            listView1 = new ListView();
            groupBox4 = new GroupBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            btnFilter = new Button();
            txtFilter = new TextBox();
            btnTestModel = new Button();
            groupBox3 = new GroupBox();
            lstDataList = new ListBox();
            lblRange = new Label();
            btnRestore = new Button();
            btnSetPoint2 = new Button();
            btnDelete = new Button();
            btnSetPoint1 = new Button();
            groupBox2 = new GroupBox();
            pbThrottle = new ProgressBar();
            label7 = new Label();
            lblThrottleValue = new Label();
            pbSteering = new ProgressBar();
            lblSteeringValue = new Label();
            label5 = new Label();
            lblTitle = new Label();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            cmbSpeed = new ComboBox();
            btnPlay = new Button();
            btnPrevFrame = new Button();
            btnNextFrame = new Button();
            lblFrameIndex = new Label();
            tbFrameSlider = new TrackBar();
            pbCameraView = new PictureBox();
            lblPath = new Label();
            btnLoadData = new Button();
            tabPage3 = new TabPage();
            label3 = new Label();
            button2 = new Button();
            groupBox5 = new GroupBox();
            textBox2 = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbFrameSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCameraView).BeginInit();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1388, 650);
            tabControl1.TabIndex = 32;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox5);
            tabPage1.Controls.Add(listView1);
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Controls.Add(btnTestModel);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(lblTitle);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(tbFrameSlider);
            tabPage1.Controls.Add(pbCameraView);
            tabPage1.Controls.Add(lblPath);
            tabPage1.Controls.Add(btnLoadData);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1380, 622);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1380, 622);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(1, 141);
            button1.Name = "button1";
            button1.Size = new Size(208, 38);
            button1.TabIndex = 50;
            button1.Text = "데이터 옵션 세팅";
            button1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Location = new Point(277, 506);
            listView1.Name = "listView1";
            listView1.Size = new Size(915, 97);
            listView1.TabIndex = 49;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox4.Controls.Add(textBox2);
            groupBox4.Controls.Add(comboBox2);
            groupBox4.Controls.Add(comboBox1);
            groupBox4.Controls.Add(btnFilter);
            groupBox4.Controls.Add(txtFilter);
            groupBox4.Location = new Point(17, 163);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(244, 140);
            groupBox4.TabIndex = 48;
            groupBox4.TabStop = false;
            groupBox4.Text = "범위 필터링";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { ">", "<", "≥", "≤" });
            comboBox2.Location = new Point(123, 63);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(105, 23);
            comboBox2.TabIndex = 25;
            comboBox2.Text = "범위";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "방향", "속도" });
            comboBox1.Location = new Point(123, 25);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(105, 23);
            comboBox1.TabIndex = 24;
            comboBox1.Text = "방향/속도";
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFilter.Location = new Point(7, 24);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(104, 31);
            btnFilter.TabIndex = 22;
            btnFilter.Text = "범위 필터링";
            btnFilter.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtFilter.Location = new Point(123, 139);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(105, 23);
            txtFilter.TabIndex = 23;
            txtFilter.Text = "0.1";
            // 
            // btnTestModel
            // 
            btnTestModel.Location = new Point(17, 316);
            btnTestModel.Name = "btnTestModel";
            btnTestModel.Size = new Size(90, 31);
            btnTestModel.TabIndex = 47;
            btnTestModel.Text = "파일생성";
            btnTestModel.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox3.Controls.Add(lstDataList);
            groupBox3.Location = new Point(1198, 6);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(176, 608);
            groupBox3.TabIndex = 46;
            groupBox3.TabStop = false;
            groupBox3.Text = "데이터 리스트";
            // 
            // lstDataList
            // 
            lstDataList.Dock = DockStyle.Fill;
            lstDataList.FormattingEnabled = true;
            lstDataList.Location = new Point(3, 19);
            lstDataList.Name = "lstDataList";
            lstDataList.Size = new Size(170, 586);
            lstDataList.TabIndex = 29;
            // 
            // lblRange
            // 
            lblRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRange.AutoSize = true;
            lblRange.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lblRange.Location = new Point(85, 71);
            lblRange.Name = "lblRange";
            lblRange.Size = new Size(42, 21);
            lblRange.TabIndex = 45;
            lblRange.Text = "[0,0)";
            // 
            // btnRestore
            // 
            btnRestore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRestore.Location = new Point(105, 100);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(104, 35);
            btnRestore.TabIndex = 44;
            btnRestore.Text = "삭제 복원";
            btnRestore.UseVisualStyleBackColor = true;
            // 
            // btnSetPoint2
            // 
            btnSetPoint2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSetPoint2.Location = new Point(108, 22);
            btnSetPoint2.Name = "btnSetPoint2";
            btnSetPoint2.Size = new Size(104, 35);
            btnSetPoint2.TabIndex = 42;
            btnSetPoint2.Text = "끝 지점 선택";
            btnSetPoint2.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.Location = new Point(1, 100);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 35);
            btnDelete.TabIndex = 43;
            btnDelete.Text = "선택 삭제";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSetPoint1
            // 
            btnSetPoint1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSetPoint1.Location = new Point(4, 22);
            btnSetPoint1.Name = "btnSetPoint1";
            btnSetPoint1.Size = new Size(98, 35);
            btnSetPoint1.TabIndex = 41;
            btnSetPoint1.Text = "시작 지점 선택";
            btnSetPoint1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox2.Controls.Add(pbThrottle);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(lblThrottleValue);
            groupBox2.Controls.Add(pbSteering);
            groupBox2.Controls.Add(lblSteeringValue);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(17, 39);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(244, 118);
            groupBox2.TabIndex = 38;
            groupBox2.TabStop = false;
            groupBox2.Text = "주행 데이터";
            // 
            // pbThrottle
            // 
            pbThrottle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pbThrottle.Location = new Point(115, 90);
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
            lblThrottleValue.Location = new Point(70, 87);
            lblThrottleValue.Name = "lblThrottleValue";
            lblThrottleValue.Size = new Size(39, 15);
            lblThrottleValue.TabIndex = 14;
            lblThrottleValue.Text = "label8";
            // 
            // pbSteering
            // 
            pbSteering.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pbSteering.Location = new Point(114, 42);
            pbSteering.Name = "pbSteering";
            pbSteering.Size = new Size(97, 10);
            pbSteering.TabIndex = 2;
            // 
            // lblSteeringValue
            // 
            lblSteeringValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSteeringValue.AutoSize = true;
            lblSteeringValue.Location = new Point(69, 38);
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
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblTitle.Location = new Point(6, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(159, 25);
            lblTitle.TabIndex = 37;
            lblTitle.Text = "주행 데이터 관리";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(cmbSpeed);
            groupBox1.Controls.Add(btnPlay);
            groupBox1.Controls.Add(btnPrevFrame);
            groupBox1.Controls.Add(btnNextFrame);
            groupBox1.Controls.Add(lblFrameIndex);
            groupBox1.Location = new Point(977, 25);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(215, 224);
            groupBox1.TabIndex = 36;
            groupBox1.TabStop = false;
            groupBox1.Text = "재생 설정";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(29, 92);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 32;
            label2.Text = "배속";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(16, 56);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 30;
            label1.Text = "프레임 이동";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(101, 52);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(103, 23);
            textBox1.TabIndex = 29;
            // 
            // cmbSpeed
            // 
            cmbSpeed.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cmbSpeed.FormattingEnabled = true;
            cmbSpeed.Items.AddRange(new object[] { "1.0", "1.5", "2.0", "2.5", "3.0" });
            cmbSpeed.Location = new Point(101, 88);
            cmbSpeed.Name = "cmbSpeed";
            cmbSpeed.Size = new Size(103, 23);
            cmbSpeed.TabIndex = 28;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnPlay.Location = new Point(12, 293);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(201, 45);
            btnPlay.TabIndex = 11;
            btnPlay.Text = "▶";
            btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnPrevFrame
            // 
            btnPrevFrame.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnPrevFrame.Location = new Point(9, 117);
            btnPrevFrame.Name = "btnPrevFrame";
            btnPrevFrame.Size = new Size(98, 45);
            btnPrevFrame.TabIndex = 11;
            btnPrevFrame.Text = "<";
            btnPrevFrame.UseVisualStyleBackColor = true;
            // 
            // btnNextFrame
            // 
            btnNextFrame.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnNextFrame.Location = new Point(107, 117);
            btnNextFrame.Name = "btnNextFrame";
            btnNextFrame.Size = new Size(101, 45);
            btnNextFrame.TabIndex = 12;
            btnNextFrame.Text = ">";
            btnNextFrame.UseVisualStyleBackColor = true;
            // 
            // lblFrameIndex
            // 
            lblFrameIndex.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblFrameIndex.AutoSize = true;
            lblFrameIndex.Location = new Point(16, 27);
            lblFrameIndex.Name = "lblFrameIndex";
            lblFrameIndex.Size = new Size(157, 15);
            lblFrameIndex.TabIndex = 0;
            lblFrameIndex.Text = "해당 프레임    :        00000";
            // 
            // tbFrameSlider
            // 
            tbFrameSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbFrameSlider.Location = new Point(277, 452);
            tbFrameSlider.Name = "tbFrameSlider";
            tbFrameSlider.Size = new Size(915, 45);
            tbFrameSlider.TabIndex = 35;
            // 
            // pbCameraView
            // 
            pbCameraView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbCameraView.Location = new Point(279, 39);
            pbCameraView.Name = "pbCameraView";
            pbCameraView.Size = new Size(689, 401);
            pbCameraView.SizeMode = PictureBoxSizeMode.Zoom;
            pbCameraView.TabIndex = 34;
            pbCameraView.TabStop = false;
            // 
            // lblPath
            // 
            lblPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPath.AutoSize = true;
            lblPath.Location = new Point(317, 12);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(31, 15);
            lblPath.TabIndex = 33;
            lblPath.Text = "경로";
            // 
            // btnLoadData
            // 
            btnLoadData.ImageAlign = ContentAlignment.MiddleRight;
            btnLoadData.Location = new Point(171, 6);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(135, 27);
            btnLoadData.TabIndex = 32;
            btnLoadData.Text = "주행 데이터 불러오기";
            btnLoadData.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1380, 622);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Location = new Point(8, 3);
            label3.Name = "label3";
            label3.Size = new Size(95, 25);
            label3.TabIndex = 38;
            label3.Text = "모델 훈련";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button2.Location = new Point(10, 168);
            button2.Name = "button2";
            button2.Size = new Size(197, 45);
            button2.TabIndex = 33;
            button2.Text = "▶";
            button2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btnSetPoint1);
            groupBox5.Controls.Add(button1);
            groupBox5.Controls.Add(btnDelete);
            groupBox5.Controls.Add(btnSetPoint2);
            groupBox5.Controls.Add(btnRestore);
            groupBox5.Controls.Add(lblRange);
            groupBox5.Location = new Point(977, 255);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(215, 185);
            groupBox5.TabIndex = 51;
            groupBox5.TabStop = false;
            groupBox5.Text = "선택 범위 편집";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(123, 101);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(105, 23);
            textBox2.TabIndex = 26;
            textBox2.Text = "0.0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1388, 650);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Donkeycar UI";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbFrameSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCameraView).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button1;
        private ListView listView1;
        private GroupBox groupBox4;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Button btnFilter;
        private TextBox txtFilter;
        private Button btnTestModel;
        private GroupBox groupBox3;
        private ListBox lstDataList;
        private Label lblRange;
        private Button btnRestore;
        private Button btnSetPoint2;
        private Button btnDelete;
        private Button btnSetPoint1;
        private GroupBox groupBox2;
        private ProgressBar pbThrottle;
        private Label label7;
        private Label lblThrottleValue;
        private ProgressBar pbSteering;
        private Label lblSteeringValue;
        private Label label5;
        private Label lblTitle;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private TextBox textBox1;
        private ComboBox cmbSpeed;
        private Button btnPlay;
        private Button btnPrevFrame;
        private Button btnNextFrame;
        private Label lblFrameIndex;
        private TrackBar tbFrameSlider;
        private PictureBox pbCameraView;
        private Label lblPath;
        private Button btnLoadData;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label label3;
        private GroupBox groupBox5;
        private Button button2;
        private TextBox textBox2;
    }
}
