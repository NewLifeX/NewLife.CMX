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
            try
            {
                ArticleProvider.CurrentChannel = ChannelID;

                var text = Text.FindByID(ID);
                if (text == null) return "不存在该记录！";

                LeftMenu = LeftMenuContent.GetContent(Channel, text.CategoryID);

                var dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("ID", ID.ToString());

                var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                //engine.Header = Header;
                //engine.Foot = Foot;
                //engine.LeftMenu = LeftMenu;
                engine.Entity = text as IEntity;
                engine.Suffix = Channel.Suffix;
                engine.ModelShortName = ModelShortName;

                return engine.Render(Address.EnsureEnd(".html"));
            }
            finally
            {
                ArticleProvider.CurrentChannel = 0;
            }
        }
    }
}