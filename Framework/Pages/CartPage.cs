using OpenQA.Selenium;

namespace Framework.Pages
{
    class CartPage : BasePage
    {
        private readonly By PageTitle = By.CssSelector(".entry-title");
        public CartPage(IWebDriver driver) : base(driver) { }
        public bool PageDisplays() => IsDisplayed(PageTitle, 10);
    }
}
