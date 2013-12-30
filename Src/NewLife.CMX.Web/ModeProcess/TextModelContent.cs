using System;
using System.Collections.Generic;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;

namespace NewLife.CMX.Web
{
    public class TextModelContent : ModelContentBase
    {
        public override string Process()
        {
            ArticleProvider.CurrentChannel = ChannelID;
            //Text.SetChannelSuffix(Suffix);

            var text = Text.FindByID(ID);
            if (text == null) return "不存在该记录！";

            LeftMenu = LeftMenuContent.GetContent(Channel, text.CategoryID);

            var dic = new Dictionary<string, string>();
            dic.Add("Address", Address);
            dic.Add("ID", ID.ToString());
            //dic.Add("Suffix", Suffix);

            var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
            engine.ArgDic = dic;
            engine.Header = Header;
            engine.Foot = Foot;
            engine.LeftMenu = LeftMenu;
            engine.Entity = text as IEntity;
            engine.Suffix = Channel.Suffix;

            String content = engine.Render(Address + ".html");
            return content;
        }
    }
}