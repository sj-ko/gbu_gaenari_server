//#define FAST_DETECT

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using gx;
using cm;

namespace GBU_Server_DotNet
{
    static class Constants
    {
        public const int CANDIDATE_REMOVE_TIME = 600000; //60000; // ms
        //public const int CANDIDATE_COUNT_FOR_PASS = 5; // default is 3, for gas station stop is 20
        public const int MAX_IMAGE_BUFFER = 30;
    }

    class ANPR
    {
        public delegate void OnANPRDetected(int channel, string plateStr, byte[] frame);
        public event OnANPRDetected ANPRDetected;

        public string[] KOREA_LOCALAREA_LIST = {"서울","인천","세종","대전","대구","부산","광주","울산",
                                            "경기","강원","충북","충남","경북","경남","전북","전남","제주"};

        public string[] KOREA_CHAR_LIST = {"가","나","다","라","마","바","사","아","자",
                                           "거","너","더","러","머","버","서","어","저",
                                           "고","노","도","로","모","보","소","오","조",
                                           "구","누","두","루","무","부","수","우","주",
                                           "하","허","호","배"};

        public int CANDIDATE_COUNT_FOR_PASS = 5; // default is 3, for gas station stop is 20

        public struct PLATE_CANDIDATE
        {
            public int id; // reserved
            public int foundCount;
            public int firstfoundTime;
            public string plate_string;
        }

        public struct GBUVideoFrame
        {
            public Byte[] frame;
            public int camindex;
        };

        private int _camID;
        private GBUVideoFrame[] _imageBuffer = new GBUVideoFrame[Constants.MAX_IMAGE_BUFFER];
        private int _imageBufferCount = 0;
        private int _imageBufferEmptyIndex = 0;

        // Creates the ANPR object
        private cmAnpr _anpr = new cmAnpr("default");
        // Creates the image object
        private gxImage _image = new gxImage("default");

        Thread ANPRThread;
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _isANPRThreadRun = false;

        public int camID
        {
            get
            {
                return _camID;
            }

            set
            {
                _camID = value;
            }
        }

        public ANPR()
        {
            initANPR(_anpr);

            ANPRThread = new Thread(ANPRThreadFunction);
        }

        public ANPR(int timeout, int countForPass, int size)
        {
            initANPR(_anpr, timeout, size);

            CANDIDATE_COUNT_FOR_PASS = countForPass;

            ANPRThread = new Thread(ANPRThreadFunction);
        }

        ~ANPR()
        {
            ANPRStopThread();
        }

        private void initANPR(cmAnpr anpr)
        {
            anpr.SetProperty("anprname", "cmanpr-7.2.7.68:kor");
            anpr.SetProperty("size", "25"); // default 25  (20-->15)
            anpr.SetProperty("size_min", "6"); //"8"); // Default 6
            anpr.SetProperty("size_max", "93"); //"40"); // Default 93

            anpr.SetProperty("nchar_min", "6"); // "7"); // Default 8
            anpr.SetProperty("nchar_max", "9"); // Default 9

            anpr.SetProperty("slope", "9"); // "-5"); // Default -22
            anpr.SetProperty("slope_min", "-22"); //-20"); // Default -22
            anpr.SetProperty("slope_max", "34"); // "10"); // Default 34

            anpr.SetProperty("slant", "10"); // "0"); // Default 10
            anpr.SetProperty("slant_min", "-55"); // "-10"); // Default -55
            anpr.SetProperty("slant_max", "27"); // "10"); // Default 27

            anpr.SetProperty("timeout", "500"); // default 100 

            anpr.SetProperty("contrast_min", "10");
            anpr.SetProperty("xtoyres", "0");
            anpr.SetProperty("colortype", "0");
            anpr.SetProperty("gaptospace", "0");
            anpr.SetProperty("unicode_in_text", "0");
            anpr.SetProperty("general", "1");
            anpr.SetProperty("depth", "137");
            anpr.SetProperty("adapt_environment", "1");
            anpr.SetProperty("unit", "1");
            anpr.SetProperty("analyzecolors", "0");
            anpr.SetProperty("whitebalance", "100");
            /*anpr.SetProperty("posfreqhalflife", "0");
            anpr.SetProperty("posfreqweight", "61");
            anpr.SetProperty("posfreqhistxs", "16");
            anpr.SetProperty("posfreqhistys", "16");
            anpr.SetProperty("posfreq", "1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000");*/
        }

