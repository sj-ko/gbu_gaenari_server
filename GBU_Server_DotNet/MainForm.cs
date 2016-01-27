﻿//#define TEST_PAINTEVENT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using gx;
using cm;

namespace GBU_Server_DotNet
{
    public partial class MainForm : Form
    {
        public Camera camera;
        private MediaPlayer player;
        private ANPR anpr;
        private System.Threading.Timer timer;
        private AutoResetEvent timerEvent;

        // for MJPEG
        private MediaPlayer_MJPEG playerMjpeg;

        //public struct PLATE_FOUND
        //{
        //    public int id;
        //    public int cam;
        //    public DateTime dateTime;
        //    public string plateStr;
        //    public string imageFilePath;
        //    //public Image snapshot;
        //};

        //private List<PLATE_FOUND> _plateList = new List<PLATE_FOUND>();
        //private int _plateListIdx = 0;

        private Database dbManager = new Database();

        public int cropX = 0;
        public int cropY = 0;
        public int cropWidth = 320;
        public int cropHeight = 180;

        public Graphics formGraphics;
        public Rectangle notifyBorder;
        public Color notifyColor = Color.LightGray;
        public int notifyCount = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        ~MainForm()
        {
            Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            listView1.Columns.Add("Camera ID", 90, HorizontalAlignment.Left);
            listView1.Columns.Add("Data & Time", 170, HorizontalAlignment.Left);
            listView1.Columns.Add("Plate String", 80, HorizontalAlignment.Left);

            InitCamera();
            UpdateFormUIValue();

            formGraphics = this.CreateGraphics();
        }

        private void InitCamera()
        {
            camera = new Camera();
            camera.PropertyChanged += camera_PropertyChanged;
        }

        private void UpdateFormUIValue()
        {
            this.Text = "GBU ANPR Service" + " - Camera " + camera.camID;
        }

        private void camera_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateFormUIValue();
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            
        }

        private void anpr_ANPRDetected(int channel, string plateStr, byte[] frame)
        {
            Console.WriteLine("ANPR Detected channel " + channel + " Time is " + DateTime.Now);

            if (this.panel1.InvokeRequired)
            {
                this.panel1.BeginInvoke(new Action(() =>
                    {
                        string[] itemStr = { Convert.ToString(channel, 10), DateTime.Now.ToString(), plateStr };
                        ListViewItem item = new ListViewItem(itemStr);
                        listView1.Items.Add(item);
                        listView1.Items[listView1.Items.Count - 1].EnsureVisible();

                        //textBox_anpr.Text = plateStr;

                        MemoryStream ms = new MemoryStream(frame);
                        Image returnImage = Image.FromStream(ms);
                        //anprResultThumbnail.Image = returnImage;

                        //PLATE_FOUND plate = new PLATE_FOUND();
                        //plate.cam = camera.camID;
                        //plate.dateTime = DateTime.Now;
                        //plate.id = _plateListIdx;
                        //plate.plateStr = plateStr;
                        //plate.snapshot = returnImage;

                        //_plateList.Add(plate);
                        //_plateListIdx++;

                        // id is auto-increment value in DB
                        dbManager.InsertPlate(camera.camID, DateTime.Now, plateStr, returnImage); // db write 
                        dbManager.InsertPlateText(camera.camID, DateTime.Now, plateStr, returnImage); // file write test

                        notifyColor = Color.LightGreen;
                        notifyCount = 0;
                    }
                ));
            }

        }

        private void Btn_Disconnect_Click(object sender, EventArgs e)
        {
            
        }

        private void Stop()
        {
            if (player != null)
            {
                player.Stop();
            }

            if (playerMjpeg != null)
            {
                playerMjpeg.Stop();
            }

            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite); // stop timer
                timer.Dispose();
                timer = null;
            }

            if (anpr != null)
            {
                anpr.ANPRStopThread();
                anpr.ANPRDetected -= anpr_ANPRDetected;
                anpr = null;
            }

            notifyColor = Color.LightGray;
            notifyCount = 0;
            DrawBorder();

            //_plateListIdx = 0;
            //_plateList.Clear();
        }

