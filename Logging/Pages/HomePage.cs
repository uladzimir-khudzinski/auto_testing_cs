using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Logging.TestData;
using log4net;

namespace Logging.Pages;

public class HomePage : BasePage 
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(HomePage));
    private readonly By aboutLink = By.XPath("//ul//a[text()='About']");
    private readonly By searchBar = By.ClassName("header-search");
    private readonly By searchField = By.XPath("//input[@name='s']");
    private readonly By languageSwitcher = By.ClassName("language-switcher");

    public HomePage OpenHomePage()
    {
        logger.Info($"Navigating to Home Page - {EhuTestData.Urls.Home}");
        Driver.Navigate().GoToUrl(EhuTestData.Urls.Home);
        return this;
    }

    public AboutPage GoToAboutPage()
    {
        logger.Info("Clicking 'About' link to navigate to About page.");
        Driver.FindElement(aboutLink).Click();
        return new AboutPage();
    }

    public SearchResultsPage Search(string query)
    {
        logger.Info($"Performing search for query: {query}");
        new Actions(Driver).MoveToElement(Driver.FindElement(searchBar)).Perform();
        var searchInput = Driver.FindElement(searchField);
        searchInput.Clear();
        searchInput.SendKeys(query + Keys.Enter);
        return new SearchResultsPage(query);
    }

    public string ChangeLanguage(string languageCode)
    {
        logger.Info($"Changing website language to: {languageCode}");
        var switcher = Driver.FindElement(languageSwitcher);
        new Actions(Driver).MoveToElement(switcher).Perform();
        var option = switcher.FindElement(By.XPath($"//a[text()='{languageCode}']"));
        option.Click();
        return Driver.Url;
    }
}
