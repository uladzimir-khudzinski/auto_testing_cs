using log4net;
using log4net.Config;
using Logging.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using System.Reflection;

namespace Logging.Tests;

public abstract class TestBase
{
    private readonly BrowserType browser;
    private static readonly ILog logger = LogManager.GetLogger(typeof(TestBase));

    protected TestBase(BrowserType browser)
    {
        this.browser = browser;
    }

    protected IWebDriver Driver { get; private set; } = null!;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        var log4netRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
        XmlConfigurator.Configure(log4netRepository, new FileInfo("log4net.config"));
    }

    [SetUp]
    public void SetUpDriver()
    {
        logger.Info($"Setting up WebDriver for {browser} browser.");
        Driver = DriverSingleton.Instance.GetDriver(browser);
    }

    [TearDown]
    public void TearDownDriver()
    {
        logger.Info("Tearing down WebDriver.");
        DriverSingleton.Instance.QuitDriver();
    }
}
