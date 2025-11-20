using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;
using Logging.Drivers;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
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
    private DateTime testStartTime;

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        var log4netRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
        XmlConfigurator.Configure(log4netRepository, new FileInfo("log4net.config"));

        var runId = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var hierarchy = LogManager.GetRepository() as Hierarchy;
        var fileAppender = hierarchy.Root.GetAppender("FileAppender") as RollingFileAppender;
        var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        fileAppender.File = Path.Combine(assemblyDir, "Logs", $"test-run-{runId}.log");
        fileAppender.ActivateOptions();
    }

    [SetUp]
    public void SetUpDriver()
    {
        logger.Debug($"Setting up WebDriver for {browser} browser.");
        log4net.ThreadContext.Properties["TestName"] = TestContext.CurrentContext.Test.Name;
        testStartTime = DateTime.Now;
        Driver = DriverSingleton.Instance.GetDriver(browser);
        logger.Info($"Test '{TestContext.CurrentContext.Test.Name} started.");

    }

    [TearDown]
    public void TearDownDriver()
    {
        var testName = TestContext.CurrentContext.Test.Name;
        var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
        var message = TestContext.CurrentContext.Result.Message;
        var stackTrace = TestContext.CurrentContext.Result.StackTrace;
        var duration = DateTime.Now - testStartTime;

        switch (testStatus)
        {
            case TestStatus.Failed:
                var screenshotPath = TakeScreenshot(testName);
                logger.Error($"Test '{testName}' FAILED. \n\nError: {message}. \n\nStackTrace: {stackTrace}. \n\nScreenshot: {screenshotPath}");
                break;
            case TestStatus.Passed:
                logger.Info($"Test '{testName}' PASSED in {duration.TotalSeconds:F2} seconds.");
                break;
            default:
                logger.Info($"Test '{testName}' finished with status {testStatus} in {duration.TotalSeconds:F2} seconds.");
                break;

        }
        logger.Debug("Tearing down WebDriver.");
        log4net.ThreadContext.Properties.Remove("TestName");
        DriverSingleton.Instance.QuitDriver();
    }

    private string TakeScreenshot(string testName)
    {
        var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
        var screenshotsDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Screenshots");
        Directory.CreateDirectory(screenshotsDirectory);

        var screenshotFilePath = Path.Combine(screenshotsDirectory, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        screenshot.SaveAsFile(screenshotFilePath);

        return screenshotFilePath;
    }
}
