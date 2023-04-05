using Core;
using Core.Models.Api;
using Core.Services.Api;
using Core.Views;
using System.Net;

namespace Presenters;

public class LogsPresenter : PresenterBase<ILogsView>
{
    private readonly ILogsApiHandler _api;

    public LogsPresenter(ILogsView view, ILogsApiHandler logsApiHandler) : base(view)
    {
        _api = logsApiHandler;

        view.Load += OnViewLoad;
        view.FetchLogFileEvent += FetchLogFile;
    }

    private async void OnViewLoad(object? sender, EventArgs e)
    {
        ResponseBag<string[]>? response;
        try
        {
            response = await _api.GetLogFileNames();
        }
        catch (HttpRequestException)
        {
            View.ShowAlert("Failed to establish connection");
            return;
        }

        if (response!.Code == HttpStatusCode.OK)
        {
            View.LogFileNames = response.Body;
        }
        else
        {
            View.ShowAlert($"Something goes wrong ({response.Code})");
        }
    }

    private async void FetchLogFile(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(View.SelectedLogFile))
        {
            View.ShowAlert("Please select the file first");
            return;
        }

        View.UiBlocked = true;

        ResponseBag<byte[]>? response;
        try
        {
            response = await _api.GetLogFile(View.SelectedLogFile);
        }
        catch (HttpRequestException)
        {
            View.ShowAlert("Failed to establish connection");
            View.UiBlocked = false;
            return;
        }

        if (response!.Code == HttpStatusCode.OK)
        {
            if (response!.Body!.Length > 0)
            {
                View.SaveLogFile(View.SelectedLogFile, response.Body);
            }
            else
            {
                View.ShowAlert("This file is empty");
            }
        }
        else
        {
            View.ShowAlert($"Something goes wrong ({response.Code})");
        }

        View.UiBlocked = false;
    }
}