        private void initANPR(cmAnpr anpr, int timeout, int size)
        {
            anpr.SetProperty("anprname", "cmanpr-7.2.7.68:kor");
            anpr.SetProperty("size", size); // default 25  (20-->15)
            anpr.SetProperty("size_min", "6"); //"8"); // Default 6
            anpr.SetProperty("size_max", "93"); //"40"); // Default 93

            anpr.SetProperty("nchar_min", "6"); // "7"); // Default 8
            anpr.SetProperty("nchar_max", "9"); // Default 9

            anpr.SetProperty("slope", "9"); // "-5"); // Default -22
            anpr.SetProperty("slope_min", "-22"); //-20"); // Default -22
            anpr.SetProperty("slope_max", "34"); // "10"); // Default 34

            anpr.SetProperty("slant", "10"); // "0"); // Default 10
            anpr.SetProperty("slant_min", "-55"); // "-10"); // Default -55
            anpr.SetProperty("slant_max", "27"); // "10"); // Default 27

            anpr.SetProperty("timeout", timeout); // default 100 

            anpr.SetProperty("contrast_min", "10");
            anpr.SetProperty("xtoyres", "0");
            anpr.SetProperty("colortype", "0");
            anpr.SetProperty("gaptospace", "0");
            anpr.SetProperty("unicode_in_text", "0");
            anpr.SetProperty("general", "1");
            anpr.SetProperty("depth", "137");
            anpr.SetProperty("adapt_environment", "1");
            anpr.SetProperty("unit", "1");
            anpr.SetProperty("analyzecolors", "0");
            anpr.SetProperty("whitebalance", "100");
            /*anpr.SetProperty("posfreqhalflife", "0");
            anpr.SetProperty("posfreqweight", "61");
            anpr.SetProperty("posfreqhistxs", "16");
            anpr.SetProperty("posfreqhistys", "16");
            anpr.SetProperty("posfreq", "1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000;1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000,1000");*/
        }

        public void ANPRRunThread()
        {
            _isANPRThreadRun = true;
            if (ANPRThread != null)
            {
                ANPRThread.Start();
            }
        }

        public void ANPRStopThread()
        {
            _isANPRThreadRun = false;
            if (ANPRThread != null && ANPRThread.IsAlive)
            {
                ANPRThread.Join();
            }
        }

        public void ANPRThreadFunction()
        {
            List<string> anpr_result = new List<string>();
            List<PLATE_CANDIDATE> plate_candidates = new List<PLATE_CANDIDATE>();
            GBUVideoFrame frame = new GBUVideoFrame();

            while (_isANPRThreadRun)
            {

                if (popMedia(ref frame))
                {
                    anpr_result.Clear();

                    _image.LoadFromMem(frame.frame, (int)GX_PIXELFORMATS.GX_RGB);

                    if (getValidPlates(_image, ref anpr_result) > 0)
                    {

                        // Remove old results
                        int currentTime = Environment.TickCount;
                        for (int i = plate_candidates.Count - 1; i >= 0; i--)
                        {
                            if (currentTime - plate_candidates[i].firstfoundTime > Constants.CANDIDATE_REMOVE_TIME)
                            {
                                plate_candidates.RemoveAt(i);
                            }
                        }

                        // Check duplicate
                        for (int i = 0; i < anpr_result.Count; i++)
                        {
                            bool isNew = true;

                            for (int j = 0; j < plate_candidates.Count; j++)
                            {
                                //if (_tcsnccmp(anpr_result[i].c_str(), plate_candidates[j].plate_string, _tcslen(anpr_result[i].c_str())) == 0)  {
                                if (anpr_result[i].Equals(plate_candidates[j].plate_string) ||
                                     (anpr_result[i].Substring(anpr_result[i].Length - 4, 4).Equals(plate_candidates[j].plate_string.Substring(plate_candidates[j].plate_string.Length - 4, 4))))
                                {
                                    isNew = false;
#if (!FAST_DETECT)
                                    PLATE_CANDIDATE modified;
                                    modified.firstfoundTime = plate_candidates[j].firstfoundTime;
                                    modified.foundCount = plate_candidates[j].foundCount + 1;
                                    modified.id = plate_candidates[j].id;
                                    modified.plate_string = plate_candidates[j].plate_string;
                                    plate_candidates.RemoveAt(j);
                                    plate_candidates.Add(modified);

                                    if (modified.foundCount == CANDIDATE_COUNT_FOR_PASS)
                                    {
                                        // Announce Event
                                        //SetLog(cropregion, plate_candidates[j].plate_string, TEXT("msg"));

                                        //wchar_t eventlog[1024];
                                        //wsprintf(eventlog, TEXT("%s\t"), plate_candidates[j].plate_string);
                                        //OutputDebugString(eventlog);
                                        Console.WriteLine("Detected candidate : " + modified.plate_string);
                                        ANPRDetected(_camID, modified.plate_string, frame.frame);
                                    }
                                    break;
#endif
                                }
                            }

                            if (isNew)
                            {
                                currentTime = Environment.TickCount;
                                PLATE_CANDIDATE newItem;
                                newItem.firstfoundTime = currentTime;
                                newItem.foundCount = 1;
                                newItem.plate_string = anpr_result[i];
                                newItem.id = 0;

                                plate_candidates.Add(newItem);
#if (FAST_DETECT)
                                Console.WriteLine("Detected candidate : " + newItem.plate_string);
                                ANPRDetected(_camID, newItem.plate_string, frame.frame);
#endif
                            }
                        }
                    }


                }

                // end of thread cycle
                Thread.Sleep(1);
            }
        }

