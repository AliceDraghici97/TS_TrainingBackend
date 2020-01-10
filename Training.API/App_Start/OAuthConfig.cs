using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Training.API.AuthProvider;
using System.IdentityModel;
using System.Security;
using System.IdentityModel.Tokens.Jwt;
using Training.IRepository.Entity;
using Training.EFRepository;

namespace Training.API.App_Start
{
    public class OAuthConfig
    {

        public static OAuthAuthorizationServerOptions OAuthOptions(IUserRepository userRepository)
        {
            return new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthAppProvider(userRepository),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                AllowInsecureHttp = true,
                RefreshTokenProvider = new RefreshTokenProvider()
            };
        }
    }
}
