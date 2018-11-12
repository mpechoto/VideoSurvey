﻿using System;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form8 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;
        private const int ID_QUESTION = 3;
        private string answer;

        public Form8 (RealSenseImageStream imageStream, FileManager fileManager)
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

            Form9 form9 = new Form9(imageStream, fileManager);
            form9.Show();
            this.Visible = false;
        }
    }
}
