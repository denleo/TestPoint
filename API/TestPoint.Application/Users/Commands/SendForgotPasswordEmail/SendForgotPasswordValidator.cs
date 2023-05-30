using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Users.Commands.SendForgotPasswordEmail;

public class SendForgotPasswordValidator : AbstractValidator<SendForgotPasswordEmailCommand>
{
    public SendForgotPasswordValidator()
    {
        RuleFor(x => x.Username).ApplyUsernameRules();
    }
}
