using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    /// <summary>
    /// Test suite for verifying the HTTP integrity of all page navigation elements.
    /// Utilizes Playwright APIRequestContext to perform parallel backend health checks.
    /// </summary>
    public class BrokenLinksTest : BaseTest
    {
        /// <summary>
        /// Test Case 8: Scrapes all anchor tags present on the page, extracts the Href targets, 
        /// and asynchronously pings each endpoint verifying 200 OK statuses globally.
        /// Priority: 8
        /// </summary>
        [Test, Category("Priority8"), Order(8)]
        public async Task ValidateBrokenLinksOnWebpage()
        {
            // Test case 8: Validate broken links on a webpage.
            await Page.GotoAsync("https://commitquality.com/");

            // Extract all anchor tags with href attributes
            var links = await Page.Locator("a[href]").AllAsync();
            var brokenLinks = new List<string>();

            await using var requestContext = await Playwright.APIRequest.NewContextAsync();

            int linkCount = 0;
            foreach (var link in links)
            {
                var href = await link.GetAttributeAsync("href");
                if (string.IsNullOrWhiteSpace(href)) continue;

                // Handle relative paths
                if (href.StartsWith("/"))
                {
                    href = $"https://commitquality.com{href}";
                }
                
                // Skip non-http links like mailto: etc
                if (!href.StartsWith("http")) continue;

                linkCount++;
                try
                {
                    // Ping the link
                    var response = await requestContext.GetAsync(href);
                    if (!response.Ok)
                    {
                        brokenLinks.Add($"Link: {href} returned status {response.Status}");
                    }
                }
                catch (Exception ex)
                {
                    brokenLinks.Add($"Exception fetching Link: {href}. Error: {ex.Message}");
                }
            }

            // Report any broken links, or assert success
            if (brokenLinks.Count > 0)
            {
                string brokenLinkDetails = string.Join("\n", brokenLinks);
                Assert.Fail($"Found {brokenLinks.Count} broken links out of {linkCount} total links:\n{brokenLinkDetails}");
            }
            else
            {
                TestContext.Progress.WriteLine($"Successfully validated {linkCount} valid links with no broken paths.");
            }
        }
    }
}
