using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string actualSearchTitle = searchResultPage.GetSearchTitleText();
            
            StringAssert.Contains(searchTitle, actualSearchTitle, $"Search is not performed by {productName}");
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
            string actualSearchTitle = searchResultPage.GetSearchTitleText();
            
            StringAssert.Contains(searchTitle, actualSearchTitle, "Product search titles are not the same");
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
            string actualSearchTitle = searchResultPage.GetSearchTitleText();
            
            StringAssert.Contains(searchTitle, actualSearchTitle, $"Search is not performed by {specialCharacters}");
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

        [Test(Description = " Test FAILED - The product name is missing in the title (Набор - Rilastil Anti-Age Summer Kit (f/cr/40ml + serum/15ml)), Expected: String containing (крем)")]
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
