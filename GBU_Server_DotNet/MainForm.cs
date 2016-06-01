/* Copyright (C)GBU Datalinks, Co. Ltd. - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by SJ Ko <sj.ko@gbudatalinks.com>, November 2015
 */

//#define TEST_PAINTEVENT
#define RECONNECT

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
        public Setting camera;
        private MediaPlayer player;
        private ANPR anpr;
        private System.Threading.Timer timer;
        private AutoResetEvent timerEvent;
        private double lastPlayTime;
        private int mediaMonitorCount;

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

        public Database dbManager = new Database();

        //public int cropX = 0;
        //public int cropY = 0;
        //public int cropWidth = 320;
        //public int cropHeight = 180;
        //public string savepath = @"D:\anprtest";

        public Graphics formGraphics;
        public Rectangle notifyBorder;
        public Color notifyColor = Color.LightGray;
        public int notifyCount = 0;

        public int overflowCount = 0;

        public string configPath = "";
        public bool isAutoStart = false;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string[] args)
        {
            InitializeComponent();

            foreach (string arg in args)
            {
                if (arg.Equals("--autostart"))
                {
                    isAutoStart = true;
                }
                else if (arg.Equals("--enlarge"))
                {
                    this.panel1.Width = 1600;
                    this.panel1.Height = 900;

                    this.Width = 1622;
                    this.Height = 1050;

                    this.listView1.Location = new Point(this.listView1.Location.X, this.listView1.Location.Y + 180);
                    this.textBox1.Location = new Point(this.textBox1.Location.X, this.textBox1.Location.Y + 180);

                    this.listView1.Height -= 50;
                    this.textBox1.Height -= 50;
                }
                else
                {
                    // config path
                    configPath = arg;
                }
            }
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

            dbManager.SavePath = camera.savePath;

            if (isAutoStart)
            {
                Play();
            }

            Console.SetOut(new ControlWriter(this.textBox1));
        }

        private void InitCamera()
        {
            camera = new Setting();
            camera.PropertyChanged += camera_PropertyChanged;

            if (configPath.Length > 0)
            {
                camera.LoadConfigFile(configPath);
            }
        }

        private void UpdateFormUIValue()
        {
            this.Text = "GBU ANPR Service" + " - Camera " + camera.camID;
        }

        private void camera_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateFormUIValue();

            dbManager.SavePath = camera.savePath;
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

        private void Play()
        {
            anpr = new ANPR(camera.timeout, camera.countForPass, camera.size);
            anpr.camID = camera.camID;

            string path = camera.camURL;
            //string path = @".\netDev_04.ts";
            player = new MediaPlayer(".\\plugins");

            player.SetRenderWindow((int)this.panel1.Handle);
            player.PlayStream(path, 1920, 1080);
            //player.PlayFile(path);

#if TEST_PAINTEVENT
            panel1.Paint += panel1_Paint; // change to timer
#else
            //
            timerEvent = new AutoResetEvent(true);
            timer = new System.Threading.Timer(MediaTimerCallBack, null, 100, camera.timeout);
            anpr.ANPRRunThread();
            anpr.ANPRDetected += anpr_ANPRDetected;
#endif

            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = true;

            notifyColor = Color.Red;
        }

        private void StopLibVLCThread()
        {
            if (player != null)
            {
                player.Stop();
            }
        }

        private void Stop()
        {
            /*if (player != null)
            {
                player.Stop();
            }*/
            Thread stopLibVLCThread = new Thread(new ThreadStart(StopLibVLCThread));
            stopLibVLCThread.Start();

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

            lastPlayTime = 0;
            mediaMonitorCount = 0;

            overflowCount = 0;

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
                            bool isReconnect = false;
                            Bitmap bmp = new Bitmap(this.panel1.Width, this.panel1.Height);

                            bmp = (Bitmap)ImageCapture.DrawToImage(this.panel1, camera.cropX, camera.cropY, camera.cropWidth, camera.cropHeight); // 108, 110, 800, 450);
                            if (camera.isResize)
                            {
                                if (camera.cropHeight > camera.cropWidth)
                                {
                                    Console.WriteLine("resize for corridor mode!");
                                    bmp = ResizeBitmap(bmp, 360, 640); // size of anpr input image
                                }
                                else
                                {
                                    Console.WriteLine("resize!");
                                    bmp = ResizeBitmap(bmp, 640, 360); // size of anpr input image
                                }
                            }
                            int pushMediaResult = anpr.pushMedia(bmp, bmp.Width, bmp.Height);
                            bmp.Dispose();

#if RECONNECT
                            if (pushMediaResult < 1)
                            {
                                Console.WriteLine("Overflow count " + (overflowCount++));
                            }
                            
                            if (overflowCount > 50)
                            {
                                Console.WriteLine("Overflow count exceeded. Try to reconnect!");
                                isReconnect = true;
                            }
#endif

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

#if RECONNECT
                            // get libvlc media state and reconnect
                            if (player != null)
                            {
                                double playtime = player.GetPlayTime();
                                //Console.WriteLine("libVlc playtime " + playtime + " lastplaytime " + lastPlayTime + " mediaMonitorCount " + mediaMonitorCount);
                                if (lastPlayTime == playtime)
                                {
                                    mediaMonitorCount++;
                                }
                                else
                                {
                                    mediaMonitorCount = 0;
                                }

                                if (mediaMonitorCount > 50)
                                {
                                    // reconnect
                                    isReconnect = true;
                                }
                                else
                                {
                                    lastPlayTime = playtime;
                                }
                            }

                            if (isReconnect)
                            {
                                Stop();
                                Play();
                            }
#endif

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
            Play();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength >= textBox1.MaxLength)
            {
                textBox1.ResetText();
            }

            textBox1.SelectionStart = textBox1.Text.Length; //Set the current caret position at the end
            textBox1.ScrollToCaret(); //Now scroll it automatically
        }
    }
}
