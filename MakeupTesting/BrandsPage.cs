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
        public BrandsPage(IWebDriver driver) : base(driver) {}
        public void SelectBrandVariant(string brandVariant)
        {
            IWebElement btnBrandVariant(string brandVariant) => webDriver.FindElement(By.XPath($"//li[contains(text(), '{brandVariant}')]"));
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnBrandVariant(brandVariant).Displayed);
            btnBrandVariant(brandVariant).Click();
        }

        public List<string> GetBrandText()
        {
            return webDriver.FindElements(By.XPath("//div[@class='brands__column active']/ul[@class='brands__list']/li/a"))
                    .ToList()
                    .ConvertAll(e => e.Text);
        }
    }
}
