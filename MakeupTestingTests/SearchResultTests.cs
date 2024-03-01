using MakeupTestingPageObjects;
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
        public void VerifyInputProductName()
        {
            string productName = "крем";
            string searchTitle = $"Результати пошуку за запитом «{productName}»";
            InitPage initPage = new InitPage(driver);
            initPage.SearchClick();
            initPage.InputProductName(productName);

            SearchResultPage searchResultPage = new SearchResultPage(driver);
            string searchTitleText = searchResultPage.GetSearchTitleText();
            StringAssert.Contains(searchTitle, searchTitleText, "Search title text do not match");
            
        }

        [Test]
        public void VerifyInputSpaceInSearch()
        {
            string spaceKey = " ";
            string searchTitle = "Знайдено 0 товарів";
            InitPage initPage = new InitPage(driver);
            initPage.SearchClick();
            initPage.InputProductName(spaceKey);

            SearchResultPage searchResultPage = new SearchResultPage(driver);
            string searchTitleText = searchResultPage.GetSearchTitleText();
            StringAssert.Contains(searchTitle, searchTitleText, "Search title text do not match");

        }
    }
}
