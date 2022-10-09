using Core;
using Core.Models;
using Core.Models.Api;
using Core.Models.Api.CreateAdmin;
using Core.Models.Api.ResetAdminPassword;
using Core.Services.Api;
using Core.Views;
using System.Net;

namespace Presenters;

public class AdminSetupPresenter : PresenterBase<IAdminSetupView>
{
    private readonly IAdminApiHandler _api;

    public AdminSetupPresenter(IAdminSetupView view, IAdminApiHandler adminApiHandler) : base(view)
    {
        _api = adminApiHandler;

        view.CreateEvent += OnCreate;
        view.ResetPasswordEvent += OnResetPassword;
    }

    private async void OnResetPassword(object? sender, EventArgs e)
    {
        if (!IsValidationPassed()) return;

        View.UiBlocked = true;

        var request = new ResetAdminPasswordRequest
        {
            Username = View.Username
        };

        ResponseBag<ResetAdminPasswordResponse> response;
        try
        {
            response = await _api.ResetAdminPassword(request);
        }
        catch (HttpRequestException)
        {
            View.ShowAlert("Failed to establish connection");
            View.UiBlocked = false;
            return;
        }

        switch (response.Code)
        {
            case HttpStatusCode.OK:
                View.ShowAlert($"Password has been reset for {request.Username}\nNew password to enter the platform: {response.Body.TempPassword}", response.Body.TempPassword);
                break;

            case HttpStatusCode.NotFound:
                View.ShowAlert($"Admin with {request.Username} username doesn't exist");
                break;

            case HttpStatusCode.Unauthorized:
                View.ShowAlert("Unauthorized request");
                break;

            default:
                View.ShowAlert($"Something goes wrong ({response.Code})");
                break;
        }

        View.UiBlocked = false;
    }

    private async void OnCreate(object? sender, EventArgs e)
    {
        if (!IsValidationPassed()) return;

        View.UiBlocked = true;

        var request = new CreateAdminRequest
        {
            Username = View.Username
        };

        ResponseBag<CreateAdminResponse> response;
        try
        {
            response = await _api.CreateNewAdmin(request);
        }
        catch (HttpRequestException)
        {
            View.ShowAlert("Failed to establish connection");
            View.UiBlocked = false;
            return;
        }

        switch (response.Code)
        {
            case HttpStatusCode.OK:
                View.ShowAlert($"Administrator was created\nPassword to enter the platform: {response.Body.TempPassword}", response.Body.TempPassword);
                break;

            case HttpStatusCode.Conflict:
                View.ShowAlert($"Admin with {request.Username} username already exists");
                break;

            case HttpStatusCode.Unauthorized:
                View.ShowAlert("Unauthorized request");
                break;

            default:
                View.ShowAlert($"Something goes wrong ({response.Code})");
                break;
        }

        View.UiBlocked = false;
    }

    private bool IsValidationPassed()
    {
        if (string.IsNullOrEmpty(View.Username))
        {
            View.ValidationError = "Field is required";
            return false;
        }

        if (View.Username.Length is < Constants.UsernameMinLength or > Constants.UsernameMaxLength)
        {
            View.ValidationError = "Valid length is [5-16] characters";
            return false;
        }

        if (!Constants.UsernameRegex.IsMatch(View.Username))
        {
            View.ValidationError = "Invalid format (a-z A-Z or numeric)";
            return false;
        }

        return true;
    }
}