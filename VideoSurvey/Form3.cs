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
    public partial class Form3 : Form
    {
        PXCMSenseManager senseManager;
        PXCMCapture.DeviceInfo DeviceInfo { get; set; }
        public PXCMCapture.StreamType StreamType { get; private set; }

        public float FPS = 30;
        public int height = 480;
        public int width = 640;
        int timer = 6; //6 seconds

        string path;
        public bool IsRunning { get; private set; }
        public Thread CaptureThread { get; private set; }

        public Form3()
        {
            InitializeComponent(); 
        }
        public Form3(PXCMSenseManager senseManager, PXCMCapture.DeviceInfo DeviceInfo, string path)
        {
            InitializeComponent();
            this.senseManager = senseManager;
            this.DeviceInfo = DeviceInfo;
            this.path = path;
            ConfigStream();
            StartStream();
            //timer1.Start();
            Timer(6);
        }

        public void StartStream()
        {
            if (senseManager == null)
            {
                throw new NullReferenceException("The SenseManager isn't initialized. Please check if you already called the InitializeStream method.");
            }

            IsRunning = true;

            CaptureThread = new Thread(() =>
            {
                while (IsRunning)
                {
                    // Acquiring a frame with ifall=true to wait for both samples to be ready (aligned samples)
                    pxcmStatus acquireFrameStatus = senseManager.AcquireFrame(true);
                    if (acquireFrameStatus != pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        throw new InvalidRealSenseStatusException(acquireFrameStatus, string.Format("Failed to acquire a frame. Return code: {0}", acquireFrameStatus));
                    }
                    senseManager.ReleaseFrame();
                }
            });
            CaptureThread.Start();
        }

        public void StopStream()
        {
            if (CaptureThread != null)
            {
                
                int delayMillis = 1000;
                try
                {
                    IsRunning = false;
                    CaptureThread.Join(delayMillis);
                    if (CaptureThread.IsAlive)
                    {
                        Console.WriteLine("Tempo excedido; Thread ainda não terminou seu processamento");
                    }
                    else
                        Console.WriteLine("thread terminou de uma forma não bloqueante!");
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e + "Thread foi interrompida por alguma excessão lançada");
                }
                //CaptureThread.Join();
                //CaptureThread = null;
                senseManager.Dispose();
            }
            
            
            
        }


        public void ConfigStream()
        {
            //if playback or recording
            Console.WriteLine(path);
            //string[] lines = System.IO.File.ReadAllLines(path + "\\id.txt");

            //Console.WriteLine(lines[3].Split());

            senseManager.captureManager.SetFileName(path+ "\\teste.rssdk", true);

            //Enabling the stream
            //STREAM_TYPE_COLOR
            StreamType = PXCMCapture.StreamType.STREAM_TYPE_COLOR;
            pxcmStatus enableStreamStatus = senseManager.EnableStream(StreamType, width, height, 0);
            if (enableStreamStatus != pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                throw new InvalidRealSenseStatusException(enableStreamStatus, string.Format("Failed to enable the {0} stream. Return code: {1}", StreamType, enableStreamStatus));
            }
            //STREAM_TYPE_DEPTH 
            //StreamType = PXCMCapture.StreamType.STREAM_TYPE_DEPTH;
            //enableStreamStatus = senseManager.EnableStream(StreamType, width, height, FPS);
            //if (enableStreamStatus != pxcmStatus.PXCM_STATUS_NO_ERROR)
            //{
            //    throw new InvalidRealSenseStatusException(enableStreamStatus, string.Format("Failed to enable the {0} stream. Return code: {1}", StreamType, enableStreamStatus));
            //}            

            Console.WriteLine(DeviceInfo.name);
            senseManager.captureManager.FilterByDeviceInfo(DeviceInfo);
            
            // Initializing the SenseManager
            pxcmStatus initSenseManagerStatus = senseManager.Init();
            
            // Initializing the SenseManager
            if (initSenseManagerStatus != pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                throw new InvalidRealSenseStatusException(enableStreamStatus, string.Format("Failed to initialize the SenseManager. Return code: {0}", initSenseManagerStatus));
            }            
        }

        public class InvalidRealSenseStatusException : Exception
        {
            public pxcmStatus InvalidStatus { get; private set; }

            public InvalidRealSenseStatusException(pxcmStatus status, string message) : base(message)
            {
                InvalidStatus = status;
            }
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
                    Form5 form5 = new Form5(this);
                    form5.Show();
                    this.Visible = false;
                }
            };
            clock.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer > 0)
            {
                timer = timer - 1;
                label2.Text = timer.ToString();
            }
            else
                timer1.Stop();
        }
    }
}
