using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Base class for all Playwright NUnit tests within this framework.
    /// Extends PageTest to provide built-in reporting, logging, and screenshot generation per test.
    /// </summary>
    public class BaseTest : PageTest
    {
        /// <summary>
        /// Global TearDown method executed immediately after each [Test] method completes.
        /// Determines pass/fail status, captures the browser UI state, and generates a timestamped execution log.
        /// </summary>
        [TearDown]
        public async Task TakeScreenshotOnTeardown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var testName = TestContext.CurrentContext.Test.Name;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var projectDirectory = Directory.GetParent(TestContext.CurrentContext.WorkDirectory).Parent.Parent.FullName;

            if (status == TestStatus.Failed)
            {
                var folderPath = Path.Combine(projectDirectory, "FailedScreenshots", timestamp);
                Directory.CreateDirectory(folderPath);
                
                var screenshotPath = Path.Combine(folderPath, $"{testName}.png");

                await Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = screenshotPath,
                    FullPage = true
                });

                TestContext.AddTestAttachment(screenshotPath);
                TestContext.Progress.WriteLine($"Test failed. Screenshot saved at: {screenshotPath}");
                
                // Save the text log file
                var logFilePath = Path.Combine(folderPath, $"{testName}_{timestamp}.txt");
                var logContent = $"Test Name: {testName}\nStatus: FAILED\nExecution Time: {DateTime.Now}\nMessage: {TestContext.CurrentContext.Result.Message}\nStackTrace: {TestContext.CurrentContext.Result.StackTrace}\n";
                File.WriteAllText(logFilePath, logContent);
                TestContext.AddTestAttachment(logFilePath);
            }
            else if (status == TestStatus.Passed)
            {
                var folderPath = Path.Combine(projectDirectory, "PasssScreenshort", timestamp);
                Directory.CreateDirectory(folderPath);
                
                var screenshotPath = Path.Combine(folderPath, $"{testName}.png");

                await Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = screenshotPath,
                    FullPage = true
                });

                TestContext.AddTestAttachment(screenshotPath);
                TestContext.Progress.WriteLine($"Test passed. Screenshot saved at: {screenshotPath}");
                
                // Save the text log file
                var logFilePath = Path.Combine(folderPath, $"{testName}_{timestamp}.txt");
                var logContent = $"Test Name: {testName}\nStatus: PASSED\nExecution Time: {DateTime.Now}\nMessage: {TestContext.CurrentContext.Result.Message}\n";
                File.WriteAllText(logFilePath, logContent);
                TestContext.AddTestAttachment(logFilePath);
            }
        }
    }
}
