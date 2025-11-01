using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace NUnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Category("EHU Website Tests")]
    public class Program
    {
        private ChromeDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();
        }

        [Test, Category("Navigation")]
        public void VerifyNavigationToAboutEhuPage()
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
            var aboutButton = driver.FindElement(By.XPath("//ul//a[text()='About']"));
            aboutButton.Click();
            Assert.That(driver.Url, Is.EqualTo("https://en.ehuniversity.lt/about/"), "The URL is not correct");
            Assert.That(driver.Title, Is.EqualTo("About"), "The Title is not correct");
            var contentTitle = driver.FindElement(By.XPath("//div[@class='content']//h1[@class='subheader__title']"));
            Assert.That(contentTitle.Text, Is.EqualTo("About"), "The content title is not correct");
        }

        [Test, Category("Search")]
        public void VerifySearchFunctionality()
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
            var searchBar = driver.FindElement(By.ClassName("header-search"));
            var actions = new Actions(driver);
            actions.MoveToElement(searchBar).Perform();
            var searchField = driver.FindElement(By.XPath("//input[@name='s']"));
            searchField.SendKeys("study programs" + Keys.Enter);
            Assert.That(driver.Url, Does.Contain("/?s=study+programs"), "The URL is not correct");
        }

        [Test, Category("Language")]
        [TestCase("lt")]
        [TestCase("be")]
        [TestCase("ru")]
        public void VerifyLanguageChange(string languageCode)
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
            var languageSwitcher = driver.FindElement(By.ClassName("language-switcher"));
            var actions = new Actions(driver);
            actions.MoveToElement(languageSwitcher).Perform();
            var locator = $"//a[text()='{languageCode}']";
            languageSwitcher.FindElement(By.XPath(locator)).Click();
            var siteUrl = $"https://{languageCode}.ehuniversity.lt/";
            Assert.That(driver.Url, Is.EqualTo(siteUrl), "The URL is not correct");
        }

        [Test, Category("Contacts")]
        public void VerifyContactSectionElements()
        {
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/research/projects/contact-us/");
            var email = driver.FindElement(By.XPath("//a[@href='mailto:franciskscarynacr@gmail.com']"));
            var phoneLt = driver.FindElement(By.XPath("//li[contains(., '+370 68 771365')]"));
            var phoneBy = driver.FindElement(By.XPath("//li[contains(., '+375 29 5781488')]"));
            var facebook = driver.FindElement(By.XPath("//a[@href='https://www.facebook.com/groups/434978221124539/']"));
            var telegram = driver.FindElement(By.XPath("//a[@href='https://t.me/skaryna_cultural_route']"));
            var vk = driver.FindElement(By.XPath("//a[@href='https://vk.com/public203605228']"));

            Assert.Multiple(() =>
            {
                Assert.That(email.Displayed, "Email is not displayed");
                Assert.That(phoneLt.Displayed, "Phone LT is not displayed");
                Assert.That(phoneBy.Displayed, "Phone BY is not displayed");
                Assert.That(facebook.Displayed, "Facebook link is not displayed");
                Assert.That(telegram.Displayed, "Telegram link is not displayed");
                Assert.That(vk.Displayed, "VK link is not displayed");
            });
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
