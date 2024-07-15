using Newtonsoft.Json;
using System;
using Wfs.Business.Models;
using Wfs.Model.Models;

namespace Wfs.Business.BusinessLogic
{
    public interface IHelperBusinessLogic
    {
        WfsMessageTransmit CreateMessageTransmit(object referencedata, int stateId);
    }

    public class HelperBusinessLogic : IHelperBusinessLogic
    {
        public WfsMessageTransmit CreateMessageTransmit(object referencedata, int stateId)
        {
            try
            {
                CommandModelTemplate modelTemplate = (CommandModelTemplate)referencedata;
                var messageTransmit = new WfsMessageTransmit()
                {
                    MessageTypeId = modelTemplate.MessageTypeId,
                    StateId = stateId,
                    CreatedByLoginId = 1,
                    CreatedDateTime = DateTimeOffset.Now,
                    MessageReferenceData = JsonConvert.SerializeObject(referencedata)
                };


                //TODO Record will be added to wfs message transmit database table

                //this._WfsMessageTransmitdataService.Add(messageTransmit);
                //this._WfsMessageTransmitdataService.SaveChanges;

                //return messageTransmit

            }
            catch (Exception ex)
            {
                //TODO Logger
               
            }
            return null;
        }
    }
}
