using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Interface
{
    public interface ILeftMenuModel
    {
        /// <summary></summary>
        String Address { get; set; }

        /// <summary>频道编号</summary>
        Int32 ChannelID { get; set; }

        /// <summary>分类编号</summary>
        Int32 CategoryID { get; set; }

        /// <summary>模型缩写</summary>
        String ModelShortName { get; set; }

        /// <summary>显示深度</summary>
        Int32 DisDeepth { get; set; }

        /// <summary>当前分类的父级根目录级别</summary>
        Int32 RootDeepth { get; set; }

        /// <summary>是否包含父类</summary>
        Boolean IsContainParent { get; set; }

        /// <summary></summary>
        String Process();
    }
}
