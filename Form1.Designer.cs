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
            button6 = new Button();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            trackBar1 = new TrackBar();
            groupBox1 = new GroupBox();
            button12 = new Button();
            button11 = new Button();
            button10 = new Button();
            button8 = new Button();
            button9 = new Button();
            button7 = new Button();
            label3 = new Label();
            label4 = new Label();
            groupBox2 = new GroupBox();
            progressBar2 = new ProgressBar();
            label7 = new Label();
            label8 = new Label();
            progressBar1 = new ProgressBar();
            label6 = new Label();
            label5 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            button13 = new Button();
            button14 = new Button();
            button15 = new Button();
            label9 = new Label();
            button4 = new Button();
            textBox2 = new TextBox();
            groupBox3 = new GroupBox();
            textBox3 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // button6
            // 
            button6.ImageAlign = ContentAlignment.MiddleRight;
            button6.Location = new Point(173, 11);
            button6.Name = "button6";
            button6.Size = new Size(135, 27);
            button6.TabIndex = 6;
            button6.Text = "주행 데이터 불러오기";
            button6.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(319, 17);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 7;
            label2.Text = "경로";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(262, 45);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(326, 240);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // trackBar1
            // 
            trackBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBar1.Location = new Point(2, 291);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(797, 45);
            trackBar1.TabIndex = 9;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(button12);
            groupBox1.Controls.Add(button11);
            groupBox1.Controls.Add(button10);
            groupBox1.Controls.Add(button8);
            groupBox1.Controls.Add(button9);
            groupBox1.Controls.Add(button7);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(594, 45);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(208, 240);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "재생 설정";
            // 
            // button12
            // 
            button12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button12.Location = new Point(8, 175);
            button12.Name = "button12";
            button12.Size = new Size(186, 45);
            button12.TabIndex = 11;
            button12.Text = "▶";
            button12.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            button11.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button11.Location = new Point(104, 124);
            button11.Name = "button11";
            button11.Size = new Size(90, 45);
            button11.TabIndex = 14;
            button11.Text = ">>";
            button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button10.Location = new Point(8, 123);
            button10.Name = "button10";
            button10.Size = new Size(90, 45);
            button10.TabIndex = 13;
            button10.Text = "<<";
            button10.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(8, 72);
            button8.Name = "button8";
            button8.Size = new Size(90, 45);
            button8.TabIndex = 11;
            button8.Text = "<";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button9.Location = new Point(104, 73);
            button9.Name = "button9";
            button9.Size = new Size(90, 45);
            button9.TabIndex = 12;
            button9.Text = ">";
            button9.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button7.Location = new Point(104, 22);
            button7.Name = "button7";
            button7.Size = new Size(90, 45);
            button7.TabIndex = 1;
            button7.Text = "1.0";
            button7.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 37);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 0;
            label3.Text = "000000";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label4.Location = new Point(8, 11);
            label4.Name = "label4";
            label4.Size = new Size(159, 25);
            label4.TabIndex = 11;
            label4.Text = "주행 데이터 관리";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(progressBar2);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(progressBar1);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(12, 45);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(244, 132);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "주행 데이터";
            // 
            // progressBar2
            // 
            progressBar2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            progressBar2.Location = new Point(142, 90);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(97, 10);
            progressBar2.TabIndex = 13;
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
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(97, 87);
            label8.Name = "label8";
            label8.Size = new Size(39, 15);
            label8.TabIndex = 14;
            label8.Text = "label8";
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            progressBar1.Location = new Point(141, 42);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(97, 10);
            progressBar1.TabIndex = 2;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(96, 38);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 1;
            label6.Text = "label6";
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
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(12, 183);
            button1.Name = "button1";
            button1.Size = new Size(90, 31);
            button1.TabIndex = 13;
            button1.Text = "학습";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(12, 220);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(238, 65);
            textBox1.TabIndex = 14;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(12, 325);
            button2.Name = "button2";
            button2.Size = new Size(126, 40);
            button2.TabIndex = 15;
            button2.Text = "지점 설정 1";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(144, 325);
            button3.Name = "button3";
            button3.Size = new Size(126, 40);
            button3.TabIndex = 16;
            button3.Text = "지점 설정 2";
            button3.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            button13.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button13.Location = new Point(408, 325);
            button13.Name = "button13";
            button13.Size = new Size(126, 40);
            button13.TabIndex = 18;
            button13.Text = "삭제";
            button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            button14.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button14.Location = new Point(540, 325);
            button14.Name = "button14";
            button14.Size = new Size(126, 40);
            button14.TabIndex = 19;
            button14.Text = "복원";
            button14.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            button15.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button15.Location = new Point(672, 325);
            button15.Name = "button15";
            button15.Size = new Size(126, 40);
            button15.TabIndex = 20;
            button15.Text = "새로고침";
            button15.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label9.Location = new Point(316, 334);
            label9.Name = "label9";
            label9.Size = new Size(42, 21);
            label9.TabIndex = 21;
            label9.Text = "[0,0)";
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button4.Location = new Point(12, 371);
            button4.Name = "button4";
            button4.Size = new Size(106, 24);
            button4.TabIndex = 22;
            button4.Text = "필터";
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(124, 372);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(674, 23);
            textBox2.TabIndex = 23;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox3.Controls.Add(textBox3);
            groupBox3.Location = new Point(808, 17);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(176, 378);
            groupBox3.TabIndex = 24;
            groupBox3.TabStop = false;
            groupBox3.Text = "데이터 리스트";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox3.Location = new Point(8, 22);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(160, 350);
            textBox3.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(988, 416);
            Controls.Add(groupBox3);
            Controls.Add(textBox2);
            Controls.Add(button4);
            Controls.Add(label9);
            Controls.Add(button15);
            Controls.Add(button14);
            Controls.Add(button13);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(groupBox2);
            Controls.Add(label4);
            Controls.Add(groupBox1);
            Controls.Add(trackBar1);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(button6);
            Name = "Form1";
            Text = "Donkeycar UI";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
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
        private Button button6;
        private Label label2;
        private PictureBox pictureBox1;
        private TrackBar trackBar1;
        private GroupBox groupBox1;
        private Button button11;
        private Button button10;
        private Button button8;
        private Button button9;
        private Button button7;
        private Label label3;
        private Button button12;
        private Label label4;
        private GroupBox groupBox2;
        private ProgressBar progressBar2;
        private Label label7;
        private Label label8;
        private ProgressBar progressBar1;
        private Label label6;
        private Label label5;
        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private Button button13;
        private Button button14;
        private Button button15;
        private Label label9;
        private Button button4;
        private TextBox textBox2;
        private GroupBox groupBox3;
        private TextBox textBox3;
    }
}
