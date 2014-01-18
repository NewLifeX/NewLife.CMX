using System;

namespace NewLife.CMX
{
    public interface IModeList
    {
        /// <summary>频道编号</summary>
        Int32 ChannelID { get; set; }

        /// <summary>频道</summary>
        Channel Channel { get; }

        ///// <summary></summary>
        //String Suffix { get; set; }

        ///// <summary>模板缩写</summary>
        String ModelShortName { get; set; }

        /// <summary>分类编号</summary>
        Int32 CategoryID { get; set; }

        /// <summary>请求模板地址</summary>
        String Address { get; set; }

        /// <summary>页面索引</summary>
        Int32 Pageindex { get; set; }

        /// <summary>页面记录条数</summary>
        Int32 RecordNum { get; set; }

        /// <summary>页面底部</summary>
        String Foot { get; set; }

        /// <summary>页面头部</summary>
        String Header { get; set; }

        /// <summary>左侧菜单</summary>
        String LeftMenu { get; set; }

        /// <summary>频道名称</summary>
        String ChannelName { get; set; }

        /// <summary>前页</summary>
        Int32 BeforePage { get; }

        /// <summary>下页</summary>
        Int32 NextPage { get; }

        /// <summary>总记录数</summary>
        Int32 PageCount { get; set; }

        /// <summary></summary>
        String Process();
    }
}