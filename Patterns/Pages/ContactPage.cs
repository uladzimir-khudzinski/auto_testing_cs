using OpenQA.Selenium;
using Patterns.TestData;

namespace Patterns.Pages;

public class ContactPage : BasePage
{
    private readonly By email = By.XPath($"//a[@href='mailto:{EhuTestData.Contacts.Email}']");
    private readonly By phoneLt = By.XPath($"//li[contains(., '{EhuTestData.Contacts.PhoneLt}')]");
    private readonly By phoneBy = By.XPath($"//li[contains(., '{EhuTestData.Contacts.PhoneBy}')]");
    private readonly By facebook = By.XPath($"//a[@href='{EhuTestData.Contacts.Facebook}']");
    private readonly By telegram = By.XPath($"//a[@href='{EhuTestData.Contacts.Telegram}']");
    private readonly By vk = By.XPath($"//a[@href='{EhuTestData.Contacts.Vk}']");

    public ContactPage OpenConactPage()
    {
        Driver.Navigate().GoToUrl(EhuTestData.Urls.Contact);
        return this;
    }

    public bool IsEmailVisible() => Driver.FindElement(email).Displayed;

    public bool IsPhoneLtVisible() => Driver.FindElement(phoneLt).Displayed;

    public bool IsPhoneByVisible() => Driver.FindElement(phoneBy).Displayed;

    public bool IsFacebookVisible() => Driver.FindElement(facebook).Displayed;

    public bool IsTelegramVisible() => Driver.FindElement(telegram).Displayed;

    public bool IsVkVisible() => Driver.FindElement(vk).Displayed;
}
