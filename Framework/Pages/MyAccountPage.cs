using OpenQA.Selenium;
using UI_Tests.Data;

namespace Framework.Pages
{
    class MyAccountPage : BasePage
    {
        private readonly By PageTitle          = By.LinkText("My account");
        private readonly By LoggInContent      = By.CssSelector(".woocommerce-MyAccount-content");
        private readonly By UsernameText       = By.Id("username");
        private readonly By PasswordText       = By.Id("password");
        private readonly By LoginButton        = By.CssSelector("input[name='login']");
        private readonly By LogoutButton       = By.LinkText("Log out");
        private readonly By DashboardLink      = By.LinkText("Dashboard");
        private readonly By OrdersLink         = By.LinkText("Orders");
        private readonly By DownloadsLink      = By.LinkText("Downloads");
        private readonly By AddressesLink      = By.LinkText("Addresses");
        private readonly By AccountDetailsLink = By.LinkText("Account details");
        private readonly By LogoutLink         = By.LinkText("Logout");
        public MyAccountPage(IWebDriver driver) : base(driver) { }
        public bool PageDisplays() => IsDisplayed(PageTitle, 10);
        public bool UserContentDisplays() => IsDisplayed(LoggInContent, 10);
        public MyAccountPage Login(DataObject d)
        {
            Type(UsernameText, d.Username);
            Type(PasswordText, d.Password);
            Click(LoginButton);
            return this;
        }
        public MyAccountPage Logout()
        {
            Click(LogoutButton);
            return this;
        }
    }
}
