using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Web
{
    public interface ICommon
    {
        /// <summary></summary>
        string Address { get; set; }

        /// <summary></summary>
        String Foot { get; set; }

        /// <summary></summary>
        String Header { get; set; }

        String Process();
    }
}
