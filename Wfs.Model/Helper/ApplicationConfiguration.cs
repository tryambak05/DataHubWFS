using Microsoft.Extensions.Configuration;
using System;


namespace Wfs.Model.Helper
{
    public interface IApplicationConfiguration
    {
        int ApplicationId { get; }

        ProgramJobName ProgramJobName { get; }

        DataImportConfiguration DataImportConfiguration { get; }

        ApplicationService ApplicationService { get; }
        OAuthCredentails OAuthCredentails { get; }

    }

    public class ApplicationConfiguration : IApplicationConfiguration
    {

        public ApplicationConfiguration(IConfiguration configuration)
        {
            this._Configuration = configuration;
        }

        private IConfiguration _Configuration { get; }

        int ApplicationId { get; set; }

        public ProgramJobName ProgramJobName { get { return this.GetProgramJobName(); } }

        int IApplicationConfiguration.ApplicationId => 123;

        public DataImportConfiguration DataImportConfiguration => GetDataImportConfiguration();

        public ApplicationService ApplicationService => GetApplicationServiceConfiguration();

        public OAuthCredentails OAuthCredentails => GetOAuthCredentailsConfiguration();

        private ProgramJobName GetProgramJobName()
        {
            return new ProgramJobName() { DataHubUpStream = this._Configuration["ProgramJobName:DataHubUpStream"] };
        }

        private DataImportConfiguration GetDataImportConfiguration()
        {
            return new DataImportConfiguration()
            {
                DataHubImportFolder = this._Configuration["ImportFile:DataHubImportFolder"]
            };
        }

        private ApplicationService GetApplicationServiceConfiguration()
        {
            return new ApplicationService()
            {
                DataHubServiceUrl = this._Configuration["ApplicationService:DataHubServiceUrl"],
                DataHubBaseUrl = this._Configuration["ApplicationService:DataHubBaseUrl"]
            };
        }

        private OAuthCredentails GetOAuthCredentailsConfiguration()
        {
            return new OAuthCredentails()
            {
                client_id = this._Configuration["OAuthCredentails:client_id"],
                client_secret = this._Configuration["OAuthCredentails:client_secret"],
                audience = this._Configuration["OAuthCredentails:audience"],
                grant_type = this._Configuration["OAuthCredentails:grant_type"],
            };
        }
    }
}
