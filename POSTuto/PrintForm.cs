using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace POSTuto
{
    public partial class PrintForm : Form
    {
      
        public PrintForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            pictureBox1.Visible = false;
            PrintScreen();
            printPreviewDialog1.ShowDialog();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 100, 200);
            PrintDialog pd = new PrintDialog();
            pd.Document = printDocument1;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            printDocument1.DefaultPageSettings.PaperSize.Height = 820;
            printDocument1.DefaultPageSettings.PaperSize.Width = 520;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                printPreviewDialog1.Document = printDocument1;
                result = printPreviewDialog1.ShowDialog();
                
                if (result == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;

        private void PrintScreen()
        {
            
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }

        
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

		private void printPreviewDialog1_Load(object sender, EventArgs e)
		{

		}

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void amntlbl_Click(object sender, EventArgs e)
        {

        }
    }
}
