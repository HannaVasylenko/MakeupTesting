using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingTests
{
    public class DecorativeCosmeticsTests : BaseTest
    {
        [Test]
        public void VerifySelectCategory()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            string categoryTitleText = config["categoryTitleText"];
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage dekoratyvnaKosmetyka = new DecorativeСosmeticsPage(driver);
            string titleText = dekoratyvnaKosmetyka.GetCategoryTitleText(config["categoryTitle"]);
            ClassicAssert.AreEqual(categoryTitleText, titleText, "The titles do not match");
        }

        [Test]
        public void VerifyFilterProducts()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string nameOfBrand = config["nameOfBrand"];
            string filterProductName = config["filterProductName"];

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            Thread.Sleep(3000);
            List<(string productName, string productType)> productTitles = decorativeCosmetics.GetProductTitleText();
            foreach (var productTitleText in productTitles)
            {
                StringAssert.Contains(nameOfBrand.ToLower(), productTitleText.productName.ToLower(), $"The product name is missing in the title {productTitleText}");
                StringAssert.Contains(filterProductName.ToLower(), productTitleText.productType.ToLower(), $"The product name is missing in the title {productTitleText}");
            }
        }

        [Test]
        public void VerifyFilterProductsByPrice()
        {
            // complete the test
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            double minPrice = double.Parse(config["minPrice"]);
            double maxPrice = double.Parse(config["maxPrice"]);

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SetFilterByPrice(minPrice, maxPrice);

            var searchResultDetails = decorativeCosmetics.GetSearchResultDetails();
            ClassicAssert.IsNotNull(searchResultDetails);

            foreach (var item in searchResultDetails)
            {
                ClassicAssert.LessOrEqual(item.Value, maxPrice, $"Product {item.Key} price {item.Value} is not less than maximum price");
                ClassicAssert.GreaterOrEqual(item.Value, minPrice, $"Product {item.Key} price {item.Value} is not less than maximum price");
            }
        }

        [Test]
        public void VerifySortProductsBy()
        {
            // complete the test
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.SelectDropdownSortBy();
            decorativeCosmetic.SelectValueSortBy("вартістю");
            //List<double> pricesList = ;
            //List<double> orderedPricesList = pricesList.OrderBy(x => x).ToList();
            //CollectionAssert.AreEqual(pricesList, orderedPricesList);
        }

        [Test]
        public void VerifyRemoveFilters()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            bool isButtonPresentBefore = decorativeCosmetics.IsRemoveFiltersButtonPresent();
            decorativeCosmetics.RemoveFilters();
            bool isButtonNotPresentAfter = decorativeCosmetics.IsRemoveFiltersButtonNotPresent();

            ClassicAssert.IsTrue(isButtonPresentBefore, "Remove filters button is not present before clicking.");
            ClassicAssert.IsTrue(isButtonNotPresentAfter, "Remove filters button is still present after clicking.");
        }
        [Test]
        public void VerifyProductsCountByClickBtnMoreProducts()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage dekorativeCosmetic = new DecorativeСosmeticsPage(driver);
            int initialCount = dekorativeCosmetic.CountProductsInList();
            dekorativeCosmetic.AddMoreProducts();
            Thread.Sleep(3000);
            int updatedCount = dekorativeCosmetic.CountProductsInList();
            int expectedCount = initialCount + 36;
            ClassicAssert.AreEqual(expectedCount, updatedCount, "The products count did not increase by 36 after clicking the 'More products' button.");
        }
        [Test]
        public void VerifyMovementTestimonialsInSlider()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage dekorativeCosmetic = new DecorativeСosmeticsPage(driver);
            dekorativeCosmetic.ClickRightArrowInTestimonialsSlider();
            int expectedIndex = dekorativeCosmetic.GetIndexOfActiveTestimonialPage();
            int actualIndex = dekorativeCosmetic.GetNumberOfClicksOnArrow() + 1;

            ClassicAssert.AreEqual(expectedIndex, actualIndex, "The active page index does not match the expected index after clicking the right arrow.");
        }
    }
}
