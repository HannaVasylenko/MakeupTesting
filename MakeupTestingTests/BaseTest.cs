using Framework;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MakeupTestingTests
{
    public class BaseTest
    {
        public IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new BrowsersList().GetBrowserByName(BrowserEnum.Chrome);
            driver.Navigate().GoToUrl("https://makeup.com.ua/ua/");
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
