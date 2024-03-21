using MakeupTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingPageObjects
{
    public class BrandsPage : BasePage
    {
        public BrandsPage(IWebDriver driver) : base(driver)
        {
        }
        private IWebElement btnBrandVariant(string brandVariant) => webDriver.FindElement(By.XPath($"//li[contains(text(), '{brandVariant}')]"));
        public void SelectBrandVariant(string brandVariant) => btnBrandVariant(brandVariant).Click();
        private List<IWebElement> brandstList => webDriver.FindElements(By.XPath("//div[@class='brands__column active']/ul[@class='brands__list']/li")).ToList();
        public List<string> GetBrandText()
        {
            List<string> result = new List<string>();

            foreach (var product in brandstList)
            {
                string productText = product.FindElement(By.XPath("./a")).Text;
                result.Add(productText);
            }
            return result;
        }
    }
}
