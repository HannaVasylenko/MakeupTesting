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
    /// <summary>
    /// This class represents the Main page, which starts testing the website.
    /// </summary>
    public class InitPage : BasePage
    {
        public InitPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement btnPageLanguageru => webDriver.FindElement(By.XPath("//header//a[text()='Рус']"));
        private IWebElement btnPageLanguageUA => webDriver.FindElement(By.XPath("//header//a[text()='Укр']"));
        private IWebElement btnSearch => webDriver.FindElement(By.XPath("//div[@data-popup-handler='search']"));
        private IWebElement txtSearch => webDriver.FindElement(By.XPath("//input[@itemprop='query-input']"));
        private IWebElement linkDecorativeCosmetics(string category) => webDriver.FindElement(By.XPath($"//a[text()='{category}']")); // Макіяж
        private IWebElement linkSubCategory(string subCategory) => webDriver.FindElement(By.XPath($"//a[text()='{subCategory}']"));
        private IWebElement titleSubCategory(string titlesubCategory) => webDriver.FindElement(By.XPath($"//span[text()='{titlesubCategory}']")); // Косметика для очей

        public void SwitchLanguageToru() => btnPageLanguageru.Click();

        public void SwitchLanguageToUA() => btnPageLanguageUA.Click();
        public void SelectCategory(string category) => linkDecorativeCosmetics(category).Click();

        public void SelectSubCategory(string subCategory) => linkSubCategory(subCategory).Click();

        public void InputProductName(string text)
        {
            txtSearch.SendKeys(text);
            txtSearch.SendKeys(Keys.Enter);
        }

        public void SearchClick() => btnSearch.Click();

        public string GetSubCategoryTitleText(string titlesubCategory) => titleSubCategory(titlesubCategory).Text;

        public IWebElement GetDecorativeСosmeticsElement(string category) => linkDecorativeCosmetics(category);
    }
}
