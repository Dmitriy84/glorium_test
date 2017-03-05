using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OlxUaTests.Pages
{
    class HeadingPage : BasePage
    {
        public HeadingPage(IWebDriver driver) : base(driver)
        {
            _wait.Until(ExpectedConditions.ElementExists(By.XPath("//ul[@class='icongrid clr marginleft10 margintop10 marginbott10']")));
        }

        public IWebElement ChooseCategory(string category)
        {
            return _driver.FindElement(By.XPath("//ul[@class='icongrid clr marginleft10 margintop10 marginbott10']/li[div/a[@data-category-name='" + category + "']]"));
        }
    }
}
