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
    public class DecorativeCosmeticsTests : BaseTest
    {
        [Test]
        public void VerifySelectCategory()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();

            string categoryTitleText = config["categoryTitleText"];
            InitPage initPage = new InitPage(driver);
            initPage.SelectCategory(config["category"]);
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

            InitPage initPage = new InitPage(driver);
            initPage.SelectCategory(config["category"]);
            DecorativeСosmeticsPage decorativeCosmetics = new DecorativeСosmeticsPage(driver);
            decorativeCosmetics.CheckFilters(config["nameOfBrand"], config["filterProductName"]);
            List<string> productTitles = decorativeCosmetics.GetProductTitleText();
            foreach (var productTitleText in productTitles)
            {
                StringAssert.Contains(nameOfBrand.ToLower(), productTitleText.ToLower(), $"The product name is missing in the title {productTitleText}");
                StringAssert.Contains(filterProductName.ToLower(), productTitleText.ToLower(), $"The product name is missing in the title {productTitleText}");
            }
        }
    }
}
