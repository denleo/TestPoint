namespace Core.Views;

public interface IAdminSetupView : IViewBase
{
    public string Username { get; }
    public string ValidationError { set; }
    public bool UiBlocked { get; set; }

    event EventHandler CreateEvent;
    event EventHandler ResetPasswordEvent;

    void ShowAlert(string text, string? clipboardCopy = null);
}