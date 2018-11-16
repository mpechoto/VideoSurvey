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
            if (fileManager.TestSession)
            {
                button1.Text = "Finalizar Teste";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileManager.Answers.Add(new Answers { Id = 6, Answer = textBox1.Text });            
            //Save Answers to Json File
            fileManager.SaveSurvey();

            if (fileManager.TestSession)
            {
                fileManager.TestSession = false;//Finish Test Session
                Form13 form13 = new Form13(imageStream, fileManager);
                form13.Show();
                this.Visible = false;
            }
            else
            {
                //bypass Form 6
                // Looping in all videos
                if (fileManager.Cont < fileManager.Qtde)
                {
                    Form3 form3 = new Form3(imageStream, fileManager);
                    form3.Show();
                    this.Visible = false;
                }
                //Finished all videos, go to "Thank You" Form
                else
                {
                    imageStream.Dispose();
                    Form12 form12 = new Form12();
                    form12.Show();
                    this.Visible = false;
                }
            }
        }
    }
}