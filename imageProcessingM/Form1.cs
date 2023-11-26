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

namespace imageProcessingM
{
    public partial class Form1 : Form
    {
        Bitmap loaded;
        Bitmap processed;
        Bitmap imageA, imageB, colorGreen, resultImage;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {   
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }
        

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Image Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif|All Files|*.*";
            saveFileDialog1.ShowDialog();
            pictureBox2.Image.Save(saveFileDialog1.FileName);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            processed = new Bitmap(loaded.Width,loaded.Height);

            for(int i = 0; i < loaded.Width; i++)
                for(int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    processed.SetPixel(i, j, pixel);
                }
            pictureBox2.Image = processed;
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            int gray;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    gray = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    processed.SetPixel(i, j, Color.FromArgb(gray,gray,gray));
                }
            pictureBox2.Image = processed;
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    processed.SetPixel((loaded.Width-1)-i, j,pixel);
                }
            pictureBox2.Image = processed;
        }

        private void yToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    processed.SetPixel( i, (loaded.Height - 1) - j, pixel);
                }
            pictureBox2.Image = processed;
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    processed.SetPixel(i, j, Color.FromArgb(255-pixel.R, 255-pixel.G, 255-pixel.B));
                }
            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            int gray;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    gray = ((pixel.R + pixel.G + pixel.B) / 3);
                    processed.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            Color sample;
            int[] hisdata = new int[256];
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    sample = processed.GetPixel(i, j);
                    hisdata[sample.R]++;
                }
            }
            Bitmap mydata = new Bitmap(265, 800);
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 800; j++)
                {
                    mydata.SetPixel(i, j, Color.White);
                }
            }

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < Math.Min(hisdata[i] / 5, 800); j++)
                {
                    mydata.SetPixel(i, j, Color.Black);
                }
            }

            pictureBox2.Image = mydata;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);
                    int sepiaR = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int sepiaG = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int sepiaB = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    sepiaR = Math.Max(0, Math.Min(sepiaR, 255));
                    sepiaG = Math.Max(0, Math.Min(sepiaG, 255));
                    sepiaB = Math.Max(0, Math.Min(sepiaB, 255));

                    processed.SetPixel(i, j, Color.FromArgb(sepiaR, sepiaG, sepiaB));
                }
            }

            pictureBox2.Image = processed;
        }

        private void openFileDialog2_FileOk_1(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog2.FileName);
            pictureBox1.Image = imageA;
        }

        private void openFileDialog3_FileOk_1(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog3.FileName);
            pictureBox2.Image = imageB;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resultImage = new Bitmap(imageB.Width, imageB.Height);
            Color mygreen = Color.FromArgb(0, 255, 0);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;
            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageB.Height; y++)
                {
                    Color front = imageA.GetPixel(x, y);
                    Color back = imageB.GetPixel(x, y);
                    int grey = (front.R + front.G + front.B) / 3;
                    int subtract = Math.Abs(grey - greygreen);
                    if (subtract < threshold)
                        resultImage.SetPixel(x, y, back);
                    else
                        resultImage.SetPixel(x, y, front);
                }
            }
            pictureBox3.Image = resultImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog3.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog3.ShowDialog();
        }
    }
}
