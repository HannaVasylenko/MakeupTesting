using MakeupTestingPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Legacy;
using NUnit.Framework;

namespace MakeupTestingTests
{
    public class BrandsTests : BaseTest
    {
        [Test]
        public void VerifySelectProductsByBrands()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string brandVariant = config["brandVariant"];

            Header header = new Header(driver);
            header.SelectBrandsPage();
            BrandsPage brandsPage = new BrandsPage(driver);
            brandsPage.SelectBrandVariant(config["brandVariant"]);
            List<string> brandsVariantsText = brandsPage.GetBrandText();
            foreach (var brandVariantText in brandsVariantsText)
            {
                StringAssert.StartsWith(brandVariant, brandVariantText, "The brand name starts with a different character");
            }
        }
    }
}
