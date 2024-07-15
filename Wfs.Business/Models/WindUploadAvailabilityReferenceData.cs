using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wfs.Business.Models
{
    public class WindUploadAvailabilityReferenceData
    {

        public string FileName { get; set; }
        public string ProcessedFolder { get; set; }

        public string Currentpath { get; set; }

        public int MessageTypeId { get; set; }

        public string MethodName { get; set; }

        public string OmegaMessageType { get; set; }

        public List<PaylodDetail> PayloadDetails { get; set; }



    }
}
