namespace GBU_Server_DotNet
{
    partial class SearchWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.search_textBox_search = new System.Windows.Forms.TextBox();
            this.Search_listView1 = new System.Windows.Forms.ListView();
            this.Search_button_OK = new System.Windows.Forms.Button();
            this.Search_button_search = new System.Windows.Forms.Button();
            this.pictureBox_searchImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Channel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_searchImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "검색어";
            // 
            // search_textBox_search
            // 
            this.search_textBox_search.Location = new System.Drawing.Point(98, 53);
            this.search_textBox_search.Name = "search_textBox_search";
            this.search_textBox_search.Size = new System.Drawing.Size(145, 21);
            this.search_textBox_search.TabIndex = 1;
            this.search_textBox_search.TextChanged += new System.EventHandler(this.search_textBox_search_TextChanged);
            this.search_textBox_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_textBox_search_KeyDown);
            // 
            // Search_listView1
            // 
            this.Search_listView1.Location = new System.Drawing.Point(31, 116);
            this.Search_listView1.Name = "Search_listView1";
            this.Search_listView1.Size = new System.Drawing.Size(369, 203);
            this.Search_listView1.TabIndex = 2;
            this.Search_listView1.UseCompatibleStateImageBehavior = false;
            this.Search_listView1.SelectedIndexChanged += new System.EventHandler(this.Search_listView1_SelectedIndexChanged);
            // 
            // Search_button_OK
            // 
            this.Search_button_OK.Location = new System.Drawing.Point(688, 326);
            this.Search_button_OK.Name = "Search_button_OK";
            this.Search_button_OK.Size = new System.Drawing.Size(75, 23);
            this.Search_button_OK.TabIndex = 3;
            this.Search_button_OK.Text = "확인";
            this.Search_button_OK.UseVisualStyleBackColor = true;
            this.Search_button_OK.Click += new System.EventHandler(this.Search_button_OK_Click);
            // 
            // Search_button_search
            // 
            this.Search_button_search.Location = new System.Drawing.Point(249, 53);
            this.Search_button_search.Name = "Search_button_search";
            this.Search_button_search.Size = new System.Drawing.Size(75, 21);
            this.Search_button_search.TabIndex = 4;
            this.Search_button_search.Text = "검색";
            this.Search_button_search.UseVisualStyleBackColor = true;
            this.Search_button_search.Click += new System.EventHandler(this.Search_button_search_Click);
            // 
            // pictureBox_searchImage
            // 
            this.pictureBox_searchImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_searchImage.Location = new System.Drawing.Point(406, 116);
            this.pictureBox_searchImage.Name = "pictureBox_searchImage";
            this.pictureBox_searchImage.Size = new System.Drawing.Size(357, 203);
            this.pictureBox_searchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_searchImage.TabIndex = 5;
            this.pictureBox_searchImage.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "채널";
            // 
            // comboBox_Channel
            // 
            this.comboBox_Channel.FormattingEnabled = true;
            this.comboBox_Channel.Items.AddRange(new object[] {
            "전체",
            "Cam 0",
            "Cam 1",
            "Cam 2",
            "Cam 3",
            "Cam 4",
            "Cam 5",
            "Cam 6",
            "Cam 7",
            "Cam 8",
            "Cam 9",
            "Cam 10",
            "Cam 11",
            "Cam 12",
            "Cam 13",
            "Cam 14",
            "Cam 15",
            "Cam 16",
            "Cam 17",
            "Cam 18",
            "Cam 19"});
            this.comboBox_Channel.Location = new System.Drawing.Point(98, 23);
            this.comboBox_Channel.Name = "comboBox_Channel";
            this.comboBox_Channel.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Channel.TabIndex = 7;
            this.comboBox_Channel.SelectedIndexChanged += new System.EventHandler(this.comboBox_Channel_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "결과";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "차량 이미지";
            // 
            // SearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 352);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_Channel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox_searchImage);
            this.Controls.Add(this.Search_button_search);
            this.Controls.Add(this.Search_button_OK);
            this.Controls.Add(this.Search_listView1);
            this.Controls.Add(this.search_textBox_search);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "차량 검색";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_searchImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox search_textBox_search;
        private System.Windows.Forms.ListView Search_listView1;
        private System.Windows.Forms.Button Search_button_OK;
        private System.Windows.Forms.Button Search_button_search;
        private System.Windows.Forms.PictureBox pictureBox_searchImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Channel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}