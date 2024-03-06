﻿using MakeupTestingPageObjects;
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
        public void VerifyInputProductName()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            string productName = config["productName"];
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
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            string spaceKey = config["spaceKey"];
            string searchTitle = "Знайдено 0 товарів";
            InitPage initPage = new InitPage(driver);
            initPage.SearchClick();
            initPage.InputProductName(spaceKey);

            SearchResultPage searchResultPage = new SearchResultPage(driver);
            string searchTitleText = searchResultPage.GetSearchTitleText();
            StringAssert.Contains(searchTitle, searchTitleText, "Search title text do not match");
        }

        [Test]
        public void VerifyInputSpecialCharactersInSearch()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            string specialCharacters = config["specialCharacters"];
            string searchTitle = $"Результати пошуку за запитом «{specialCharacters}»";
            InitPage initPage = new InitPage(driver);
            initPage.SearchClick();
            initPage.InputProductName(specialCharacters);

            SearchResultPage searchResultPage = new SearchResultPage(driver);
            string searchTitleText = searchResultPage.GetSearchTitleText();
            StringAssert.Contains(searchTitle, searchTitleText, "Search title text do not match");
        }

        [Test]
        public void VerifySearchResultOnFirstPage()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            string productName = config["productName"];
            InitPage initPage = new InitPage(driver);
            initPage.SearchClick();
            initPage.InputProductName(productName);

            SearchResultPage searchResultPage = new SearchResultPage(driver);
            List<string> productTitles = searchResultPage.GetProductTitleText();
            foreach (var productTitleText in productTitles)
            {
                StringAssert.Contains(productName.ToLower(), productTitleText.ToLower(), $"The product name is missing in the title {productTitleText}");
            }
        }

        [Test]
        public void VerifySearchResultOnLastPage()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            string productName = config["productName"];
            InitPage initPage = new InitPage(driver);
            initPage.SearchClick();
            initPage.InputProductName(productName);

            SearchResultPage searchResultPage = new SearchResultPage(driver);
            searchResultPage.LastPageClick();
            List<string> productTitles = searchResultPage.GetProductTitleText();
            foreach (var productTitleText in productTitles)
            {
                StringAssert.Contains(productName.ToLower(), productTitleText.ToLower(), $"The product name is missing in the title {productTitleText}");
            }
        }
    }
}
