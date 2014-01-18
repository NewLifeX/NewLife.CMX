using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX
{
    public interface ICommon
    {
        /// <summary></summary>
        string Address { get; set; }

        /// <summary></summary>
        String Foot { get; set; }

        /// <summary></summary>
        String Header { get; set; }

        /// <summary></summary>
        String Process();
    }
}