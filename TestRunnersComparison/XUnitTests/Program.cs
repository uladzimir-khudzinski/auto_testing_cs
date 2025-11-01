
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace XUnitTests;

public abstract class ChromeTestBase : IDisposable
{
    protected IWebDriver Driver { get; }

    protected ChromeTestBase()
    {
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
    }

    public void Dispose()
    {
        Driver.Quit();
    }
}

public class NavigationTests : ChromeTestBase
{
    [Fact]
    [Trait("Category", "Navigation")]
    public void VerifyNavigationToAboutEhuPage()
    {
        Driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
        var aboutButton = Driver.FindElement(By.XPath("//ul//a[text()='About']"));
        aboutButton.Click();
        Assert.True(Driver.Url.Equals("https://en.ehuniversity.lt/about/"), "The URL is not correct");
        Assert.True(Driver.Title.Equals("About"), "The Title is not correct");
        var contentTitle = Driver.FindElement(By.XPath("//div[@class='content']//h1[@class='subheader__title']"));
        Assert.True(contentTitle.Text.Equals("About"), "The content title is not correct");
    }
}

public class SearchTests : ChromeTestBase
{
    [Fact]
    [Trait("Category", "Search")]
    public void VerifySearchFunctionality()
    {
        Driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
        var searchBar = Driver.FindElement(By.ClassName("header-search"));
        var actions = new Actions(Driver);
        actions.MoveToElement(searchBar).Perform();
        var searchField = Driver.FindElement(By.XPath("//input[@name='s']"));
        searchField.SendKeys("study programs" + Keys.Enter);
        Assert.True(Driver.Url.Contains("/?s=study+programs"), "The URL is not correct");
    }
}

public class LanguageTests : ChromeTestBase
{
    [Theory]
    [InlineData("lt")]
    [InlineData("ru")]
    [InlineData("be")]
    [Trait("Category", "Language")]
    public void VerifyLanguageChange(string languageCode)
    {
        Driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
        var languageSwitcher = Driver.FindElement(By.ClassName("language-switcher"));
        var actions = new Actions(Driver);
        actions.MoveToElement(languageSwitcher).Perform();
        var locator = $"//a[text()='{languageCode}']";
        languageSwitcher.FindElement(By.XPath(locator)).Click();
        var siteUrl = $"https://{languageCode}.ehuniversity.lt/";
        Assert.True(Driver.Url.Equals(siteUrl), "The URL is not correct");
    }
}

public class ContactTests : ChromeTestBase
{
    [Fact]
    [Trait("Cat egory", "Contacts")]
    public void VerifyContactSectionElements()
    {
        Driver.Navigate().GoToUrl("https://en.ehuniversity.lt/research/projects/contact-us/");
        var email = Driver.FindElement(By.XPath("//a[@href='mailto:franciskscarynacr@gmail.com']"));
        var phoneLt = Driver.FindElement(By.XPath("//li[contains(., '+370 68 771365')]"));
        var phoneBy = Driver.FindElement(By.XPath("//li[contains(., '+375 29 5781488')]"));
        var facebook = Driver.FindElement(By.XPath("//a[@href='https://www.facebook.com/groups/434978221124539/']"));
        var telegram = Driver.FindElement(By.XPath("//a[@href='https://t.me/skaryna_cultural_route']"));
        var vk = Driver.FindElement(By.XPath("//a[@href='https://vk.com/public203605228']"));

        Assert.Multiple(() =>
        {
            Assert.True(email.Displayed, "Email is not displayed");
            Assert.True(phoneLt.Displayed, "Phone LT is not displayed");
            Assert.True(phoneBy.Displayed, "Phone BY is not displayed");
            Assert.True(facebook.Displayed, "Facebook link is not displayed");
            Assert.True(telegram.Displayed, "Telegram link is not displayed");
            Assert.True(vk.Displayed, "VK link is not displayed");
        });
    }
}
