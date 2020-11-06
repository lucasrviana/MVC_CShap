using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Security.Claims;

namespace CoreIdentity.Controllers
{
    public class CustomAuthorization
    {
        public static bool validarClainsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
       
    }

    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequistoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
    public class RequistoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        public RequistoClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                (string areas, string page, string returnUrl) values = (areas: "Identity", page: "/Account/login", returnUrl: context.HttpContext.Request.Path.ToString());
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(values));
            }

            if (!CustomAuthorization.validarClainsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }

    }
}


