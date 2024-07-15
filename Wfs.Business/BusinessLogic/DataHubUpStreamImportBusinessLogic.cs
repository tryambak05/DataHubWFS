using System;
using System.Collections.Generic;
using Wfs.Business.Helpers;
using Wfs.Business.HttpClientServices;
using Wfs.Business.Models;
using Wfs.Business.Models.HttpClientServiceModel;
using Wfs.Business.Models.XmlModels;
using Wfs.Model.Helper;
using Wfs.Model.Models;

namespace Wfs.Business.BusinessLogic
{
    public enum DataHubImprtStatus
    {
        None = 0,
        Okay = 1,
        Error = -1,
        NullItemInput = -2,
        DataValidationError = -3,
        NullResponse = -4
    }

    public interface IDataHubUpStreamImportBusinessLogic
    {
        IBusinessResult<DataHubImprtStatus, object> Import();
    }

    public class DataHubUpStreamImportBusinessLogic : IDataHubUpStreamImportBusinessLogic
    {
        private const int MessageTypeId = 1;
        private const string WindFarmAvailabilityMessageType = "Wind Farm";

        public DataHubUpStreamImportBusinessLogic(IApplicationConfiguration applicationConfiguration,
            IXmlHelper xmlHelper,
            IHelperBusinessLogic helperBusinessLogic,
            IWorldGraphServiceClient worldGraphServiceClient)
        {
            this._ApplicationConfiguration = applicationConfiguration;
            this._XmlHelper = xmlHelper;
            this._HelperBusinessLogic = helperBusinessLogic;
            this._WorldGraphServiceClient = worldGraphServiceClient;
        }

        private IWorldGraphServiceClient _WorldGraphServiceClient { get; set; }
        private IApplicationConfiguration _ApplicationConfiguration { get; set; }
        private IXmlHelper _XmlHelper { get; set;}
        private IHelperBusinessLogic _HelperBusinessLogic { get; set; }

        public IBusinessResult<DataHubImprtStatus, object> Import()
        {
            var filePath = $"c:\\wfs\\{this._ApplicationConfiguration.DataImportConfiguration.DataHubImportFolder}";
            var fileNamePreFix = $"WIND_";
            var processedFolder = $"c:\\wfs\\{this._ApplicationConfiguration.DataImportConfiguration.DataHubImportFolder}_prc";
            string fileName = string.Empty;
            string methodName = "DataHubUpStreamImportBusinessLogic.Import()";

            WfsMessageTransmit messageTransmit = null;
            WindUploadAvailabilityReferenceData referenceData = null;
            try
            {
                var files = this._XmlHelper.GetXmlFiles(filePath, fileNamePreFix);

                if (files.Length > 0)
                {
                    foreach(var file in files)
                    {
                        try
                        {
                            fileName = file.Name;
                            referenceData = new WindUploadAvailabilityReferenceData()
                            {
                                FileName = fileName,
                                ProcessedFolder = processedFolder,
                                Currentpath = filePath,
                                MessageTypeId = DataHubUpStreamImportBusinessLogic.MessageTypeId,
                                MethodName = methodName,
                                OmegaMessageType = DataHubUpStreamImportBusinessLogic.WindFarmAvailabilityMessageType,
                                PayloadDetails = new List<PaylodDetail>()
                            };
                            messageTransmit = this._HelperBusinessLogic.CreateMessageTransmit(referenceData, (int)StateEnum.PickedUpByJob);
                            var xmlData = this._XmlHelper.ReadFromXml<Data>(filePath, file.Name);
                            if (xmlData == null)
                            {
                                string errorMessage = $"{methodName} could not read data from xml. possibly because of unexpected format in xml file {filePath + "\\" + file.Name}";

                                //TODO Adding exception to the log
                               // this._HelperBusinessLogic.AddErrorLog(messageTransmit, referenceData, errorMessage ErrorSeverity.Critical, null,"0", (int)StateEnum.FailedToTransmit);
                                continue;
                            }

                            var data = MapAvailabilityDataCommandModel(xmlData);
                            var result = _WorldGraphServiceClient.PostWinUploadAvailabilityData(data);
                        }
                        catch (Exception ex)
                        {
                            //TODO Logger
                        }
                    }
                }
                // PROCESS FILE

            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public AvailabilityDataCommandModel MapAvailabilityDataCommandModel(Data data)
        {
            AvailabilityDataCommandModel availabilityDataCommandModels = new AvailabilityDataCommandModel();
            List<UploadAvailabilityData> uploadAvailabilityData = new List<UploadAvailabilityData>();
            foreach (var item in data.Record)
            {
                uploadAvailabilityData.Add(new UploadAvailabilityData()
                {
                    unitId = item.UnitId,
                    periodStart = DateTimeOffsetHelper.FromString(item.PeriodStart),
                    volume = item.Volume
                });
            }
            availabilityDataCommandModels.availabilityData = uploadAvailabilityData;

            return availabilityDataCommandModels;
        }
    }
}