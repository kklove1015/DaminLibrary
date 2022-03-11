using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DaminLibrary.Expansion
{
    public static class ItemCollectionExpansion
    {
        public static void AddRange(this ItemCollection itemCollection, params object[] objectArray)
        {
            List<object> objectList = objectArray.ToList();
            objectList.ForEach(x => { itemCollection.Add(x); });
        }
    }
}
