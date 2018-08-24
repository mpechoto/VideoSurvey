using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Close Application at all
            System.Windows.Forms.Application.Exit();
        }
               
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


            string folder = @"Record_" + textBox1.Text;
            Directory.CreateDirectory(folder);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

     
    }
}
