using NUnit.Framework;
using OpenQA.Selenium;
using Patterns.Drivers;

namespace Patterns.Tests;

public abstract class TestBase
{
    private readonly BrowserType browser;

    protected TestBase(BrowserType browser)
    {
        this.browser = browser;
    }

    protected IWebDriver Driver { get; private set; } = null!;

    [SetUp]
    public void SetUpDriver()
    {
        Driver = DriverSingleton.Instance.GetDriver(browser);
    }

    [TearDown]
    public void TearDownDriver()
    {
        DriverSingleton.Instance.QuitDriver();
    }
}
