using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.IO;
using ExcelDataReader;
using DocumentFormat.OpenXml.Packaging;
using Ex = Microsoft.Office.Interop.Excel;
using System.Xml.Linq;

namespace Framework
{
    public class Library
    {
        public DataTable ExcelToDataTable(string fileName, string tableName)
        {
            //open file and returns as Stream
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx

            //Set the First Row as Column Name
            //excelReader.IsFirstRowAsColumnNames = true;

            //Return as DataSet
            using (DataSet result = excelReader.AsDataSet())
            {
                //Get all the Tables
                DataTableCollection table = result.Tables;

                //Store it in DataTable
                DataTable resultTable = table[tableName];

                excelReader.Close();

                //return
                return resultTable;
            }
        }

        public  List<Datacollection> DataCol = new List<Datacollection>();

        public void PopulateInCollection(string fileName, string tableName)
        {
            DataTable table = new Library().ExcelToDataTable(fileName, tableName);

            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    var dtTable = new Datacollection
                    {
                        RowNumber = row,
                        ColName = table.Columns[col].ColumnName,
                        ColValue = table.Rows[row - 1][col].ToString()
                    };
                    //Add all the details for each row
                    DataCol.Add(dtTable);
                }
            }
        }

        public string Data(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce iterations
                string data = (from colData in DataCol
                               where colData.ColName == columnName && colData.RowNumber == rowNumber
                               select colData.ColValue).SingleOrDefault();

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void OpenAndSave(string fileName)
        {
            var lastOpened = File.GetLastWriteTime(fileName).Date;

            Debug.WriteLine("File: " + fileName);

            if (lastOpened != DateTime.Today)
            {
                var src = new FileInfo(fileName);

                using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(src.FullName, true))
                {
                    var calculationProperties = spreadSheet.WorkbookPart.Workbook.CalculationProperties;
                    calculationProperties.ForceFullCalculation = true;
                    calculationProperties.FullCalculationOnLoad = true;
                }

                //Use Excel automation to open and save the workbook, thereby running the calculation engine.
                var app = new Ex.Application();
                //string execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                Ex.Workbook book = app.Workbooks.Open(src.FullName);
                book.Save();
                book.Close();
                app.Quit();
            }
        }

        public string[] GetTable(string testClass)
        {
            string[] tableQuery = new string[2];

            var table = XDocument.Load("../../../Tables.xml").Descendants("Table");

            Debug.WriteLine("TestClass: " + testClass);
            Debug.WriteLine("Table: " + table);

            var xElements = table as IList<XElement> ?? table.ToList();
            var stateQuery = xElements
                .Where(t => (t.Element("TestClass")?.Value) == testClass)
                .Select(t => t.Element("State")?.Value).Single();

            var sheetQuery = xElements
                .Where(t => (t.Element("TestClass")?.Value) == testClass)
                .Select(t => t.Element("Sheet")?.Value).Single();

            tableQuery[0] = stateQuery;
            tableQuery[1] = sheetQuery;

            return tableQuery;
        }
    }

    public class Datacollection
    {
        public int RowNumber { get; set; }
        public string ColName { get; set; }
        public string ColValue { get; set; }
    }
}
