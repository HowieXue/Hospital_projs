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
using Dicom.Imaging;
using net;

namespace testDicomPrint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var printJob = new PrintJob("DICOM PRINT JOB")
                {
                    RemoteAddress = spu.RemoteAddress,
                    RemotePort = spu.RemotePort,
                    CallingAE = spu.CallingAE,
                    CalledAE = spu.CalledAE
                };

                printJob.StartFilmBox("STANDARD\\1,1", "PORTRAIT", "A4");

                printJob.FilmSession.IsColor = checkBox1.Checked; //set to true to print in color

                //greyscale
                var dicomImage = new DicomImage(textBox1.Text);

                //color
                //var dicomImage = new DicomImage(@"Data\US-RGB-8-epicard.dcm");

                var bitmap = dicomImage.RenderImage() as System.Drawing.Bitmap;

                printJob.AddImage(bitmap, 0);

                bitmap.Dispose();

                printJob.EndFilmBox();

                printJob.Print();
            }
            catch (System.Exception ex)
            {
                Helper.msgbox_info("打印失败",
                    ex.Message + Environment.NewLine + ex.StackTrace,
                    this);
            }
        }
    }
}
