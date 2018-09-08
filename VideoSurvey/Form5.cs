using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VideoSurvey
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            

            //string video = VideoRandomSelection(path);
            //Console.WriteLine(video);

            //axWindowsMediaPlayer1.URL = video;
            axWindowsMediaPlayer1.settings.volume = 100;
        }
    }
}
