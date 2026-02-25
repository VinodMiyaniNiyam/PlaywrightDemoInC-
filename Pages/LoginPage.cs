using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightDemoInC.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly ILocator _usernameInput;
        private readonly ILocator _passwordInput;
        private readonly ILocator _loginButton;

        public LoginPage(IPage page)
        {
            _page = page;
            _usernameInput = _page.GetByTestId("username-textbox");
            _passwordInput = _page.GetByTestId("password-textbox");
            _loginButton = _page.GetByTestId("login-button");
        }

        public async Task NavigateAsync()
        {
            await _page.GotoAsync("https://commitquality.com/login");
        }

        public async Task LoginAsync(string username, string password)
        {
            await _usernameInput.FillAsync(username);
            await _passwordInput.FillAsync(password);
            await _loginButton.ClickAsync();
        }
    }
}
