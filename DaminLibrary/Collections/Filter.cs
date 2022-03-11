using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Collections
{
    public class Filter
    {
        public string field;
        public string value;
        public List<string> valueList;
        public Filter(string field, string value) 
        {
            this.field = field;
            this.value = value;
        }
        public Filter(string field, List<string> valueList) 
        {
            this.field = field;
            this.valueList = valueList;
        }
    }
}
