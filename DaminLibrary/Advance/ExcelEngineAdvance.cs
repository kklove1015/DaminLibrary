using DaminLibrary.Expansion;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaminLibrary.Advance
{
    public class ExcelEngineAdvance : IDisposable
    {
        private readonly string path;
        private ExcelEngine excelEngine;
        private ExcelEngine ExcelEngine
        {
            get
            {
                if (this.excelEngine.IsNull() == true) { this.excelEngine = new ExcelEngine(); }
                return this.excelEngine;
            }
        }
        private IWorkbook iWorkbook;
        private IWorkbook IWorkbook
        {
            get
            {
                if (this.iWorkbook.IsNull() == true)
                {
                    switch (File.Exists(this.path))
                    {
                        case true:
                            this.iWorkbook = this.ExcelEngine.Excel.Workbooks.Open(this.path, ExcelOpenType.Automatic);
                            break;
                        case false:
                            this.iWorkbook = this.ExcelEngine.Excel.Workbooks.Create(1);
                            break;
                    }
                    this.iWorkbook.Version = ExcelVersion.Excel97to2003;
                }
                return this.iWorkbook;
            }
        }
        public void Dispose()
        {
            //ProcessExpansion.Kill("EXCEL");
        }
        public ExcelEngineAdvance(string path)
        {
            this.path = path;
            this.Dispose();
        }
        public List<string> GetIWorkSheetNameList()
        {
            var iWorkSheetNameList = new List<string>();
            foreach (IWorksheet iWorksheet in this.IWorkbook.Worksheets)
            {
                iWorkSheetNameList.Add(iWorksheet.Name);
            }
            return iWorkSheetNameList;
        }
        public bool ExistsIWorkSheetName(string iWorksheetName)
        {
            List<string> getIWorkSheetNameList = this.GetIWorkSheetNameList();
            bool existsIWorkSheetName = getIWorkSheetNameList.Exists(x => x == iWorksheetName);
            return existsIWorkSheetName;
        }
        public IWorksheet CreateIWorksheet(string iWorksheetName)
        {

            IWorksheet createIWorksheet = this.IWorkbook.Worksheets.Create(iWorksheetName);
            return createIWorksheet;
        }
        public int ImportDataTable(DataTable dataTable)
        {
            IWorksheet createIWorksheet = this.CreateIWorksheet(dataTable.TableName);
            createIWorksheet.Name = dataTable.TableName;
            int importDataTable = createIWorksheet.ImportDataTable(dataTable, true, 1, 1, true);
            return importDataTable;
        }
        public void ExportDataTable(string iWorksheetName, out DataTable dataTable)
        {
            try
            {
                IWorksheet iWorksheet = this.IWorkbook.Worksheets[iWorksheetName];
                dataTable = iWorksheet.ExportDataTable(1, 1, iWorksheet.UsedRange.End.Row, iWorksheet.UsedRange.End.Column, ExcelExportDataTableOptions.ColumnNames | ExcelExportDataTableOptions.DetectColumnTypes);
            }
            catch { throw new Exception("ElementComparisonException"); }
        }
        public void RemoveIWorkSheet(string iWorkSheetName)
        {
            this.IWorkbook.Worksheets.Remove(iWorkSheetName);
        }
        public void SaveAs()
        {
            this.RemoveIWorkSheet("Sheet1");
            this.IWorkbook.SaveAs(this.path);
        }
    }
}
