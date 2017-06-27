
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using UI_Tests;

namespace Framework
{
    internal abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        //Locators to find elements on the page
        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
        protected void Visit(string url)
        {
            if (url.StartsWith("http"))
                Driver.Navigate().GoToUrl(url);
            else
                Driver.Navigate().GoToUrl(BaseTest.BaseUrl + url);
        }
        private IWebElement Find(By locator) => Driver.FindElement(locator);
        protected void Click(By locator)
        {
            Find(locator).Click();
        }
        protected void EnabledClick(By locator)
        {
            if(IsEnabled(locator))
                Find(locator).Click();
        }
        protected void Type(By locator, string inputText)
        {
            Find(locator).SendKeys(inputText);
        }
        protected void EnabledType(By locator, string inputText)
        {
            if (IsEnabled(locator))
                Find(locator).SendKeys(inputText);
        }
        protected void DisplayedType(By locator, string inputText)
        {
            if (IsDisplayed(locator))
                Find(locator).SendKeys(inputText);
        }
        protected void Replace(By locator, string inputText)
        {
            Find(locator).Clear();
            Find(locator).SendKeys(inputText);
        }
        protected void EnabledReplace(By locator, string inputText)
        {
            if (IsEnabled(locator))
            {
                Find(locator).Clear();
                Find(locator).SendKeys(inputText);
            }
        }
        protected void DisplayedReplace(By locator, string inputText)
        {
            if (IsDisplayed(locator))
            {
                Find(locator).Clear();
                Find(locator).SendKeys(inputText);
            }
        }
        protected void Select(By locator, string inputText)
        {
            new SelectElement(Find(locator)).SelectByText(inputText);
        }
        protected void EnabledSelect(By locator, string inputText)
        {
            if (IsEnabled(locator))
                new SelectElement(Find(locator)).SelectByText(inputText);
        }
        protected void DisplayedSelect(By locator, string inputText)
        {
            if (IsDisplayed(locator))
                new SelectElement(Find(locator)).SelectByText(inputText);
        }
        protected void Check(By locator, string check)
        {
            if (!string.IsNullOrEmpty(check))
               Find(locator).Click();
        }
        protected void EnabledCheck(By locator, string check)
        {
            if (IsEnabled(locator))
            {
                if (!string.IsNullOrEmpty(check))
                    Find(locator).Click();
            }
        }
        public void SelectAllCheckboxes()
        {
            IList<IWebElement> ckOverrides = Driver.FindElements(By.XPath("//input[@type='checkbox']"));

            foreach (var ck in ckOverrides)
                if (ck.Displayed && !ck.Selected)
                    ck.Click();
        }
        protected void DeselectAllCheckBoxes()
        {
            IList<IWebElement> ckOverrides = Driver.FindElements(By.XPath("//input[@type='checkbox']"));

            foreach (var ck in ckOverrides)
                if (ck.Displayed && ck.Selected)
                    ck.Click();
        }
        protected void TabOut(By locator)
        {
            Type(locator, Keys.Tab);
        }
        protected bool IsDisplayed(By locator)
        {
            try
            {
                return Find(locator).Displayed;
            } catch (NoSuchElementException)
            {
                return false;
            }
        }
        protected bool IsDisplayed(By locator, int maxWaitTime)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(maxWaitTime));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
            } catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        protected bool IsDisplayed(By locator, string text, int maxWaitTime)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(maxWaitTime));
                wait.Until(ExpectedConditions.TextToBePresentInElementValue(locator, text));
                return true;
            } catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        protected bool IsEnabled(By locator)
        {
            try
            {
                return Find(locator).Enabled;
            } catch (InvalidElementStateException)
            {
                return false;
            }
        }
        protected void SwitchToFrame(string locator)
        {
            Driver.SwitchTo().Frame(locator);
        }
        protected void SwitchToDefaultFrame() => Driver.SwitchTo().DefaultContent();
        protected string GetText(By locator)
        {
            var text = Find(locator);
            return text.Text;
        }
        protected void AcceptPopup()
        {
            try
            {
                Driver.SwitchTo().Alert().Accept();
            } catch (NoAlertPresentException e) { }
        }
    }
}