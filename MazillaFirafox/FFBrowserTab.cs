using Gecko;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazillaFirafox
{
    public partial class FFBrowserTab : UserControl
    {
        string home = "google.com";
        string defaultsearch = "http://www.google.com/search?q=";
        FFHistory hist;
        internal void SetHome(string mode)
        {
            home = mode;
        }

        public FFBrowserTab()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox64");
            hist = new FFHistory();
            hist.AttachToUControl(this);
        }

        private void FFBrowserTab_Load(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate(home);
            aboutToolStripMenuItem.Text += getName() + "...";
        }

        //history
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

        public void ClearHistory()
        {
            geckoWebBrowser1.History.Clear();
        }

        public void GoToUrL(string URL)
        {
            geckoWebBrowser1.Navigate(URL);
        }

        private void UriChanged(string text)
        {
            if (text.Length > 0)
            {
                CurrentUrl.Text = text;
                if (!GoToUrl.Focused)
                {
                    GoToUrl.Text = text;
                }
            }
        }

        private string getName()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != "")
                {
                    return titleAttribute.Title;
                }
            }
            return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }

        private void geckoWebBrowser1_ProgressChanged(object sender, GeckoProgressEventArgs e)
        {
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0 && e.CurrentProgress < e.MaximumProgress)
            {
                long progress = 100 / (e.MaximumProgress / e.CurrentProgress);
                if (progress >= 1) toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripProgressBar1.Value = System.Convert.ToInt32(progress);
                UriChanged(Convert.ToString(geckoWebBrowser1.Url));
                status.Text = "Downloading...";
            }
        }

        private void geckoWebBrowser1_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            if (toolStripProgressBar1.Style != ProgressBarStyle.Blocks)
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripProgressBar1.Value = 100;
            }
            int oldstep = toolStripProgressBar1.Step;
            int step = oldstep;
            step = toolStripProgressBar1.Maximum - toolStripProgressBar1.Value;
            toolStripProgressBar1.Step = step;
            toolStripProgressBar1.PerformStep();
            toolStripProgressBar1.Step = oldstep;
            Reload.Image = Properties.Resources.arrow_reload;
            status.Text = "Done";
        }

        private void Back_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.GoBack();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate(home);
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Reload();
        }

        private void Forward_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.GoForward();
        }

        private void GoTo_Click(object sender, EventArgs e) => webNavigate(GoToUrl.Text);

        private void Search_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate(defaultsearch + GoToUrl.Text);
        }

        private async void CurrentUrl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CurrentUrl.Text);
            CurrentUrl.Text = "Copied to clipboard";
            await Task.Delay(500);
            CurrentUrl.Text = System.Convert.ToString(geckoWebBrowser1.Url);
        }

        private void geckoWebBrowser1_Navigated(object sender, Gecko.GeckoNavigatedEventArgs e)
        {
            string title = String.Concat(geckoWebBrowser1.Url.ToString());
            if (Convert.ToString(geckoWebBrowser1.Url).IndexOf("\\") == -1)
                if (geckoWebBrowser1.Document.Title.Length > 0) title = geckoWebBrowser1.Document.Title;
            UriChanged(Convert.ToString(geckoWebBrowser1.Url));
            this.Text = title;
            if (!geckoWebBrowser1.CanGoBack) { Back.Image = Properties.Resources.arrow_back_disabled; Back.Enabled = false; }
            else { Back.Image = Properties.Resources.arrow_back; Back.Enabled = true; }
            if (!geckoWebBrowser1.CanGoForward) { Forward.Image = Properties.Resources.arrow_forward_disabled; Forward.Enabled = false; }
            else { Forward.Image = Properties.Resources.arrow_forward; Forward.Enabled = true; }
            status.Text = "Downloaded";
        }

        private void GoToUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                webNavigate(GoToUrl.Text);
            }
        }

        private void webNavigate(string text)
        {
            if (text.Length > 0)
            {
                geckoWebBrowser1.Navigate(GoToUrl.Text);
            }
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

        /*private void ScrErr_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Remove(geckoWebBrowser1);
            tableLayoutPanel1.Controls.Add(split, 0, 1);
            split.Panel1.Controls.Add(geckoWebBrowser1);
            tableLayoutPanel1.SetColumnSpan(split, 10);
        }

        private void ScrErrClose_Click(object sender, EventArgs e)
        {
            split.Panel1.Controls.Remove(geckoWebBrowser1);
            tableLayoutPanel1.Controls.Remove(split);
            tableLayoutPanel1.Controls.Add(geckoWebBrowser1, 0, 1);
        }*/

        private void geckoWebBrowser1_Navigating(object sender, Gecko.Events.GeckoNavigatingEventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.Value = 50;
            status.Text = "Searching for host...";
            Reload.Image = Properties.Resources.cancel;
            Reload.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Stop();
            Reload.Image = Properties.Resources.arrow_reload;
            Reload.Click += Reload_Click;

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

        private void More_Click(object sender, EventArgs e)
        {
            PictureBox btnSender = (PictureBox)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            MoreContextMenuStrip.Show(ptLowerLeft);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.Show();
        }
    }
}
