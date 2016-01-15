using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using MjpegProcessor;

namespace GBU_Server_DotNet
{
    class MediaPlayer_MJPEG
    {
        private MjpegDecoder _decoder;
        private Panel _panel;
        private Image _image;

        public MediaPlayer_MJPEG()
        {
            _decoder = new MjpegDecoder();
        }

        ~MediaPlayer_MJPEG()
        {
            Stop();
            _decoder = null;
        }

        public void SetRenderWindow(Panel panel)
        {
            _panel = panel;
        }

        public void PlayStream(string urlPath, uint width, uint height, string username, string password)
        {
            //http://14.52.220.82:50080/cgi-bin/video.cgi?msubmenu=mjpg&profileno=2
            _decoder.FrameReady += _decoder_FrameReady;
            _decoder.ParseStream(new Uri(urlPath), username, password);
        }

        private void _decoder_FrameReady(object sender, FrameReadyEventArgs e)
        {
            _panel.BackgroundImage = e.Bitmap;
        }

        public void Stop()
        {
            _decoder.FrameReady -= _decoder_FrameReady;
            _decoder.StopStream();
        }

    }
}
