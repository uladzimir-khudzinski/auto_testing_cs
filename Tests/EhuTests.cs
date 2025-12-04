using Allure.NUnit;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;
using EhuWebTests.Drivers;
using EhuWebTests.Pages;
using EhuWebTests.TestData;
using NUnit.Framework;
using Shouldly;

namespace EhuWebTests.Tests;

[TestFixture(BrowserType.Chrome)]
[TestFixture(BrowserType.Firefox)]
[AllureNUnit]
[AllureSuite("EHU Website Tests")]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Category("EHU Website Tests")]
public class EhuTests : TestBase
{
    public EhuTests(BrowserType browser)
        : base(browser) { }

    [Test, Category("Navigation")]
    [AllureFeature("Navigation")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Verifies navigation to the About EHU page and checks URL, title and content.")]
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
    [AllureFeature("Search")]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureDescription("Verifies that search functionality works and URL contains query parameter.")]
    public void VerifySearchFunctionality()
    {
        var searchResultsPage = new HomePage().OpenHomePage().Search("study programs");
        searchResultsPage.UrlContainsQuery().ShouldBeTrue();
    }

    [Test, Category("Language")]
    [TestCase("lt")]
    [TestCase("be")]
    [TestCase("ru")]
    [AllureFeature("Language")]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureDescription("Verifies that language switching works correctly for different locales.")]
    public void VerifyLanguageChange(string languageCode)
    {
        var currentUrl = new HomePage().OpenHomePage().ChangeLanguage(languageCode);
        var expectedUrl = EhuTestData.Urls.Language(languageCode);
        currentUrl.ShouldBe(expectedUrl);
    }

    [Test, Category("Contacts")]
    [AllureFeature("Contacts")]
    [AllureSeverity(SeverityLevel.minor)]
    [AllureDescription("Verifies that contact page displays all required elements: email, phones, social links.")]
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
