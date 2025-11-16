using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Logging.Drivers;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(BrowserType browserType)
    {
        return browserType switch
        {
            BrowserType.Firefox => BuildFirefoxDriver(),
            _ => BuildChromeDriver(),
        };
    }

    private static IWebDriver BuildChromeDriver()
    {
        var driver = new ChromeDriver();
        return ConfigureDriver(driver);
    }

    private static IWebDriver BuildFirefoxDriver()
    {
        var driver = new FirefoxDriver();
        return ConfigureDriver(driver);
    }

    private static IWebDriver ConfigureDriver(IWebDriver driver)
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        driver.Manage().Window.Maximize();
        return driver;
    }
}
