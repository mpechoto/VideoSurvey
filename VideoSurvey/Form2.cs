using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form2 : Form
    {
        public static string path;
        PXCMSenseManager senseManager;
        public PXCMCapture.DeviceInfo DeviceInfo { get; set; }

        public Form2()
        {
            InitializeComponent();            
        }
        public Form2(PXCMSenseManager senseManager, PXCMCapture.DeviceInfo DeviceInfo)
        {
            InitializeComponent();
            this.senseManager = senseManager;
            this.DeviceInfo = DeviceInfo;
            Console.WriteLine(DeviceInfo.name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Close Application at all
            System.Windows.Forms.Application.Exit();
        }

        private string Create_Folder()
        {
            int current = 0;
            int next = 0;
            try
            {
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(Directory.GetCurrentDirectory()));
                foreach (var dir in dirs)
                {
                    var subs = dir.Split('_');
                    current = Int32.Parse(subs[1]);
                    
                    if (next < current)
                    {
                        next = current;
                    }
                }
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
            
            string date = DateTime.Now.ToString("dd-MM-yyyy_HH-mm");
            //Console.WriteLine(date);
            next += 1;
            string folder = @"Record_" + next + "_@" + date;
            Directory.CreateDirectory(folder);
            return folder;
        }

        private void Write_Info(string path, List<string> list)
        {
            StreamWriter writer = new StreamWriter(path + "\\id.txt", append: true);
            using (writer)
            {
                writer.WriteLine(textBox1.Text);
                writer.WriteLine(numericUpDown1.Value);
                writer.WriteLine(comboBox1.Text);
                foreach (String s in list)
                    writer.WriteLine(s);
            }
            writer.Close();
        }

        private List<string> VideoRandomList()
        {
            DirectoryInfo root = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent;
            string path = root.FullName + @"\public\";
            //Console.WriteLine("path {0}",path);

            List<string> fileList = new List<string>(Directory.GetFiles(path));         
            List<string> randomList = new List<string>();
            Random rand = new Random();
            int randomIndex = 0;

            while (fileList.Count > 0)
            {
                randomIndex = rand.Next(0, fileList.Count);//Choose a random object in the list
                randomList.Add(fileList[randomIndex]);//add it to the new, random list
                fileList.RemoveAt(randomIndex);//remove to avoid duplicates
            }            
            return randomList; //return the new random list
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string folder_name = Create_Folder();
            // string 
                path = Directory.GetCurrentDirectory() + "\\" + folder_name;

            List<string> videoRandomList = VideoRandomList();
            //foreach (String s in videoRandomList)
                //Console.WriteLine("after " + s);
            Write_Info(path, videoRandomList);
            //Console.WriteLine(path);

            Form3 form3 = new Form3(senseManager, DeviceInfo, path);
            form3.Show();
            this.Visible = false;

        }
    }
}
