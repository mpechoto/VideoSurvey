using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form1 : Form
    {
        //PXCMSession session;
        //PXCMSenseManager senseManager;

        RealSenseImageStream imageStream;
        FileManager fileManager;
        
        private Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo> devices = new Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo>();
        private Dictionary<ToolStripMenuItem, int> devices_iuid = new Dictionary<ToolStripMenuItem, int>();

        public Form1()
        {
            InitializeComponent();

            imageStream = new RealSenseImageStream(new PXCMCapture.StreamType[] 
                { PXCMCapture.StreamType.STREAM_TYPE_COLOR/*, PXCMCapture.StreamType.STREAM_TYPE_DEPTH*/ });
            imageStream.InitializeStream(640,480,30);
            label1.Text = imageStream.Status_pipeline;

            //CreateSenseManager(ses);
            //session = ses;
            CheckDevices();
            //sm = session.CreateSenseManager();
        }

        /*public void CreateSenseManager(PXCMSession ses)
        {
            // Creating a SDK session
            //session = PXCMSession.CreateInstance();
            session = ses;
            // Creating the SenseManager
            senseManager = ses.CreateSenseManager();
            if (senseManager == null)
            {
                label1.Text = "Failed to create an SDK pipeline object";
                //Console.WriteLine("Failed to create the SenseManager object.");
                return;
            }
            else
                label1.Text = "Pipeline created";
        }*/

        private void CheckDevices()
        {
            devices.Clear();
            devices_iuid.Clear();

            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;

            devicesToolStripMenuItem.DropDownItems.Clear();

            for (int i = 0; ; i++)
            {                
                if (imageStream.Session.QueryImpl(desc, i, out PXCMSession.ImplDesc desc1) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    break;                
                if (imageStream.Session.CreateImpl<PXCMCapture>(desc1, out PXCMCapture capture) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    continue;
                for (int j = 0; ; j++)
                {                    
                    if (capture.QueryDeviceInfo(j, out PXCMCapture.DeviceInfo dinfo) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                        break;

                    ToolStripMenuItem sm1 = new ToolStripMenuItem(dinfo.name, null, new EventHandler(Device_Item_Click));
                    devices[sm1] = dinfo;
                    devices_iuid[sm1] = desc1.iuid;
                    devicesToolStripMenuItem.DropDownItems.Add(sm1);
                }
                capture.Dispose();
            }
        }

        private void Device_Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in devicesToolStripMenuItem.DropDownItems)
                e1.Checked = (sender == e1);
                label2.Text = GetCheckedDevice().name;
            //PopulateColorDepthMenus(sender as ToolStripMenuItem);
        }

        public PXCMCapture.DeviceInfo GetCheckedDevice()
        {
            foreach (ToolStripMenuItem e in devicesToolStripMenuItem.DropDownItems)
            {
                if (devices.ContainsKey(e))
                {
                    if (e.Checked) return devices[e];
                }
            }            
            return new PXCMCapture.DeviceInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Get the Parent Path C:\Users\user\source\repos\VideoSurvey
            DirectoryInfo root = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent;
            
            string path = root.FullName + "\\Records";

            //Create the folder "Records" if it does not exist. There's no need to do an explicit check first
            //This folder will record all user data
            Directory.CreateDirectory(path);

            //Set the new Current Directory to this path, it will be useful to create new subfolders.
            try
            {
                Directory.SetCurrentDirectory(path);
            }
            catch (DirectoryNotFoundException exp)
            {
                Console.WriteLine("The specified directory does not exist. {0}", exp);
            }
            //Debug purpose
            //MessageBox.Show(root.FullName);
            imageStream.DeviceInfo = GetCheckedDevice();
            Form2 form2 = new Form2(imageStream);           
            form2.Show();
            this.Visible = false;
        }
    }
}
