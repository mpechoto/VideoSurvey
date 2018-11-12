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
    public partial class Form7 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;
        private const int ID_QUESTION = 2;
        private string answer;

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

        private void GetCheckedRadioButton()
        {
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radioButton = control as RadioButton;
                    if (radioButton.Checked)
                        answer = radioButton.Tag.ToString();                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetCheckedRadioButton();
            fileManager.UpdateSurvey(ID_QUESTION, answer);

            Form8 form8 = new Form8(imageStream, fileManager);
            form8.Show();
            this.Visible = false;
        }
    }
}
