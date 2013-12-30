using System;
using System.Collections.Generic;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;

namespace NewLife.CMX.Web
{
    public class ArticleModelContent : ModelContentBase
    {
        override public string Process()
        {
            Article.SetChannelSuffix(Suffix);

            var article = Article.FindByID(ID);
            if (article == null) return "不存在该记录！";

            LeftMenu = LeftMenuContent.GetContent(Suffix, article.CategoryID);

            var dic = new Dictionary<String, String>();
            dic.Add("Address", Address);
            dic.Add("ID", ID.ToString());
            //dic.Add("Suffix", Suffix);

            var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
            engine.ArgDic = dic;
            engine.Header = Header;
            engine.Foot = Foot;
            engine.LeftMenu = LeftMenu;
            engine.Suffix = Suffix;
            engine.Entity = article as IEntity;

            String content = engine.Render(Address + ".html");
            return content;
        }
    }
}