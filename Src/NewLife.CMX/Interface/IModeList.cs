using System;

namespace NewLife.CMX
{
    public interface IModeList
    {
        /// <summary></summary>
        Int32 ChannelID { get; set; }

        /// <summary></summary>
        Channel Channel { get; }

        ///// <summary></summary>
        //String Suffix { get; set; }

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