using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Data;
using Data.Contracts;
using Owin;

namespace Web
{
    public static class AutofacConfig
    {
        public static IContainer Container { get; private set; }
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);
            //config.EnableCors();

            //Global dependencies
            builder.RegisterType<EntityMapperDbContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<IdentityAuthAdapter>().As<IAuthenticationAdapter>().InstancePerRequest();
            builder.RegisterType<EntityMapperDbContext>().As<EntityMapperDbContext>().InstancePerRequest();
            builder.RegisterType<InMemoryLogger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<EntityMappingRepository>().As<IMappingRepository>().InstancePerRequest();

            //Domain dependencies
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            // Set the dependency resolver to be Autofac.
            Container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