        public int getValidPlates(gxImage image, ref List<string> list)
        {
	        int count = 0;

            lock (this)
            {

                try
                {

                    // Finds the first plate and displays it
                    //QueryPerformanceCounter(&counter_before);
                    bool found = _anpr.FindFirst(image);
                    //QueryPerformanceCounter(&counter_after);
                    //double anprTime = (double)(counter_after.QuadPart - counter_before.QuadPart) / freq.QuadPart;

                    list.Clear();

                    while (found)
                    {
                        string resultLoop = _anpr.GetText();

                        if (isValidPlateString(resultLoop))
                        {
                            //Console.WriteLine("[OK]" + resultLoop);
                            list.Add(getAdjustPlate(resultLoop));
                            count++;

                        }
                        else
                        {
                            //Console.WriteLine("[NG]" + resultLoop);
                        }

                        // Finds other plates
                        found = _anpr.FindNext();
                    }
                    //printf("\n");
                    //printf("\nMemLoad:%lf\tAnpr:%lf\n", loadTime, anprTime);
                }
                catch (gxException e)
                {
                    System.Diagnostics.Debug.WriteLine("Get Plates Failed : " + e.ToString());
                }

            }
	        return count;
        }

        // for test
        public void getValidPlate2(Bitmap anprImage, int width, int height)
        {
            byte[] anprByteArray = imageToByteArray(anprImage);

            Console.WriteLine("image size " + anprByteArray.Length);

            bool ret = _image.LoadFromMem(anprByteArray, (int)GX_PIXELFORMATS.GX_RGB);

            Console.WriteLine("Load ANPR Image " + ret);

            // Finds the first plate and displays it
            if (_anpr.FindFirst(_image))
            {
                Console.WriteLine("Result: '{0}'", _anpr.GetText());
                Console.WriteLine("Type: '{0}'", _anpr.GetType());
            }
            else
            {
                Console.WriteLine("No plate found");
            }
        }

        public int pushMedia(Bitmap anprImage, int width, int height)
        {
            if (_imageBufferCount >= Constants.MAX_IMAGE_BUFFER)
            {
                Console.WriteLine("Media Buffer Overflow!");
                return 0;
            }
            
            byte[] anprByteArray = imageToByteArray(anprImage);
            int index = _imageBufferEmptyIndex;

            _imageBuffer[index].frame = anprByteArray;
            _imageBuffer[index].camindex = _camID;

            _imageBufferEmptyIndex++;
            //Console.WriteLine("image size " + anprByteArray.Length);

            if (_imageBufferEmptyIndex >= Constants.MAX_IMAGE_BUFFER)
            {
                _imageBufferEmptyIndex = 0;
            }
            _imageBufferCount++;

            return 1;
        }

        public bool popMedia(ref GBUVideoFrame frame)
        {
            //Console.WriteLine("ImageBuffer" + _imageBufferCount);
            if (_imageBufferCount > 0)
            {
                int frontindex = _imageBufferEmptyIndex - _imageBufferCount;
                if (frontindex < 0)
                {
                    frontindex += Constants.MAX_IMAGE_BUFFER;
                }
                frame.frame = _imageBuffer[frontindex].frame;
                frame.camindex = _imageBuffer[frontindex].camindex;

                _imageBufferCount--;

                return true;
            }
            else
            {
                return false;
            }
        }

        bool isValidPlateString(string plateValue)
        {
            bool isValidChar = false;

	        if (plateValue.Length > 8 && (plateValue[0] < 0 || Char.IsDigit(plateValue[0]) == false)) 
            {
		        // Check Old (Loca-12-Kr-1234)
                if (plateValue[2] < 0 || Char.IsDigit(plateValue[2]) == false) return false;
                if (plateValue[3] < 0 || Char.IsDigit(plateValue[3]) == false) return false;
                if (plateValue[5] < 0 || Char.IsDigit(plateValue[5]) == false) return false;
                if (plateValue[6] < 0 || Char.IsDigit(plateValue[6]) == false) return false;
                if (plateValue[7] < 0 || Char.IsDigit(plateValue[7]) == false) return false;
                if (plateValue[8] < 0 || Char.IsDigit(plateValue[8]) == false) return false;

                foreach (string str in KOREA_CHAR_LIST)
                {
                    if (plateValue.Substring(4, 1).Equals(str))
                    {
                        isValidChar = true;
                    }
                }
	        }
	        else
            {
		        // 2006 yr. (12-Kr-1234)
                if (plateValue.Length != 7) return false;
                if (plateValue[1] < 0 || Char.IsDigit(plateValue[1]) == false) return false;
                if (plateValue[3] < 0 || Char.IsDigit(plateValue[3]) == false) return false;
                if (plateValue[4] < 0 || Char.IsDigit(plateValue[4]) == false) return false;
                if (plateValue[5] < 0 || Char.IsDigit(plateValue[5]) == false) return false;
                if (plateValue[6] < 0 || Char.IsDigit(plateValue[6]) == false) return false;

                foreach (string str in KOREA_CHAR_LIST)
                {
                    if (plateValue.Substring(2, 1).Equals(str))
                    {
                        isValidChar = true;
                    }
                }
	        }

            return isValidChar;
        }

