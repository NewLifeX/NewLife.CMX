using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;

namespace NewLife.CMX.Web
{
    public class Common : CommonBase
    {
        override public string Process()
        {
            try
            {
                CMXEngine engine = new CMXEngine(TemplateConfig.Current);
                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Foot", Foot);
                dic.Add("Header", Header);
                dic.Add("Address", Address);

                engine.ArgDic = dic;

                String content = engine.Render(Address + ".html");

                return content;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
