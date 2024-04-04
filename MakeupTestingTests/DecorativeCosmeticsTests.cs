using MakeupTestingModels;
using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Text.Json;

namespace MakeupTestingTests
{
    public class DecorativeCosmeticsTests : BaseTest
    {
        [Test]
        public void VerifySelectCategory()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string categoryTitle = config["categoryTitleText"];
            
            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            string titleText = decorativeCosmetic.GetCategoryTitle(config["categoryTitle"]);
            
            ClassicAssert.AreEqual(categoryTitle, titleText, "Another page is displayed");
        }

        [Test(Description = "Test FAILED")]
        public void VerifyFilterProducts()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string nameOfBrand = config["nameOfBrand"];
            string filterProductName = config["filterProductName"];

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            
            List<(string productName, string productType)> productTitles = decorativeCosmetic.GetProductTitle();
            foreach (var productTitleText in productTitles)
            {
                StringAssert.Contains(nameOfBrand.ToLower(), productTitleText.productName.ToLower(), $"The product name is missing in the title {productTitleText}");
                StringAssert.Contains(filterProductName.ToLower(), productTitleText.productType.ToLower(), $"The product type is missing in the title {productTitleText}");
            }
        }

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
            decorativeCosmetic.SetFilterByPrice(minPrice, maxPrice);

            var searchResultDetails = decorativeCosmetic.GetProductsTitlesAndPrices();
            ClassicAssert.IsNotNull(searchResultDetails);

            foreach (var item in searchResultDetails)
            {
                ClassicAssert.LessOrEqual(item.Value, maxPrice, $"Product {item.Key} price {item.Value} is not less than maximum price");
                ClassicAssert.GreaterOrEqual(item.Value, minPrice, $"Product {item.Key} price {item.Value} is not less than maximum price");
            }
        }

        [Test(Description = "Test FAILED")]
        public void VerifySortProductsByPrice()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            decorativeCosmetic.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            
            List<double> orderedPricesList = decorativeCosmetic.GetProductsTitlesAndPrices()
                .ToList()
                .ConvertAll(x => x.Value);
            
            orderedPricesList.Sort(new DecorativeСosmeticsPage.ProductComparator());
            
            decorativeCosmetic.SelectDropdownSortBy();
            decorativeCosmetic.SelectSortingByPrice();
            
            List<double> pricesList = decorativeCosmetic.GetProductsTitlesAndPrices()
                .ToList()
                .ConvertAll(x => x.Value);

            CollectionAssert.AreEqual(pricesList, orderedPricesList, "The products are not sorted by prices");
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
        public void VerifyProductsCountByClickOnBtnMoreProducts()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage dekorativeCosmetic = new DecorativeСosmeticsPage(driver);
            int initialCount = dekorativeCosmetic.CountProductsInList();
            dekorativeCosmetic.ClickOnBtnAddMoreProducts();
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
            int actualIndex = dekorativeCosmetic.GetNumberOfClicksOnArrowInSlider() + 1;

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
            decorativeCosmetic.SelectProduct(config["productAddToCart3"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.SelectProductImage(int.Parse(config["imgNumber"]));
            int index = productPage.GetActiveProductImageIndex();

            ClassicAssert.AreEqual(imgNumber, index, "The photo does not match the selected one");
        }

        [Test]
        public void SerializeTest()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetic = new DecorativeСosmeticsPage(driver);
            List<Product> products = decorativeCosmetic.GetProductsDetailsForSerialization();
            string json = JsonSerializer.Serialize(products);
            File.WriteAllText("Serialize.json", json);
        }
    }
}
