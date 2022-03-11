using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DaminLibrary.Expansion
{
    public static class UriBuilderExpansion
    {
        public static void SetQuery(this UriBuilder uriBuilder, string key, string value)
        {
            NameValueCollection parseQueryString = HttpUtility.ParseQueryString(uriBuilder.Query);
            parseQueryString[key] = value;
            string query = parseQueryString.ToString();
            uriBuilder.Query = query;
        }
    }
}
