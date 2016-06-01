using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Xml.Serialization;

namespace GBU_Server_DotNet
{
    public partial class ConfigureWindow : Form
    {
        private Rectangle _cropRect;
        private Point _cropRectOffset;

        private bool _isCapture = false;

        public ConfigureWindow()
        {
            InitializeComponent();
        }

        public void Init(Setting camera = null)
        {
            MainForm form = (MainForm)this.Owner;
            Setting cam;

            if (camera == null)
            {
                cam = form.camera;
            }
            else
            {
                cam = camera;
            }

            double downRatio = ((double)Configure_croparea.Width / (double)form.panel1.Width);

            Configure_textbox_camID.Text = Convert.ToString(cam.camID, 10);
            Configure_textbox_rtspurl.Text = cam.camURL;
            Configure_textbox_savepath.Text = cam.savePath;

            _cropRect = new Rectangle((int)(cam.cropX * downRatio), (int)(cam.cropY * downRatio), (int)(cam.cropWidth * downRatio), (int)(cam.cropHeight * downRatio));
            Configure_croparea.Invalidate();

            Configure_UpDown_CropX.Value = _cropRect.X;
            Configure_UpDown_CropY.Value = _cropRect.Y;
            Configure_UpDown_CropWidth.Value = _cropRect.Width;
            Configure_UpDown_CropHeight.Value = _cropRect.Height;

            Configure_textbox_timeout.Text = Convert.ToString(cam.timeout, 10);
            Configure_textbox_countForPass.Text = Convert.ToString(cam.countForPass, 10);
            Configure_checkBox_isResize.Checked = cam.isResize;
            Configure_textbox_size.Text = Convert.ToString(cam.size, 10);
        }

        private void Configure_button_OK_Click(object sender, EventArgs e)
        {
            MainForm form = (MainForm)this.Owner;
            double upRatio = ((double)form.panel1.Width / (double)Configure_croparea.Width);

            form.camera.camID = Convert.ToInt32(Configure_textbox_camID.Text, 10);
            form.camera.camURL = Configure_textbox_rtspurl.Text;
            form.camera.savePath = Configure_textbox_savepath.Text;

            form.camera.cropX = (int)(_cropRect.X * upRatio);
            form.camera.cropY = (int)(_cropRect.Y * upRatio);
            form.camera.cropWidth = (int)(_cropRect.Width * upRatio);
            form.camera.cropHeight = (int)(_cropRect.Height * upRatio);

            form.camera.timeout = Convert.ToInt32(Configure_textbox_timeout.Text);
            form.camera.countForPass = Convert.ToInt32(Configure_textbox_countForPass.Text);
            form.camera.isResize = Configure_checkBox_isResize.Checked;
            form.camera.size = Convert.ToInt32(Configure_textbox_size.Text);

            this.Close();
        }

        private void Configure_button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Configure_croparea_Click(object sender, EventArgs e)
        {
            
        }

        private void Configure_croparea_MouseDown(object sender, MouseEventArgs e)
        {
            _isCapture = true;

            _cropRectOffset = e.Location;
            Configure_croparea.Invalidate();
        }

        private void Configure_croparea_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isCapture == false) return;
            _isCapture = false;

            if (_cropRect.Width < 1)
            {
                _cropRect.Width = 1;
            }
            if (_cropRect.Height < 1)
            {
                _cropRect.Height = 1;
            }

            if (_cropRect.X + _cropRect.Width > Configure_croparea.Width)
                _cropRect.Width = Configure_croparea.Width - _cropRect.X;

            if (_cropRect.Y + _cropRect.Height > Configure_croparea.Height)
                _cropRect.Height = Configure_croparea.Height - _cropRect.Y;

            Configure_UpDown_CropX.Value = _cropRect.X;
            Configure_UpDown_CropY.Value = _cropRect.Y;
            Configure_UpDown_CropWidth.Value = _cropRect.Width;
            Configure_UpDown_CropHeight.Value = _cropRect.Height;

            Configure_croparea.Invalidate();
        }

        private void Configure_croparea_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isCapture == false) return;

            if (e.Button == MouseButtons.Left)
            {
                Point p = e.Location;
                int x = Math.Min(_cropRectOffset.X, p.X);
                int y = Math.Min(_cropRectOffset.Y, p.Y);
                int w = Math.Abs(p.X - _cropRectOffset.X);
                int h = Math.Abs(p.Y - _cropRectOffset.Y);
                _cropRect = new Rectangle(x, y, w, h);
                Configure_croparea.Invalidate();
            }
        }

        private void Configure_croparea_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Blue, _cropRect);
        }

        private void Configure_checkBox_fullscreen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Configure_UpDown_CropX_ValueChanged(object sender, EventArgs e)
        {
            _cropRect.X = (int)Configure_UpDown_CropX.Value;
            Configure_croparea.Invalidate();
        }

        private void Configure_UpDown_CropY_ValueChanged(object sender, EventArgs e)
        {
            _cropRect.Y = (int)Configure_UpDown_CropY.Value;
            Configure_croparea.Invalidate();
        }

        private void Configure_UpDown_CropWidth_ValueChanged(object sender, EventArgs e)
        {
            _cropRect.Width = (int)Configure_UpDown_CropWidth.Value;
            Configure_croparea.Invalidate();
        }

        private void Configure_UpDown_CropHeight_ValueChanged(object sender, EventArgs e)
        {
            _cropRect.Height = (int)Configure_UpDown_CropHeight.Value;
            Configure_croparea.Invalidate();
        }

        private void Configure_button_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "설정 파일|*.cfg";
            dialog.Title = "설정 파일 열기";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream fs = (System.IO.FileStream)dialog.OpenFile();
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer);
                Setting camera = new Setting();
                XmlSerializer formatter = new XmlSerializer(camera.GetType());
                camera = (Setting)formatter.Deserialize(stream);

                Init(camera);

                stream.Close();
                fs.Close();
            }

            
        }

        private void Configure_button_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            MainForm form = (MainForm)this.Owner;

            dialog.Filter = "설정 파일|*.cfg";
            dialog.Title = "설정 파일 저장";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                Setting camera = new Setting();

                double upRatio = ((double)form.panel1.Width / (double)Configure_croparea.Width);

                camera.camID = Convert.ToInt32(Configure_textbox_camID.Text, 10);
                camera.camURL = Configure_textbox_rtspurl.Text;
                camera.savePath = Configure_textbox_savepath.Text;

                camera.cropX = (int)(_cropRect.X * upRatio);
                camera.cropY = (int)(_cropRect.Y * upRatio);
                camera.cropWidth = (int)(_cropRect.Width * upRatio);
                camera.cropHeight = (int)(_cropRect.Height * upRatio);

                camera.timeout = Convert.ToInt32(Configure_textbox_timeout.Text);
                camera.countForPass = Convert.ToInt32(Configure_textbox_countForPass.Text);
                camera.isResize = Configure_checkBox_isResize.Checked;
                camera.size = Convert.ToInt32(Configure_textbox_size.Text);
                
                System.IO.FileStream fs = (System.IO.FileStream)dialog.OpenFile();
                XmlSerializer formatter = new XmlSerializer(camera.GetType());
                formatter.Serialize(fs, camera);

                fs.Close();
            }

            
        }
    }
}
