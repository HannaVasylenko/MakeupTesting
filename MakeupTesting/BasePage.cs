using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MakeupTesting
{
    public abstract class BasePage
    {
        protected readonly IWebDriver webDriver;

        protected BasePage(IWebDriver driver)
        {
            webDriver = driver;
        }

        protected IWebElement WaitUntilWebElementExists(By by, int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            return wait.Until(e => webDriver.FindElement(by));
        }

        protected bool IsElementExists(By by)
        {
            try
            {
                webDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        protected void WaitUntil(Func<IWebDriver, bool> func, int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds));
            wait.Until(func);
        }
    }
}
