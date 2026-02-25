using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    public class DropdownSelectionTest : BaseTest
    {
        [Test, Category("Priority6"), Order(6)]
        public async Task ValidateDropdownSelection()
        {
            // Test case 6: Perform dropdown selection and validate selected value.
            await Page.GotoAsync("https://the-internet.herokuapp.com/dropdown");

            var dropdownLocator = Page.Locator("#dropdown");
            
            // Select Option 2 by label
            await dropdownLocator.SelectOptionAsync(new[] { new SelectOptionValue { Label = "Option 2" } });
            
            // Validate that the selected value is '2'
            await Expect(dropdownLocator).ToHaveValueAsync("2");

            TestContext.Progress.WriteLine("Successfully selected 'Option 2' and validated the dropdown value is '2'.");
        }
    }
}
