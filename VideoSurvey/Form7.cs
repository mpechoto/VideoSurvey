using System;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form7 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form7(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;
        }

        private void Form7_Load(object sender, EventArgs e)
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
            fileManager.Answers.Add( new Answers { Id = 2, Answer = GetCheckedRadioButton() });
            //fileManager.Answers.Q2 = GetCheckedRadioButton();
            Form8 form8 = new Form8(imageStream, fileManager);
            form8.Show();
            this.Visible = false;
        }
    }
}