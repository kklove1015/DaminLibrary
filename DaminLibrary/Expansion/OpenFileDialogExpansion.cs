using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class OpenFileDialogExpansion
    {
        public static string GetFileName(string title, string filter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.Filter = filter;
            string getFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            openFileDialog.InitialDirectory = getFolderPath;
            bool? showDialog = openFileDialog.ShowDialog();
            if (showDialog != true) { return null; }

            return openFileDialog.FileName;
        }
    }
}
