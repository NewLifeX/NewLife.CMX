using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Interface
{
    public interface ILeftMenu
    {
        /// <summary></summary>
        String Address { get; set; }

        /// <summary>频道编号</summary>
        Int32 ChannelID { get; set; }

        /// <summary>分类编号</summary>
        Int32 CategoryID { get; set; }

        /// <summary>深度</summary>
        Int32 Deepth { get; set; }

        /// <summary></summary>
        String Process();
    }
}
