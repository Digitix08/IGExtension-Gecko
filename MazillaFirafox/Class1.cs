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
            FFbrowser form1 = new FFbrowser();
            form1.SetHome(mode);
            form1.Show();
        }
    }

    public class IGTab
    {
        //private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

        private static void LoadWin32Library(string libPath)
        {
            //IntPtr moduleHandle = LoadLibraryEx(libPath, IntPtr.Zero, LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR);
            //Console.WriteLine(moduleHandle);
        }

        public UserControl init(string mode)
        {
            //LoadWin32Library("Geckofx - Core.dll");
            //LoadWin32Library("Geckofx - Winforms.dll");
            FFBrowserTab ftab = new FFBrowserTab();
            ftab.SetHome(mode);
            return ftab;
        }
    }
}
