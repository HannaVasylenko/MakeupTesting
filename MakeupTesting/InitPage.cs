using MakeupTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class InitPage : BasePage
    {
        public InitPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement btnPageLanguageru => webDriver.FindElement(By.XPath("//header//a[text()='Рус']"));
        private IWebElement btnPageLanguageUA => webDriver.FindElement(By.XPath("//header//a[text()='Укр']"));
        public void SwitchLanguageToru() => btnPageLanguageru.Click();

        public void SwitchLanguageToUA() => btnPageLanguageUA.Click();
    }
}
