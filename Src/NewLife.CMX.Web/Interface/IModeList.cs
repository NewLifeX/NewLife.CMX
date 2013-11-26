using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX.Web
{
    public interface IModeList
    {
        String Suffix { get; set; }

        Int32 CategoryID { get; set; }

        String Address { get; set; }

        Int32 Pageindex { get; set; }

        Int32 RecordNum { get; set; }

        String Foot { get; set; }

        String Header { get; set; }

        String Process();
    }
}