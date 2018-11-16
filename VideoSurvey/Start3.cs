using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Start3 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Start3(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;            
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {            
            label1.Hide();
            button1.Hide();
            player.URL = fileManager.VideosPath + @"\init\pre_teste.mov";
            player.settings.volume = 100;
            player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(player_PlayStateChange);
        }

        private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Test the current state of the player and display a message for each state.
            switch (e.newState)
            {
                case 1:// Stopped                    
                       //When video stops                                      
                    player.Hide();
                    label1.Left = (this.Size.Width - label1.Size.Width) / 2;
                    button1.Left = (this.Size.Width - button1.Size.Width) / 2;
                    label1.Show();
                    button1.Show();
                    player.close();
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileManager.TestSession = true;
            
            Form3 form3 = new Form3(imageStream, fileManager);
            form3.Show();
            this.Visible = false;
        }
    }
}
