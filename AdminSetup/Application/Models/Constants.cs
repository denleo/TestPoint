using System.Text.RegularExpressions;

namespace Core.Models;

public static class Constants
{
    #region AppGlobal

    public const string AppTitle = "Admin Setup Tool";

    #endregion

    #region Username

    public static readonly Regex UsernameRegex = new("^[a-z|A-Z|\\d]+$");
    public const short UsernameMinLength = 5;
    public const short UsernameMaxLength = 16;

    #endregion
}