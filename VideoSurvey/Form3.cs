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
    public partial class Form3 : Form
    {
        

        public Form3()
        {
            InitializeComponent();
            Timer(6);
        }

        public void Timer(int time)
        {
            Timer clock = new Timer();
            clock.Interval = 1000;

            clock.Tick += delegate
            {
                time -= 1;
                label2.Text = time.ToString();
                                
                if (time == 0)
                {
                    clock.Stop();
                    Form5 form5 = new Form5();
                    form5.Show();
                    this.Visible = false;
                }
            };
            clock.Start();
            
        }
    }
}
