[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AirFlight.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AirFlight.App_Start.NinjectWebCommon), "Stop")]

namespace AirFlight.App_Start
{
    using System;
    using System.Configuration;
    using System.Web;
    using AirFlight.Repository;
    using AirFlight.Repository.Interfaces;
    using AirFlight.Service;
    using AirFlight.Service.Interfaces;
    using Geocoding;
    using Geocoding.Google;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .InSingletonScope();
            kernel.Bind<IGeocoder>().To<GoogleGeocoder>()
                .WithConstructorArgument("apiKey", ConfigurationManager.AppSettings["GoogleApiKey"]);
            kernel.Bind<IAircraftService>().To<AircraftService>();
            kernel.Bind<IAirportService>().To<AirportService>();
            kernel.Bind<IFlightService>().To<FlightService>();
        }        
    }
}