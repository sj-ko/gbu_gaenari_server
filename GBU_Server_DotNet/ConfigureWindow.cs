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

namespace GBU_Server_DotNet
{
    public partial class ConfigureWindow : Form
    {
        private Rectangle _cropRect;
        private Point _cropRectOffset;

        public ConfigureWindow()
        {
            InitializeComponent();
        }

        public void Init()
        {
            MainForm form = (MainForm)this.Owner;
            double downRatio = ((double)Configure_croparea.Width / (double)form.panel1.Width);

            Configure_textbox_camID.Text = Convert.ToString(form.camera.camID, 10);
            Configure_textbox_rtspurl.Text = form.camera.camURL;

            _cropRect = new Rectangle((int)(form.cropX * downRatio), (int)(form.cropY * downRatio), (int)(form.cropWidth * downRatio), (int)(form.cropHeight * downRatio));
            Configure_croparea.Invalidate();

            Configure_UpDown_CropX.Value = _cropRect.X;
            Configure_UpDown_CropY.Value = _cropRect.Y;
            Configure_UpDown_CropWidth.Value = _cropRect.Width;
            Configure_UpDown_CropHeight.Value = _cropRect.Height;
        }

        private void Configure_button_OK_Click(object sender, EventArgs e)
        {
            MainForm form = (MainForm)this.Owner;
            double upRatio = ((double)form.panel1.Width / (double)Configure_croparea.Width);

            form.camera.camID = Convert.ToInt32(Configure_textbox_camID.Text, 10);
            form.camera.camURL = Configure_textbox_rtspurl.Text;

            form.cropX = (int)(_cropRect.X * upRatio);
            form.cropY = (int)(_cropRect.Y * upRatio);
            form.cropWidth = (int)(_cropRect.Width * upRatio);
            form.cropHeight = (int)(_cropRect.Height * upRatio);

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
            _cropRectOffset = e.Location;
            Configure_croparea.Invalidate();
        }

        private void Configure_croparea_MouseUp(object sender, MouseEventArgs e)
        {
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
    }
}
