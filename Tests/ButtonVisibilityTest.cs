using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    public class ButtonVisibilityTest : BaseTest
    {
        [Test, Category("Priority3"), Order(3)]
        public async Task ValidateButtonPresenceAndVisibility()
        {
            // 3rd test case: Validate presence and visibility of a button using Playwright selectors.
            await Page.GotoAsync("https://commitquality.com/login");

            // Locate the button using Playwright's GetByTestId selector
            var loginButton = Page.GetByTestId("login-button");

            // Validate presence (it is attached to the DOM) and visibility (it is visually displayed)
            await Expect(loginButton).ToBeAttachedAsync();
            await Expect(loginButton).ToBeVisibleAsync();
            await Expect(loginButton).ToBeEnabledAsync();

            TestContext.Progress.WriteLine("Successfully validated the presence and visibility of the login button.");
        }
    }
}
