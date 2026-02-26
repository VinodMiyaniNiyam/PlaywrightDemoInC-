using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Test suite responsible for validating native browser JavaScript dialogs.
    /// Demonstrates Playwright's Page.Dialog event listener capabilities.
    /// </summary>
    public class AlertValidationTest : BaseTest
    {
        /// <summary>
        /// Test Case 4: Triggers a JavaScript Alert on the DOM, intercepts the event, 
        /// captures its message, validates the message string, and affirmatively dismisses the prompt.
        /// Priority: 4
        /// </summary>
        [Test, Category("Priority4"), Order(4)]
        public async Task HandleAndValidateJavaScriptAlert()
        {
            // 4th test case: Handle JavaScript alert and validate alert text.
            await Page.GotoAsync("https://the-internet.herokuapp.com/javascript_alerts");

            // Setup the dialog handler BEFORE triggering the alert
            // Playwright automatically dismisses dialogs if no handler is attached, 
            // so we must attach one to read the message and accept it.
            var alertMessage = string.Empty;
            Page.Dialog += async (_, dialog) =>
            {
                alertMessage = dialog.Message;
                // Accept the alert (Click "OK")
                await dialog.AcceptAsync();
            };

            // Trigger the alert by clicking the button
            await Page.GetByText("Click for JS Alert").ClickAsync();

            // Validate that the alert text matches expectations
            Assert.That(alertMessage, Is.EqualTo("I am a JS Alert"), "The JavaScript alert text did not match the expected value.");

            // Verify the result text on the page confirms the alert was successfully accepted
            await Expect(Page.Locator("#result")).ToHaveTextAsync("You successfully clicked an alert");

            TestContext.Progress.WriteLine($"Successfully validated JavaScript alert. Alert text was: '{alertMessage}'");
        }
    }
}
