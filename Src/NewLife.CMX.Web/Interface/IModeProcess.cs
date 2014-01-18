using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Web.Interface
{
    public interface IModeProcess
    {
        String Process(Dictionary<String, object> dic);
    }
}
