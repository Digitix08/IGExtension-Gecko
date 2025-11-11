using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazillaFirafox
{
    [ComVisible(true)]
    public partial class FFHistory : Form
    {
        Form1 firafox;
        TextBox textBox1 = new TextBox();
        DateTime time = new DateTime();
        public void add(string s)
        {
            time = DateTime.Now;
            GetText(time.ToString(), s);
        }
        public void GetText(string desc, string text)
        {
            string[] row = { text };
            var listViewItem = listView1.Items.Add(desc);
            listViewItem.SubItems.AddRange(row);
        }

        public FFHistory(Form1 firaInst)
        {
            InitializeComponent();
            firafox = firaInst;
        }

        private void FFHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            firafox.ClearHistory();
        }
        private void listView1_DoubleClick(object sender, EventArgs e) => GetItem();
        private void GetItem()
        {
            firafox.GoToUrL(listView1.SelectedItems[0].SubItems[1].Text);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
