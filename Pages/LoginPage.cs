using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Pages
{
    /// <summary>
    /// Represents the Page Object Model (POM) for the CommitQuality Login Page.
    /// Contains locators and actions specific to the login functionality.
    /// </summary>
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly ILocator _usernameInput;
        private readonly ILocator _passwordInput;
        private readonly ILocator _loginButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// Maps the required web elements to their Playwright locators.
        /// </summary>
        /// <param name="page">The Playwright page instance.</param>
        public LoginPage(IPage page)
        {
            _page = page;
            _usernameInput = _page.GetByTestId("username-textbox");
            _passwordInput = _page.GetByTestId("password-textbox");
            _loginButton = _page.GetByTestId("login-button");
        }

        /// <summary>
        /// Navigates the browser directly to the CommitQuality login page.
        /// </summary>
        /// <returns>A predefined task indicating when the navigation finishes.</returns>
        public async Task NavigateAsync()
        {
            await _page.GotoAsync("https://commitquality.com/login");
        }

        /// <summary>
        /// Performs the login action by filling in the username and password fields, then clicking the login button.
        /// </summary>
        /// <param name="username">The username to input.</param>
        /// <param name="password">The password to input.</param>
        /// <returns>A task that completes when all login interactions are executed.</returns>
        public async Task LoginAsync(string username, string password)
        {
            await _usernameInput.FillAsync(username);
            await _passwordInput.FillAsync(password);
            await _loginButton.ClickAsync();
        }
    }
}
