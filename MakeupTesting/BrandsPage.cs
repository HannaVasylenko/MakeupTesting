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
    public class BrandsPage : BasePage
    {
        public BrandsPage(IWebDriver driver) : base(driver)
        {
        }
        private IWebElement btnBrandVariant(string brandVariant) => webDriver.FindElement(By.XPath($"//li[contains(text(), '{brandVariant}')]"));
        private List<IWebElement> brandList => webDriver.FindElements(By.XPath("//div[@class='brands__column active']/ul[@class='brands__list']/li")).ToList();
        public void SelectBrandVariant(string brandVariant)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnBrandVariant(brandVariant).Displayed);
            btnBrandVariant(brandVariant).Click();
        }

        public List<string> GetBrandText()
        {
            List<string> result = new List<string>();

            foreach (var product in brandList)
            {
                string productText = product.FindElement(By.XPath("./a")).Text;
                result.Add(productText);
            }
            return result;
        }
    }
}
