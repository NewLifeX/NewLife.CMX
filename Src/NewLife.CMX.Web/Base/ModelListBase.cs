using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Web
{
    public abstract class ModelListBase : IModeList
    {
        private String _Suffix;
        /// <summary></summary>
        public virtual String Suffix { get { return _Suffix; } set { _Suffix = value; } }

        private int _CategoryID;
        /// <summary></summary>
        public virtual int CategoryID { get { return _CategoryID; } set { _CategoryID = value; } }

        private String _Address;
        /// <summary></summary>
        public virtual String Address { get { return _Address; } set { _Address = value; } }

        private Int32 _Pageindex;
        /// <summary>页面索引</summary>
        public virtual Int32 Pageindex { get { return _Pageindex; } set { _Pageindex = value; } }

        private Int32 _RecordNum;
        /// <summary>记录数</summary>
        public virtual Int32 RecordNum { get { return _RecordNum; } set { _RecordNum = value; } }

        private string _Foot;
        /// <summary>页尾</summary>
        public virtual string Foot
        {
            get
            {
                if (String.IsNullOrEmpty(_Foot))
                {
                    _Foot = FootContent.GetContent();
                    return _Foot;
                }
                return _Foot;
            }
            set { _Foot = value; }
        }

        private string _Header;
        /// <summary>页头</summary>
        public virtual string Header
        {
            get
            {
                if (String.IsNullOrEmpty(_Header))
                    _Header = HeaderContent.GetContent();
                return _Header;
            }
            set { _Header = value; }
        }

        private String _LeftMenu;
        /// <summary>左侧导航</summary>
        public virtual String LeftMenu
        {
            get
            {
                if (Suffix != null && _LeftMenu == null)
                {
                    _LeftMenu = LeftMenuContent.GetContent(Suffix, CategoryID);
                }
                return _LeftMenu;
            }
            set { _LeftMenu = value; }
        }

        /// <summary>频道</summary>
        public Channel channel
        {
            get { return Channel.FindBySuffix(Suffix); }
        }

        private String _ChannelName;
        /// <summary>频道名称</summary>
        public String ChannelName
        {
            get
            {
                if (_ChannelName == null) _ChannelName = channel == null ? "" : channel.Name;
                return _ChannelName;
            }
            set { _ChannelName = value; }
        }

        /// <summary>上一页</summary>
        public virtual Int32 BeforePage { get { return Pageindex > 1 ? Pageindex - 1 : 0; } }

        /// <summary>下一页</summary>
        public virtual Int32 NextPage { get { return PageCount > 1 ? Pageindex + 1 : 0; } }

        private Int32 _PageCount = 1;
        /// <summary>总页数</summary>
        public virtual Int32 PageCount { get { return _PageCount; } set { _PageCount = value; } }

        public abstract string Process();
    }
}
