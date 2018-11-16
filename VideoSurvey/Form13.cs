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
    public partial class Form13 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form13(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(imageStream, fileManager);
            form3.Show();
            this.Visible = false;
        }
    }
}
