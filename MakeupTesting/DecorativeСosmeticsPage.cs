using MakeupTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class DecorativeСosmeticsPage : BasePage
    {
        public DecorativeСosmeticsPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement titleDecorativeCosmetics => webDriver.FindElement(By.XPath("//span[text()='Декоративна косметика']"));

        public string GetCategoryTitleText() => titleDecorativeCosmetics.Text;
    }
}
