using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wfs.Model.Models
{
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CreatedByLoginId { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        public int UpdatedByLoginId { get; set; }

        public DateTimeOffset UpdatedDateTime { get; set; }

        public bool IsVoid { get; set; }

    }
}
