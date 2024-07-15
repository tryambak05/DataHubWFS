using System;
using Wfs.Business.BusinessLogic;
using Wfs.Model.Helper;

namespace DataHubService.Workers
{

    public interface IDataHubUpStreamImportWorkerProgram
    {
        void Run();
    }

    public class DataHubUpStreamImportWorkerProgram : IDataHubUpStreamImportWorkerProgram
    {
        public DataHubUpStreamImportWorkerProgram(IApplicationConfiguration applicationConfiguration, IDataHubUpStreamImportBusinessLogic dataHubUpStreamImportBusinessLogic)
        {
            this._ApplicationConfiguration = applicationConfiguration;
            this._DataHubUpStreamImportBusinessLogic = dataHubUpStreamImportBusinessLogic;
        }

        private IApplicationConfiguration _ApplicationConfiguration { get; }

        private IDataHubUpStreamImportBusinessLogic _DataHubUpStreamImportBusinessLogic { get; }


        public void Run()
        {
            try
            {
                var result = this._DataHubUpStreamImportBusinessLogic.Import();

            } catch (Exception ex)
            {
                // TODO
                // Logger service 
            }
        }
    }
}
