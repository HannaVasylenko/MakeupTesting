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
    public class ProductPage : BasePage
    {
        public ProductPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement btnBuy => webDriver.FindElement(By.XPath("//div[@class='button buy']"));
        private IWebElement titleProductVariant(string productVariant) => webDriver.FindElement(By.XPath($"//label[@class='product-variant-selected']//span[contains(text(),'{productVariant}')]"));

        private IWebElement ddProductVariants => webDriver.FindElement(By.XPath("//div[@class='select']"));

        private List<IWebElement> productVariantsList => webDriver.FindElements(By.XPath("//div[@class='variants scrolling full-width']/div")).ToList();
        public string GetProductVariantText(string productVariant) => titleProductVariant(productVariant).Text;


        public void AddProductToCart()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => btnBuy.Displayed);
            btnBuy.Click();
        }
       
        public void SelectProductVariants(string variantText)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(x => ddProductVariants.Displayed);
            ddProductVariants.Click();

            foreach (var product in productVariantsList)
            {
                string productTitle = product.FindElement(By.XPath("./span")).Text;
                if (productTitle.Contains(variantText))
                {
                    product.Click();
                    break;
                }
            }
        }
    }
}
