using System;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form6 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;  

        public Form6(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;            
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            foreach(Control item in this.Controls)
            {
                if (item is RadioButton)
                {
                    RadioButton radioButton = item as RadioButton;
                    radioButton.Checked = false;
                }      
             }
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
            fileManager.Answers.Add (new Answers {Id = 1, Answer = GetCheckedRadioButton()} );
            //fileManager.Answers.Q1 = GetCheckedRadioButton();
            
            Form7 form7 = new Form7(imageStream, fileManager);
            form7.Show();
            this.Visible = false;
        }
    }
}