using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Test suite verifying the manipulation and validation of HTML Select (Dropdown) inputs.
    /// </summary>
    public class DropdownSelectionTest : BaseTest
    {
        /// <summary>
        /// Test Case 6: Targets a dropdown select element, clicks 'Option 2' by its rendered label, 
        /// and verifies the DOM assigns the correct backing value '2' to the target locator.
        /// Priority: 6
        /// </summary>
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
