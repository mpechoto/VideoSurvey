using System;
using System.Drawing;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;

namespace VideoSurvey
{
    public partial class Form4 : Form
    {       
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form4()
        {           
            InitializeComponent();                       
        }
        public Form4(RealSenseImageStream imageStream, FileManager fileManager)
        {
            this.imageStream = imageStream;
            this.fileManager = fileManager;
            InitializeComponent();
            label1.Location = new Point((this.Width - label1.Width) / 2, (this.Height - label1.Height) - 50);
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            player.URL = fileManager.NextVideo;
            player.settings.volume = 100;
            //var Player = new WindowsMediaPlayer();
            //Console.WriteLine(player.currentMedia.duration);
            

            // Add a delegate for the PlayStateChange event.
            player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(player_PlayStateChange);
        }

        private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // Test the current state of the player and display a message for each state.
            switch (e.newState)
            {
                case 0:    // Undefined
                    label1.Text = "Undefined";
                    //Console.WriteLine("Undefined");
                    break;
                case 1:    // Stopped
                    label1.Text = "Stopped";
                    //Console.WriteLine("Stopped");
                    //When video stops, call next form to wait 5 seconds
                    Form5 form5 = new Form5(imageStream,fileManager);
                    form5.Show();
                    this.Visible = false;
                    break;
                case 2:    // Paused
                    label1.Text = "Paused";
                    //Console.WriteLine("Paused");
                    break;
                case 3:    // Playing
                    label1.Text = "Playing";
                    //Console.WriteLine("Playing");
                    break;
                case 4:    // ScanForward
                    label1.Text = "ScanForward";
                    //Console.WriteLine("ScanForward");
                    break;
                case 5:    // ScanReverse
                    label1.Text = "ScanReverse";
                    //Console.WriteLine("ScanReverse");
                    break;
                case 6:    // Buffering
                    label1.Text = "Buffering";
                    //Console.WriteLine("Buffering");
                    break;
                case 7:    // Waiting
                    label1.Text = "Waiting";
                    //Console.WriteLine("Waiting");
                    break;
                case 8:    // MediaEnded                    
                    label1.Text = "MediaEnded";
                    //Console.WriteLine("MediaEnded");
                    break;
                case 9:    // Transitioning
                    label1.Text = "Transitioning";
                    //Console.WriteLine("Transitioning");
                    break;
                case 10:   // Ready
                    label1.Text = "Ready";
                    //Console.WriteLine("Ready");
                    break;
                case 11:   // Reconnecting
                    label1.Text = "Reconnecting";
                    //Console.WriteLine("Reconnecting");
                    break;
                case 12:   // Last
                    label1.Text = "Last";
                    //Console.WriteLine("Last");
                    break;
                default:
                    label1.Text = "Unknown State: " + e.newState.ToString();
                    //Console.WriteLine("Unknown State");
                    break;
            }            
        }
    }
}