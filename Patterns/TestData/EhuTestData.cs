namespace Patterns.TestData;

public static class EhuTestData
{
    public static class Urls
    {
        public const string Home = "https://en.ehuniversity.lt/";
        public const string About = "https://en.ehuniversity.lt/about/";
        public const string Contact = "https://en.ehuniversity.lt/research/projects/contact-us/";
        public static string Language(string code) => $"https://{code}.ehuniversity.lt/";
    }

    public static class ExpectedTitles
    {
        public const string About = "About";
    }

    public static class Contacts
    {
        public const string Email = "franciskscarynacr@gmail.com";
        public const string PhoneLt = "+370 68 771365";
        public const string PhoneBy = "+375 29 5781488";
        public const string Facebook = "https://www.facebook.com/groups/434978221124539/";
        public const string Telegram = "https://t.me/skaryna_cultural_route";
        public const string Vk = "https://vk.com/public203605228";
    }
}
