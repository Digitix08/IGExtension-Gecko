using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MazillaFirafox
{
    public class IGExtension
    {
        //private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

        private static uint LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100;

        private static void LoadWin32Library(string libPath)
        {
            //IntPtr moduleHandle = LoadLibraryEx(libPath, IntPtr.Zero, LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR);
            //Console.WriteLine(moduleHandle);
        }

        public void init(string mode)
        {
            //LoadWin32Library("Geckofx - Core.dll");
            //LoadWin32Library("Geckofx - Winforms.dll");
            Form1 form1 = new Form1();
            form1.SetHome(mode);
            form1.Show();
        }
    }
}
