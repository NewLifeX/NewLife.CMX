using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Interface;

namespace NewLife.CMX.Web.Base
{
    /// <summary></summary>
    public abstract class LeftMenuBase : ILeftMenu
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

        public abstract string Process();
    }
}
