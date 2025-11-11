using OpenQA.Selenium;
using Patterns.Drivers;

namespace Patterns.Pages;

public abstract class BasePage
{
    protected IWebDriver Driver { get; }

    protected BasePage()
    {
        Driver = DriverSingleton.Instance.CurrentDriver;
    }
}
