using System;

namespace NewLife.CMX
{
    public interface IModelContent
    {
        /// <summary>频道编号</summary>
        Int32 ChannelID { get; set; }

        ///// <summary>频道扩展名</summary>
        //String Suffix { get; set; }

        ///// <summary>模板缩写</summary>
        String ModelShortName { get; set; }

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