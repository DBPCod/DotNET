namespace Frontend.Services;

public class ToastService
{
    public event Action<ToastMessage>? OnShow;
    
    public void ShowSuccess(string message, string? title = null)
    {
        Show(message, ToastType.Success, title ?? "Thành công");
    }
    
    public void ShowError(string message, string? title = null)
    {
        Show(message, ToastType.Error, title ?? "Lỗi");
    }
    
    public void ShowWarning(string message, string? title = null)
    {
        Show(message, ToastType.Warning, title ?? "Cảnh báo");
    }
    
    public void ShowInfo(string message, string? title = null)
    {
        Show(message, ToastType.Info, title ?? "Thông báo");
    }
    
    private void Show(string message, ToastType type, string title)
    {
        var toast = new ToastMessage
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Message = message,
            Type = type,
            Timestamp = DateTime.Now
        };
        
        OnShow?.Invoke(toast);
    }
}

public class ToastMessage
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Message { get; set; } = "";
    public ToastType Type { get; set; }
    public DateTime Timestamp { get; set; }
}

public enum ToastType
{
    Success,
    Error,
    Warning,
    Info
}