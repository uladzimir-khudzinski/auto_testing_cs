using OpenQA.Selenium;
using EhuWebTests.Drivers;

namespace EhuWebTests.Pages;

public abstract class BasePage
{
    protected IWebDriver Driver { get; }

    protected BasePage()
    {
        Driver = DriverSingleton.Instance.CurrentDriver;
    }
}
