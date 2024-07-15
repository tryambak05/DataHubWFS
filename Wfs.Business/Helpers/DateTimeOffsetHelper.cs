using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wfs.Business.Helpers
{
    public class DateTimeOffsetHelper
    {
        public static DateTimeOffset FromString(string offsetString)
        {

            DateTimeOffset offset;
            if (!DateTimeOffset.TryParse(offsetString, out offset))
            {
                offset = DateTimeOffset.Now;
            }

            return offset;
        }
    }
}
