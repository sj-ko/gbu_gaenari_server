namespace GBU_Server_DotNet
{
    partial class ConfigureWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Configure_button_OK = new System.Windows.Forms.Button();
            this.Configure_button_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Configure_textbox_rtspurl = new System.Windows.Forms.TextBox();
            this.Configure_textbox_camID = new System.Windows.Forms.TextBox();
            this.Configure_croparea = new System.Windows.Forms.PictureBox();
            this.Configure_groupBox1 = new System.Windows.Forms.GroupBox();
            this.Configure_groupBox2 = new System.Windows.Forms.GroupBox();
            this.Configure_UpDown_CropHeight = new System.Windows.Forms.NumericUpDown();
            this.Configure_UpDown_CropWidth = new System.Windows.Forms.NumericUpDown();
            this.Configure_UpDown_CropY = new System.Windows.Forms.NumericUpDown();
            this.Configure_UpDown_CropX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Configure_groupBox3 = new System.Windows.Forms.GroupBox();
            this.Configure_textbox_savepath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Configure_groupBox4 = new System.Windows.Forms.GroupBox();
            this.Configure_checkBox_isResize = new System.Windows.Forms.CheckBox();
            this.Configure_textbox_countForPass = new System.Windows.Forms.TextBox();
            this.Configure_textbox_timeout = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Configure_button_load = new System.Windows.Forms.Button();
            this.Configure_button_save = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.Configure_textbox_size = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_croparea)).BeginInit();
            this.Configure_groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_UpDown_CropHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_UpDown_CropWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_UpDown_CropY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_UpDown_CropX)).BeginInit();
            this.Configure_groupBox3.SuspendLayout();
            this.Configure_groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Configure_button_OK
            // 
            this.Configure_button_OK.Location = new System.Drawing.Point(845, 469);
            this.Configure_button_OK.Name = "Configure_button_OK";
            this.Configure_button_OK.Size = new System.Drawing.Size(75, 23);
            this.Configure_button_OK.TabIndex = 0;
            this.Configure_button_OK.Text = "확인";
            this.Configure_button_OK.UseVisualStyleBackColor = true;
            this.Configure_button_OK.Click += new System.EventHandler(this.Configure_button_OK_Click);
            // 
            // Configure_button_Cancel
            // 
            this.Configure_button_Cancel.Location = new System.Drawing.Point(926, 469);
            this.Configure_button_Cancel.Name = "Configure_button_Cancel";
            this.Configure_button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Configure_button_Cancel.TabIndex = 1;
            this.Configure_button_Cancel.Text = "취소";
            this.Configure_button_Cancel.UseVisualStyleBackColor = true;
            this.Configure_button_Cancel.Click += new System.EventHandler(this.Configure_button_Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "RTSP URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "카메라 ID";
            // 
            // Configure_textbox_rtspurl
            // 
            this.Configure_textbox_rtspurl.Location = new System.Drawing.Point(99, 48);
            this.Configure_textbox_rtspurl.Name = "Configure_textbox_rtspurl";
            this.Configure_textbox_rtspurl.Size = new System.Drawing.Size(337, 21);
            this.Configure_textbox_rtspurl.TabIndex = 4;
            // 
            // Configure_textbox_camID
            // 
            this.Configure_textbox_camID.Location = new System.Drawing.Point(99, 75);
            this.Configure_textbox_camID.Name = "Configure_textbox_camID";
            this.Configure_textbox_camID.Size = new System.Drawing.Size(132, 21);
            this.Configure_textbox_camID.TabIndex = 5;
            // 
            // Configure_croparea
            // 
            this.Configure_croparea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Configure_croparea.Location = new System.Drawing.Point(18, 16);
            this.Configure_croparea.Name = "Configure_croparea";
            this.Configure_croparea.Size = new System.Drawing.Size(480, 270);
            this.Configure_croparea.TabIndex = 6;
            this.Configure_croparea.TabStop = false;
            this.Configure_croparea.Click += new System.EventHandler(this.Configure_croparea_Click);
            this.Configure_croparea.Paint += new System.Windows.Forms.PaintEventHandler(this.Configure_croparea_Paint);
            this.Configure_croparea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Configure_croparea_MouseDown);
            this.Configure_croparea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Configure_croparea_MouseMove);
            this.Configure_croparea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Configure_croparea_MouseUp);
            // 
            // Configure_groupBox1
            // 
            this.Configure_groupBox1.Location = new System.Drawing.Point(12, 12);
            this.Configure_groupBox1.Name = "Configure_groupBox1";
            this.Configure_groupBox1.Size = new System.Drawing.Size(507, 95);
            this.Configure_groupBox1.TabIndex = 7;
            this.Configure_groupBox1.TabStop = false;
            this.Configure_groupBox1.Text = "카메라 연결";
            // 
            // Configure_groupBox2
            // 
            this.Configure_groupBox2.Controls.Add(this.Configure_croparea);
            this.Configure_groupBox2.Controls.Add(this.Configure_UpDown_CropHeight);
            this.Configure_groupBox2.Controls.Add(this.Configure_UpDown_CropWidth);
            this.Configure_groupBox2.Controls.Add(this.Configure_UpDown_CropY);
            this.Configure_groupBox2.Controls.Add(this.Configure_UpDown_CropX);
            this.Configure_groupBox2.Controls.Add(this.label6);
            this.Configure_groupBox2.Controls.Add(this.label5);
            this.Configure_groupBox2.Controls.Add(this.label4);
            this.Configure_groupBox2.Controls.Add(this.label3);
            this.Configure_groupBox2.Location = new System.Drawing.Point(12, 117);
            this.Configure_groupBox2.Name = "Configure_groupBox2";
            this.Configure_groupBox2.Size = new System.Drawing.Size(507, 329);
            this.Configure_groupBox2.TabIndex = 8;
            this.Configure_groupBox2.TabStop = false;
            this.Configure_groupBox2.Text = "감지 영역";
            // 
            // Configure_UpDown_CropHeight
            // 
            this.Configure_UpDown_CropHeight.Location = new System.Drawing.Point(438, 292);
            this.Configure_UpDown_CropHeight.Maximum = new decimal(new int[] {
            270,
            0,
            0,
            0});
            this.Configure_UpDown_CropHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Configure_UpDown_CropHeight.Name = "Configure_UpDown_CropHeight";
            this.Configure_UpDown_CropHeight.Size = new System.Drawing.Size(57, 21);
            this.Configure_UpDown_CropHeight.TabIndex = 16;
            this.Configure_UpDown_CropHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Configure_UpDown_CropHeight.ValueChanged += new System.EventHandler(this.Configure_UpDown_CropHeight_ValueChanged);
            // 
            // Configure_UpDown_CropWidth
            // 
            this.Configure_UpDown_CropWidth.Location = new System.Drawing.Point(292, 292);
            this.Configure_UpDown_CropWidth.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.Configure_UpDown_CropWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Configure_UpDown_CropWidth.Name = "Configure_UpDown_CropWidth";
            this.Configure_UpDown_CropWidth.Size = new System.Drawing.Size(57, 21);
            this.Configure_UpDown_CropWidth.TabIndex = 15;
            this.Configure_UpDown_CropWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Configure_UpDown_CropWidth.ValueChanged += new System.EventHandler(this.Configure_UpDown_CropWidth_ValueChanged);
            // 
            // Configure_UpDown_CropY
            // 
            this.Configure_UpDown_CropY.Location = new System.Drawing.Point(162, 292);
            this.Configure_UpDown_CropY.Maximum = new decimal(new int[] {
            270,
            0,
            0,
            0});
            this.Configure_UpDown_CropY.Name = "Configure_UpDown_CropY";
            this.Configure_UpDown_CropY.Size = new System.Drawing.Size(57, 21);
            this.Configure_UpDown_CropY.TabIndex = 14;
            this.Configure_UpDown_CropY.ValueChanged += new System.EventHandler(this.Configure_UpDown_CropY_ValueChanged);
            // 
            // Configure_UpDown_CropX
            // 
            this.Configure_UpDown_CropX.Location = new System.Drawing.Point(48, 292);
            this.Configure_UpDown_CropX.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.Configure_UpDown_CropX.Name = "Configure_UpDown_CropX";
            this.Configure_UpDown_CropX.Size = new System.Drawing.Size(55, 21);
            this.Configure_UpDown_CropX.TabIndex = 13;
            this.Configure_UpDown_CropX.ValueChanged += new System.EventHandler(this.Configure_UpDown_CropX_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(365, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "CropHeight";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(226, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "CropWidth";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "CropY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "CropX";
            // 
            // Configure_groupBox3
            // 
            this.Configure_groupBox3.Controls.Add(this.Configure_textbox_savepath);
            this.Configure_groupBox3.Controls.Add(this.label7);
            this.Configure_groupBox3.Location = new System.Drawing.Point(547, 12);
            this.Configure_groupBox3.Name = "Configure_groupBox3";
            this.Configure_groupBox3.Size = new System.Drawing.Size(476, 193);
            this.Configure_groupBox3.TabIndex = 8;
            this.Configure_groupBox3.TabStop = false;
            this.Configure_groupBox3.Text = "저장 경로";
            // 
            // Configure_textbox_savepath
            // 
            this.Configure_textbox_savepath.Location = new System.Drawing.Point(117, 33);
            this.Configure_textbox_savepath.Name = "Configure_textbox_savepath";
            this.Configure_textbox_savepath.Size = new System.Drawing.Size(337, 21);
            this.Configure_textbox_savepath.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "경로";
            // 
            // Configure_groupBox4
            // 
            this.Configure_groupBox4.Controls.Add(this.label11);
            this.Configure_groupBox4.Controls.Add(this.Configure_textbox_size);
            this.Configure_groupBox4.Controls.Add(this.label10);
            this.Configure_groupBox4.Controls.Add(this.Configure_checkBox_isResize);
            this.Configure_groupBox4.Controls.Add(this.Configure_textbox_countForPass);
            this.Configure_groupBox4.Controls.Add(this.Configure_textbox_timeout);
            this.Configure_groupBox4.Controls.Add(this.label9);
            this.Configure_groupBox4.Controls.Add(this.label8);
            this.Configure_groupBox4.Location = new System.Drawing.Point(547, 230);
            this.Configure_groupBox4.Name = "Configure_groupBox4";
            this.Configure_groupBox4.Size = new System.Drawing.Size(476, 193);
            this.Configure_groupBox4.TabIndex = 9;
            this.Configure_groupBox4.TabStop = false;
            this.Configure_groupBox4.Text = "번호판 인식 설정";
            // 
            // Configure_checkBox_isResize
            // 
            this.Configure_checkBox_isResize.AutoSize = true;
            this.Configure_checkBox_isResize.Location = new System.Drawing.Point(72, 157);
            this.Configure_checkBox_isResize.Name = "Configure_checkBox_isResize";
            this.Configure_checkBox_isResize.Size = new System.Drawing.Size(128, 16);
            this.Configure_checkBox_isResize.TabIndex = 15;
            this.Configure_checkBox_isResize.Text = "이미지 사이즈 조정";
            this.Configure_checkBox_isResize.UseVisualStyleBackColor = true;
            // 
            // Configure_textbox_countForPass
            // 
            this.Configure_textbox_countForPass.Location = new System.Drawing.Point(133, 64);
            this.Configure_textbox_countForPass.Name = "Configure_textbox_countForPass";
            this.Configure_textbox_countForPass.Size = new System.Drawing.Size(129, 21);
            this.Configure_textbox_countForPass.TabIndex = 14;
            // 
            // Configure_textbox_timeout
            // 
            this.Configure_textbox_timeout.Location = new System.Drawing.Point(133, 37);
            this.Configure_textbox_timeout.Name = "Configure_textbox_timeout";
            this.Configure_textbox_timeout.Size = new System.Drawing.Size(129, 21);
            this.Configure_textbox_timeout.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(70, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "검출 횟수";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(76, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "Timeout";
            // 
            // Configure_button_load
            // 
            this.Configure_button_load.Location = new System.Drawing.Point(12, 469);
            this.Configure_button_load.Name = "Configure_button_load";
            this.Configure_button_load.Size = new System.Drawing.Size(129, 23);
            this.Configure_button_load.TabIndex = 10;
            this.Configure_button_load.Text = "설정 파일 불러오기";
            this.Configure_button_load.UseVisualStyleBackColor = true;
            this.Configure_button_load.Click += new System.EventHandler(this.Configure_button_load_Click);
            // 
            // Configure_button_save
            // 
            this.Configure_button_save.Location = new System.Drawing.Point(147, 469);
            this.Configure_button_save.Name = "Configure_button_save";
            this.Configure_button_save.Size = new System.Drawing.Size(129, 23);
            this.Configure_button_save.TabIndex = 11;
            this.Configure_button_save.Text = "설정 파일 저장";
            this.Configure_button_save.UseVisualStyleBackColor = true;
            this.Configure_button_save.Click += new System.EventHandler(this.Configure_button_save_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(74, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "글자크기";
            // 
            // Configure_textbox_size
            // 
            this.Configure_textbox_size.Location = new System.Drawing.Point(133, 92);
            this.Configure_textbox_size.Name = "Configure_textbox_size";
            this.Configure_textbox_size.Size = new System.Drawing.Size(129, 21);
            this.Configure_textbox_size.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(76, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(187, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "(상기 설정은 카메라 재연결 필요)";
            // 
            // ConfigureWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 500);
            this.Controls.Add(this.Configure_button_save);
            this.Controls.Add(this.Configure_button_load);
            this.Controls.Add(this.Configure_groupBox4);
            this.Controls.Add(this.Configure_groupBox3);
            this.Controls.Add(this.Configure_textbox_camID);
            this.Controls.Add(this.Configure_textbox_rtspurl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Configure_button_Cancel);
            this.Controls.Add(this.Configure_button_OK);
            this.Controls.Add(this.Configure_groupBox1);
            this.Controls.Add(this.Configure_groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigureWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "설정";
            ((System.ComponentModel.ISupportInitialize)(this.Configure_croparea)).EndInit();
            this.Configure_groupBox2.ResumeLayout(false);
            this.Configure_groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_UpDown_CropHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_UpDown_CropWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_UpDown_CropY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Configure_UpDown_CropX)).EndInit();
            this.Configure_groupBox3.ResumeLayout(false);
            this.Configure_groupBox3.PerformLayout();
            this.Configure_groupBox4.ResumeLayout(false);
            this.Configure_groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Configure_button_OK;
        private System.Windows.Forms.Button Configure_button_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Configure_textbox_rtspurl;
        private System.Windows.Forms.TextBox Configure_textbox_camID;
        private System.Windows.Forms.PictureBox Configure_croparea;
        private System.Windows.Forms.GroupBox Configure_groupBox1;
        private System.Windows.Forms.GroupBox Configure_groupBox2;
        private System.Windows.Forms.NumericUpDown Configure_UpDown_CropHeight;
        private System.Windows.Forms.NumericUpDown Configure_UpDown_CropWidth;
        private System.Windows.Forms.NumericUpDown Configure_UpDown_CropY;
        private System.Windows.Forms.NumericUpDown Configure_UpDown_CropX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox Configure_groupBox3;
        private System.Windows.Forms.GroupBox Configure_groupBox4;
        private System.Windows.Forms.TextBox Configure_textbox_savepath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox Configure_checkBox_isResize;
        private System.Windows.Forms.TextBox Configure_textbox_countForPass;
        private System.Windows.Forms.TextBox Configure_textbox_timeout;
        private System.Windows.Forms.Button Configure_button_load;
        private System.Windows.Forms.Button Configure_button_save;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Configure_textbox_size;
        private System.Windows.Forms.Label label10;
    }
}