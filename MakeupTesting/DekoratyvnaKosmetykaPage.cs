using MakeupTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class DekoratyvnaKosmetykaPage : BasePage
    {
        public DekoratyvnaKosmetykaPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement titleCategoryDekoratyvnaKosmetyka => webDriver.FindElement(By.XPath("//span[text()='Декоративна косметика']"));

        public string GetCategoryTitleText() => titleCategoryDekoratyvnaKosmetyka.Text;

    }
}
