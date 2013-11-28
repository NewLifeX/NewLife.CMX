using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;

namespace NewLife.CMX.Web
{
    public class TextModelContent : ModelContentBase
    {
        override public string Process()
        {
            try
            {
                Text.Meta.TableName += Suffix;
                Text text = Text.FindByKey(ID);
                Text.ChannelSuffix = Suffix;

                LeftMenu = LeftMenuContent.GetContent(Suffix, text.CategoryID);

                if (text == null) return "不存在该记录！";

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("ID", ID.ToString());
                dic.Add("Suffix", Suffix);
                dic.Add("Header", Header);
                dic.Add("Foot", Foot);
                dic.Add("LeftMenu", LeftMenu);

                CMXEngine engine = new CMXEngine(TemplateConfig.Current);
                engine.ArgDic = dic;
                engine.Entity = text as IEntity;

                String content = engine.Render(Address + ".html");
                return content;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Text.Meta.TableName = "";
            }
        }
    }
}
