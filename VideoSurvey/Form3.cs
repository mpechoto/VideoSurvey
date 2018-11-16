using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace VideoSurvey
{
    public partial class Form3 : Form
    {        
        public PXCMCapture.StreamType StreamType { get; private set; }

        RealSenseImageStream imageStream;
        FileManager fileManager;
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

            string videoName;// = Path.GetFileNameWithoutExtension(fileManager.GetNextVideo());

            if (fileManager.TestSession)//Test Session
            {
                //fileManager.TestSession = false;
                videoName = "teste";
                fileManager.NextVideo = fileManager.Record.Videos[fileManager.Qtde];                
                imageStream.SetFileName(Path.Combine(fileManager.CurrentPath, videoName)+".rssdk");
                imageStream.StartStream();
            }
            else
            {
                videoName = Path.GetFileNameWithoutExtension(fileManager.GetNextVideo());
                //Initialize Stream again during the loop
                imageStream.InitializeStream();
                imageStream.SetFileName(Path.Combine(fileManager.CurrentPath, videoName)+".rssdk");

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
                    Form4 form4 = new Form4(imageStream, fileManager);
                    form4.Show();
                    this.Visible = false;
                }
            };
            clock.Start();            
        }
    }
}