using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DaminLibrary.Expansion
{
    public static class ComboBoxExpansion
    {
        public static void Initialize(this ComboBox comboBox)
        {
            comboBox.SelectedIndex = 0;
        }
        public static void Initialize(this ComboBox comboBox, params object[] objectArray)
        {
            comboBox.Items.Clear();
            comboBox.Items.AddRange(objectArray);
            comboBox.Initialize();
        }
        public static void Initialize(this ComboBox comboBox, List<string> stringList)
        {
            string[] stringArray = stringList.ToArray();
            comboBox.Initialize(stringArray);
        }
        public static void Initialize(this ComboBox comboBox, Type type)
        {
            string[] getNames = Enum.GetNames(type);
            comboBox.Initialize(getNames);
        }
        public static string GetSelectedItemString(this ComboBox comboBox)
        {
            string comboBoxSelectedItemString = comboBox.SelectedItem.ToString();
            return comboBoxSelectedItemString;
        }
        public static T GetSelectedItemValue<T>(this ComboBox comboBox)
        {
            string getSelectedItemString = comboBox.GetSelectedItemString();
            Type type = typeof(T);
            T parseSelectedItemValue = (T)Enum.Parse(type, getSelectedItemString);
            return parseSelectedItemValue;
        }
        public static void SetSelectedItem(this ComboBox comboBox, object selectedItem)
        {
            comboBox.SelectedItem = selectedItem;
        }

        public static void Enable(this ComboBox comboBox, bool isEnabled)
        {
            comboBox.IsEnabled = isEnabled;
        }

        public static Int32 GetSelectedItemInt32(this ComboBox comboBox)
        {
            string getSelectedItemString = comboBox.GetSelectedItemString();
            int getSelectedItemInt32 = getSelectedItemString.ToInt32();
            return getSelectedItemInt32;
        }

    }
}
