using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Collections
{
    public class Property
    {
        public Names names;
        public Type type;
        public Property() { }
        public Property(Names names, Type type) 
        {
            this.names = names;
            this.type = type;
        }
    }
}
