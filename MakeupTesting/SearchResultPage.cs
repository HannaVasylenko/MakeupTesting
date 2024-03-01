using MakeupTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class SearchResultPage : BasePage
    {
        public SearchResultPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement titleText => webDriver.FindElement(By.XPath("//div[@class='search-results info-text']"));
        public string GetSearchTitleText() => titleText.Text;
    }
}
