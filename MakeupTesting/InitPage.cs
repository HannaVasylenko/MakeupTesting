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
        public InitPage(IWebDriver driver) : base(driver) {}

        public void ScrollUp() => webDriver.FindElement(By.XPath("//div[@class='button-up']")).Click();

        public void SelectSubCategory(string subCategory) => WaitUntilWebElementExists(By.XPath($"//a[text()='{subCategory}']")).Click();

        public string GetSubCategoryTitleText(string titleSubCategory) => WaitUntilWebElementExists(By.XPath($"//span[text()='{titleSubCategory}']")).Text;
    }
}
