using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX;

namespace NewLife.CMX.Web
{
    public abstract class CommonBase : ICommon
    {
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

        public abstract string Process();
    }
}