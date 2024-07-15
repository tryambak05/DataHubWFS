using System;
using System.Collections.Generic;

namespace Wfs.Business.Models.HttpClientServiceModel
{
    public class AvailabilityDataCommandModel
    {
        public List<UploadAvailabilityData> availabilityData { get; set; }
    }
    public class UploadAvailabilityData
    {
        public bool overwrite { get; set; }
        public Guid unitId { get; set; }
        public DateTimeOffset periodStart { get; set; }
        public decimal volume { get; set; }
    }
}