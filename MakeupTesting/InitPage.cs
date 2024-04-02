using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    /// <summary>
    /// This class represents the Main page, which starts testing the website.
    /// </summary>
    public class InitPage : BasePage
    {
        public InitPage(IWebDriver driver) : base(driver) {}

        public void ClickOnScrollUpArrow() => WaitUntilWebElementExists(By.XPath("//div[@class='button-up']")).Click();

        public void SelectSubCategory(string subCategory) => WaitUntilWebElementExists(By.XPath($"//a[text()='{subCategory}']")).Click();

        public string GetSubCategoryTitle(string titleSubCategory) => WaitUntilWebElementExists(By.XPath($"//span[text()='{titleSubCategory}']")).Text;
    }
}
