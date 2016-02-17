using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GBU_Server_DotNet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                Application.Run(new MainForm(args));
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}
