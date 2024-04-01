using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace MakeupTestingTests
{
    public class SearchResultTests : BaseTest
    {
        [Test]
        public void VerifyInputProductNameInSearch()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productName = config["productName"];
            string searchTitle = $"Результати пошуку за запитом «{productName}»";

            Header header = new Header(driver);
            header.SearchClick();
            header.InputProductName(productName);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            
            StringAssert.Contains(searchTitle, searchResultPage.GetSearchTitleText(), $"Search is not performed by {productName}");
        }

        [Test]
        public void VerifyInputSpaceInSearch()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string spaceKey = config["spaceKey"];
            string searchTitle = "Знайдено 0 товарів";

            Header header = new Header(driver);
            header.SearchClick();
            header.InputProductName(spaceKey);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            
            StringAssert.Contains(searchTitle, searchResultPage.GetSearchTitleText(), "Product search titles are not the same");
        }

        [Test]
        public void VerifyInputSpecialCharactersInSearch()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string specialCharacters = config["specialCharacters"];
            string searchTitle = $"Результати пошуку за запитом «{specialCharacters}»";
        
            Header header = new Header(driver);
            header.SearchClick();
            header.InputProductName(specialCharacters);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            
            StringAssert.Contains(searchTitle, searchResultPage.GetSearchTitleText(), $"Search is not performed by {specialCharacters}");
        }

        [Test]
        public void VerifySearchResultOnFirstPage()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productName = config["productName"];
            
            Header header = new Header(driver);
            header.SearchClick();
            header.InputProductName(productName);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            List<string> productTitles = searchResultPage.GetProductTitleText();
            foreach (var productTitleText in productTitles)
            {
                StringAssert.Contains(productName.ToLower(), productTitleText.ToLower(), $"The product name is missing in the title {productTitleText}");
            }
        }

        [Test(Description = " Test FAILED - The product name is missing in the title, Expected: String containing (крем) but was [набор]")]
        public void VerifySearchResultOnLastPage()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productName = config["productName"];

            Header header = new Header(driver);
            header.SearchClick();
            header.InputProductName(productName);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.LastPageClick();
            List<string> productTitles = searchResultPage.GetProductTitleText();
            foreach (var productTitleText in productTitles)
            {
                StringAssert.Contains(productName.ToLower(), productTitleText.ToLower(), $"The product name is missing in the title {productTitleText}");
            }
        }

        [Test]
        public void VerifyFollowLinkBreadCrumbs()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string linkBreadCrumbs = config["breadCrumbsTitle"];

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.SelectProductCard(config["productAddToCart3"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.ClickBreadCrumbs(config["breadCrumbsTitle"]);
            SearchResultPage searchResultPage = new SearchResultPage(driver);
            
            ClassicAssert.AreEqual(linkBreadCrumbs, searchResultPage.GetBreadCrumbsTitle(config["breadCrumbsTitle"]), "The title does not match the selected category");
        }
    }
}
