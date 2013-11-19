using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Web
{
    public class ProductModelContent : IModelContent
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

        public string Process()
        {
            throw new NotImplementedException();
        }
    }
}
