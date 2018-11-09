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
    public partial class Form5 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Form5(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.imageStream = imageStream;
            this.fileManager = fileManager;
            
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

                    if (fileManager.Cont < fileManager.Qtde)
                    {
                        Form6 form6 = new Form6(imageStream, fileManager);
                        form6.Show();
                        this.Visible = false;

                        /*Form3 form3 = new Form3(imageStream, fileManager);
                        form3.Show();
                        this.Visible = false;*/
                    }
                    else
                    //Close Application at all
                        System.Windows.Forms.Application.Exit();
                }
            };
            clock.Start();

        }
    }
}
