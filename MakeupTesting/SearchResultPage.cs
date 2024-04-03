using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    /// <summary>
    /// A class is a page with search results with selected products according to certain criteria.
    /// </summary>
    public class SearchResultPage : BasePage
    {
        public SearchResultPage(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Retrieves the title of the search results displayed on the page.
        /// </summary>
        /// <returns>The title of the search results.</returns>
        public string GetSearchTitle() => WaitUntilWebElementExists(By.XPath($"//div[@class='search-results info-text']")).Text;

        /// <summary>
        /// Clicks on the last page of search results.
        /// </summary>
        public void ClickOnLastPage()
        {
            IWebElement lastPage = WaitUntilWebElementExists(By.XPath($"(//li[@class='page__item']/label)[last()]"));
            if (Convert.ToInt32(lastPage.Text) > 1)
            {
                List<string> before = GetProductsTitlesInSearch();
                lastPage.Click();
                WaitUntil(e => !before.SequenceEqual(GetProductsTitlesInSearch()));
            }
        }

        /// <summary>
        /// Retrieves the titles of products displayed in the search results.
        /// </summary>
        /// <returns>A list of product titles.</returns>
        public List<string> GetProductsTitlesInSearch()
        {
            List<string> result = new List<string>();
            WaitUntil(e =>
            {
                try
                {
                    var list = webDriver.FindElements(By.XPath("//div[@class='catalog-products']//ul[@class='simple-slider-list']//div[@class='info-product-wrapper']/a"))
                        .ToList()
                        .ConvertAll(e => e.GetAttribute("data-default-name"));

                    result.AddRange(list);
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            return result;
        }

        /// <summary>
        /// Retrieves the title from the breadcrumbs on the page based on the specified variant.
        /// </summary>
        /// <param name="breadCrumbsVariant">The variant to search for in the breadcrumbs.</param>
        /// <returns>The title from the breadcrumbs corresponding to the specified variant.</returns>
        public string GetBreadCrumbsTitle(string breadCrumbsVariant) => WaitUntilWebElementExists(By.XPath($"//h1[contains(text(), '{breadCrumbsVariant}')]")).Text;
    }
}
