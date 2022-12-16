using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TranTriTaiBlog.DTOs.Responses;
using TranTriTaiBlog.Infrastructures.Helper.MessageUtil;
using TranTriTaiBlog.Infrastructures.Intefaces.UserServices;

namespace TranTriTaiBlog.Filter
{
    public class ApiAuthentication : Attribute, IAuthorizationFilter
    {
        public ApiAuthentication()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<Microsoft.AspNetCore.Mvc.Controller>>();
            try
            {
                var userId = ExtractUserIdFromToken(context.HttpContext.Request);
                if (userId == Guid.Empty)
                {
                    context.Result = new UnauthorizedObjectResult(
                          new CommonResponse<string>(
                              StatusCodes.Status403Forbidden, ErrorMsgUtil.GetUnauthorizedMsg(), null));
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                context.Result = new UnauthorizedObjectResult(
                     new CommonResponse<string>(
                         StatusCodes.Status403Forbidden, ErrorMsgUtil.GetUnauthorizedMsg(), null));
            }
        }

        private Guid ExtractUserIdFromToken(HttpRequest request)
        {
            string token = request.Headers["Authorization"].ToString();
            if (token.Length > 0)
            {
                //keep the token only
                token = token
                    .Replace("Bearer", string.Empty, true, null)
                    .Replace(" ", string.Empty, true, null);

                JwtSecurityToken tokenData = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string userId = tokenData.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
                if (Guid.TryParse(userId, out Guid result))
                {
                    return result;
                }
            }
            return Guid.Empty;
        }
    }
}