#if TEST_PAINTEVENT
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.panel1.Width, this.panel1.Height);

            // In order to use DrawToBitmap, the image must have an INITIAL image. 
            // Not sure why. Perhaps it uses this initial image as a mask??? Dunno.
            /*using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(Color.Yellow);
            }*/

            bmp = (Bitmap)ImageCapture.DrawToImage(this.panel1);
            anpr.getValidPlate2(bmp, bmp.Width, bmp.Height);

            //bmp.Save("c:\\save.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            bmp.Dispose();
        }
#endif

        private void MediaTimerCallBack(Object obj)
        {
            //Console.WriteLine("MediaTimerCallBack");
            

            if (this.panel1.InvokeRequired)
            {
                this.panel1.BeginInvoke(new Action(() =>
                    {
                        if (anpr != null)
                        {
                            Bitmap bmp = new Bitmap(this.panel1.Width, this.panel1.Height);

                            bmp = (Bitmap)ImageCapture.DrawToImage(this.panel1, cropX, cropY, cropWidth, cropHeight); // 108, 110, 800, 450);
                            //bmp = ResizeBitmap(bmp, 480, 270); // size of anpr input image
                            anpr.pushMedia(bmp, bmp.Width, bmp.Height);
                            bmp.Dispose();

                            if (notifyColor == Color.Red || notifyColor == Color.LightGreen)
                            {
                                DrawBorder();
                            }

                            if (notifyColor == Color.LightGreen)
                            {
                                notifyCount++;

                                if (notifyCount > 30)
                                {
                                    notifyCount = 0;
                                    notifyColor = Color.Red;
                                }

                            }
                        }
                    }
                ));
            }
            else
            {
                // do nothing
            }
        }

        private static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
            Application.Exit();
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigureWindow configureWindow = new ConfigureWindow();
            configureWindow.Owner = this;
            configureWindow.Init();
            configureWindow.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GBU ANPR Service " + Application.ProductVersion + "\n" + "For Gaenari Gas Station"
                + "\n\n" + "(C) 2015 GBU Datalinks Co. Ltd.");
        }

        private void searchPlateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Owner = this;
            searchWindow.Init();
            searchWindow.Show();
        }

        private void anprResultThumbnail_Click(object sender, EventArgs e)
        {

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anpr = new ANPR();
            anpr.camID = camera.camID;

            string path = camera.camURL;
            //string path = @"C:\NetDEVSDK\Record\netDev_04.ts";
            player = new MediaPlayer(".\\plugins");

            player.SetRenderWindow((int)this.panel1.Handle);
            player.PlayStream(path, 1920, 1080);
            //player.PlayFile(path);

#if TEST_PAINTEVENT
            panel1.Paint += panel1_Paint; // change to timer
#else
            //
            timerEvent = new AutoResetEvent(true);
            timer = new System.Threading.Timer(MediaTimerCallBack, null, 100, 1000);
            anpr.ANPRRunThread();
            anpr.ANPRDetected += anpr_ANPRDetected;
#endif

            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = true;

            notifyColor = Color.Red;
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();

            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listView1.FocusedItem.Index;
            //textBox_anpr.Text = listView1.Items[index].SubItems[2].Text;
            //anprResultThumbnail.Image = _plateList[index].snapshot;
        }

        private void DrawBorder()
        {
            Point locationOnForm = panel1.FindForm().PointToClient(panel1.Parent.PointToScreen(panel1.Location));
            notifyBorder = new Rectangle(locationOnForm.X - 2, locationOnForm.Y - 2, panel1.Width + 2, panel1.Height + 2);
            formGraphics.DrawRectangle(new Pen(notifyColor, 2.5f), notifyBorder);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }

    }
}
