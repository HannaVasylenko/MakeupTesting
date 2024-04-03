using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    /// <summary>
    /// The class is a page of a selected product.
    /// </summary>
    public class ProductPage : BasePage
    {
        public ProductPage(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Retrieves the color variant of a product based on the provided product variant.
        /// </summary>
        /// <param name="productVariant">The product variant to search for.</param>
        /// <returns>The text representing the color variant of the product.</returns>
        public string GetProductColorVariant(string productVariant) => WaitUntilWebElementExists(By.XPath($"//label[@class='product-variant-selected']//span[contains(text(),'{productVariant}')]")).Text;

        /// <summary>
        /// Adds the current product to the Cart.
        /// </summary>
        public void AddProductToCart() => WaitUntilWebElementExists(By.XPath("//div[@class='button buy']")).Click();

        /// <summary>
        /// Selects a specific product image based on the provided image number.
        /// </summary>
        /// <param name="imgNumber">The number corresponding to the desired image.</param>
        public void SelectProductImage(int imgNumber) => WaitUntilWebElementExists(By.XPath($"//div[@id='product-image']//ul[@class='simple-slider-list']/li/label[contains(@for, 'product-slider_id_{imgNumber}')]")).Click();

        /// <summary>
        /// Clicks on a specific breadcrumb link variant.
        /// </summary>
        /// <param name="linkVariant">The variant of the breadcrumb link to click on.</param>
        public void ClickOnBreadCrumbs(string linkVariant) => WaitUntilWebElementExists(By.XPath($"//div[@class='bread-crumbs']//span[contains(text(), '{linkVariant}')]")).Click();

        /// <summary>
        /// Selects a product variant from the dropdown menu based on the provided variant text.
        /// </summary>
        /// <param name="variantText">The text of the variant to select.</param>
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

        /// <summary>
        /// Retrieves the index of the currently active product image in the slider.
        /// </summary>
        /// <returns>The index of the active product image, starting from 1.</returns>
        public int GetActiveProductImageIndex()
        {
            WaitUntilWebElementExists(By.XPath("//div[@class='product-slider sl']//ul[@class='simple-slider-list']"));
            int index = webDriver.FindElements(By.XPath("//div[@id='product-image']//ul[@class='simple-slider-list']/li/label")).ToList()
                .FindIndex(e => e.GetAttribute("class").Contains("active"));

            return index == -1 ? index : index + 1;
        }
    }
}
