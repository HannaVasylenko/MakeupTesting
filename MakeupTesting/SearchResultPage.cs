using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    public class SearchResultPage : BasePage
    {
        public SearchResultPage(IWebDriver driver) : base(driver) { }

        public string GetSearchTitle() => WaitUntilWebElementExists(By.XPath($"//div[@class='search-results info-text']")).Text;

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
        public string GetBreadCrumbsTitle(string breadCrumbsVariant) => WaitUntilWebElementExists(By.XPath($"//h1[contains(text(), '{breadCrumbsVariant}')]")).Text;
    }
}
