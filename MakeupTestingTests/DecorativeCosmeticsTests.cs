using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            string titleText = decorativeCosmetic.GetCategoryTitleText(config["categoryTitle"]);
            
            ClassicAssert.AreEqual(categoryTitleText, titleText, "Another page is displayed");
        }

        // 5 complete remove Thread.Sleep(3000)
        [Test(Description = " Test FAILED - The product type is missing in the title (Artdeco Hydra Lip Booster, Блеск для губ с эффектом увеличения - Artdeco Hydra Lip Booster). Expected: String containing [блиск для губ]  But was: [блеск для губ с эффектом увеличения - artdeco hydra lip booster]")]
        public void VerifyFilterProducts()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string nameOfBrand = config["nameOfBrand"];
            string filterProductName = config["filterProductName"];

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            Thread.Sleep(3000);
            List<(string productName, string productType)> productTitles = decorativeCosmetic.GetProductTitleText();
            foreach (var productTitleText in productTitles)
            {
                StringAssert.Contains(nameOfBrand.ToLower(), productTitleText.productName.ToLower(), $"The product name is missing in the title {productTitleText}");
                StringAssert.Contains(filterProductName.ToLower(), productTitleText.productType.ToLower(), $"The product type is missing in the title {productTitleText}");
            }
        }

        // 6 complete remove Thread.Sleep(3000)
        [Test]
        public void VerifyFilterProductsByPrice()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            double minPrice = double.Parse(config["minPrice"]);
            double maxPrice = double.Parse(config["maxPrice"]);

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            Thread.Sleep(2000);
            decorativeCosmetic.SetFilterByPrice(minPrice, maxPrice);
            decorativeCosmetic.AddMoreProducts();
            Thread.Sleep(5000);

            var searchResultDetails = decorativeCosmetic.GetSearchResultDetails();
            ClassicAssert.IsNotNull(searchResultDetails);

            foreach (var item in searchResultDetails)
            {
                ClassicAssert.LessOrEqual(item.Value, maxPrice, $"Product {item.Key} price {item.Value} is not less than maximum price");
                ClassicAssert.GreaterOrEqual(item.Value, minPrice, $"Product {item.Key} price {item.Value} is not less than maximum price");
            }
        }

        // 7 complete remove Thread.Sleep(3000)
        [Test]
        public void VerifySortProductsBy()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            Thread.Sleep(2000);
            List<double> orderedPricesList = decorativeCosmetic.GetSearchResultDetails()
                .ToList()
                .ConvertAll(x => x.Value);
            
            orderedPricesList.Sort(new DecorativeСosmeticsPage.ProductComparator());
            
            decorativeCosmetic.SelectDropdownSortBy();
            decorativeCosmetic.SelectValueSortBy(config["variantSortBy"]);
            Thread.Sleep(2000);
            List<double> pricesList = decorativeCosmetic.GetSearchResultDetails()
                .ToList()
                .ConvertAll(x => x.Value);

            CollectionAssert.AreEqual(pricesList, orderedPricesList);
        }

        [Test]
        public void VerifyRemoveFilters()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            bool isButtonPresentBefore = decorativeCosmetic.IsRemoveFiltersButtonPresent();
            decorativeCosmetic.RemoveFilters();
            bool isButtonNotPresentAfter = decorativeCosmetic.IsRemoveFiltersButtonPresent();

            ClassicAssert.IsTrue(isButtonPresentBefore, "Remove filters button is not present before clicking.");
            ClassicAssert.IsFalse(isButtonNotPresentAfter, "Remove filters button is still present after clicking.");
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

        [Test]
        public void VerifySelectProductImage()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            int imgNumber = int.Parse(config["imgNumber"]);
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.SelectProductCard(config["productAddToCart3"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.SelectImage(int.Parse(config["imgNumber"]));
            
            ClassicAssert.AreEqual(imgNumber, productPage.GetActiveProductImageIndex(), "The photo does not match the selected one");
        }
    }
}
