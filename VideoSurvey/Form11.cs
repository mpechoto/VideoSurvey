using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form11 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;
        //private const int ID_QUESTION = 6;
        //private string answer;

        public Form11(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;
        }   

        private void button1_Click(object sender, EventArgs e)
        {
            fileManager.Answers.Q6 = textBox1.Text;
            //fileManager.UpdateSurvey(ID_QUESTION, answer);

            //Save Answers to Json File
            fileManager.SaveSurvey();

            //bypass Form 6
            if (fileManager.Cont < fileManager.Qtde)
            {
                Form3 form3 = new Form3(imageStream, fileManager);
                form3.Show();
                this.Visible = false;
            }
            else //precisar implementar a tela de saida final
                //Close Application at all 
                System.Windows.Forms.Application.Exit();
        }
    }
}
