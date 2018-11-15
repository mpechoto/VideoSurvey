using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoSurvey
{
    public partial class Start2 : Form
    {
        RealSenseImageStream imageStream;
        FileManager fileManager;

        public Start2(RealSenseImageStream imageStream, FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            this.imageStream = imageStream;
            button1.Enabled = false;
            label2.Text = "Aplicador: "+fileManager.User+ ", Participante: " + fileManager.Participant;            
        }
        
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            var cont = 0;
            foreach (Control control in this.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = control as CheckBox;
                    if (checkBox.Checked)
                        cont++;
                    else
                        cont--;
                }
            }
            if (cont == 2)
                button1.Enabled = true;
            else 
                button1.Enabled = false;
        }

        private void TakeScreenshot()
        {            
            // Cria os objetos necessários
            Bitmap bmpScreenshot;
            Graphics gfxScreenshot;            

            // Seta o objeto bitmap com o tamanho da tela
            // Set the bitmap object to the size of the screen
            bmpScreenshot = new Bitmap(Screen.GetWorkingArea(new Point(100, 100)).Width, Screen.GetWorkingArea(new Point(100, 100)).Height, PixelFormat.Format32bppArgb);

            // Cria um objeto graphics a partir do bitmap
            // Create a graphics object from the bitmap
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Tira o screenshot do canto superior esquerdo até o canto inferior direito
            // Take the screenshot from the upper left corner to the right bottom corner
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            // Salva o screenshot no local indicado e no formato esolhido
            // Save the screenshot to the specified path
            bmpScreenshot.Save(fileManager.CurrentPath + "\\" + fileManager.Participant + "_termo.png", ImageFormat.Png);         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TakeScreenshot();
            Start3 start3 = new Start3(imageStream, fileManager);
            start3.Show();
            this.Visible = false;

        }
    }
}
