using log4net;
using Logging.TestData;
using OpenQA.Selenium;

namespace Logging.Pages;

public class AboutPage : BasePage
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(ContactPage));
    private readonly By contentTitle = By.XPath(
        "//div[@class='content']//h1[@class='subheader__title']"
    );

    public string CurrentUrl => Driver.Url;
    public string PageTitle => Driver.Title;

    public string GetContentTitle()
    {
        logger.Info("Retrieving content title from About page.");
        return Driver.FindElement(contentTitle).Text;
    }

    public bool IsAtExpectedPage()
    {
        logger.Info("Verifying if the About page is at the expected URL, title, and content title.");
        return CurrentUrl == EhuTestData.Urls.About
            && PageTitle == EhuTestData.ExpectedTitles.About
            && GetContentTitle() == EhuTestData.ExpectedTitles.About;
    }
}
