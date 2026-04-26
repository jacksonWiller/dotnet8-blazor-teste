namespace GoodHamburger.Web.Services;

public enum ToastType { Success, Error, Warning, Info }

public class ToastMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Message { get; set; } = string.Empty;
    public ToastType Type { get; set; }
}

public interface IToastService
{
    event Action<ToastMessage>? OnShow;
    void ShowSuccess(string message);
    void ShowError(string message);
    void ShowWarning(string message);
    void ShowInfo(string message);
}

public class ToastService : IToastService
{
    public event Action<ToastMessage>? OnShow;

    public void ShowSuccess(string message) => Show(message, ToastType.Success);
    public void ShowError(string message)   => Show(message, ToastType.Error);
    public void ShowWarning(string message) => Show(message, ToastType.Warning);
    public void ShowInfo(string message)    => Show(message, ToastType.Info);

    private void Show(string message, ToastType type)
    {
        OnShow?.Invoke(new ToastMessage { Message = message, Type = type });
    }
}
