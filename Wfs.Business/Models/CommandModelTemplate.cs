using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wfs.Business.Models
{
    public class CommandModelTemplate
    {
        public string FileName { get; set; }

        [JsonIgnore]

        public string CurrentPath { get; set; }

        [JsonIgnore]

        public string ProcesswdFolder { get; set; }

        [JsonIgnore]

        public string OmegaMessageType { get; set; }

        [JsonIgnore]

        public string MethodName { get; set; }

        [JsonIgnore]

        public int MessageTypeId { get; set; }

        public List<PaylodDetail> PaylodDetails { get; set; }
    }

    public class PaylodDetail
    {
        public object payload { get; set; }

        public string ResourceLink { get; set; }

        public object Response { get; set; } 
    }
}
