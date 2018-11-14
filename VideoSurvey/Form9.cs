using System;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form9 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form9(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private string GetCheckedRadioButton()
        {
            string answer = null;
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radioButton = control as RadioButton;
                    if (radioButton.Checked)
                        answer = radioButton.Tag.ToString();
                }
            }
            return answer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileManager.Answers.Q4 = GetCheckedRadioButton();
            Form10 form10 = new Form10(imageStream, fileManager);
            form10.Show();
            this.Visible = false;
        }              
    }
}
