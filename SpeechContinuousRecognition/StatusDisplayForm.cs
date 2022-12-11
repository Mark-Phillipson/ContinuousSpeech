using SpeechContinuousRecognition.Properties;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechContinuousRecognition
{
    public partial class StatusDisplayForm : Form
    {
        public StatusDisplayForm()
        {
            InitializeComponent();
        }
        public string LabelStatus
        {
            get => labelStatus.Text;
            set
            {
                try
                {
                    labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.Text = value; }));
                    if (value.Contains("Stopped"))
                    {
                        labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.Image = Resources.Mic_03; }));
                    }
                    else
                    {
                        labelStatus.Invoke(new MethodInvoker(delegate { labelStatus.Image = Resources.Mic_04; }));
                    }
                }
                catch (Exception exception)
                {
                    global::System.Console.WriteLine(exception.Message);
                }
            }
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
