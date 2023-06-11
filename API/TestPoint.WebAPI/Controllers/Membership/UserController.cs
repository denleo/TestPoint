using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TestPoint.Application.Users;
using TestPoint.Application.Users.Commands.BindGoogleAccount;
using TestPoint.Application.Users.Commands.ChangeAvatar;
using TestPoint.Application.Users.Commands.ChangeContactInfo;
using TestPoint.Application.Users.Commands.ChangePassword;
using TestPoint.Application.Users.Commands.CreateUser;
using TestPoint.Application.Users.Commands.UnbindGoogleAccount;
using TestPoint.Application.Users.Queries.FilterUsers;
using TestPoint.Application.Users.Queries.GetCurrentUser;
using TestPoint.WebAPI.HttpClients.Google;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;
using TestPoint.WebAPI.Models.User;

namespace TestPoint.WebAPI.Controllers.Membership;

public class UserController : BaseController
{
    private readonly GoogleApiService _googleApi;

    public UserController(GoogleApiService googleApiService)
    {
        _googleApi = googleApiService;
    }

    [SwaggerOperation(Summary = "Create a new user")]
    [HttpPost("users"), AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] UserDto newUser)
    {
        var createUserCommand = new CreateUserCommand
        {
            Username = newUser.Username,
            Password = newUser.Password,
            Email = newUser.Email,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName
        };

        await Mediator.Send(createUserCommand);

        return Ok();
    }

    [SwaggerOperation(Summary = "Get current user data (roles:user)")]
    [HttpGet("session/user"), Authorize(Roles = "User")]
    public async Task<ActionResult<GetCurrentUserResponse>> GetCurrentUser()
    {
        var getCurrentUserQuery = new GetCurrentUserQuery
        {
            UserId = LoginId!.Value
        };

        var userData = await Mediator.Send(getCurrentUserQuery);
        return userData;
    }

    [SwaggerOperation(Summary = "Change current user password (roles:user)")]
    [HttpPatch("session/user/password"), Authorize(Roles = "User")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var changeUserPasswordCommand = new ChangeUserPasswordCommand
        {
            UserId = LoginId!.Value,
            OldPassword = changePasswordDto.OldPassword,
            NewPassword = changePasswordDto.NewPassword
        };

        await Mediator.Send(changeUserPasswordCommand);
        return Ok();
    }

    [DisableRequestSizeLimit]
    [SwaggerOperation(Summary = "Change current user avatar image (roles:user)")]
    [HttpPatch("session/user/avatar"), Authorize(Roles = "User")]
    public async Task<IActionResult> ChangeUserAvatar([FromBody] string base64Avatar)
    {
        var changeUserAvatarCommand = new ChangeUserAvatarCommand
        {
            UserId = LoginId!.Value,
            Base64Avatar = base64Avatar
        };

        await Mediator.Send(changeUserAvatarCommand);
        return Ok();
    }

    [SwaggerOperation(Summary = "Change current user contact information (roles:user)")]
    [HttpPatch("session/user/contactinfo"), Authorize(Roles = "User")]
    public async Task<IActionResult> ChangeUserContactInfo([FromBody] UserContactInfoDto userContactInfoDto)
    {
        var changeContactInfoCommand = new ChangeContactInfoCommand
        {
            UserId = LoginId!.Value,
            FirstName = userContactInfoDto.FirstName,
            LastName = userContactInfoDto.LastName,
            Email = userContactInfoDto.Email
        };

        await Mediator.Send(changeContactInfoCommand);
        return Ok();
    }

    [SwaggerOperation(Summary = "Get users by filter (roles:admin)")]
    [HttpGet("users"), Authorize(Roles = "Administrator")]
    public async Task<ActionResult<List<UserInformation>>> FilterUsers([FromQuery] string filter)
    {
        var filterUsersQuery = new FilterUsersQuery
        {
            FilterParameter = filter
        };

        var users = await Mediator.Send(filterUsersQuery);
        return users;
    }

    [SwaggerOperation(Summary = "Bind google account for the current user (roles:user)")]
    [HttpPut("session/user/bind-google"), Authorize(Roles = "User")]
    public async Task<IActionResult> BindGoogleAccount([FromBody] string googleToken)
    {
        var userInfo = await _googleApi.GetUserInfoAsync(googleToken);

        if (userInfo is null)
        {
            return BadRequest(new ErrorResult(HttpStatusCode.BadRequest, "Google account can't be accessed."));
        }

        var bindGoogleAccountCommand = new BindGoogleAccountCommand
        {
            UserId = LoginId!.Value,
            GoogleSub = userInfo.sub
        };

        await Mediator.Send(bindGoogleAccountCommand);
        return Ok();
    }

    [SwaggerOperation(Summary = "Remove google authentication for the current user (roles:user)")]
    [HttpDelete("session/user/unbind-google"), Authorize(Roles = "User")]
    public async Task<IActionResult> UnbindGoogleAccount()
    {
        var unbindGoogleAccountCommand = new UnbindGoogleAccountCommand
        {
            UserId = LoginId!.Value
        };

        await Mediator.Send(unbindGoogleAccountCommand);
        return Ok();
    }
}