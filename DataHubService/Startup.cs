using DataHubService.Workers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wfs.Business.BusinessLogic;
using Wfs.Business.HttpClientServices;
using Wfs.Model.Helper;
using Wfs.Business.Helpers;
using System;

namespace DataHubService
{
    /// <summary>
    /// Startup File
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this._Configuration = configuration;
        }

        private IConfiguration _Configuration { get; }

        public void ConfigurationServices(IServiceCollection services)
        {
            services.AddSingleton(this._Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IXmlHelper, XmlHelper>();

            // Http Service Client
            services.AddHttpClient<IWorldGraphServiceClient, WorldGraphServiceClient>().SetHandlerLifetime(TimeSpan.FromMinutes(1)); ;
            services.AddHttpClient<IAuthServiceClient, AuthServiceClient>();


            // App config
            services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>();

            // Data Hub Import Worker Program
            services.AddSingleton<IDataHubUpStreamImportWorkerProgram, DataHubUpStreamImportWorkerProgram>();


            // Business Logic
            services.AddSingleton<IDataHubUpStreamImportBusinessLogic, DataHubUpStreamImportBusinessLogic>();
            services.AddSingleton<IHelperBusinessLogic, HelperBusinessLogic>();
            

            // TODO 
            // services.AddSingleton<IwfsInfraLogger, wfsInfraLogger>();

            // TODO
            //services.AddDbContext<IWfsDataContext, WfsDataContext>();
            

        }
    }
}
