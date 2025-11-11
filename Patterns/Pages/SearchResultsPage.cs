namespace Patterns.Pages;

public class SearchResultsPage : BasePage
{
    private readonly string query;

    public SearchResultsPage(string query)
    {
        this.query = query;
    }

    public bool UrlContainsQuery()
    {
        var normalizedQuery = query.Trim().Replace(" ", "+");
        return Driver.Url.Contains($"/?s={normalizedQuery}", StringComparison.OrdinalIgnoreCase);
    }
}
