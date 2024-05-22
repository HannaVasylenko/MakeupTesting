using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace MakeupTestingTests
{
    public class ProductTests : BaseTest
    {
        [Test]
        public void VerifySelectProductByColor()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string productVariant = config["productVariant"];

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProduct(config["productAddToCart2"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.SelectProductVariants(config["productVariant"]);

            Assert.That(productPage.GetProductColorVariant(config["productVariant"]), Is.EqualTo(productVariant), "Another color is selected");
        }
    }
}
