using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Collections
{
    public class Names
    {
        public string originalName;
        public string translationName;
        public Names() { }
        public Names(string originalName, string translationName) 
        {
            this.originalName = originalName;
            this.translationName = translationName;
        }
    }
}
