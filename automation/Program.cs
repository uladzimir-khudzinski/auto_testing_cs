using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace automation
{
    public class Program
    {
        public static void VerifyNavigationToAboutEhuPage()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
            IWebElement aboutButton = driver.FindElement(By.XPath("//ul//a[text()='About']"));
            aboutButton.Click();

            if (driver.Url.Equals("https://en.ehuniversity.lt/about/"))
                Console.WriteLine("The URL is correct");
            else
                Console.WriteLine($"The URL is not correct: {driver.Url}");

            if (driver.Title.Equals("About"))
                Console.WriteLine("The Title is correct");
            else
                Console.WriteLine($"The Title is not correct: {driver.Title}");

            IWebElement contentTitle = driver.FindElement(By.XPath("//div[@class='content']//h1[@class='subheader__title']"));

            if (contentTitle.Text.Equals("About"))
                Console.WriteLine("The content title is correct");
            else
                Console.WriteLine($"The content title is not correct: {contentTitle.Text}");

            driver.Quit();
        }

        public static void VerifySearchFunctionality()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
            IWebElement seactrBar = driver.FindElement(By.ClassName("header-search"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(seactrBar).Perform();
            IWebElement searchField = driver.FindElement(By.XPath("//input[@name='s']"));
            searchField.SendKeys("study programs \n");

            if(driver.Url.Contains("/?s=study+programs"))
                Console.WriteLine("The URL is correct");
            else
                Console.WriteLine($"The URL is not correct: {driver.Url}");
            driver.Quit();
        }

        public static void VerifyLanguageChangeToLithuanian()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/");
            IWebElement languageSwithcer = driver.FindElement(By.ClassName("language-switcher"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(languageSwithcer).Perform();
            languageSwithcer.FindElement(By.XPath("//a[text()='lt']")).Click();
            
            if (driver.Url.Equals("https://lt.ehuniversity.lt/"))
                Console.WriteLine("The URL is correct");
            else
                Console.WriteLine($"The URL is not correct: {driver.Url}");
            driver.Quit();
        }

        public static void VerifyContactSectionElements()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://en.ehuniversity.lt/research/projects/contact-us/");

            IWebElement email = driver.FindElement(By.XPath("//a[@href='mailto:franciskscarynacr@gmail.com']"));
            IWebElement phoneLt = driver.FindElement(By.XPath("//li[contains(., '+370 68 771365')]"));
            IWebElement phoneBy = driver.FindElement(By.XPath("//li[contains(., '+375 29 5781488')]"));
            IWebElement facebook = driver.FindElement(By.XPath("//a[@href='https://www.facebook.com/groups/434978221124539/']"));
            IWebElement telegram = driver.FindElement(By.XPath("//a[@href='https://t.me/skaryna_cultural_route']"));
            IWebElement vk = driver.FindElement(By.XPath("//a[@href='https://vk.com/public203605228']"));

            if (email.Displayed)
                Console.WriteLine("Email is displayed");
            else
                Console.WriteLine("Email is not displayed");

            if (phoneLt.Displayed)
                Console.WriteLine("Phone LT is displayed");
            else
                Console.WriteLine("Phone LT is not displayed");

            if (phoneBy.Displayed)
                Console.WriteLine("Phone BY is displayed");
            else
                Console.WriteLine("Phone BY is not displayed");

            if (facebook.Displayed)
                Console.WriteLine("Facebook link is displayed");
            else
                Console.WriteLine("Facebook link is not displayed");

            if (telegram.Displayed)
                Console.WriteLine("Telegram link is displayed");
            else
                Console.WriteLine("Telegram link is not displayed");

            if (vk.Displayed)
                Console.WriteLine("VK link is displayed");
            else
                Console.WriteLine("VK link is not displayed");

            driver.Quit();
        }
    }
}
