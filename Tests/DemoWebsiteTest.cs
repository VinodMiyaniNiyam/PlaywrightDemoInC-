using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Test suite validating basic browser navigation and document property assertions.
    /// </summary>
    public class DemoWebsiteTest : BaseTest
    {
        /// <summary>
        /// Test Case 2: Launches the Chromium browser, navigates to the Example Domain, 
        /// and strictly validates the browser's Document Title attribute.
        /// Priority: 1
        /// </summary>
        [Test, Category("Priority1"), Order(1)]
        public async Task VerifyDemoWebsiteTitle()
        {
            // 2nd test case: Launch a Chromium browser and navigate to a demo website. Verify the page title.

            // The 'PageTest' base class automatically launches the browser (Chromium by default)
            // and provides a new 'Page' object for each test execution.
            
            // Navigate to a demo website
            await Page.GotoAsync("https://example.com/");
            
            // Verify the title
            var title = await Page.TitleAsync();
            
            // NUnit Assertion to verify the title matches the expected value
            Assert.That(title, Is.EqualTo("Example Domain"), "The page title did not match the expected value.");
            
            // Or using Playwright's built in assertion:
            await Expect(Page).ToHaveTitleAsync("Example Domain");
            
            TestContext.Progress.WriteLine($"Successfully verified demo website title. Title was: {title}");
        }
    }
}
