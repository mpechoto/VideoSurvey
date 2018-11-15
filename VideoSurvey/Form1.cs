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
        RealSenseImageStream imageStream;
        FileManager fileManager;        
        private Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo> devices = new Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo>();
        private Dictionary<ToolStripMenuItem, int> devices_iuid = new Dictionary<ToolStripMenuItem, int>();

        public Form1()
        {            
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Closing);
            //Initilize the imageStream with a list of the Stream types
            imageStream = new RealSenseImageStream(new PXCMCapture.StreamType[] 
                { PXCMCapture.StreamType.STREAM_TYPE_COLOR, PXCMCapture.StreamType.STREAM_TYPE_DEPTH, PXCMCapture.StreamType.STREAM_TYPE_IR });
            imageStream.InitializeStream();
            label1.Text = imageStream.Status_pipeline;

            fileManager = new FileManager();           
            CheckDevices();
        }        

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
                    if (e.Checked) return devices[e];                
            }            
            return new PXCMCapture.DeviceInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private new void Closing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair da aplicação?", "Sair",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
            else
                e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imageStream.DeviceInfo = GetCheckedDevice();
            fileManager.User = GetCheckedUser();

            if (fileManager.User != "" && imageStream.DeviceInfo.name != "")
            {
                //Creates the Records folder if it does not exist
                fileManager.CreateRecordsFolder();

                //If Webcam
                if (imageStream.DeviceInfo.model == PXCMCapture.DeviceModel.DEVICE_MODEL_GENERIC)
                {
                    imageStream.Dispose();
                    imageStream.StreamType = new PXCMCapture.StreamType[] { PXCMCapture.StreamType.STREAM_TYPE_COLOR };
                    imageStream.FramesPerSecond = 30;
                    imageStream.InitializeStream(640, 480, 30);
                }

                //imageStream.SetStreams();
                //Console.WriteLine(imageStream.DeviceInfo.model);

                Form2 form2 = new Form2(imageStream, fileManager);
                form2.Show();
                this.Visible = false;
            }
            else
                MessageBox.Show("Selecione um Device e um Usuário para continuar");
        }

        public string GetCheckedUser()
        {
            foreach (ToolStripMenuItem e in usuárioToolStripMenuItem.DropDownItems)
            {
                if (e.Checked) return e.Text;
            }
            return "";
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in usuárioToolStripMenuItem.DropDownItems)
                e1.Checked = (sender == e1); 
        }
    }
}


//PENSAR NO SenseManager.captureManager.FilterByDeviceInfo(DeviceInfo);