using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ExtensionTest
{
    public class IGExtension
    {
        public void init(string mode)
        {
            MessageBox.Show("works", mode);
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
