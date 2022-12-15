using System;
namespace TranTriTaiBlog.DTOs.Responses
{
    public class UserLoginResponse
    {
        public UserLoginResponse(string jwtToken)
        {
            JWTToken = jwtToken;
        }

        public string JWTToken { get; set; }
    }
}

