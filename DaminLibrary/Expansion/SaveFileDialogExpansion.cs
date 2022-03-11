using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class SaveFileDialogExpansion
    {
        public static string GetFileName(string title, string filter, string fileName)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = filter;
            string getFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            saveFileDialog.InitialDirectory = getFolderPath;

            string initialDirectoryFile = getFolderPath + @"\" + saveFileDialog.FileName + ".xlsx";

            bool? showDialog = saveFileDialog.ShowDialog();
            if (File.Exists(initialDirectoryFile)) { File.Delete(initialDirectoryFile); return saveFileDialog.FileName; }

            if (showDialog != true) { return null; }
            return saveFileDialog.FileName;
        }
    }
}
