using DaminLibrary.Collections;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DaminLibrary.Expansion
{
    public static class StringExpansion
    {
        #region Null Check
        public static bool IsNullOrWhiteSpace(this string text) 
        {
            bool isNullOrWhiteSpace = string.IsNullOrWhiteSpace(text);
            return isNullOrWhiteSpace;
        }
        #endregion

        #region Split
        public static List<string> Split(this string text, int length) 
        {
            string[] split = text.Split(' ');
            List<string> stringList = new List<string>();
            for (int i = 0; i < split.Length; i++)
            {
                string trim = split[i].Trim();
                var a = stringList.Join(" ");
            }
            return null;
        }
        public static List<string> Split(this string text, char separator)
        {
            string[] split = text.Split(separator);
            List<string> stringList = split.ToList();
            return stringList;
        }

        public static List<string> Split(this string text, string separator) 
        {
            if (text.IsNullOrWhiteSpace() == true) { return null; }
            string[] split = text.Split(new string[] { separator}, StringSplitOptions.None);
            var splitList = split.ToList();
            return splitList;
        }
        #endregion

        #region Encode
        public static string ToUrlEncode(this string address)
        {
            var urlDecode = WebUtility.UrlEncode(address);
            return urlDecode;
        }
        public static string ToBase64Encoding(this string text, Encoding encoding = null)
        {
            string result = null;
            try
            {
                if (encoding.IsNull() == true) { encoding = Encoding.UTF8; }
                byte[] getBytes = encoding.GetBytes(text);
                string toBase64String = Convert.ToBase64String(getBytes);
                result = toBase64String;
            }
            catch { }
            return result;
        }
        #endregion

        #region Decode
        public static string ToHtmlDecode(this string address)
        {
            string htmlDecode = WebUtility.HtmlDecode(address);
            return htmlDecode;
        }
        public static string ToUrlDecode(this string address)
        {
            var urlDecode = WebUtility.UrlDecode(address);
            return urlDecode;
        }
        public static string ToUrlDecode(this string address, int count)
        {
            for (int i = 0; i <= count - 1; i++)
            {
                address = address.ToUrlDecode();
            }
            return address;
        }
        public static string ToBase64Decoding(this string text, Encoding encoding = null)
        {
            string result = null;
            try
            {
                if (encoding.IsNull() == true) { encoding = Encoding.UTF8; }
                byte[] toBase64String = Convert.FromBase64String(text);
                string getString = encoding.GetString(toBase64String);
                result = getString;
            }
            catch { }
            return result;
        }
        #endregion

        #region SubString
        public static string Push(this string text, string word)
        {
            if (text.IsNullOrWhiteSpace() == true) { return null; }
            if (word.IsNull() == true) { return null; }
            if (text.Contains(word) == false) { return null; }
            if (text.Contains(word) == false) { return null; }
            int indexOf = text.IndexOf(word);
            int startIndex = indexOf + word.Length;
            string substring = text.Substring(startIndex);
            return substring;
        }
        public static string Cut(this string text, string word)
        {
            if (text.IsNullOrWhiteSpace() == true) { return null; }
            if (word.IsNull() == true) { return null; }
            int indexOfText = text.IndexOf(word);
            if (indexOfText == -1) { return null; }

            string substringText = text.Substring(0, indexOfText);
            return substringText;
        }
        public static string Cut(this string text, string startWord, string endWord)
        {
            string push = text.Push(startWord);
            string cut = push.Cut(endWord);
            return cut;
        }
        public static string Truncate(this string text, string startWord, string endWord)
        {
            string push = text.Push(startWord);
            string cut = push.Cut(endWord);
            return cut;
        }
        public static string Truncate(this string text, string startWord, string endWord, Includes include = Includes.none)
        {
            string push = text.Push(startWord);
            if (push.IsNullOrWhiteSpace() == true) { return null; }

            string cut = push.Cut(endWord);
            if (cut.IsNullOrWhiteSpace() == true) { return null; }

            switch (include)
            {
                case Includes.none:
                    return cut;
                case Includes.start:
                    return startWord + cut;
                case Includes.end:
                    return cut + endWord;
                case Includes.all:
                    return startWord + cut + endWord;
                default:
                    return null;
            }
        }
        public static List<string> TrunscateList(this string text, string startWord, string endWord, Includes include = Includes.none)
        {
            List<string> result = null;
            var truscateList = new List<string>();
            try
            {
                if (text.IsNullOrWhiteSpace() == true) { throw new Exception(); }
                if (startWord.IsNullOrWhiteSpace() == true) { throw new Exception(); }
                if (endWord.IsNullOrWhiteSpace() == true) { throw new Exception(); }

                while (true)
                {
                    if (text.Contains(startWord) != true) { break; }
                    string push = text.Push(startWord);
                    if (push.Contains(endWord) != true) { break; }
                    string cut = push.Cut(endWord);
                    if (cut.IsNullOrWhiteSpace() == true) { break; }

                    switch (include)
                    {
                        case Includes.none:
                            truscateList.Add(cut);
                            text = push;
                            break;
                        case Includes.start:
                            truscateList.Add(startWord + cut);
                            text = push;
                            break;
                        case Includes.end:
                            truscateList.Add(cut + endWord);
                            text = push;
                            break;
                        case Includes.all:
                            truscateList.Add(startWord + cut + endWord);
                            text = push;
                            break;
                    }
                }
                result = truscateList;
            }
            catch { }
            return result;

        }
        #endregion

        #region DirectoryInfo
        public static DirectoryInfo GetDirectoryInfo(this string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo;
        }
        public static DirectoryInfo CreateDirectory(this string path)
        {
            bool exists = Directory.Exists(path);
            switch (exists)
            {
                case true:
                    DirectoryInfo getDirectoryInfo = path.GetDirectoryInfo();
                    return getDirectoryInfo;
                case false:
                    DirectoryInfo createDirectory = Directory.CreateDirectory(path);
                    return createDirectory;
                default:
                    return null;
            }
        }
        #endregion

        #region Html
        public static List<HtmlNode> GetHtmlNodeList(this string html, string xpath)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNodeCollection selectNodes = htmlDocument.DocumentNode.SelectNodes(xpath);
            if (selectNodes.IsNull() == true) { return null; }

            IEnumerable<HtmlNode> cast = selectNodes.Cast<HtmlNode>();
            List<HtmlNode> list = cast.ToList();
            return list;
        }

        public static List<string> GetHtmlAttributeValueList(this string html, string xpath, XPaths xPaths)
        {
            List<HtmlNode> getHtmlNodeList = html.GetHtmlNodeList(xpath);
            if (getHtmlNodeList.IsNullOrEmpty() == true) { return null; }

            IEnumerable<string> select = null;
            switch (xPaths)
            {
                case XPaths.InnerHtml:
                    select = getHtmlNodeList.Select(x => x.InnerHtml);
                    break;
                case XPaths.OuterHtml:
                    select = getHtmlNodeList.Select(x => x.OuterHtml);
                    break;
                case XPaths.InnerText:
                    select = getHtmlNodeList.Select(x => x.InnerText);
                    break;
            }
            List<string> list = select.ToList();
            return list;
        }

        public static string GetHtmlAttributeValue(this string html, string xPath, XPaths xPaths)
        {
            List<string> getHtmlAttributeValueList = html.GetHtmlAttributeValueList(xPath, xPaths);
            if (getHtmlAttributeValueList.IsNullOrEmpty() == true) { return null; }

            string first = getHtmlAttributeValueList.First();
            return first;
        }

        public static string GetHtmlNodeAttributeValue(this string text, string xPath, string value)
        {
            string result = null;
            try
            {
                List<HtmlNode> htmlNodeList = text.GetHtmlNodeList(xPath);
                if (htmlNodeList.IsNullOrEmpty() == true) { return null; }
                HtmlNode htmlNodeFirst = htmlNodeList.First();
                IEnumerable<HtmlAttribute> htmlNodeAttributeIEnumerable = htmlNodeFirst.Attributes.Cast<HtmlAttribute>();
                List<HtmlAttribute> htmlNodeAttributeList = htmlNodeAttributeIEnumerable.ToList();
                HtmlAttribute htmlAttribute = htmlNodeAttributeList.Find(x => x.Name == value);
                if (htmlAttribute.IsNull()) { return null; }
                result = htmlAttribute.Value;
            }
            catch { }
            return result;
        }

        public static string GetHtmlNodeAttributeValue(this string text, string xPath, HtmlType htmlType)
        {
            string result = null;
            try
            {
                List<HtmlNode> htmlNodeList = text.GetHtmlNodeList(xPath);
                HtmlNode htmlNodeFirst = htmlNodeList.First();
                switch (htmlType)
                {
                    case HtmlType.InnerText:
                        result = htmlNodeFirst.InnerText;
                        break;
                    case HtmlType.InnerHtml:
                        result = htmlNodeFirst.InnerHtml;
                        break;
                    case HtmlType.OuterHtml:
                        result = htmlNodeFirst.OuterHtml;
                        break;
                    default: throw new Exception();
                }
            }
            catch { }
            return result;
        }

        public static List<string> GetHtmlNodeAttributeValueList(this string text, string xPath, string value)
        {
            List<string> result = new List<string>();
            try
            {
                List<HtmlNode> htmlNodeList = text.GetHtmlNodeList(xPath);
                if (htmlNodeList.IsNullOrEmpty() == true) { throw new Exception(); }
                for (int i = 0; i <= htmlNodeList.Count - 1; i++)
                {
                    try
                    {
                        IEnumerable<HtmlAttribute> htmlAttributeIEnumerable = htmlNodeList[i].Attributes.Cast<HtmlAttribute>();
                        List<HtmlAttribute> htmlAttributeList = htmlAttributeIEnumerable.ToList();
                        HtmlAttribute htmlAttribute = htmlAttributeList.Find(x => x.Name == value);
                        if (htmlAttribute.IsNull() == true) { throw new Exception(); }
                        result.Add(htmlAttribute.Value);
                    }
                    catch { }
                }
            }
            catch { }
            return result;
        }

        public static List<string> GetHtmlNodeAttributeValueList(this string text, string xPath, HtmlType htmlType)
        {
            List<string> result = new List<string>();
            try
            {
                List<HtmlNode> htmlNodeList = text.GetHtmlNodeList(xPath);
                if (htmlNodeList.IsNullOrEmpty() == true) { return null; }
                for (int i = 0; i <= htmlNodeList.Count - 1; i++)
                {
                    try
                    {
                        switch (htmlType)
                        {
                            case HtmlType.InnerText:
                                result.Add(htmlNodeList[i].InnerText.Trim());
                                break;
                            case HtmlType.InnerHtml:
                                result.Add(htmlNodeList[i].InnerHtml.Trim());
                                break;
                            case HtmlType.OuterHtml:
                                result.Add(htmlNodeList[i].OuterHtml.Trim());
                                break;
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return result;
        }

        #endregion

        #region Convert
        public static decimal ToPrice(this string text)
        {
            List<string> split = text.Split("-");
            string last = split.Last();
            var wordList = new List<char>();
            for (int i = last.Length - 1; i >= 0; i--)
            {
                switch (last[i].IsNumber() == true)
                {
                    case true:
                        wordList.Insert(0, last[i]);
                        break;
                    case false:
                        if ((last[i] == ',' || last[i] == '.') && (wordList.Count == 0 || wordList.Count == 1 || wordList.Count == 2))
                        {
                            wordList.Insert(0, '.');
                        }
                        break;
                }
            }
            string join = wordList.Join();
            decimal _decimal = join.ToDecimal();
            return _decimal;
        }
        public static string ToUrlHexEscape(this string text)
        {
            char[] testArray = { '!', '(', ')', '*', '-', '.', '_' };
            foreach (var value in testArray)
            {
                if (text.Contains(value) == true)
                {
                    string hexEscape = Uri.HexEscape(value);
                    text = text.Replace(value.ToString(), hexEscape);
                }
            }
            return text;
        }
        public static T ToEnum<T>(this string value)
        {
            Type type = typeof(T);
            object parse = Enum.Parse(type, value);
            T t = (T)parse;
            return t;
        }
        public static int ToInt32(this string text)
        {
            int textInt32 = Convert.ToInt32(text);
            return textInt32;
        }
        public static double ToDouble(this string text)
        {
            double textToDouble = Convert.ToDouble(text);
            return textToDouble;
        }
        public static decimal ToDecimal(this string text)
        {
            decimal textToDecimal = Convert.ToDecimal(text);
            return textToDecimal;
        }
        public static T ToClass<T>(this string json)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            jsonSerializerSettings.Error = (se, ev) => { ev.ErrorContext.Handled = true; };
            T deserializeObject = json.DeserializeObject<T>(jsonSerializerSettings);
            return deserializeObject;
        }
        public static XmlDocument ToXmlDocument(this string text) 
        {
            XmlDocument result = null;
            try
            {
                if (text.IsNullOrWhiteSpace() == true) { throw new Exception(); }
                var xml = new XmlDocument();
                xml.LoadXml(text);
                result = xml;
            }
            catch { }
            return result;
        }
       

        #endregion

        #region Json
        private static T DeserializeObject<T>(this string json, JsonSerializerSettings jsonSerializerSettings)
        {
            T deserializeObject = JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
            return deserializeObject;
        }


        #endregion

        #region Enum
        public enum Include
        {
            none,
            include
        }
        public enum Includes
        {
            none,
            start,
            end,
            all
        }
        public enum HtmlType
        {
            InnerText = 0,
            InnerHtml = 1,
            OuterHtml = 2
        }

        #endregion

        #region ETC


        public static string EncryptSHA1(this string s)
        {
            var sHA1Managed = new SHA1Managed();
            byte[] getBytes = Encoding.UTF8.GetBytes(s);
            var computeHash = sHA1Managed.ComputeHash(getBytes);
            int capacity = computeHash.Length * 2;
            var stringBuilder = new StringBuilder(capacity);
            foreach (byte _byte in computeHash)
            {
                string value = _byte.ToString("X2");
                stringBuilder.Append(value);
            }
            string _string = stringBuilder.ToString();
            sHA1Managed.Dispose();
            return _string;
        }

        public static bool IsWellFormedUriString(this string uriString, UriKind uriKind = UriKind.RelativeOrAbsolute)
        {
            bool isWellFormedUriString = Uri.IsWellFormedUriString(uriString, uriKind);
            return isWellFormedUriString;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="imageUrl">바이트로 전활할 이미지 주소</param>
        /// <param name="encoding">기본값은 UTF-8</param>
        /// <returns></returns>
        public static byte[] GetImageUrlByteArray(this string imageUrl, Encoding encoding = null)
        {
            byte[] result = null;
            if (encoding.IsNull() == true) { encoding = Encoding.UTF8; }
            try
            {
                if (imageUrl.IsNullOrWhiteSpace() == true) { throw new Exception(); }
                WebRequest webRequest = WebRequest.Create(imageUrl);
                if (webRequest.IsNull() == true) { throw new Exception(); }
                var httpWebRequest = webRequest as HttpWebRequest;
                if (httpWebRequest.IsNull() == true) { throw new Exception(); }
                using (WebResponse getResponse = httpWebRequest.GetResponse())
                {
                    try
                    {
                        if (getResponse.IsNull() == true) { throw new Exception(); }
                        using (Stream getResponseStream = getResponse.GetResponseStream())
                        {
                            try
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    try
                                    {
                                        getResponseStream.CopyTo(memoryStream);
                                        result = memoryStream.ToArray();
                                    }
                                    catch { }
                                }

                                //using (BinaryReader binaryReader = new BinaryReader(getResponseStream, encoding))
                                //{
                                //    if (binaryReader.IsNull() == true) { throw new Exception(); }
                                //    result = binaryReader.ReadBytes(10000000);
                                //}
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch (Exception exception) { exception.WriteLine(); }
            return result;
        }
        #endregion
    }
}
