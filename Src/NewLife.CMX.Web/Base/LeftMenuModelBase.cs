using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Interface;

namespace NewLife.CMX.Web.Base
{
    /// <summary></summary>
    public abstract class LeftMenuModelBase : ILeftMenuModel
    {
        private String _Address;
        /// <summary>请求模板</summary>
        public String Address { get { return _Address; } set { _Address = value; } }

        private Int32 _ChannelID;
        /// <summary>频道编号</summary>
        public Int32 ChannelID { get { return _ChannelID; } set { _ChannelID = value; } }

        private Int32 _CategoryID;
        /// <summary>分类编号</summary>
        public Int32 CategoryID { get { return _CategoryID; } set { _CategoryID = value; } }

        private Int32 _Deepth;
        /// <summary>深度</summary>
        public Int32 Deepth { get { return _Deepth; } set { _Deepth = value; } }

        private string _ModelShortName;
        /// <summary>模型缩写</summary>
        public string ModelShortName { get { return _ModelShortName; } set { _ModelShortName = value; } }

        private int _DisDeepth;
        /// <summary>显示深度</summary>
        public int DisDeepth { get { return _DisDeepth; } set { _DisDeepth = value; } }

        private int _RootDeepth;
        /// <summary>根目录深度</summary>
        public int RootDeepth { get { return _RootDeepth; } set { _RootDeepth = value; } }

        private Boolean _IsContainParent;
        /// <summary>是否包含父类</summary>
        public Boolean IsContainParent { get { return _IsContainParent; } set { _IsContainParent = value; } }

        /// <summary>
        /// 处理
        /// </summary>
        /// <returns></returns>
        public abstract string Process();
    }
}
