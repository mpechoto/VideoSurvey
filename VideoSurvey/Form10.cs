using System;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form10 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form10(RealSenseImageStream imageStream, FileManager fileManager)
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
            fileManager.Answers.Q5 = GetCheckedRadioButton();
            Form11 form11 = new Form11(imageStream, fileManager);
            form11.Show();
            this.Visible = false;
        }
    }
}