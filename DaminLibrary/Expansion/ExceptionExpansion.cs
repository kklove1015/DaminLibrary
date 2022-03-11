using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Expansion
{
    public static class ExceptionExpansion
    {
        public static void WriteLine(this Exception exception)
        {
            try
            {
                if (exception.IsNull() == true) { throw new Exception(); }
                if (exception.Message == "'System.Exception' 형식의 예외가 Throw되었습니다.") { throw new Exception(); }
                if (exception.StackTrace.IsNull() == true) { throw new Exception(); }
                Console.WriteLine(exception.Message);
            }
            catch { }
        }
    }
}
