using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Smoothie.Domain.Dto;
using Smoothie.Domain.Repositories;
using Smoothie.Services;
using Smoothie.Web.Infrastructure.AutoMapper;
using Smoothie.Web.Infrastructure.Binders;

namespace Smoothie.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "Smoothie.Web.Controllers" }
            );

        }

        protected void Application_Start()
        {
            SetupDi();

            AutoMapperConfiguration.Configure();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(UserDataDto), new UserDataModelBinder<UserDataDto>());



        }

        private void SetupDi()
        {
            var builder = new ContainerBuilder();


            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<FoodRepository>().As<IFoodRepository>();
            builder.RegisterType<SmoothieRepository>().As<ISmoothieRepository>();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<FoodService>().As<IFoodService>();
            builder.RegisterType<SmoothieService>().As<ISmoothieService>(); 

            builder.RegisterType<MappingService>().As<IMappingService>();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<FormsAuthorizationService>().As<IAuthorizationService>();

            


            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}