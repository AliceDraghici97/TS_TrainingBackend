using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Training.EFRepository;
using Training.IRepository.Entity;
using Unity;
using Unity.WebApi;

namespace Training.API.App_Start
{
    public static class UnityConfig
    {
        private static IUnityContainer GetUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IStudentRepository, StudentEFRepository>();
            container.RegisterType<IStudentXSubjectRepository, StudentXSubjectEFRepository>();
            container.RegisterType<ISubjectRepository, SubjectEFRepository>();
            container.RegisterType<IUserRepository, UserEFRepository>() ;
            return container;
        }

        public static void Configure(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.DependencyResolver = new UnityDependencyResolver(GetUnityContainer());
        }
    }
}