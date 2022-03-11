using DaminLibrary.Collections;
using DaminLibrary.Expansion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Advance
{
    public class UriAdvance
    {
        private readonly string uriString;
        private Uri uri;
        public Uri Uri
        {
            get
            {
                if (this.uri.IsNull() == true)
                {
                    try
                    {
                        this.uri = new Uri(this.uriString);
                    }
                    catch
                    {
                        return null;
                    }
                }
                return this.uri;
            }
        }
        private List<string> GetQueryParameterTextList()
        {
            string push = this.Uri.Query.Push("?");
            List<string> split = push.Split("&");
            return split;
        }
        private List<Parameter> GetQueryParameterList()
        {
            List<string> getQueryParameterTextList = this.GetQueryParameterTextList();
            var parameterList = new List<Parameter>();
            if (getQueryParameterTextList.IsNullOrEmpty() == false)
            {
                getQueryParameterTextList.ForEach(x =>
                {
                    List<string> split = x.Split("=");
                    string name = split.First();
                    string value = split.Last();
                    var parameter = new Parameter(name, value);
                    parameterList.Add(parameter);
                });
            }
            return parameterList;
        }

        private List<Parameter> queryParameterList;
        public List<Parameter> QueryParameterList
        {
            get
            {
                if (this.queryParameterList.IsNullOrEmpty() == true)
                {
                    this.queryParameterList = this.GetQueryParameterList();
                }
                return this.queryParameterList;
            }
        }
        public void SetQueryParameter(Parameter parameter)
        {
            Parameter find = this.QueryParameterList.Find(x => x.name == parameter.name);
            switch (find.IsNull() == true)
            {
                case true:
                    this.QueryParameterList.Add(parameter);
                    break;
                case false:
                    find.value = parameter.value;
                    break;
            }
        }
        public string GetUriString()
        {
            var queryParameterTextList = new List<string>();
            this.QueryParameterList.ForEach(x => { queryParameterTextList.Add(x.name + "=" + x.value); });
            string query = "?" + queryParameterTextList.Join("&");
            string uriString = this.Uri.Scheme + "://" + this.Uri.Host + this.Uri.LocalPath + query;
            return uriString;
        }
        public UriAdvance(string uriString)
        {
            this.uriString = uriString;
        }
    }
}
