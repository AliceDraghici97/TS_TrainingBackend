using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Training.API.App_Start;
using Training.DBContext;
using Training.EFRepository;
using Training.IRepository.Entity;
using Training.Model;

namespace Training.API.AuthProvider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserRepository _userRepository;

        public OAuthAppProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {       

            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                User user = _userRepository.GetByCredentials(username, password);
                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("UserID", user.Id.ToString())
                    };          
                    
                    string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
                    var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                    var header = new JwtHeader(credentials);
                    var payload = new JwtPayload(claims);                   
                    var secToken = new JwtSecurityToken(header, payload);
                    var handler = new JwtSecurityTokenHandler();
                    var tokenString = handler.WriteToken(secToken);
                    var token = handler.ReadJwtToken(tokenString);

                    ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, OAuthConfig.OAuthOptions(_userRepository).AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));             

                }
                else
                {
                    context.SetError("invalid_grant", "Error");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}