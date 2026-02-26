using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Test suite demonstrating advanced asynchronous synchronization handling.
    /// </summary>
    public class ExplicitWaitTest : BaseTest
    {
        /// <summary>
        /// Test Case 7: Clicks a button to trigger a dynamically loaded element with a built-in delay.
        /// Implements an explicit Playwright WaitForAsync mechanics targeting the visible state 
        /// to ensure synchronization before validating the text overlay.
        /// Priority: 7
        /// </summary>
        [Test, Category("Priority7"), Order(7)]
        public async Task ImplementExplicitWaitForElement()
        {
            // Test case 7: Implement explicit wait for an element to appear.
            await Page.GotoAsync("https://the-internet.herokuapp.com/dynamic_loading/1");

            // Click the Start button which triggers a delayed UI change
            await Page.GetByRole(AriaRole.Button, new() { Name = "Start" }).ClickAsync();

            var finishTextLocator = Page.Locator("#finish h4");

            // Implement an explicit wait for the element to appear
            await finishTextLocator.WaitForAsync(new LocatorWaitForOptions 
            { 
                State = WaitForSelectorState.Visible,
                Timeout = 10000 // explicit wait up to 10 seconds
            });
            
            // Validate the wait was successful by asserting the text
            await Expect(finishTextLocator).ToHaveTextAsync("Hello World!");

            TestContext.Progress.WriteLine("Successfully explicitly waited for the dynamic 'Hello World!' element to appear.");
        }
    }
}
