using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GBU_Server_DotNet
{
    public class ControlWriter : TextWriter
    {
        private Control textbox;
        public ControlWriter(Control textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            if (this.textbox.InvokeRequired)
            {
                this.textbox.BeginInvoke(new Action(() =>
                    {
                        textbox.Text += value;
                    }
                ));
            }
            else
            {
                textbox.Text += value;
            }
        }

        public override void Write(string value)
        {
            if (this.textbox.InvokeRequired)
            {
                this.textbox.BeginInvoke(new Action(() =>
                {
                    textbox.Text += value;
                }
                ));
            }
            else
            {
                textbox.Text += value;
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
