using Core;
using Core.Models;
using Core.Models.CreateAdmin;
using Core.Models.ResetAdminPassword;
using System.Net;
using System.Text.RegularExpressions;

namespace AdminSetup;

public partial class Main : Form
{
    private static readonly string ApiKey = Program.Configuration.GetSection("AppSettings:Key").Value;
    private static readonly string AdminsEndPoint = Program.Configuration.GetSection("AppSettings:Endpoints:Admins").Value;
    private static readonly string AdminPasswordEndPoint = Program.Configuration.GetSection("AppSettings:Endpoints:AdminPassword").Value;
    private static readonly Regex UsernameRegex = new("^[a-z|A-Z|\\d]+$");

    public Main()
    {
        InitializeComponent();
    }

    private async void CreateButton_Click(object sender, EventArgs e)
    {
        if (!IsValidationPassed()) return;

        ButtonToggle();

        var request = new CreateAdminRequest
        {
            Username = UsernameTB.Text //TODO VALIDATION
        };

        ResponseBag<CreateAdminResponse> response;
        try
        {
            response = await new RequestHandler(ApiKey)
                .SendRequest<CreateAdminRequest, CreateAdminResponse>(AdminsEndPoint, RequestMethod.POST, request);
        }
        catch (HttpRequestException)
        {
            MessageBox.Show("Failed to create connection", "Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            ButtonToggle();
            return;
        }

        switch (response.Code)
        {
            case HttpStatusCode.OK:
                if (MessageBox.Show($"Administrator was created\nPassword to enter the platform: {response.Body.TempPassword}", "Admin Setup",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Clipboard.SetText(response.Body.TempPassword);
                }
                break;

            case HttpStatusCode.Conflict:
                MessageBox.Show($"{request.Username} username already exists", "Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;

            case HttpStatusCode.Unauthorized:
                MessageBox.Show("Unauthorized request", "Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;

            default:
                MessageBox.Show($"Something goes wrong ({response.Code})", "Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
        }

        ButtonToggle();
    }

    private async void ResetButton_Click(object sender, EventArgs e)
    {
        if (!IsValidationPassed()) return;

        ButtonToggle();

        var request = new ResetAdminPasswordRequest
        {
            Username = UsernameTB.Text //TODO VALIDATION
        };

        ResponseBag<ResetAdminPasswordResponse> response;
        try
        {
            response = await new RequestHandler(ApiKey)
                .SendRequest<ResetAdminPasswordRequest, ResetAdminPasswordResponse>(AdminPasswordEndPoint, RequestMethod.PATCH, request);
        }
        catch (HttpRequestException)
        {
            MessageBox.Show("Failed to create connection", "Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ButtonToggle();
            return;
        }

        switch (response.Code)
        {
            case HttpStatusCode.OK:
                if (MessageBox.Show($"Password has been reset for {request.Username}\nNew password to enter the platform: {response.Body.TempPassword}", "Admin Setup",
                        MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Clipboard.SetText(response.Body.TempPassword);
                }
                break;

            case HttpStatusCode.NotFound:
                MessageBox.Show("Admin with such username doesn't exist", "Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;

            case HttpStatusCode.Unauthorized:
                MessageBox.Show("Unauthorized request", "Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;

            default:
                MessageBox.Show($"Something goes wrong ({response.Code})", "Admin Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
        }

        ButtonToggle();
    }

    private void ButtonToggle()
    {
        if (CreateButton.Enabled)
        {
            CreateButton.Enabled = ResetButton.Enabled = false;
        }
        else
        {
            CreateButton.Enabled = ResetButton.Enabled = true;
        }
    }

    private bool IsValidationPassed()
    {
        if (string.IsNullOrEmpty(UsernameTB.Text))
        {
            errorProvider1.SetError(UsernameTB, "Field is required");
            return false;
        }

        if (UsernameTB.Text.Length is < 5 or > 16)
        {
            errorProvider1.SetError(UsernameTB, "Valid length is [5-16] characters");
            return false;
        }

        if (!UsernameRegex.IsMatch(UsernameTB.Text))
        {
            errorProvider1.SetError(UsernameTB, "Invalid format (a-z A-Z or numeric)");
            return false;
        }

        return true;
    }
}