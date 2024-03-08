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

        private IWebElement btnScrollUp => webDriver.FindElement(By.XPath("//div[@class='button-up']"));

        private IWebElement linkSubCategory(string subCategory) => webDriver.FindElement(By.XPath($"//a[text()='{subCategory}']"));
        private IWebElement titleSubCategory(string titlesubCategory) => webDriver.FindElement(By.XPath($"//span[text()='{titlesubCategory}']"));

        public void ScrollUp() => btnScrollUp.Click();

        public void SelectSubCategory(string subCategory) => linkSubCategory(subCategory).Click();

        public string GetSubCategoryTitleText(string titlesubCategory) => titleSubCategory(titlesubCategory).Text;
    }
}
