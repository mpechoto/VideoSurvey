using System;
using System.Threading;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form3 : Form
    {        
        public PXCMCapture.StreamType StreamType { get; private set; }

        RealSenseImageStream imageStream;
        FileManager fileManager;

        public float FPS = 30;
        public int height = 480;
        public int width = 640;
        int timer = 6; //6 seconds
        
        public bool IsRunning { get; private set; }
        public Thread CaptureThread { get; private set; }

        public Form3()
        {
            InitializeComponent(); 
        }

        public Form3(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.imageStream = imageStream;
            this.fileManager = fileManager;

            string videoName = System.IO.Path.GetFileNameWithoutExtension(fileManager.GetNextVideo());

            if (fileManager.Cont == 0)
            {
                imageStream.SetFileName(System.IO.Path.Combine(fileManager.CurrentPath, videoName)+".rssdk");
                imageStream.StartStream();
            }
            else
            {
                imageStream.InitializeStream();
                imageStream.SetFileName(System.IO.Path.Combine(fileManager.CurrentPath, videoName)+".rssdk");

                imageStream.StartStream();
            }
                       
            Timer(timer);
        }      

        public void Timer(int time)
        {
            System.Windows.Forms.Timer clock = new System.Windows.Forms.Timer();
            clock.Interval = 1000;

            clock.Tick += delegate
            {
                time -= 1;
                label2.Text = time.ToString();
                                
                if (time == 0)
                {
                    clock.Stop();
                    Form5 form5 = new Form5(imageStream, fileManager);
                    form5.Show();
                    this.Visible = false;
                }
            };
            clock.Start();            
        }  
    }
}
