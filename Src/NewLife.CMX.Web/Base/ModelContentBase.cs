using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Web
{
    public abstract class ModelContentBase : IModelContent
    {
        private Int32 _ChannelID;
        /// <summary>频道编号</summary>
        public Int32 ChannelID { get { return _ChannelID; } set { _ChannelID = value; } }

        private Channel _Channel;
        /// <summary>频道</summary>
        public Channel Channel
        {
            get
            {
                if (_Channel == null && ChannelID > 0)
                {
                    _Channel = Channel.FindByID(ChannelID);
                }
                return _Channel;
            }
        }

        //private string _Suffix;
        ///// <summary></summary>
        //public virtual string Suffix { get { return _Suffix; } set { _Suffix = value; } }

        private String _ModelShortName;
        /// <summary>模型缩写</summary>
        public virtual String ModelShortName { get { return _ModelShortName; } set { _ModelShortName = value; } }

        private int _ID;
        /// <summary></summary>
        public virtual int ID { get { return _ID; } set { _ID = value; } }

        private string _Address;
        /// <summary></summary>
        public virtual string Address { get { return _Address; } set { _Address = value; } }

        private string _Foot;
        /// <summary></summary>
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
        /// <summary></summary>
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
        /// <summary></summary>
        public virtual String LeftMenu { get { return _LeftMenu; } set { _LeftMenu = value; } }

        /// <summary>
        ///  处理方法
        /// </summary>
        /// <returns></returns>
        public abstract string Process();
    }
}