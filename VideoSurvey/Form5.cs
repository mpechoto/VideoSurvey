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
       public static int video_number = 3;

        public Form5()
        {
            InitializeComponent();            
        }

        

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(Form2.path + "\\id.txt");
            
            axWindowsMediaPlayer1.URL = lines[video_number];//continuar daqui, vetor de videos
            axWindowsMediaPlayer1.settings.volume = 100;

        }
    }
}
