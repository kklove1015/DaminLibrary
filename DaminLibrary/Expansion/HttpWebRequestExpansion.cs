using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class HttpWebRequestExpansion
    {
        public static void Write(this HttpWebRequest httpWebRequest, byte[] byteArray)
        {
            using (Stream getRequestStream = httpWebRequest.GetRequestStream())
            {
                getRequestStream.Write(byteArray, 0, byteArray.Length);
                getRequestStream.Close();
            }
        }
        public static void Write(this HttpWebRequest httpWebRequest, string text, Encoding encoding)
        {
            byte[] getBytes = encoding.GetBytes(text);
            httpWebRequest.Write(getBytes);
        }
        public static void Write(this HttpWebRequest httpWebRequest, string text)
        {
            httpWebRequest.Write(text, Encoding.Default);
        }
        public static string ReadToEnd(this HttpWebRequest httpWebRequest, Encoding encoding)
        {
            WebResponse getResponse = null;
            try { getResponse = httpWebRequest.GetResponse(); }
            //catch (Exception exception) { exception.WriteLine(); return null; }
            catch { return null; }
            var httpWebResponse = getResponse as HttpWebResponse;
            Stream getResponseStream = httpWebResponse.GetResponseStream();
            var streamReader = new StreamReader(getResponseStream, encoding);
            string readToEnd = streamReader.ReadToEnd();
            streamReader.Dispose();
            getResponseStream.Dispose();
            httpWebResponse.Dispose();
            getResponse.Dispose();
            return readToEnd;
        }
        public static T ReadToEnd<T>(this HttpWebRequest httpWebRequest, Encoding encoding = null)
        {
            if (encoding.IsNull() == true) { encoding = Encoding.Default; }
            var readToEnd = httpWebRequest.ReadToEnd(encoding);
            return readToEnd.ToClass<T>();
        }
    }
}