        private string getAdjustPlate(string plate)
        {
            string str = plate;

            bool isWrongArea = false;

            if (str.Length > 8)
            {
                // Check Old and Biz (Loca-12-Kr-1234)
                if (Char.IsDigit(str[0]) == false)
                {
                    // Check Old (Loca-12-Kr-1234) Local area
                    foreach (string areaName in KOREA_LOCALAREA_LIST)
                    {
                        if (!str.Contains(areaName))
                        {
                            isWrongArea = true;
                        }
                    }

                    // adjust area
                    if (isWrongArea == true)
                    {
                        if (str.StartsWith("서"))
                        {
                            str = str.Remove(0, 2);
                            str = str.Insert(0, "서울");
                        }

                        else if (str.Substring(0, 2).EndsWith("울"))
                        {
                            str = str.Remove(0, 2);
                            str = str.Insert(0, "서울");
                        }

                        else if (str.Substring(0, 2).Equals("무산"))
                        {
                            str = str.Remove(0, 2);
                            str = str.Insert(0, "부산");
                        }

                        else if (str.StartsWith("부"))
                        {
                            str = str.Remove(0, 2);
                            str = str.Insert(0, "부산");
                        }

                        else if (str.Substring(0, 2).EndsWith("산"))
                        {
                            str = str.Remove(0, 2);
                            str = str.Insert(0, "부산");
                        }

                        else if (str.Substring(0, 2).Equals("경거"))
                        {
                            str = str.Remove(0, 2);
                            str = str.Insert(0, "경기");
                        }
                    }
                }

            }
            else
            {
                // 2006 yr. (12-Kr-1234)
                // Adjust for only White plate (type 8203,8204)
                if (_anpr.GetType() == 8203 || _anpr.GetType() == 8204)
                {
                    // 호
                    if (_anpr.GetCharacter(2).confidence < 95 && str.Substring(2, 1).Equals("오"))
                    {
                        str = str.Replace("오", "호");
                    }

                    else if (_anpr.GetCharacter(2).confidence < 95 && str.Substring(2, 1).Equals("로"))
                    {
                        str = str.Replace("로", "호");
                    }

                    else if (_anpr.GetCharacter(2).confidence < 90 && str.Substring(2, 1).Equals("자"))
                    {
                        str = str.Replace("자", "호");
                    }

                    else if (_anpr.GetCharacter(2).confidence < 90 && str.Substring(2, 1).Equals("소"))
                    {
                        str = str.Replace("소", "호");
                    }

                    /*else if (_anpr.GetCharacter(2).confidence < 90 && str.Substring(2, 1).Equals("보"))
                    {
                        str = str.Replace("보", "호");
                    }*/

                    else if (_anpr.GetCharacter(2).confidence < 90 && str.Substring(2, 1).Equals("다"))
                    {
                        str = str.Replace("다", "호");
                    }

                    // 하
                    // '아' are reserved for business vehicles. (Loca-12-Kr-1234)
                    else if (/*_anpr.GetCharacter(2).confidence < 95 &&*/ str.Substring(2, 1).Equals("아"))
                    {
                        str = str.Replace("아", "하");
                    }

                    else if (_anpr.GetCharacter(2).confidence < 95 && str.Substring(2, 1).Equals("라"))
                    {
                        str = str.Replace("라", "하");
                    }

                    else if (_anpr.GetCharacter(2).confidence < 95 && str.Substring(2, 1).Equals("허"))
                    {
                        str = str.Replace("허", "하");
                    }

                    /*else if (_anpr.GetCharacter(2).confidence < 80 && str.Substring(2, 1).Equals("마"))
                    {
                        str = str.Replace("마", "하");
                    }*/

                    // 로
                    /*else if (_anpr.GetCharacter(2).confidence < 95 && str.Substring(2, 1).Equals("고"))
                    {
                        str = str.Replace("고", "로");
                    }*/
                }

            }

            return str;
        }

        private byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

    }
}
