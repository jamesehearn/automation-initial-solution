using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;
using UI_Tests.Data;

namespace Framework.Pages
{
    class MyAccountPage : BasePage
    {
        private readonly By PageTitle     = By.LinkText("My account");
        private readonly By LoggInContent = By.CssSelector(".woocommerce-MyAccount-content");
        private readonly By UsernameText = By.Id("username");
        private readonly By PasswordText = By.Id("password");
        private readonly By LoginButton  = By.CssSelector("input[name='login']");
        public MyAccountPage(IWebDriver driver) : base(driver) { }
        public bool PageDisplays() => IsDisplayed(PageTitle, 10);
        public bool UserContentDisplays() => IsDisplayed(LoggInContent, 10);
        public MyAccountPage Login(string username, string password)
        {
            Type(UsernameText, username);
            Type(PasswordText, password);
            Click(LoginButton);
            return this;
        }
    }
}
