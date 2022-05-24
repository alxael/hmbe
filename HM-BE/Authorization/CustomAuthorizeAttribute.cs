using HM.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace HM.WebAPI
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {

        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeAttributeImp)) { }

        private class CustomAuthorizeAttributeImp: IAsyncActionFilter
        {
            private readonly IUserService _userService;

            public CustomAuthorizeAttributeImp(IUserService userService)
            {
                _userService = userService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {

                var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
                if (allowAnonymous)
                {
                    await next();
                    return;
                }
                if (context.HttpContext.Request.Headers.Authorization.Count == 0)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var stringToken = context.HttpContext.Request.Headers.Authorization[0].Replace("Bearer ", string.Empty);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(stringToken);
                var userId = jwtToken?.Payload["userId"].ToString();


                if (jwtToken?.ValidTo > DateTime.UtcNow && await _userService.IsUserValidAsync(userId, stringToken))
                {
                    context.RouteData.Values.Add("userId", userId);
                    await next();
                    return;
                }

                context.Result = new UnauthorizedResult();
            }
        }


    }
}
