using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using TestPoint.Application.Interfaces.Services;
using TestPoint.Application.Users.Commands.ConfirmEmail;
using TestPoint.Application.Users.Commands.ResetUserPassword;
using TestPoint.Application.Users.Commands.SendEmailConfirmation;
using TestPoint.Application.Users.Commands.SendForgotPasswordEmail;
using TestPoint.WebAPI.Models.User;

namespace TestPoint.WebAPI.Controllers.Email;

public class EmailController : BaseController
{
    private readonly IJwtService _jwtService;
    private readonly string? proxyHost, proxyPort;

    public EmailController(IJwtService jwtService)
    {
        _jwtService = jwtService;
        proxyHost = Environment.GetEnvironmentVariable("PROXY_HOST");
        proxyPort = Environment.GetEnvironmentVariable("PROXY_HTTPS_PORT");

        if (proxyPort is null || proxyHost is null)
        {
            throw new ArgumentNullException("Proxy enviroment variables was null");
        }
    }

    [SwaggerOperation(Summary = "Send verification email for the current user (roles:user)")]
    [HttpPost("session/user/email-verification"), Authorize(Roles = "User")]
    public async Task<IActionResult> SendEmailVerification()
    {
        var sendEmailConfirmationCommand = new SendEmailConfirmationCommand()
        {
            UserId = LoginId!.Value,
            EmailConfirmUrl = $"https://{proxyHost}:{proxyPort}/api/user/email/verify/"
        };

        await Mediator.Send(sendEmailConfirmationCommand);
        return Accepted(value: "Verification email will be sent, please check your mailbox.");
    }

    [SwaggerOperation(Summary = "Confirm user email link")]
    [HttpGet("user/email/verify/{token}"), AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        List<Claim> claims;
        try
        {
            claims = _jwtService.ParseToken(token);
        }
        catch
        {
            return BadRequest("Invalid confirmation URL.");
        }

        if (claims.Find(x => x.Type == "TokenType")?.Value != "EmailConfirmation")
        {
            return BadRequest("Invalid confirmation URL.");
        }

        var confirmEmailCommand = new ConfirmEmailCommand
        {
            UserId = Guid.Parse(claims.First(c => c.Type == ClaimTypes.Sid).Value),
            EmailForConfirmation = claims.First(c => c.Type == ClaimTypes.Email).Value
        };

        try
        {
            await Mediator.Send(confirmEmailCommand);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Email confirmed, this page can be closed.");
    }

    [SwaggerOperation(Summary = "Send forgot password email for the user")]
    [HttpPost("user/password/forgot-password"), AllowAnonymous]
    public async Task<IActionResult> SendForgotPasswordEmail([FromBody] UserForgotPasswordDto userForgotPassword)
    {
        var sendForgotPasswordEmailCommand = new SendForgotPasswordEmailCommand
        {
            Username = userForgotPassword.Username,
            PasswordResetUrl = $"https://{proxyHost}:{proxyPort}/api/user/password/reset/"
        };

        await Mediator.Send(sendForgotPasswordEmailCommand);
        return Accepted(value: "Email with further instructions will be sent, please check your mailbox.");
    }

    [SwaggerOperation(Summary = "Reset user password link")]
    [HttpGet("user/password/reset/{token}"), AllowAnonymous]
    public async Task<IActionResult> ResetUserPassword(string token)
    {
        List<Claim> claims;
        try
        {
            claims = _jwtService.ParseToken(token);
        }
        catch
        {
            return BadRequest("Invalid password reset URL.");
        }

        if (claims.Find(x => x.Type == "TokenType")?.Value != "PasswordReset")
        {
            return BadRequest("Invalid password reset URL.");
        }

        var resetUserPasswordCommand = new ResetUserPasswordCommand
        {
            UserId = Guid.Parse(claims.Find(x => x.Type == ClaimTypes.Sid)!.Value)
        };

        try
        {
            await Mediator.Send(resetUserPasswordCommand);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Accepted(value: "Email with a new password will be sent, please check your mailbox.");
    }
}