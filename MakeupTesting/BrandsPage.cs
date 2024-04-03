using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{   /// <summary>
    /// The class displays a separate Brands page
    /// </summary>
    public class BrandsPage : BasePage
    {
        public BrandsPage(IWebDriver driver) : base(driver) {}

        /// <summary>
        /// Selects a brand variant from the list based on the provided name.
        /// </summary>
        /// <param name="brandVariant">The name of the brand variant to select.</param>
        public void SelectBrandVariant(string brandVariant) => WaitUntilWebElementExists(By.XPath($"//li[contains(text(), '{brandVariant}')]")).Click();

        /// <summary>
        /// Retrieves a list of brand names currently displayed on the page.
        /// </summary>
        /// <returns>A list of strings representing the brand names.</returns>
        public List<string> GetBrandNames() => webDriver.FindElements(By.XPath("//div[@class='brands__column active']/ul[@class='brands__list']/li/a"))
                    .ToList()
                    .ConvertAll(e => e.Text);
    }
}
