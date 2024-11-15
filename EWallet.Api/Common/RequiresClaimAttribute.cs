namespace EWallet.Api.Common;

public class RequiresClaimAttribute(string claimName, string claimValue) : Attribute, IAuthorizationFilter
{
    private readonly string _claimName = claimName;
    private readonly string _claimValue = claimValue;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.HasClaim(_claimName, _claimValue))
        {
            context.Result = new ForbidResult();
        }
    }
}