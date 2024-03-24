using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeupTestingTests
{
    public class ProductTests : BaseTest
    {
        [Test]
        public void VerifySelectByProductColor()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string titleproductVariant = config["productVariant"];

            Header header = new Header(driver);
            header.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFiltersByNameAndTypeOfProduct(config["nameOfBrand"], config["filterProductName"]);
            decorativeCosmetics.SelectProductCard(config["productAddToCart2"]);
            ProductPage productPage = new ProductPage(driver);
            productPage.SelectProductVariants(config["productVariant"]);
            
            ClassicAssert.AreEqual(titleproductVariant, productPage.GetProductVariantText(config["productVariant"]), "Another color is selected");
        }
    }
    
}
