using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;

namespace NewLife.CMX.Web
{
    public class LeftMenuContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static String GetContent(String Suffix)
        {
            Channel channel = Channel.FindBySuffix(Suffix);
            String classname = channel.Model.ClassName;

            IEntityOperate ieo = EntityFactory.CreateOperate(classname);

            IEntityList list = ieo.FindAll("ParentID", 0);

            CMXEngine engine = new CMXEngine(TemplateConfig.Current);
            engine.ListCategory = list;

            String content = engine.Render(TemplateConfig.Current.LeftAddress);

            return content;
        }
    }
}
