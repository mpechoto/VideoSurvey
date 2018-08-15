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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 newMDIChild = new Form2();
            // Set the Parent Form of the Child window.  
            newMDIChild.MdiParent = this;
            // Display the new form.  
            newMDIChild.Show();
        }
    }
}
