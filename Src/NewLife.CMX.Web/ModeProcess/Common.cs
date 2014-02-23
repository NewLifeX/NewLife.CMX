using System;
using System.Collections.Generic;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;

namespace NewLife.CMX.Web
{
    public class Common : CommonBase
    {
        override public string Process()
        {
            var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
            var dic = new Dictionary<string, string>();
            dic.Add("Address", Address);

            engine.ArgDic = dic;
            //engine.Header = Header;
            //engine.Foot = Foot;

            return engine.Render(Address.EnsureEnd(".html"));
        }
    }
}