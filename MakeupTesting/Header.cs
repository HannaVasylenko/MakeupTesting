using MakeupTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class Header : BasePage
    {
        public Header(IWebDriver driver) : base(driver) {}

        public void SelectBeautyClub() => webDriver.FindElement(By.XPath("//a[@class='header-top-list__link bc-about-link']")).Click();

        public void SelectBrandsPage() => webDriver.FindElement(By.XPath("//a[text()='Бренди']")).Click();

        public void OpenDeliveryPage() => webDriver.FindElement(By.XPath("//li[@class='header-top-list__item']/a[text()='Доставка та Оплата']")).Click();

        public void SwitchLanguageToru() => webDriver.FindElement(By.XPath("//header//a[text()='Рус']")).Click();

        public void SwitchLanguageToUA() => webDriver.FindElement(By.XPath("//header//a[text()='Укр']")).Click();

        public void SelectCategory(string category) => GetDecorativeСosmeticsElement(category).Click();

        public void SearchClick() => webDriver.FindElement(By.XPath("//div[@data-popup-handler='search']")).Click();

        public IWebElement GetDecorativeСosmeticsElement(string category) => WaitUntilWebElementExists(By.XPath($"//a[text()='{category}']"));
        
        public void InputProductName(string text)
        {
            IWebElement txtSearch = webDriver.FindElement(By.XPath("//input[@itemprop='query-input']"));
            txtSearch.SendKeys(text);
            txtSearch.SendKeys(Keys.Enter);
        }
        
        public IWebElement GetHintFeatures() => webDriver.FindElement(By.XPath("//span[text()='Безкоштовна доставка по Україні!']"));
        
        public string GetHintText() => webDriver.FindElement(By.XPath("//span[text()='Безкоштовна доставка по Україні!']/parent::*")).GetAttribute("data-text");
    }
}
