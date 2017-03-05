using Microsoft.VisualStudio.TestTools.UnitTesting;
using OlxUaTests.Model;
using OlxUaTests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace OlxUaTests.Steps
{
    class OlxUaSteps
    {
        private IWebDriver _driver;

        public OlxUaSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Init()
        {
            _driver.Navigate().GoToUrl(GetUrl());
        }

        public string GetUrl()
        {
            return "https://www.olx.ua/";
        }

        public void AddAdvert()
        {
            PageFactory.GetOlxUa(_driver).AdvertButton.Click();
        }

        public void LoginViaEmail(string email = "dimon24@i.ua", string password = "qwerty123")
        {
            var loginPage = PageFactory.GetLogin(_driver);
            loginPage.UserEmail.SendKeys(email);
            loginPage.UserPassword.SendKeys(password);
            loginPage.Enter.Click();
        }

        public void FillAdvert(CategoryData data)
        {
            var advertPage = PageFactory.GetAdvert(_driver);
            advertPage.Phone.Clear();
            advertPage.Address.Clear();
            advertPage.Description.Clear();
            advertPage.Title.Clear();

            advertPage.Title.SendKeys(data.Title);

            advertPage.Categories().Click();

            data.FillCategory.Invoke();

            advertPage.Description.SendKeys(data.Description);

            advertPage.Phone.SendKeys(data.Phone);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            advertPage.Address.SendKeys(string.Join(",", data.Address));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("autosuggest-geo-ul")));
            var element = wait.Until(ExpectedConditions.ElementExists(By.XPath("//ul[@id='autosuggest-geo-ul']/li/a/strong[.='" + data.Address[0] + "']")));
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(_driver);
            action.MoveToElement(element).Click(_driver.FindElement(By.XPath("//a/strong[.='" + data.Address[data.Address.Length - 1] + "']"))).Perform();

            advertPage.Submit().Click();
        }
        public void VerifyThankYouMessage()
        {
            Assert.IsTrue(PageFactory.GetThankYou(_driver).ThankYouMessage.Displayed, "Should see success message");
        }
    }
}