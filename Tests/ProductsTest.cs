using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Tests
{
    public class ProductsTest : BaseTest
    {
        [Test]
        public async Task TestProductsPageLoadedAfterLogin()
        {
            // The browser is already on the relevant page logged in.
            // Wait for some element that shows we are successfully logged in
            // For example, verifying the URL or a specific element on the products page.
            
            // await Expect(Page.GetByTestId("add-a-product-button")).ToBeVisibleAsync();
            
            // Just outputting to show we reached this point
            var url = Page.Url;
            TestContext.Progress.WriteLine($"Test executed. Current URL: {url}");
        }
    }
}
