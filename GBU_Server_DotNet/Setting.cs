using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GBU_Server_DotNet
{
    public class Setting : INotifyPropertyChanged
    {
        private int _camID;
        private string _camURL;

        private int _cropX;
        private int _cropY;
        private int _cropWidth;
        private int _cropHeight;

        private string _savePath;

        private bool _isResize;
        private int _timeout;
        private int _countForPass;
        private int _size;

        public int camID
        {
            get
            {
                return _camID;
            }
            set
            {
                if (value != _camID)
                {
                    _camID = value;
                    NotifyPropertyChanged("camID");
                }
            }
        }
        public string camURL
        {
            get
            {
                return _camURL;
            }
            set
            {
                if (value != _camURL)
                {
                    _camURL = value;
                    NotifyPropertyChanged("camURL");
                }
            }
        }

        public int cropX
        {
            get
            {
                return _cropX;
            }
            set
            {
                if (value != _cropX)
                {
                    _cropX = value;
                    NotifyPropertyChanged("cropX");
                }
            }
        }

        public int cropY
        {
            get
            {
                return _cropY;
            }
            set
            {
                if (value != _cropY)
                {
                    _cropY = value;
                    NotifyPropertyChanged("cropY");
                }
            }
        }

        public int cropWidth
        {
            get
            {
                return _cropWidth;
            }
            set
            {
                if (value != _cropWidth)
                {
                    _cropWidth = value;
                    NotifyPropertyChanged("cropWidth");
                }
            }
        }

        public int cropHeight
        {
            get
            {
                return _cropHeight;
            }
            set
            {
                if (value != _cropHeight)
                {
                    _cropHeight = value;
                    NotifyPropertyChanged("cropHeight");
                }
            }
        }

        public string savePath
        {
            get
            {
                return _savePath;
            }
            set
            {
                if (value != _savePath)
                {
                    _savePath = value;
                    NotifyPropertyChanged("savePath");
                }
            }
        }

        public bool isResize
        {
            get
            {
                return _isResize;
            }
            set
            {
                if (value != _isResize)
                {
                    _isResize = value;
                    NotifyPropertyChanged("isResize");
                }
            }
        }

        public int timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                if (value != _timeout)
                {
                    _timeout = value;
                    NotifyPropertyChanged("timeout");
                }
            }
        }

        public int countForPass
        {
            get
            {
                return _countForPass;
            }
            set
            {
                if (value != _countForPass)
                {
                    _countForPass = value;
                    NotifyPropertyChanged("countForPass");
                }
            }
        }

        public int size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value != _size)
                {
                    _size = value;
                    NotifyPropertyChanged("size");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Setting()
        {
            _camID = 0;
            //_camURL = "rtsp://admin:admin@14.52.220.82/media/video1";
            _camURL = "rtsp://admin:gbudata1234@14.52.220.82:554/Streaming/Channels/101/?transportmode=unicast";

            _cropX = 0;
            _cropY = 0;
            _cropWidth = 320;
            _cropHeight = 180;
            _savePath = @"D:\anprtest";

            _isResize = false;
            _timeout = 1000;
            _countForPass = 5;

            _size = 25;
        }

        public Setting(int id)
        {
            _camID = id;
            //_camURL = "rtsp://admin:admin@14.52.220.82/media/video1";
            _camURL = "rtsp://admin:gbudata1234@14.52.220.82:554/Streaming/Channels/101/?transportmode=unicast";

            _cropX = 0;
            _cropY = 0;
            _cropWidth = 320;
            _cropHeight = 180;
            _savePath = @"D:\anprtest";

            _isResize = false;
            _timeout = 1000;
            _countForPass = 5;

            _size = 25;
        }

        public Setting(int id, string url)
        {
            _camID = id;
            _camURL = url;

            _cropX = 0;
            _cropY = 0;
            _cropWidth = 320;
            _cropHeight = 180;
            _savePath = @"D:\anprtest";

            _isResize = false;
            _timeout = 1000;
            _countForPass = 5;

            _size = 25;
        }

        public void LoadConfigFile(string path)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer);
                Setting camera = new Setting();
                XmlSerializer formatter = new XmlSerializer(camera.GetType());
                camera = (Setting)formatter.Deserialize(stream);

                // read values
                _camID = camera.camID;
                _camURL = camera.camURL;

                _cropX = camera.cropX;
                _cropY = camera.cropY;
                _cropWidth = camera.cropWidth;
                _cropHeight = camera.cropHeight;
                _savePath = camera.savePath;

                _isResize = camera.isResize;
                _timeout = camera.timeout;
                _countForPass = camera.countForPass;

                _size = camera.size;

                stream.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Config File read error");
            }
        }

    }
}
