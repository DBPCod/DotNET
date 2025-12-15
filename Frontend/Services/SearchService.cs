namespace Frontend.Services;

public class SearchService
{
    public event Action? OnSearchChanged;
    private string _searchQuery = string.Empty;

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            OnSearchChanged?.Invoke();
        }
    }

    public void ClearSearch()
    {
        _searchQuery = string.Empty;
        OnSearchChanged?.Invoke();
    }
}
