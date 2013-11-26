using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX.Web
{
    public interface IModelContent
    {
        String Suffix { get; set; }

        Int32 ID { get; set; }

        String Address { get; set; }

        String Process();

        String Foot { get; set; }

        String Header { get; set; }
    }
}
