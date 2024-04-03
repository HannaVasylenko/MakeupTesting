using MakeupTesting;
using OpenQA.Selenium;

namespace MakeupTestingPageObjects
{
    /// <summary>
    /// The class is an page-element located at the top of each page.
    /// </summary>
    public class Header : BasePage
    {
        public Header(IWebDriver driver) : base(driver) {}

        /// <summary>
        /// Clicks on the Beauty Club link, redirecting to the appropriate page.
        /// </summary>
        public void SelectBeautyClub() => WaitUntilWebElementExists(By.XPath("//a[@class='header-top-list__link bc-about-link']")).Click();

        /// <summary>
        /// Clicks on the Brands link, redirecting to the appropriate page.
        /// </summary>
        public void SelectBrandsPage() => WaitUntilWebElementExists(By.XPath("//a[text()='Бренди']")).Click();

        /// <summary>
        /// Clicks on the Delivery link, redirecting to the appropriate page.
        /// </summary>
        public void OpenDeliveryPage() => WaitUntilWebElementExists(By.XPath("//li[@class='header-top-list__item']/a[text()='Доставка та Оплата']")).Click();

        /// <summary>
        /// Switches the page language to russian.
        /// </summary>
        public void SwitchLanguageToru() => WaitUntilWebElementExists(By.XPath("//header//a[text()='Рус']")).Click();

        /// <summary>
        /// Switches the page language to Ukrainian.
        /// </summary>
        public void SwitchLanguageToUA() => WaitUntilWebElementExists(By.XPath("//header//a[text()='Укр']")).Click();

        /// <summary>
        /// Selects a category Decorative cosmetics.
        /// </summary>
        /// <param name="category">The category of decorative cosmetics to select.</param>
        public void SelectCategory(string category) => GetDecorativeСosmeticsElement(category).Click();

        /// <summary>
        /// Clicks on the search field to initiate a search.
        /// </summary>
        public void ClickOnSearchField() => WaitUntilWebElementExists(By.XPath("//div[@data-popup-handler='search']")).Click();

        /// <summary>
        /// Gets the web element corresponding to a category Decorative cosmetics.
        /// </summary>
        /// <param name="category">The category of decorative cosmetics to get the web element for.</param>
        /// <returns>The web element category Decorative cosmetics.</returns>
        public IWebElement GetDecorativeСosmeticsElement(string category) => WaitUntilWebElementExists(By.XPath($"//a[text()='{category}']"));

        /// <summary>
        /// Inputs the provided text (product name) into the search field and submits the search.
        /// </summary>
        /// <param name="text">The text (product name) to be entered into the search field.</param>
        public void InputProductName(string text)
        {
            IWebElement txtSearch = WaitUntilWebElementExists(By.XPath("//input[@itemprop='query-input']"));
            txtSearch.SendKeys(text);
            txtSearch.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Retrieves the hint features element.
        /// </summary>
        /// <returns>The web element representing the hint features.</returns>
        public IWebElement GetHintFeatures() => WaitUntilWebElementExists(By.XPath("//span[text()='Безкоштовна доставка по Україні!']"));

        /// <summary>
        /// Retrieves the text associated with the hint features element.
        /// </summary>
        /// <returns>The text associated with the hint features element.</returns>
        public string GetHintText() => WaitUntilWebElementExists(By.XPath("//span[text()='Безкоштовна доставка по Україні!']/parent::*")).GetAttribute("data-text");
    }
}
