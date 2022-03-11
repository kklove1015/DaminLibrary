using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Collections
{
    public class Update
    {
        public string field;
        public object value;
        public Update(string field, object value) 
        {
            this.field = field;
            this.value = value;
        }
    }
}
