using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace OlxUaTests.Pages
{
    class OlxUaLoginPage : BasePage
    {
        public OlxUaLoginPage(IWebDriver driver) : base(driver)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("se_userLogin")));
        }

        [FindsBy(How = How.Id, Using = "userEmail")]
        public IWebElement UserEmail { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[input[@id='userEmail']]/descendant::label")]
        public IWebElement UserEmailError { get; set; }

        [FindsBy(How = How.Id, Using = "userPass")]
        public IWebElement UserPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[input[@id='userPass']]/descendant::label")]
        public IWebElement UserPassError { get; set; }

        [FindsBy(How = How.Id, Using = "se_userLogin")]
        public IWebElement Enter { get; set; }
    }
}