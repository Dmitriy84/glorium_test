using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OlxUaTests.Pages
{
    class ThankYouPage : BasePage
    {
        public ThankYouPage(IWebDriver driver) : base(driver)
        {
        }

        [FindsBy(How = How.XPath, Using = "//strong[.='Спасибо за размещение объявлений на OLX!']")]
        public IWebElement ThankYouMessage { get; set; }
    }
}