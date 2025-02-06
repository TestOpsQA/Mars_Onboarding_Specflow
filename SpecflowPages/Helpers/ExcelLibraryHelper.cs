using ExcelDataReader;
using System.Data;
using OpenQA.Selenium;
using System.Text;
using NUnit.Framework;

namespace Mars_Onboarding_Specflow.SpecFlowPages.Helpers
{
    class ExcelLibraryHelper
    {
        #region Constant configuration
        public static int Browser = 2;

        // Path to Mars.xlsx
        private static readonly string marsExcelPath = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName ?? string.Empty,
            "SpecflowTests",
            "Data",
            "Mars.xlsx");

        // Path to Data.xlsx
        private static readonly string dataExcelPath = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName ?? string.Empty,
            "SpecflowTests",
            "Data",
            "Data.xlsx");

        // Getter for the paths
        public static string MarsExcelPath { get => marsExcelPath; }
        public static string DataExcelPath { get => dataExcelPath; }

        #endregion

        public static string ExcelPath { get; set; } = marsExcelPath; // Default to Mars.xlsx

        #region Excel reader 
        public class ExcelLib
        {
            static List<Datacollection> dataCol = new List<Datacollection>();

            public class Datacollection
            {
                public int? rowNumber { get; set; }
                public string? colName { get; set; }
                public string? colValue { get; set; }
            }

            public static void ClearData()
            {
                dataCol.Clear();
            }

            public static DataTable ExcelToDataTable(string fileName, string SheetName)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        DataTableCollection table = result.Tables;
                        DataTable? resultTable = table[SheetName];
                        return resultTable ?? new DataTable();
                    }
                }
            }

            public static string? ReadData(int rowNumber, string columnName)
            {
                UseExcelFile("Data");
                try
                {
                    rowNumber = rowNumber - 1;
                    string? data = (from colData in dataCol
                                    where colData.colName == columnName && colData.rowNumber == rowNumber
                                    select colData.colValue).SingleOrDefault();

                    return data;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception occurred in ExcelLib Class ReadData Method!" + Environment.NewLine + e.Message.ToString());
                    return null;
                }
            }

            public static void PopulateInCollection(string fileName, string SheetName)
            {
                ClearData();
                DataTable? table = ExcelToDataTable(fileName, SheetName);

                if (table != null)
                {
                    for (int row = 1; row <= table.Rows.Count; row++)
                    {
                        for (int col = 0; col < table.Columns.Count; col++)
                        {
                            Datacollection dtTable = new Datacollection()
                            {
                                rowNumber = row,
                                colName = table.Columns[col].ColumnName,
                                colValue = table.Rows[row - 1][col]?.ToString() ?? ""
                            };

                            dataCol.Add(dtTable);
                        }
                    }
                }
            }

            // Helper method to switch between Mars.xlsx and Data.xlsx dynamically
            public static void UseExcelFile(string fileType)
            {
                if (fileType == "Mars")
                {
                    ExcelPath = MarsExcelPath;
                }
                else if (fileType == "Data")
                {
                    ExcelPath = DataExcelPath;
                }
                else
                {
                    throw new ArgumentException($"Invalid file type: {fileType}. Valid options are 'Mars' or 'Data'.");
                }
            }

            // New method: GetRowCount 
            public static int GetRowCount(string fileName, string sheetName, string columnName)
            {
                int rowCount = 0;
                try
                {
                    DataTable table = ExcelToDataTable(fileName, sheetName); 

                    // Count the number of rows where the column is not empty
                    rowCount = table.AsEnumerable()
                                    .Count(row => row[columnName] != DBNull.Value && row[columnName].ToString() != string.Empty);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in GetRowCount method: " + e.Message);
                }

                return rowCount;
            }

            // New method: GetScreenshot 
            public static string GetScreenshot()
            {
                try
                {
                    IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(); 
                    ITakesScreenshot? screenshotDriver = driver as ITakesScreenshot;
                    Screenshot? screenshot = screenshotDriver?.GetScreenshot();
                    string? screenshotBase64 = screenshot?.AsBase64EncodedString;

                    // Optionally, save the screenshot as a file if needed
                    string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "screenshot.png");
                    screenshot?.SaveAsFile(screenshotPath);
                    if (screenshotBase64 == null)
                    {
                        screenshotBase64 = "No screenshotBase64 found";
                    }

                    return screenshotBase64;  
                }
                catch (Exception e)
                {
                    TestContext.WriteLine("Error in GetScreenshot method: " + e.Message);

                    return "Error capturing screenshot";
                }
            }

        }
        #endregion
    }
}
