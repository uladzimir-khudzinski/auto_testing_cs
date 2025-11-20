using log4net;
using OpenQA.Selenium.Support.UI;
using System;

namespace Logging.Pages;

public class SearchResultsPage : BasePage
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(ContactPage));
    private readonly string query;

    public SearchResultsPage(string query)
    {
        this.query = query;
    }

    public bool UrlContainsQuery()
    {
        logger.Info($"Verifying if the URL contains the search query: {query}");
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        var normalizedQuery = Uri.EscapeDataString(query.Trim()).Replace("%20", "+");
        return wait.Until(d => d.Url.Contains($"/?s={normalizedQuery}", StringComparison.OrdinalIgnoreCase));
    }
}
