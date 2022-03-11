using DaminLibrary.Expansion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Advance
{
    public class HttpWebRequestAdvance : IDisposable
    {
        private string requestUriString;
        private HttpWebRequest httpWebRequest;
        private HttpWebRequest HttpWebRequest
        {
            get
            {
                if (this.httpWebRequest.IsNull() != true) { return this.httpWebRequest; }
                var webRequest = WebRequest.Create(this.requestUriString);
                this.httpWebRequest = webRequest as HttpWebRequest;
                return this.httpWebRequest;
            }
            set
            {
                this.httpWebRequest = value;
            }
        }
        private WebResponse webResponse;
        public WebResponse WebResponse
        {
            get
            {
                if (this.webResponse.IsNull() != true) { return this.webResponse; }
                this.webResponse = this.HttpWebRequest.GetResponse();
                return this.webResponse;
            }
        }
        public string GetResponseUriOriginalString()
        {
            string @string = this.WebResponse.ResponseUri.OriginalString;
            return @string;
        }
        public void Dispose()
        {
            this.requestUriString = null;
            this.WebResponse.Dispose();
        }

        public HttpWebRequestAdvance(string requestUriString)
        {
            this.requestUriString = requestUriString;
        }
    }
}
