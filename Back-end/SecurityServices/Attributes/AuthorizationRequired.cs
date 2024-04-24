using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SecurityServices.Common;
using SecurityServices.RnR;

namespace SecurityServices.Attributes
{
    public class AuthorizationRequired : ActionFilterAttribute
    {
        private const string Token = "Token";
        private const string UserName = "UserName";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var unAuthorizedResponse = new ValidateTokenResponse()
            {
                Errors = new List<string>() { "Unauthorized" },
                IsValid = false
            };

            if (context.HttpContext.Request.Headers.ContainsKey(Token) && context.HttpContext.Request.Headers.ContainsKey(Token))
            {
                StringValues tokenValue;
                StringValues userName;
                context.HttpContext.Request.Headers.TryGetValue(Token, out tokenValue);
                context.HttpContext.Request.Headers.TryGetValue(UserName, out userName);

                TokenManager tokenManager = new();

                if (!tokenManager.ValidateToken(tokenValue, userName))
                {
                    context.Result = new OkObjectResult(unAuthorizedResponse);
                }
            }
            else
            {
                context.Result = new OkObjectResult(unAuthorizedResponse) ; //new UnauthorizedResult(); //null
            }
        }
    
    }
}
