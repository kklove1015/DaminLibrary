using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DaminLibrary.Expansion
{
    public static class DispatcherExpansion
    {
        public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action action)
        {
            DispatcherOperation beginInvoke = dispatcher.BeginInvoke((Action)(() =>
            {
                action();
            }), DispatcherPriority.Normal, null);
            return beginInvoke;
        }
        public static DispatcherOperation BeginInvoke(this DispatcherObject dispatcherObject, Action action)
        {
            if (dispatcherObject.IsNull()) { return null; }
            if (dispatcherObject.Dispatcher.IsNull()) { return null; }
            return dispatcherObject.Dispatcher.BeginInvoke(action, DispatcherPriority.Normal, null);
        }
    }
}
