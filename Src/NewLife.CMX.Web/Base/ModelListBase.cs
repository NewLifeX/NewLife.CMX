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
        public String LeftMenu
        {
            get
            {
                if (Suffix != null && _LeftMenu == null)
                {
                    _LeftMenu = LeftMenuContent.GetContent(Suffix);
                }
                return _LeftMenu;
            }
            set { _LeftMenu = value; }
        }

        public abstract string Process();
    }
}
