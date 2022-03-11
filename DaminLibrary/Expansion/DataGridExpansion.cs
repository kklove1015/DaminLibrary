using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace DaminLibrary.Expansion
{
    public static class DataGridExpansion
    {
        public static void SetColumns(this DataGrid dataGrid, List<Collections.Property> propertyList)
        {
            for (int i = 0; i <= propertyList.Count - 1; i++)
            {
                var dataGridTextColumn = new DataGridTextColumn();
                dataGridTextColumn.Header = propertyList[i].names.translationName;
                var dataGridTextColumnBinding = new Binding(propertyList[i].names.originalName);
                dataGridTextColumn.Binding = dataGridTextColumnBinding;
                dataGrid.Columns.Add(dataGridTextColumn);
            }
        }
    }
}
