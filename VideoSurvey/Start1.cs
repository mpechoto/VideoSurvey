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
    public partial class Start1 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Start1(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.imageStream = imageStream;
            this.fileManager = fileManager;
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            player.URL = fileManager.VideosPath + @"\init\apresentacao.mov";
            player.settings.volume = 100;
            player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(player_PlayStateChange);
        }

        private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Test the current state of the player and display a message for each state.
            switch (e.newState)
            {                
                case 1:// Stopped                    
                    //When video stops, call next form to wait 5 seconds
                    Start2 start2 = new Start2(imageStream, fileManager);
                    start2.Show();
                    this.Visible = false;
                    break;                
                default:                    
                    break;
            }
        }
    }
}
