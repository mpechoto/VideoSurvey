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
        RealSenseImageStream imageStream;
        FileManager fileManager;        

        public Form2()
        {
            InitializeComponent();            
        }
        public Form2(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.imageStream = imageStream;
            this.fileManager = fileManager;
            button2.Enabled = false;
            //this.senseManager = senseManager;
            //this.DeviceInfo = DeviceInfo;
            //Console.WriteLine(imageStream.DeviceInfo.name);
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
            string path = root.FullName + @"\VideoSurvey\public\";
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
            Record record = new Record
            {
                Name = textBox1.Text,
                Age = numericUpDown1.Value,
                Gender = comboBox1.Text,
                Videos = fileManager.VideoRandomList()
            };
            //Get the first name as the filename
            fileManager.FileName = textBox1.Text.Split()[0] + ".txt";
                
            fileManager.CreateSampleFolder();
            fileManager.WriteJson(fileManager.FileName, record);

            Form3 form3 = new Form3(imageStream, fileManager);
            form3.Show();
            this.Visible = false;           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && comboBox1.Text.Trim() != "")
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }              
    }
}
