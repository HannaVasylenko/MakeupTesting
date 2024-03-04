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
    public class DecorativeCosmeticsTests : BaseTest
    {
        [Test]
        public void VerifySelectCategory()
        {
            string title = "Декоративна косметика";
            InitPage initPage = new InitPage(driver);
            initPage.SelectCategory();
            DecorativeСosmeticsPage dekoratyvnaKosmetyka = new DecorativeСosmeticsPage(driver);
            string titleText = dekoratyvnaKosmetyka.GetCategoryTitleText();
            ClassicAssert.AreEqual(title, titleText, "The titles do not match");
        }
    }
}
