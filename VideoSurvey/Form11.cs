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
        private const int ID_QUESTION = 6;
        private string answer;

        public Form11(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;
        }   

        private void button1_Click(object sender, EventArgs e)
        {
            answer = textBox1.Text;
            fileManager.UpdateSurvey(ID_QUESTION, answer);

            //bypass
            Form6 form6 = new Form6(imageStream, fileManager);
            form6.Show();
            this.Visible = false;
        }
    }
}
