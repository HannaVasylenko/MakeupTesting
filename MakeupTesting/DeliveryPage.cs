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
    public class DeliveryPage : BasePage
    {
        public DeliveryPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement txtSelectCity => webDriver.FindElement(By.XPath("//input[@id='select-city']"));
        private IWebElement txtFirstCity => webDriver.FindElement(By.XPath("//div[@class='animated-input-group']//div[@class='search-value__container']//ul[@class='search-value__list scrolling expanded']//li[1]"));
        private IWebElement txtSelectedCity => webDriver.FindElement(By.XPath("//input[@id='city-id-selected']"));

        public void SelectFirstDeliveryCity()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => txtFirstCity.Displayed);
            txtFirstCity.Click();
        }

        public string GetDeliveryCityText()
        {
            string cityTitle = txtSelectedCity.GetAttribute("title");
            return cityTitle;
        }

        public void SelectDeliveryCity()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => txtSelectCity.Displayed);
            txtSelectCity.Click();
        }

        public void InputDeliveryCity(string text)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => txtSelectCity.Displayed);
            txtSelectCity.SendKeys(text);
        }
    }
}
