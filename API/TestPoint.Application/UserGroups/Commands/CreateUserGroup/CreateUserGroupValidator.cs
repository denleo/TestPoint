using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.UserGroups.Commands.CreateUserGroup;

public class CreateUserGroupValidator : AbstractValidator<CreateUserGroupCommand>
{
    public CreateUserGroupValidator()
    {
        RuleFor(x => x.GroupName).ApplyUserGroupNameRules();
    }
}
