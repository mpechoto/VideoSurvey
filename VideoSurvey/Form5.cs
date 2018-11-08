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
using AxWMPLib;
using System.Threading;

namespace VideoSurvey
{
    public partial class Form5 : Form
    {
       // public static int video_number = 3;
        //public Thread MyThread { get; private set; }
        //Form3 form;
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form5()
        {           
            InitializeComponent();                       
        }
        public Form5(RealSenseImageStream imageStream, FileManager fileManager)
        {
            this.imageStream = imageStream;
            this.fileManager = fileManager;
            InitializeComponent();            
        }


        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            //string[] lines = System.IO.File.ReadAllLines(Form2.path + "\\id.txt");

            //player.URL = lines[video_number];//continuar daqui, vetor de videos
            player.URL = fileManager.GetNextVideo();
            player.settings.volume = 100;
            
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
                    Console.WriteLine("Undefined");
                    break;

                case 1:    // Stopped
                    label1.Text = "Stopped";
                    Console.WriteLine("Stopped");
                    //form.StopStream(); //Stop Threading

                    Form6 form6 = new Form6(imageStream);
                    form6.Show();
                    this.Visible = false;
                    break;

                case 2:    // Paused
                    label1.Text = "Paused";
                    Console.WriteLine("Paused");
                    break;

                case 3:    // Playing
                    label1.Text = "Playing";
                    Console.WriteLine("Playing");
                    break;

                case 4:    // ScanForward
                    label1.Text = "ScanForward";
                    Console.WriteLine("ScanForward");
                    break;

                case 5:    // ScanReverse
                    label1.Text = "ScanReverse";
                    Console.WriteLine("ScanReverse");
                    break;

                case 6:    // Buffering
                    label1.Text = "Buffering";
                    Console.WriteLine("Buffering");
                    break;

                case 7:    // Waiting
                    label1.Text = "Waiting";
                    Console.WriteLine("Waiting");
                    break;

                case 8:    // MediaEnded                    
                    label1.Text = "MediaEnded";
                    Console.WriteLine("MediaEnded");
                    break;

                case 9:    // Transitioning
                    label1.Text = "Transitioning";
                    Console.WriteLine("Transitioning");
                    break;

                case 10:   // Ready
                    label1.Text = "Ready";
                    Console.WriteLine("Ready");
                    break;

                case 11:   // Reconnecting
                    label1.Text = "Reconnecting";
                    Console.WriteLine("Reconnecting");
                    break;

                case 12:   // Last
                    label1.Text = "Last";
                    Console.WriteLine("Last");
                    break;

                default:
                    label1.Text = "Unknown State: " + e.newState.ToString();
                    Console.WriteLine("Unknown State");
                    break;
            }            
        }
    }
}
