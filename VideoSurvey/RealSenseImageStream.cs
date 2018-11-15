using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace VideoSurvey
{
    public class RealSenseImageStream
    {
        #region RealSense SDK attributes

        public PXCMSession Session { get; private set; }
        public PXCMSenseManager SenseManager { get; private set; }
        public PXCMCapture.StreamType[] StreamType { get; set; }
        public PXCMCapture.DeviceInfo DeviceInfo { get; set; }
        #endregion

        #region Stream parameters

        public float FramesPerSecond = 60;// { get; private set; }
        public int ResolutionHeight { get; private set; }
        public int ResolutionWidth { get; private set; }

        private const int DEFAULT_RESOLUTION_WIDTH = 640;
        private const int DEFAULT_RESOLUTION_HEIGHT = 480;
        //public const float DEFAULT_FPS = 60;

        #endregion

        public bool IsRunning { get; private set; }
        public Thread CaptureThread { get; private set; }
        public string Status_pipeline { get; private set; }
        

        public RealSenseImageStream(PXCMCapture.StreamType[] streamType)
        {
            StreamType = streamType;
        }

        public void InitializeStream()
        {
            InitializeStream(DEFAULT_RESOLUTION_WIDTH, DEFAULT_RESOLUTION_HEIGHT, FramesPerSecond);
        }

        public void Dispose()
        {
            //Session.Dispose();
            SenseManager.Dispose();
        }

        public void InitializeStream(int resolutionWidth, int resolutionHeight, float framesPerSecond)
        {
            // Creating a SDK session
            Session = PXCMSession.CreateInstance();

            // Creating the SenseManager
            SenseManager = Session.CreateSenseManager();
            if (SenseManager == null)
            {
                Status_pipeline = "Failed to create an SDK pipeline object";
                //Console.WriteLine("Failed to create the SenseManager object.");
                return;
            }
            else
                Status_pipeline = "Pipeline created";            

            foreach (var stream in StreamType)
            {
                // Enabling the stream
                pxcmStatus enableStreamStatus = SenseManager.EnableStream(stream, resolutionWidth, resolutionHeight, framesPerSecond);

                if (enableStreamStatus != pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    throw new InvalidRealSenseStatusException(enableStreamStatus, string.Format("Failed to enable the {0} stream. Return code: {1}", StreamType, enableStreamStatus));
                }
            }            
        }

        public void StartStream()
        {
            SenseManager.captureManager.FilterByDeviceInfo(DeviceInfo);

            // Initializing the SenseManager
            pxcmStatus initSenseManagerStatus = SenseManager.Init();

            // Initializing the SenseManager
            if (initSenseManagerStatus != pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                throw new InvalidRealSenseStatusException(initSenseManagerStatus, string.Format("Failed to initialize the SenseManager. Return code: {0}", initSenseManagerStatus));
            }

            if (SenseManager == null)
            {
                throw new NullReferenceException("The SenseManager isn't initialized. Please check if you already called the InitializeStream method.");
            }

            IsRunning = true;

            CaptureThread = new Thread(() => {
            while (IsRunning)
            {
                if (SenseManager == null || !SenseManager.IsConnected())
                {
                    throw new Exception("The SenseManager is not ready to stream.");
                }
                // Acquiring a frame with ifall=true to wait for both samples to be ready (aligned samples)
                pxcmStatus acquireFrameStatus = SenseManager.AcquireFrame(true);
                if (acquireFrameStatus != pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    throw new InvalidRealSenseStatusException(acquireFrameStatus, string.Format("Failed to acquire a frame. Return code: {0}", acquireFrameStatus));
                }
                SenseManager.ReleaseFrame();
                }
            });
            CaptureThread.Start();
        }

        public void SetFileName(string fileName, bool record=true)
        {
            pxcmStatus setFileName = SenseManager.captureManager.SetFileName(fileName, record);
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
                SenseManager.Dispose();
                Session.Dispose();
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
    }
}
