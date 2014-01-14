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
            CMXEngine engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("Address", Address);

            engine.ArgDic = dic;
            engine.Header = Header;
            engine.Foot = Foot;
            String content = engine.Render(Address + ".html");

            return content;
        }
    }
}
