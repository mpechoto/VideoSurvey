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
        public Form1()
        {
            InitializeComponent();
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

            Form2 form2 = new Form2();           
            form2.Show();
            this.Visible = false;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
