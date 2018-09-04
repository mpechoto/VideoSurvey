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
        public Form2()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Close Application at all
            System.Windows.Forms.Application.Exit();
        }
               
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Create_Folder()
        {
            int current = 0;
            try
            {
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(Directory.GetCurrentDirectory(),));
                foreach (var dir in dirs)
                {
                    //string subs = dir.Substring(dir.IndexOf("_") + 1 , dir.IndexOf("@"));
                    var subs = dir.Split('_');
                    var subss = subs[1];
                    Console.WriteLine(subss);
                    current = Int32.Parse(subss);
                   // Console.WriteLine("{0}", current);
                }
                //Console.WriteLine("{0} directories found.", dirs.Count);
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
            int next = current + 1;
            string folder = @"Record_" + next + "_@" + date;
            Directory.CreateDirectory(folder);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Create_Folder();


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

     
    }
}
