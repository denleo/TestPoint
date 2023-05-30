using FluentValidation;

namespace TestPoint.Application.Pipeline.Common;

public static class ValidationRulesExtensions
{
    public const int USERNAME_MIN_LEN = 5;
    public const int USERNAME_MAX_LEN = 16;

    public const int PASSWORD_MIN_LEN = 10;
    public const int PASSWORD_MAX_LEN = 64;

    public const int EMAIL_MIN_LEN = 5;
    public const int EMAIL_MAX_LEN = 254;

    public const int FIO_MIN_LEN = 1;
    public const int FIO_MAX_LEN = 64;

    public const int USERGROUP_MIN_LEN = 5;
    public const int USERGROUP_MAX_LEN = 256;

    public const int TESTNAME_MIN_LEN = 5;
    public const int TESTNAME_MAX_LEN = 256;

    public const int TESTANSWER_MAX_LEN = 1000;

    public const int TESTQUESTION_MAX_LEN = 1000;

    public static IRuleBuilderOptions<T, string> ApplyUsernameRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .MinimumLength(USERNAME_MIN_LEN)
            .MaximumLength(USERNAME_MAX_LEN)
            .Matches(@"^[a-z|A-Z|\d]+$").WithMessage("Invalid field format (a-z A-Z or numeric)");
    }

    public static IRuleBuilderOptions<T, string> ApplyPasswordRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
           .NotEmpty()
           .MinimumLength(PASSWORD_MIN_LEN)
           .MaximumLength(PASSWORD_MAX_LEN);
    }

    public static IRuleBuilderOptions<T, string> ApplyEmailRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .MinimumLength(EMAIL_MIN_LEN)
            .MaximumLength(EMAIL_MAX_LEN)
            .EmailAddress();
    }

    public static IRuleBuilderOptions<T, string> ApplyFioRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
           .NotEmpty()
           .MinimumLength(FIO_MIN_LEN)
           .MaximumLength(FIO_MAX_LEN);
    }

    public static IRuleBuilderOptions<T, string> ApplyUserGroupNameRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
           .NotEmpty()
           .MinimumLength(USERGROUP_MIN_LEN)
           .MaximumLength(USERGROUP_MAX_LEN);
    }
}
