using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OlxUaTests.Pages
{
    class OlxUaPage : BasePage
    {
        public OlxUaPage(IWebDriver driver) : base(driver)
        {
        }

        [FindsBy(How = How.Id, Using = "postNewAdLink")]
        public IWebElement AdvertButton { get; set; }
    }
}