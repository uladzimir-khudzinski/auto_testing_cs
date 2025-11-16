using System;
using OpenQA.Selenium.Support.UI;

namespace Logging.Pages;

public class SearchResultsPage : BasePage
{
    private readonly string query;

    public SearchResultsPage(string query)
    {
        this.query = query;
    }

    public bool UrlContainsQuery()
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        var normalizedQuery = Uri.EscapeDataString(query.Trim()).Replace("%20", "+");
        return wait.Until(d => d.Url.Contains($"/?s={normalizedQuery}", StringComparison.OrdinalIgnoreCase));
    }
}
