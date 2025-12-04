using log4net;
using OpenQA.Selenium;

namespace EhuWebTests.Drivers;

public sealed class DriverSingleton
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(DriverSingleton));
    private static readonly DriverSingleton instance = new();
    private readonly ThreadLocal<IWebDriver?> driver = new();
    private readonly ThreadLocal<BrowserType?> driverBrowser = new();

    private DriverSingleton() { }

    public static DriverSingleton Instance => instance;

    public IWebDriver GetDriver(BrowserType browserType)
    {
        if (driver.Value == null)
        {
            logger.Info($"Creating new WebDriver for {browserType}.");
            driver.Value = DriverFactory.CreateDriver(browserType);
            driverBrowser.Value = browserType;
            return driver.Value;
        }

        if (driverBrowser.Value != browserType)
        {
            logger.Info($"Browser type changed. Quitting old driver and creating new one for {browserType}.");
            QuitDriver();
            driver.Value = DriverFactory.CreateDriver(browserType);
            driverBrowser.Value = browserType;
        }

        return driver.Value;
    }

    public IWebDriver CurrentDriver =>
        driver.Value ?? throw new InvalidOperationException("WebDriver not created.");

    public void QuitDriver()
    {
        if (driver.Value == null)
        {
            return;
        }

        logger.Info("Quitting WebDriver.");
        driver.Value.Quit();
        driver.Value.Dispose();
        driver.Value = null;
    }
}
