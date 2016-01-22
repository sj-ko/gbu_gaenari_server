using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.IO;

namespace GBU_Server_DotNet
{
    class Database
    {
        private string strConn = "Server=192.168.0.40;Database=gbu_anpr1;Uid=test1;Pwd=test1;";
        private MySqlConnection conn;

        public Database()
        {
            conn = new MySqlConnection(strConn);
        }

        public int InsertPlate(int camid, DateTime datetime, string plate, Image image)
        {
            try
            {
                conn.Open();
                String sql = "INSERT INTO anpr_test1 (camId, dateTime, plate, image) " + "VALUES (@camid, @datetime, @plate, @image)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Connection = conn;
                cmd.CommandText = sql;
                //cmd.Parameters.Add("@id", MySqlDbType.Int32, 4);
                cmd.Parameters.Add("@camid", MySqlDbType.Int32, 4);
                cmd.Parameters.Add("@datetime", MySqlDbType.DateTime);
                cmd.Parameters.Add("@plate", MySqlDbType.VarChar, 32);
                cmd.Parameters.Add("@image", MySqlDbType.MediumBlob);

                //cmd.Parameters[0].Value = id;
                cmd.Parameters[0].Value = camid;
                cmd.Parameters[1].Value = datetime;
                cmd.Parameters[2].Value = plate;
                cmd.Parameters[3].Value = image;

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString() + "::" + e.StackTrace);
            }

            return 0;
        }

        public int DeletePlate(string name, string id, string pwd, string url)
        {
            return 0;
        }

        public int UpdatePlate(int no)
        {
            return 0;
        }

        public int SearchPlate(string str, ref DataTable resultTable)
        {
            try
            {
                conn.Open();
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM anpr_test1 WHERE plate like '%" + str + "%'", conn);
                da.Fill(ds, "mytable");

                DataTable dt = ds.Tables["mytable"];
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(string.Format("Name = {0}, Desc = {1}", dr["dateTime"], dr["plate"]));

                }
                resultTable = dt;
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString() + "::" + e.StackTrace);
            }
            return 0;
        }

        public int SearchPlateForFile(int ch, string str, ref DataTable resultTable)
        {
            DataTable dt = new DataTable("mytable");
            dt.Columns.Add("camId");
            dt.Columns.Add("dateTime");
            dt.Columns.Add("plate");
            dt.Columns.Add("imageFilePath");

            for (int i = 0; i < 20; i ++ )
            {
                if (ch != -1)
                {
                    i = ch;
                }

                string path = @"D:\anprtest\ch" + i;
                if (File.Exists(path + "\\anprresult.txt"))
                {
                    string[] lines = System.IO.File.ReadAllLines(path + "\\anprresult.txt");
                    foreach (string line in lines)
                    {
                        string[] values = line.Split(',');
                        if (values[1].Contains(str))
                        {
                            DataRow row = dt.NewRow();
                            row[0] = i;
                            row[1] = values[2]; // datetime
                            row[2] = values[1]; // plate
                            row[3] = values[3]; // imagepath
                            dt.Rows.Add(row);
                        }
                    }
                }

                if (ch != -1)
                {
                    break;
                }
            }

            resultTable = dt;

            return 0;
        }

        public void InsertPlateText(int camid, DateTime datetime, string plate, Image image)
        {
            string path = @"D:\anprtest\ch" + camid;
            string logFileName = "\\anprresult.txt";
            string dtStr = String.Format("{0:yyyyMMdd_HHmmss}", datetime);
            string imageFileName = "\\Camera" + camid + "_" + plate + "_" + dtStr + ".jpg";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(path + logFileName))
                File.Create(path + logFileName).Close();

            image.Save(path + imageFileName, ImageFormat.Jpeg);

            StreamWriter file = new StreamWriter(path + logFileName, true);
            file.WriteLine(camid + "," + plate + "," + datetime + "," + path + imageFileName);
            file.Flush();
            file.Close();
        }

        public void InsertPlateXML(int camid, DateTime datetime, string plate, Image image)
        {
            // to be added
            //

        }


    }
}
