using MakeupTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class Footer : BasePage
    {
        public Footer(IWebDriver driver) : base(driver)
        {
        }
        private IWebElement linkYouTube => webDriver.FindElement(By.XPath("//li[@class='social__item social-icon yt']"));

        public void SelectYouTube()
        {
            linkYouTube.Click();
        }
    }
}
