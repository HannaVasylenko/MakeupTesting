using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Framework
{
    public class BrowsersList
    {
        public IWebDriver GetBrowserByName(BrowserEnum browser)
        {
            IWebDriver driver;

            switch (browser)
            {
                case BrowserEnum.Chrome:
                    driver = new ChromeDriver();
                    break;
                case BrowserEnum.FireFox:
                    string geckodriverPath = "E:/Git/geckodriver.exe";
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(geckodriverPath);
                    driver = new FirefoxDriver();
                    break;
                case BrowserEnum.Edge:
                    driver = new EdgeDriver();
                    break;
                default:
                    throw new Exception("You selected wrong browser");
            }
            return driver;
        }
    }
}
