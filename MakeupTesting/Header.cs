using MakeupTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class Header : BasePage
    {
        public Header(IWebDriver driver) : base(driver)
        {
        }
        private IWebElement linkDelivery => webDriver.FindElement(By.XPath("//li[@class='header-top-list__item']/a[text()='Доставка та Оплата']"));
        public void OpenDeliveryPage() => linkDelivery.Click();
    }
}
