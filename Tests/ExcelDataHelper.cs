using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Helper class providing Data-Driven Testing (DDT) capabilities via .xlsx processing.
    /// Intercepts NUnit's TestCaseSource to supply variable matrices dynamically.
    /// </summary>
    public static class ExcelDataHelper
    {
        /// <summary>
        /// Reads test case permutations directly from the TestData.xlsx artifact.
        /// Iterates row records into parameterized NUnit yield returns.
        /// </summary>
        /// <returns>An IEnumerable collection driving parameterized [Test] executions.</returns>
        public static IEnumerable<TestCaseData> GetLoginTestData()
        {
            // Register encoding provider for ExcelDataReader over .NET Core
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var filePath = ExcelDataGenerator.GenerateTestDataFile();

            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            
            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            var dataTable = result.Tables["LoginData"];

            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var url = row["url"]?.ToString() ?? string.Empty;
                    var username = row["username"]?.ToString() ?? string.Empty;
                    var password = row["password"]?.ToString() ?? string.Empty;
                    
                    yield return new TestCaseData(url, username, password)
                        .SetName($"LoginTest_with_{username}");
                }
            }
        }
    }
}
