﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSurvey
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Creating a SDK session

            //PXCMSession session = PXCMSession.CreateInstance();
            //PXCMSession session = PXCMSession.CreateInstance();
            //PXCMSession.ImplVersion version = session.QueryVersion();
            //Console.WriteLine(version.major.ToString() + "." + version.minor.ToString());

            Application.Run(new Form1());

            /*if (session != null)
            {                
                Application.Run(new Form1(session));
                session.Dispose();
            }*/
        }
    }
}
