using MediatR;
using TestPoint.Application.Users.Queries.CheckUserLogin;

namespace TestPoint.Application.Users.Queries.CheckGoogleUserLogin;

public class CheckGoogleUserLoginQuery : IRequest<CheckUserLoginResponse>
{
    public string GoogleSub { get; set; }

    public CheckGoogleUserLoginQuery(string googleSub)
    {
        GoogleSub = googleSub;
    }
}
