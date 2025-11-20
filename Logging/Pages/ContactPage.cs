using log4net;
using Logging.TestData;
using OpenQA.Selenium;

namespace Logging.Pages;

public class ContactPage : BasePage
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(ContactPage));
    private readonly By email = By.XPath($"//a[@href='mailto:{EhuTestData.Contacts.Email}']");
    private readonly By phoneLt = By.XPath($"//li[contains(., '{EhuTestData.Contacts.PhoneLt}')]");
    private readonly By phoneBy = By.XPath($"//li[contains(., '{EhuTestData.Contacts.PhoneBy}')]");
    private readonly By facebook = By.XPath($"//a[@href='{EhuTestData.Contacts.Facebook}']");
    private readonly By telegram = By.XPath($"//a[@href='{EhuTestData.Contacts.Telegram}']");
    private readonly By vk = By.XPath($"//a[@href='{EhuTestData.Contacts.Vk}']");

    public ContactPage OpenConactPage()
    {
        logger.Info($"Navigating to URL: {EhuTestData.Urls.Contact}");
        Driver.Navigate().GoToUrl(EhuTestData.Urls.Contact);
        return this;
    }

    public bool IsEmailVisible()
    {
        var isVisible = Driver.FindElement(email).Displayed;
        logger.Info($"Checking if Email is visible. Result: {isVisible}");
        return isVisible;
    }

    public bool IsPhoneLtVisible()
    {
        var isVisible = Driver.FindElement(phoneLt).Displayed;
        logger.Info($"Checking if LT Phone is visible. Result: {isVisible}");
        return isVisible;
    }

    public bool IsPhoneByVisible()
    {
        var isVisible = Driver.FindElement(phoneBy).Displayed;
        logger.Info($"Checking if BY Phone is visible. Result: {isVisible}");
        return isVisible;
    }

    public bool IsFacebookVisible()
    {
        var isVisible = Driver.FindElement(facebook).Displayed;
        logger.Info($"Checking if Facebook is visible. Result: {isVisible}");
        return isVisible;
    }

    public bool IsTelegramVisible()
    {
        var isVisible = Driver.FindElement(telegram).Displayed;
        logger.Info($"Checking if Telegram is visible. Result: {isVisible}");
        return isVisible;
    }

    public bool IsVkVisible()
    {
        var isVisible = Driver.FindElement(vk).Displayed;
        logger.Info($"Checking if VK is visible. Result: {isVisible}");
        return isVisible;
    }
}