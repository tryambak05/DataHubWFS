using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Wfs.Business.Helpers
{
    public interface IBusinessResult<TResultCode, TResult> where TResultCode : struct, IConvertible
    {
        [DataMember]
        TResult Result { get; set; }

        [DataMember]
        TResult ResultCode { get; set; }

        [DataMember]
        IEnumerable<string> ErrorMessage { get; set; }

        [DataMember]
        bool isSuccessful { get; set; }

    }
}
