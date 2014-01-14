using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX
{
    public interface IModelContent
    {
        /// <summary>频道扩展名</summary>
        String Suffix { get; set; }

        /// <summary>对象编号</summary>
        Int32 ID { get; set; }

        /// <summary>请求地址</summary>
        String Address { get; set; }

        /// <summary>页尾</summary>
        String Foot { get; set; }

        /// <summary>页头</summary>
        String Header { get; set; }

        /// <summary>左侧导航</summary>
        String LeftMenu { get; set; }

        /// <summary></summary>
        String Process();
    }
}
