using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wfs.Model.Models
{
    public class WfsMessageTransmit
    {
        public int Id { get; set; }
        public int MessageTypeId { get; set; }

        public int StateId { get; set; }

        public string ErrorMessage { get; set; }

        public string Resolution { get; set; }

        public string Actiontaken { get; set; }

        public MessageType MessageType { get; set; }

        public State State { get; set; }

        public string MessageReferenceData { get; set; }

        public int CreatedByLoginId { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        public int UpdatedByLoginId { get; set; }

        public DateTimeOffset UpdatedDateTime { get; set; }

        public bool IsVoid { get; set; }




        public string message { get; set; }

    }
}
