using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Web
{
    public abstract class ModelContentBase : IModelContent
    {
        private string _Suffix;
        /// <summary></summary>
        public string Suffix { get { return _Suffix; } set { _Suffix = value; } }

        private int _ID;
        /// <summary></summary>
        public int ID { get { return _ID; } set { _ID = value; } }

        private string _Address;
        /// <summary></summary>
        public string Address { get { return _Address; } set { _Address = value; } }

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

        /// <summary>
        ///  处理方法
        /// </summary>
        /// <returns></returns>
        public abstract string Process();
    }
}
