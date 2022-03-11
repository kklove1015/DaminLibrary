using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class CefExpansion
    {
        public static bool Initialize()
        {
            if (Cef.IsInitialized == true) { return false; }
            var cefSettings = new CefSettings
            {
                Locale = "ko",
                LogSeverity = LogSeverity.Disable,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.7113.93 Safari/537.36"
            };
            bool initialize = Cef.Initialize(cefSettings);
            return initialize;
        }
    }
}
