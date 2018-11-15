using System;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form11 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form11(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;
        }   

        private void button1_Click(object sender, EventArgs e)
        {
            fileManager.Answers.Add(new Answers { Id = 6, Answer = textBox1.Text });
            //fileManager.Answers.Q6 = textBox1.Text;
            //Save Answers to Json File
            fileManager.SaveSurvey();

            //bypass Form 6
            if (fileManager.Cont < fileManager.Qtde)
            {
                Form3 form3 = new Form3(imageStream, fileManager);
                form3.Show();
                this.Visible = false;
            }
            else
            {
                Form12 form12 = new Form12();
                form12.Show();
                this.Visible = false;
            }
        }
    }
}