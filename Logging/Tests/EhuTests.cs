using Logging.Drivers;
using Logging.Pages;
using Logging.TestData;
using NUnit.Framework;
using Shouldly;

namespace Logging.Tests;

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
        aboutPage.ShouldSatisfyAllConditions(
            () => aboutPage.CurrentUrl.ShouldBe(EhuTestData.Urls.About),
            () => aboutPage.PageTitle.ShouldBe(EhuTestData.ExpectedTitles.About),
            () => aboutPage.GetContentTitle().ShouldBe(EhuTestData.ExpectedTitles.About)
        );
    }

    [Test, Category("Search")]
    public void VerifySearchFunctionality()
    {
        var searchResultsPage = new HomePage().OpenHomePage().Search("study programs");
        searchResultsPage.UrlContainsQuery().ShouldBeTrue();
    }

    [Test, Category("Language")]
    [TestCase("lt")]
    [TestCase("be")]
    [TestCase("ru")]
    public void VerifyLanguageChange(string languageCode)
    {
        var currentUrl = new HomePage().OpenHomePage().ChangeLanguage(languageCode);
        var expectedUrl = EhuTestData.Urls.Language(languageCode);
        currentUrl.ShouldBe(expectedUrl);
    }

    [Test, Category("Contacts")]
    public void VerifyContactSectionElements()
    {
        var contactPage = new ContactPage().OpenConactPage();
        contactPage.ShouldSatisfyAllConditions(
            p => p.IsEmailVisible().ShouldBeTrue(),
            p => p.IsPhoneLtVisible().ShouldBeTrue(),
            p => p.IsPhoneByVisible().ShouldBeTrue(),
            p => p.IsFacebookVisible().ShouldBeTrue(),
            p => p.IsVkVisible().ShouldBeTrue()
            );
    }
}
