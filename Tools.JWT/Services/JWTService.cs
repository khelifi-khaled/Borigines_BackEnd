using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToolBox.JWT.Configuration;
using Tools.JWT.Interfaces;

namespace ToolBox.JWT.Services
{
    public class JWTService : IToken
    {

        private readonly JWTConfiguration _config;
        private readonly JwtSecurityTokenHandler _handler;

        public JWTService(JWTConfiguration config, JwtSecurityTokenHandler handler)
        {
            _config = config;
            _handler = handler;
        }



        public string GenerateToken(params Claim[] claims)
        {
            JwtSecurityToken token = new(
                _config.Issuer , 
                _config.Audience ,
                claims , 
                _config.Duration != null  ? DateTime.Now :  null ,
                _config.Duration != null ? DateTime.Now.AddSeconds(_config.Duration ?? 0) : null,
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Signature)),
                    SecurityAlgorithms.HmacSha256)
            );

            return _handler.WriteToken(token);

        }//end GenerateToken

       
    }//end class
}//end name space 
