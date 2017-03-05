using OlxUaTests.Pages;
using OpenQA.Selenium;

namespace OlxUaTests.Utils
{
    static class PageFactory
    {
        public static OlxUaLoginPage GetLogin(IWebDriver driver)
        {
            return new OlxUaLoginPage(driver);
        }

        public static AdvertPage GetAdvert(IWebDriver driver)
        {
            return new AdvertPage(driver);
        }

        public static ThankYouPage GetThankYou(IWebDriver driver)
        {
            return new ThankYouPage(driver);
        }

        public static OlxUaPage GetOlxUa(IWebDriver driver)
        {
            return new OlxUaPage(driver);
        }

        public static HeadingPage GetHeading(IWebDriver driver)
        {
            return new HeadingPage(driver);
        }
    }
}