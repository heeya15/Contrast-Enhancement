using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        Image image;

        Bitmap colorBitmap;
        Bitmap grayBitmap;

        int[,] grayArray;

        int newWidth, newHeight;

        public Form1()
        {
            InitializeComponent();
        }

        // click open button
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load Image...";
            ofd.Filter = "All Files(*.*) |*.*| Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = ofd.FileName;
                image = Image.FromFile(filename);

                colorBitmap = new Bitmap(image);
                grayBitmap = new Bitmap(image);

                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = Image.FromFile(filename);

                label4.Text = image.Width + " X " + image.Height + " px";

                textBox1.Text = "" + image.Width;
                textBox2.Text = "" + image.Height;

                copy2darray();
            }
        }

        // convert gray image.
        private void copy2darray()
        {
            int x, y, brightness;
            Color color;
            grayArray = new int[image.Height, image.Width];

            for (y = 0; y < image.Height; y++)
            {
                for (x = 0; x < image.Width; x++)
                {
                    color = colorBitmap.GetPixel(x, y);
                    brightness = (int)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B);
                    grayArray[y, x] = brightness;
                }
            }

            displayImage();
        }

        // click the resize button.
        private void button8_Click(object sender, EventArgs e)
        {
            //enter key is down
            newWidth = Int32.Parse(textBox1.Text);
            newHeight = (image.Height * newWidth) / image.Width;

            textBox2.Text = "" + newHeight;

            Size size = new Size(newWidth, newHeight);
            grayBitmap = new Bitmap(grayBitmap, size);

            pictureBox1.Image = grayBitmap;

            label4.Text = newWidth + " X " + newHeight + " px";
        }

        private void displayImage()
        {
            int x, y;
            Color color;

            for (y = 0; y < image.Height; y++)
            {
                for (x = 0; x < image.Width; x++)
                {
                    color = Color.FromArgb(grayArray[y, x], grayArray[y, x], grayArray[y, x]);
                    grayBitmap.SetPixel(x, y, color);
                }
            }
            // grayBitmap 객체를 화면에 그린다.
            pictureBox1.Image = grayBitmap;

        }


    }
}
