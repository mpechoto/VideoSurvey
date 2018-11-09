using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form6 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form6(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.imageStream = imageStream;
            this.fileManager = fileManager;                      
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            button1.Enabled = false;
            
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            //radioButton1.AutoCheck = true;
            //radioButton2.AutoCheck = true;
            button1.Enabled = true;
        }
    }
}
