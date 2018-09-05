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

        private void Write_Id(string path)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string folder_name = Create_Folder();
            string path = Directory.GetCurrentDirectory() + "\\" + folder_name;

            StreamWriter writer = new StreamWriter(path + "\\id.txt", append: true);
            using (writer)
            {
                writer.WriteLine(textBox1.Text);
                writer.WriteLine(numericUpDown1.Value);
                writer.WriteLine(comboBox1.Text);
            }
            writer.Close();

            Form3 form3 = new Form3();
            form3.Show();
            this.Visible = false;

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

     
    }
}
