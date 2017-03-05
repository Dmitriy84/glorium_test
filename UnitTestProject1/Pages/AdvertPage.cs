using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace OlxUaTests.Pages
{
    class AdvertPage : BasePage
    {
        public AdvertPage(IWebDriver driver) : base(driver)
        {
        }

        [FindsBy(How = How.Id, Using = "add-title")]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='title']/descendant::p[@class='error ']/label")]
        public IWebElement TitleError { get; set; }

        public IWebElement Categories()
        {
            return _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("dl#targetrenderSelect1-0")));
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='categories']/descendant::p[@class='error ']/label")]
        public IWebElement CategoriesError { get; set; }

        [FindsBy(How = How.Id, Using = "add-description")]
        public IWebElement Description { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='description']/descendant::p[@class='error']/label")]
        public IWebElement DescriptionError { get; set; }

        [FindsBy(How = How.Id, Using = "mapAddress")]
        public IWebElement Address { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='map_address']/descendant::p[@class='error ']/label")]
        public IWebElement AddressError { get; set; }

        [FindsBy(How = How.Id, Using = "add-person")]
        public IWebElement Person { get; set; }

        [FindsBy(How = How.Id, Using = "add-phone")]
        public IWebElement Phone { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='phone']/descendant::p[@class='error ']/label")]
        public IWebElement PhoneError { get; set; }

        public IWebElement Submit()
        {
            var element = _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("save")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            return _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("save")));
        }
    }
}