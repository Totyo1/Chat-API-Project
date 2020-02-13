using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;

using ChatAPIProject.Common.Automapping;
using ChatAPIProject.Service;
using Models;
using Service;
using Service.Contracts;

using System.Reflection;
using System.Web.Http;

namespace ChatAPIProject
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterInstance(AutoMapperConfig.RegisterMappings(
                typeof(RegisterMappingModel).GetTypeInfo().Assembly))
                .As<Mapper>()
                .SingleInstance();

            builder.RegisterType(typeof(UserService))
                   .As(typeof(IUserService))
                   .InstancePerRequest();

            builder.RegisterType(typeof(MessageService))
                   .As(typeof(IMessageService))
                   .InstancePerRequest();

            builder.RegisterType(typeof(CommunicationService))
                   .As(typeof(ICommunicationService))
                   .InstancePerRequest();
            
            builder.RegisterType(typeof(FriendRequestSevice))
                   .As(typeof(IFriendRequestSevice))
                   .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}