using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Training.IRepository.Entity;

[assembly: OwinStartup(typeof(Training.API.App_Start.Startup))]

namespace Training.API.App_Start
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {            
            app.UseCors(CorsOptions.AllowAll);
            GlobalConfiguration.Configure(UnityConfig.Configure);

            var userRepository = (IUserRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserRepository));
            app.UseOAuthBearerTokens(OAuthConfig.OAuthOptions(userRepository));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string connString = ConfigurationManager.ConnectionStrings["TrainingDB"].ToString();

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.EnsureInitialized();

        }
    }
}


//SQLHelper.Initialize(connString);
//SQLHelper.AssureDatabase("Subjects");
//SQLHelper.AssureDatabase("Students");
//SQLHelper.AssureDatabase("StudentsXSubjects");
