using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wfs.Business.Models.XmlModels
{
    public class WindFarmsUpload
    {
        [XmlElement(ElementName = "data")]
        public Data data { get; set; }
    }

    [XmlRoot(ElementName = "data")]
    public class Data
    {

        [XmlElement(ElementName = "record")]
        public List<Record> Record { get; set; }
    }

    [XmlRoot(ElementName = "record")]
    public class Record
    {

        [XmlElement(ElementName = "UnitId")]
        public Guid UnitId { get; set; }

        [XmlElement(ElementName = "PeriodStart")]
        public string PeriodStart { get; set; }

        [XmlElement(ElementName = "Volume")]
        public decimal Volume { get; set; }
    }
}
