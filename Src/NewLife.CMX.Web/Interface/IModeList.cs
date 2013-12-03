using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX.Web
{
    public interface IModeList
    {
        /// <summary></summary>
        String Suffix { get; set; }

        /// <summary></summary>
        Int32 CategoryID { get; set; }

        /// <summary></summary>
        String Address { get; set; }

        /// <summary></summary>
        Int32 Pageindex { get; set; }

        /// <summary></summary>
        Int32 RecordNum { get; set; }

        /// <summary></summary>
        String Foot { get; set; }

        /// <summary></summary>
        String Header { get; set; }

        /// <summary></summary>
        String LeftMenu { get; set; }

        /// <summary></summary>
        String ChannelName { get; set; }

        /// <summary></summary>
        Int32 BeforePage { get; }

        /// <summary></summary>
        Int32 NextPage { get; }

        /// <summary></summary>
        Int32 PageCount { get; set; }

        /// <summary></summary>
        String Process();
    }
}