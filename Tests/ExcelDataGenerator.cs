using ClosedXML.Excel;
using System.IO;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Utility class responsible for generating the default Excel test data sheet.
    /// Overrides manual configuration to ensure Data-Driven paths are never broken out of the box.
    /// </summary>
    public static class ExcelDataGenerator
    {
        /// <summary>
        /// Programmatically creates TestData.xlsx if it does not already exist on disk.
        /// Populates the initial headers and base testing data rows.
        /// </summary>
        /// <returns>The fully qualified absolute path to the generated Excel configuration file.</returns>
        public static string GenerateTestDataFile()
        {
            var projectDirectory = Directory.GetParent(System.AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            var filePath = Path.Combine(projectDirectory, "TestData.xlsx");

            // Only create if it doesn't already exist to avoid overwriting user changes
            if (!File.Exists(filePath))
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("LoginData");
                
                // Headers
                worksheet.Cell(1, 1).Value = "url";
                worksheet.Cell(1, 2).Value = "username";
                worksheet.Cell(1, 3).Value = "password";

                // Valid Data Row
                worksheet.Cell(2, 1).Value = "https://commitquality.com/login";
                worksheet.Cell(2, 2).Value = "test";
                worksheet.Cell(2, 3).Value = "test";


                workbook.SaveAs(filePath);
            }
            return filePath;
        }
    }
}
