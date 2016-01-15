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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_searchImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search word";
            // 
            // search_textBox_search
            // 
            this.search_textBox_search.Location = new System.Drawing.Point(129, 36);
            this.search_textBox_search.Name = "search_textBox_search";
            this.search_textBox_search.Size = new System.Drawing.Size(145, 21);
            this.search_textBox_search.TabIndex = 1;
            this.search_textBox_search.TextChanged += new System.EventHandler(this.search_textBox_search_TextChanged);
            // 
            // Search_listView1
            // 
            this.Search_listView1.Location = new System.Drawing.Point(31, 96);
            this.Search_listView1.Name = "Search_listView1";
            this.Search_listView1.Size = new System.Drawing.Size(525, 271);
            this.Search_listView1.TabIndex = 2;
            this.Search_listView1.UseCompatibleStateImageBehavior = false;
            this.Search_listView1.SelectedIndexChanged += new System.EventHandler(this.Search_listView1_SelectedIndexChanged);
            // 
            // Search_button_OK
            // 
            this.Search_button_OK.Location = new System.Drawing.Point(481, 373);
            this.Search_button_OK.Name = "Search_button_OK";
            this.Search_button_OK.Size = new System.Drawing.Size(75, 23);
            this.Search_button_OK.TabIndex = 3;
            this.Search_button_OK.Text = "OK";
            this.Search_button_OK.UseVisualStyleBackColor = true;
            this.Search_button_OK.Click += new System.EventHandler(this.Search_button_OK_Click);
            // 
            // Search_button_search
            // 
            this.Search_button_search.Location = new System.Drawing.Point(280, 34);
            this.Search_button_search.Name = "Search_button_search";
            this.Search_button_search.Size = new System.Drawing.Size(75, 23);
            this.Search_button_search.TabIndex = 4;
            this.Search_button_search.Text = "Search";
            this.Search_button_search.UseVisualStyleBackColor = true;
            this.Search_button_search.Click += new System.EventHandler(this.Search_button_search_Click);
            // 
            // pictureBox_searchImage
            // 
            this.pictureBox_searchImage.Location = new System.Drawing.Point(562, 96);
            this.pictureBox_searchImage.Name = "pictureBox_searchImage";
            this.pictureBox_searchImage.Size = new System.Drawing.Size(357, 203);
            this.pictureBox_searchImage.TabIndex = 5;
            this.pictureBox_searchImage.TabStop = false;
            // 
            // SearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 404);
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
            this.Text = "Search Plate";
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
    }
}