using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Web
{
    public class ProductModelList : IModeList
    {
        private string _Suffix;
        /// <summary></summary>
        public string Suffix { get { return _Suffix; } set { _Suffix = value; } }

        private int _CategoryID;
        /// <summary></summary>
        public int CategoryID { get { return _CategoryID; } set { _CategoryID = value; } }

        private string _Address;
        /// <summary></summary>
        public string Address { get { return _Address; } set { _Address = value; } }

        private int _Pageindex;
        /// <summary></summary>
        public int Pageindex { get { return _Pageindex; } set { _Pageindex = value; } }

        private int _RecordNum;
        /// <summary></summary>
        public int RecordNum { get { return _RecordNum; } set { _RecordNum = value; } }

        public string Process()
        {
            return null;
        }
    }
}
