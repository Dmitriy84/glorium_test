using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OlxUaTests.Steps;
using OlxUaTests.Utils;
using System;
using OlxUaTests.Pages;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OlxUaTests.Model;

namespace OlxUaTests
{
    [TestClass]
    public class OlxUATests
    {
        private OlxUaSteps _steps;
        private IWebDriver _driver;

        [TestInitialize]
        public void Init()
        {
            _driver = new ChromeDriver(@"../../lib");
            _steps = new OlxUaSteps(_driver);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_driver != null)
                _driver.Quit();
        }

        [TestMethod]
        public void TestEmptyUserName()
        {
            _steps.Init();
            _steps.AddAdvert();

            var loginPage = PageFactory.GetLogin(_driver);
            _steps.LoginViaEmail("");

            Assert.AreEqual("Поле обязательно для заполнения", loginPage.UserEmailError.Text);
        }

        [TestMethod]
        public void TestNotValidEmail()
        {
            _steps.Init();
            _steps.AddAdvert();

            var loginPage = PageFactory.GetLogin(_driver);
            _steps.LoginViaEmail(Util.GetRandomString());

            Assert.AreEqual("это не похоже на email-адрес", loginPage.UserEmailError.Text);
        }

        [TestMethod]
        public void TestNotValidPassword()
        {
            _steps.Init();
            _steps.AddAdvert();

            var loginPage = PageFactory.GetLogin(_driver);
            _steps.LoginViaEmail(password: Util.GetRandomString());

            Assert.AreEqual("Неверный email-адрес или пароль", loginPage.UserPassError.Text);
        }

        [TestMethod]
        public void TestEmptyPassword()
        {
            _steps.Init();
            _steps.AddAdvert();

            var loginPage = PageFactory.GetLogin(_driver);
            _steps.LoginViaEmail(password: "");

            Assert.AreEqual("Поле обязательно для заполнения", loginPage.UserPassError.Text);
        }

        [TestMethod]
        public void TestPublishAdvertOnOlxUa()
        {
            var data = _commonSteps();
            _steps.FillAdvert(data);

            _steps.VerifyThankYouMessage();
        }

        [TestMethod]
        public void TestToShortTitle()
        {
            var data = _commonSteps();
            data.Title = Util.GetRandomString(Util.GetRandomInt(0, 4));
            _steps.FillAdvert(data);

            var advertPage = new AdvertPage(_driver);
            Assert.AreEqual("Заголовок должен быть не короче 5 знаков", advertPage.TitleError.Text);
        }

        [TestMethod]
        public void TestToLongTitle()
        {
            var data = _commonSteps();
            data.Title = Util.GetRandomString(Util.GetRandomInt(71, 100));
            _steps.FillAdvert(data);

            _steps.VerifyThankYouMessage();
        }

        [TestMethod]
        public void TestEmptyTitle()
        {
            var data = _commonSteps();
            data.Title = Util.GetRandomString(0);
            _steps.FillAdvert(data);

            var advertPage = new AdvertPage(_driver);
            Assert.AreEqual("Пожалуйста, укажите заголовок", advertPage.TitleError.Text);
        }

        [TestMethod]
        public void TestEmptyAddress()
        {
            var data = _commonSteps();
            data.Address = new[] { "" };
            try
            {
                _steps.FillAdvert(data);
            }
            catch (WebDriverTimeoutException)
            {
                var advertPage = new AdvertPage(_driver);
                advertPage.Submit().Click();
                Assert.AreEqual("Введите название населенного пункта или почтовый индекс и выберите местоположение из списка", advertPage.AddressError.Text);
            }
        }

        [TestMethod]
        public void TestRandomNotValidAddress()
        {
            var data = _commonSteps();
            data.Address = new[] { Util.GetRandomString() };
            try
            {
                _steps.FillAdvert(data);
            }
            catch (WebDriverTimeoutException)
            {
                var advertPage = new AdvertPage(_driver);
                advertPage.Submit().Click();
                Assert.AreEqual("Введите название населенного пункта или почтовый индекс и выберите местоположение из списка", advertPage.AddressError.Text);
            }
        }

        [TestMethod]
        public void TestEmptyDescription()
        {
            var data = _commonSteps();
            data.Description = "";
            _steps.FillAdvert(data);

            var advertPage = new AdvertPage(_driver);
            advertPage.Submit().Click();
            Assert.AreEqual("добавьте описание объявления", advertPage.DescriptionError.Text);
        }

        [TestMethod]
        public void TestToLongDescription()
        {
            var data = _commonSteps();
            data.Description = Util.GetRandomString(4097);
            _steps.FillAdvert(data);

            _steps.VerifyThankYouMessage();
        }

        [TestMethod]
        public void TestEmptyCategory()
        {
            var data = _commonSteps();
            data.FillCategory = () => { };
            _steps.FillAdvert(data);

            var advertPage = new AdvertPage(_driver);
            Assert.AreEqual("Пожалуйста, выберите рубрику", advertPage.CategoriesError.Text);
        }

        private CategoryData _commonSteps()
        {
            _steps.Init();
            _steps.AddAdvert();

            PageFactory.GetLogin(_driver);
            _steps.LoginViaEmail();

            Action action = () =>
            {
                string Header = "Бизнес и услуги";
                string SubCategory = "Строительство / ремонт / уборка";
                string Item = "Cтроительные услуги";

                HeadingPage headingPage = new HeadingPage(_driver);
                headingPage.ChooseCategory(Header).Click();
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='choosecat chooseway clr overh rel']")));
                _driver.FindElement(By.XPath("//div[descendant::td[.='" + Header + "']]/descendant::span[.='" + SubCategory + "']")).Click();
                _driver.FindElement(By.XPath("//div[descendant::td[.='" + SubCategory + "']]/descendant::span[.='" + Item + "']")).Click();

                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#targetid_private_business"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[starts-with(.,'Частное лицо')]"))).Click();
            };
            var data = CategoryDataFactory.GetSimple();
            data.FillCategory = action;
            return data;
        }
    }
}