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
    public class BrandsTests : BaseTest
    {
        [Test]
        public void VerifyBrandsVariantsText()
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
