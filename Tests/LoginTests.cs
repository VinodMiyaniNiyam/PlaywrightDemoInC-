using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PlaywrightDemoInC.Pages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Test suite controlling the core authentication validations.
    /// Leverages external Page Object Models (POM) and Data-Driven Excel iterators.
    /// </summary>
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage;

        [SetUp]
        public void SetUp()
        {
            _loginPage = new LoginPage(Page);
        }

        /// <summary>
        /// Test Case 1 & 2: Pulls username and password coordinates from TestData.xlsx.
        /// Automates login flow and explicitly validates the 'Logout' navigation success indicator.
        /// Priority: 2
        /// </summary>
        /// <param name="url">Target authentication URL row extracted from Excel.</param>
        /// <param name="username">Candidate Username string row extracted from Excel.</param>
        /// <param name="password">Candidate Password string row extracted from Excel.</param>
        [Test, Category("Priority2"), Order(2)]
        [TestCaseSource(typeof(ExcelDataHelper), nameof(ExcelDataHelper.GetLoginTestData))]
        public async Task Login_ShouldTakeScreenshot_BasedOnData(string url, string username, string password)
        {
            // Navigate to the dynamic URL from Excel
            await Page.GotoAsync(url);
            
            // Perform login with dynamic credentials
            await _loginPage.LoginAsync(username, password);

            // Expected to Pass
            await Expect(Page).Not.ToHaveURLAsync(new System.Text.RegularExpressions.Regex(".*login.*"), new() { Timeout = 5000 });
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            
            // Validate successful login message/state (e.g. Navigation bar correctly displays "Logout" or "My Account")
            await Expect(Page.GetByText("Logout")).ToBeVisibleAsync();
            TestContext.Progress.WriteLine("Successfully validated the successful login visual indicators.");
        }
    }
}
