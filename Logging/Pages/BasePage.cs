using OpenQA.Selenium;
using Logging.Drivers;

namespace Logging.Pages;

public abstract class BasePage
{
    protected IWebDriver Driver { get; }

    protected BasePage()
    {
        Driver = DriverSingleton.Instance.CurrentDriver;
    }
}
