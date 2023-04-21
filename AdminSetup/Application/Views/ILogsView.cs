namespace Core.Views;

public interface ILogsView : IViewBase
{
    public string[] LogFileNames { set; }
    public string SelectedLogFile { get; }
    public bool UiBlocked { get; set; }

    event EventHandler FetchLogFileEvent;

    void ShowAlert(string text);
    void SaveLogFile(string fileName, byte[] fileData);
}
