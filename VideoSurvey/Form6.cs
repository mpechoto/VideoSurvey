﻿using System;
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
    public partial class Form6 : Form
    {
        RealSenseImageStream imageStream;

        public Form6(RealSenseImageStream imageStream)
        {
            InitializeComponent();
            this.imageStream = imageStream;
            
            Timer(6);
        }


        public void Timer(int time)
        {
            System.Windows.Forms.Timer clock = new System.Windows.Forms.Timer();
            clock.Interval = 1000;

            clock.Tick += delegate
            {
                time -= 1;
                label2.Text = time.ToString();

                if (time == 0)
                {
                    clock.Stop();
                    imageStream.StopStream(); //Stop Threading
                    //Close Application at all
                    System.Windows.Forms.Application.Exit();
                }
            };
            clock.Start();

        }
    }
}
