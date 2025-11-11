using OpenQA.Selenium;
using Patterns.TestData;

namespace Patterns.Pages;

public class AboutPage : BasePage
{
    private readonly By contentTitle = By.XPath(
        "//div[@class='content']//h1[@class='subheader__title']"
    );

    public string CurrentUrl => Driver.Url;
    public string PageTitle => Driver.Title;

    public string GetContentTitle()
    {
        return Driver.FindElement(contentTitle).Text;
    }

    public bool IsAtExpectedPage()
    {
        return CurrentUrl == EhuTestData.Urls.About
            && PageTitle == EhuTestData.ExpectedTitles.About
            && GetContentTitle() == EhuTestData.ExpectedTitles.About;
    }
}
