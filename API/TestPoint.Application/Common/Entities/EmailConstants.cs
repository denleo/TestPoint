namespace TestPoint.Application.Common.Entities;

public static class EmailConstants
{
    private static readonly string ProxyHost = Environment.GetEnvironmentVariable("PROXY_HOST")!;
    private static readonly string ProxyPort = Environment.GetEnvironmentVariable("PROXY_HTTPS_PORT")!;

    public static string GetEmailConfirmation(string name, string emailConfirmationToken)
    {
        var emailConfirmationUrl = $"https://{ProxyHost}:{ProxyPort}/api/user/email/verify/";

        return $"<h3><b>Hello, {name}</b></h3>" +
                   $"<div>Follow the link to complete email confirmation: <a href='{emailConfirmationUrl + emailConfirmationToken}'>confirm email</a></div>" +
                   "<div><b>Test Point System</b>, do not reply on this message.</div>";
    }

    public static string GetPasswordResetRequest(string name, string passwordResetToken)
    {
        var passwordResetUrl = $"https://{ProxyHost}:{ProxyPort}/api/user/password/reset/";

        return $"<h3><b>Hello, {name}</b></h3>" +
                   $"<div>The password reset request for your account was sent from the platform.</div>" +
                   $"<div>Click here to receive email with new password: <a href='{passwordResetUrl + passwordResetToken}'>reset password</a></div>" +
                   "<div><b>Test Point System</b>, do not reply on this message.</div>";
    }

    public static string GetResetedPasswordResponse(string name, string tempPassword)
    {
        return $"<h3><b>Hello, {name}</b></h3>" +
                   $"<div>Here is your new temporary password to enter the platform: <b>{tempPassword}</b></div>" +
                   "<div><b>Test Point System</b>, do not reply on this message.</div>";
    }

    public static string GetNewTestAssignmentNotification(string name, string testName)
    {
        var notPassedTestsUrl = $"https://{ProxyHost}:{ProxyPort}/tests";

        return $"<h3><b>Hello, {name}</b></h3>" +
                   $"<div>A new test was open to you: <b>{testName}</b></div>" +
                   $"<div>Click here to review all tests available to you: <a href='{notPassedTestsUrl}'>tests</a></div>" +
                   "<div><b>Test Point System</b>, do not reply on this message.</div>";
    }
}
