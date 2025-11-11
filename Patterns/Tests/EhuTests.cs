using Patterns.Drivers;
using Patterns.Pages;
using Patterns.TestData;

namespace Patterns.Tests;

[TestFixture(BrowserType.Chrome)]
[TestFixture(BrowserType.Firefox)]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Category("EHU Website Tests")]
public class EhuTests : TestBase
{
    public EhuTests(BrowserType browser)
        : base(browser) { }

    [Test, Category("Navigation")]
    public void VerifyNavigationToAboutEhuPage()
    {
        var aboutPage = new HomePage().OpenHomePage().GoToAboutPage();
        Assert.That(
            aboutPage.CurrentUrl,
            Is.EqualTo(EhuTestData.Urls.About),
            "The URL is not correct"
        );
        Assert.That(
            aboutPage.PageTitle,
            Is.EqualTo(EhuTestData.ExpectedTitles.About),
            "The Title is not correct"
        );
        Assert.That(
            aboutPage.GetContentTitle(),
            Is.EqualTo(EhuTestData.ExpectedTitles.About),
            "The content title is not correct"
        );
    }

    [Test, Category("Search")]
    public void VerifySearchFunctionality()
    {
        var searchResultsPage = new HomePage().OpenHomePage().Search("study programs");
        Assert.That(searchResultsPage.UrlContainsQuery(), Is.True, "The URL is not correct");
    }

    [Test, Category("Language")]
    [TestCase("lt")]
    [TestCase("be")]
    [TestCase("ru")]
    public void VerifyLanguageChange(string languageCode)
    {
        var currentUrl = new HomePage().OpenHomePage().ChangeLanguage(languageCode);
        var expectedUrl = EhuTestData.Urls.Language(languageCode);
        Assert.That(currentUrl, Is.EqualTo(expectedUrl), "The URL is not correct");
    }

    [Test, Category("Contacts")]
    public void VerifyContactSectionElements()
    {
        var contactPage = new ContactPage().OpenConactPage();

        Assert.Multiple(() =>
        {
            Assert.That(contactPage.IsEmailVisible(), "Email is not displayed");
            Assert.That(contactPage.IsPhoneLtVisible(), "Phone LT is not displayed");
            Assert.That(contactPage.IsPhoneByVisible(), "Phone BY is not displayed");
            Assert.That(contactPage.IsFacebookVisible(), "Facebook link is not displayed");
            Assert.That(contactPage.IsTelegramVisible(), "Telegram link is not displayed");
            Assert.That(contactPage.IsVkVisible(), "VK link is not displayed");
        });
    }
}
