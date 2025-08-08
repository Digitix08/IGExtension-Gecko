using Gecko;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MazillaFirafox
{
    public partial class Form1 : Form
    {
        string home = "google.com";
        FFHistory hist;
        internal void SetHome(string mode)
        {
            home = mode;
        }

        public Form1()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox64");
            hist = new FFHistory(this);
        }

        public void GoToUrL(string URL)
        {
            geckoWebBrowser1.Navigate(URL);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate(home);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.GoBack();
        }

        private void GoHome_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate(home);
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Refresh();
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.GoForward();
        }

        private void GoTo_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate(GoToUrl.Text);
        }

        private void Picture_invert(object sender, MouseEventArgs e)
        {
            Bitmap pic = new Bitmap(((PictureBox)sender).Image);
            for (int y = 0; (y <= (pic.Height - 1)); y++)
            {
                for (int x = 0; (x <= (pic.Width - 1)); x++)
                {
                    Color inv = pic.GetPixel(x, y);
                    inv = Color.FromArgb(inv.A, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(x, y, inv);
                }
            }
            ((PictureBox)sender).Image = pic;
        }

        private void geckoWebBrowser1_ProgressChanged(object sender, GeckoProgressEventArgs e)
        {
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0 && e.CurrentProgress <= e.MaximumProgress)
            {
                long progress = 100 / (e.MaximumProgress / e.CurrentProgress);
                toolStripProgressBar1.Value = System.Convert.ToInt32(progress);
                CurrentUrl.Text = System.Convert.ToString(geckoWebBrowser1.Url);
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hist.Show();
        }

        private void geckoWebBrowser1_HistoryNewEntry(object sender, GeckoHistoryEventArgs e)
        {
            hist.add(e.Url.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hist.Dispose();
        }
    }
}
