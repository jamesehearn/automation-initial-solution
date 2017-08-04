using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;
using UI_Tests.Data;

namespace Framework.Pages
{
    class HomePage : BasePage
    {
        private readonly By PageTitle = By.CssSelector(".woocommerce-products-header__title");
        private readonly By OrderByDropdown = By.CssSelector("[@name='orderby']");
        public HomePage(IWebDriver driver) : base(driver) { }
        public bool PageDisplays() => IsDisplayed(PageTitle, 10);
        public HomePage GoTo(string url)
        {
            Visit(url);
            Assert.That(PageDisplays());
            return this;
        }
    }
}
