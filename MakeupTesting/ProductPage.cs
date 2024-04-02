using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    public class ProductPage : BasePage
    {
        public ProductPage(IWebDriver driver) : base(driver) { }
        
        public string GetProductColorVariant(string productVariant) => WaitUntilWebElementExists(By.XPath($"//label[@class='product-variant-selected']//span[contains(text(),'{productVariant}')]")).Text;

        public void AddProductToCart() => WaitUntilWebElementExists(By.XPath("//div[@class='button buy']")).Click();

        public void SelectProductImage(int imgNumber) => WaitUntilWebElementExists(By.XPath($"//div[@id='product-image']//ul[@class='simple-slider-list']/li/label[contains(@for, 'product-slider_id_{imgNumber}')]")).Click();

        public void ClickOnBreadCrumbs(string linkVariant) => WaitUntilWebElementExists(By.XPath($"//div[@class='bread-crumbs']//span[contains(text(), '{linkVariant}')]")).Click();

        public void SelectProductVariants(string variantText)
        {
            IWebElement ddProductVariants = WaitUntilWebElementExists(By.XPath("//div[@class='select']"));
            ddProductVariants.Click();

            webDriver.FindElements(By.XPath("//div[@class='variants scrolling full-width']/div/span"))
                .ToList()
                .ForEach(e =>
                {
                    if (e.Text.Contains(variantText))
                    {
                        e.Click();
                    }
                });
        }

        public int GetActiveProductImageIndex()
        {
            WaitUntilWebElementExists(By.XPath("//div[@class='product-slider sl']//ul[@class='simple-slider-list']"));
            int index = webDriver.FindElements(By.XPath("//div[@id='product-image']//ul[@class='simple-slider-list']/li/label")).ToList()
                .FindIndex(e => e.GetAttribute("class").Contains("active"));

            return index == -1 ? index : index + 1;
        }
    }
}
