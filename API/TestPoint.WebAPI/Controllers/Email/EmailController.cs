using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestPoint.Application.Interfaces.Services;
using TestPoint.Application.Users.Commands.ConfirmEmail;
using TestPoint.Application.Users.Commands.ResetUserPassword;
using TestPoint.Application.Users.Commands.SendEmailConfirmation;
using TestPoint.Application.Users.Commands.SendForgotPasswordEmail;
using TestPoint.WebAPI.Models;

namespace TestPoint.WebAPI.Controllers.Email;

public class EmailController : BaseController
{
    private readonly IJwtService _jwtService;

    public EmailController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("session/user/email-verification"), Authorize(Roles = "User")]
    public async Task<IActionResult> SendEmailVerification()
    {
        var sendEmailConfirmationCommand = new SendEmailConfirmationCommand()
        {
            UserId = LoginId!.Value,
            EmailConfirmUrl = $"{Request.Scheme}://{Request.Host.Value}/api/user/email/verify/"
        };

        await Mediator.Send(sendEmailConfirmationCommand);
        return Accepted(value: "Verification email will be sent, please check your mailbox");
    }

    [AllowAnonymous]
    [HttpGet("user/email/verify/{token}")]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        List<Claim> claims;
        try
        {
            claims = _jwtService.ParseToken(token);
        }
        catch
        {
            return BadRequest("Invalid confirmation URL");
        }

        if (claims.Find(x => x.Type == "TokenType")?.Value != "EmailConfirmation")
        {
            return BadRequest("Invalid confirmation URL");
        }

        var confirmEmailCommand = new ConfirmEmailCommand
        {
            UserId = int.Parse(claims.First(c => c.Type == ClaimTypes.Sid).Value),
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

        return Ok("Email confirmed");
    }

    [AllowAnonymous]
    [HttpPost("user/password/forgot-password")]
    public async Task<IActionResult> SendForgotPasswordEmail([FromBody] UserForgotPasswordDto userForgotPassword)
    {
        var sendForgotPasswordEmailCommand = new SendForgotPasswordEmailCommand
        {
            Username = userForgotPassword.Username,
            PasswordResetUrl = $"{Request.Scheme}://{Request.Host.Value}/api/user/password/reset/"
        };

        await Mediator.Send(sendForgotPasswordEmailCommand);
        return Accepted(value: "Email with further instructions will be sent, please check your mailbox");
    }

    [AllowAnonymous]
    [HttpGet("user/password/reset/{token}")]
    public async Task<IActionResult> ResetUserPassword(string token)
    {
        List<Claim> claims;
        try
        {
            claims = _jwtService.ParseToken(token);
        }
        catch
        {
            return BadRequest("Invalid password reset URL");
        }

        if (claims.Find(x => x.Type == "TokenType")?.Value != "PasswordReset")
        {
            return BadRequest("Invalid password reset URL");
        }

        var resetUserPasswordCommand = new ResetUserPasswordCommand
        {
            UserId = int.Parse(claims.Find(x => x.Type == ClaimTypes.Sid)!.Value)
        };

        try
        {
            await Mediator.Send(resetUserPasswordCommand);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Accepted(value: "Email with new password will be sent, please check your mailbox");
    }
}