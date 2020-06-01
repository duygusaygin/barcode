using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  System.IO;
using  System.Drawing.Imaging;
using System.Drawing.Printing;
using QRCoder;
using BarcodeLib;
using ZXing;

namespace Barcode
{
    public partial class GenerateBarcode : Form
    {
        public GenerateBarcode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox1.Image = barcode.Draw(textBox1.Text,50);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
         
            pictureBox1.Image = qrcode.Draw(textBox2.Text, 50);
            
        }

        private void button3_Click(object sender, EventArgs e) => printBarCode();

        private void printBarCode()
        {
            PrintDialog pd=new PrintDialog();
            PrintDocument doc=new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            pd.Document = doc;
            if (pd.ShowDialog()==DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
          Bitmap bm=new Bitmap(pictureBox1.Width, pictureBox1.Height);
          float scale= Math.Min(300 / bm.Width,300/ bm.Height);
          var scaleWidth = (int)(bm.Width * scale);
          var scaleHeight = (int)(bm.Height * scale);
            //pictureBox1.DrawToBitmap(bm, new Rectangle(50 * pictureBox1.Width / pictureBox1.Height, 50 * pictureBox1.Height / pictureBox1.Width, ((int)150 - scaleWidth) / 2, ((int)150 - scaleHeight) / 2));
            pictureBox1.DrawToBitmap(bm, new Rectangle(30 * pictureBox1.Width / pictureBox1.Height, 30 * pictureBox1.Height / pictureBox1.Width, pictureBox1.Width, pictureBox1.Height));
            pictureBox1.DrawToBitmap(bm, new Rectangle(30 * pictureBox1.Width / pictureBox1.Height, 30 * pictureBox1.Height / pictureBox1.Width, pictureBox1.Width, pictureBox1.Height));
            pictureBox1.DrawToBitmap(bm, new Rectangle(30 * pictureBox1.Width / pictureBox1.Height, 30 * pictureBox1.Height / pictureBox1.Width, pictureBox1.Width, pictureBox1.Height));
            e.Graphics.DrawImage(bm,0,0);
          bm.Dispose();
        }

        private void GenerateBarcode_Load(object sender, EventArgs e)
        {
 //dncjsd
        }

        private void button4_Click(object sender, EventArgs e)
        {
            QRCoder.QRCodeGenerator qrCodeGenerator=new QRCodeGenerator();
            var qrData = qrCodeGenerator.CreateQrCode(textBox2.Text, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qrCode=new QRCoder.QRCode(qrData);
            var image = qrCode.GetGraphic(2);
            //pictureBox1.Image = image;
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(Image.FromFile(@"C:\pencere.bmp"),110,5, Image.FromFile(@"C:\pencere.bmp").Width, Image.FromFile(@"C:\pencere.bmp").Height);
            g.DrawImage(image, 0, 0, image.Width , image.Height);
            using (Font myFont = new Font("Arial", 14))
            {
                g.DrawString("Hello!", myFont, Brushes.Green, new Point(20, 130));
            }
            //Pen pen = new Pen(Color.Black, 1);
            //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //g.DrawRectangle(pen, 0, 0, pictureBox1.Width, pictureBox1.Height);

            pictureBox1.Image = b;

            //QRCoder.QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            //var qrData = qrCodeGenerator.CreateQrCode(textBox2.Text, QRCoder.QRCodeGenerator.ECCLevel.Q);
            //var qrCode = new QRCoder.QRCode(qrData);
            //var image = qrCode.GetGraphic(1);
            //pictureBox1.Image = image;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            QRCoder.QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            var qrData = qrCodeGenerator.CreateQrCode(textBox2.Text, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.QRCode(qrData);
            var image = qrCode.GetGraphic(2);
            pictureBox1.Image = image;
        }

       
    }
}
