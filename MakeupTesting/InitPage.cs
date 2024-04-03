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

        /// <summary>
        /// Clicks on the scroll up arrow button.
        /// </summary>
        public void ClickOnScrollUpArrow() => WaitUntilWebElementExists(By.XPath("//div[@class='button-up']")).Click();

        /// <summary>
        /// Selects a subcategory based on the provided subcategory name.
        /// </summary>
        /// <param name="subCategory">The name of the subcategory to select.</param>
        public void SelectSubCategory(string subCategory) => WaitUntilWebElementExists(By.XPath($"//a[text()='{subCategory}']")).Click();

        /// <summary>
        /// Gets the title of the subcategory.
        /// </summary>
        /// <param name="titleSubCategory">The title of the subcategory to retrieve.</param>
        /// <returns>The title of the subcategory.</returns>
        public string GetSubCategoryTitle(string titleSubCategory) => WaitUntilWebElementExists(By.XPath($"//span[text()='{titleSubCategory}']")).Text;
    }
}
