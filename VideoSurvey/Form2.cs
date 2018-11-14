using System;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form2 : Form
    {               
        RealSenseImageStream imageStream;
        FileManager fileManager;
        bool bypass = false;

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Close Application at all
            System.Windows.Forms.Application.Exit();
        }             

        private void button2_Click(object sender, EventArgs e)
        {
            string fileNameId;
            Record record = new Record
            {
                Name = textBox1.Text,
                Age = numericUpDown1.Value,
                Gender = comboBox1.Text,
                Videos = fileManager.VideoRandomList()
            };

            //Get the first name as the filename
            fileNameId = textBox1.Text.Split()[0] + ".txt";
            fileManager.FileName = fileNameId;
            //Creates a folder to each Volunteer
            fileManager.CreateSampleFolder();
            fileManager.WriteRecordJson(fileNameId, record);

            if (bypass)//bypass video
            {
                Form6 form6 = new Form6(imageStream, fileManager);
                form6.Show();
                this.Visible = false;
            }
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
