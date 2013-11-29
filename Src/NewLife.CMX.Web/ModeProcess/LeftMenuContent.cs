using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;
using System.Linq;

namespace NewLife.CMX.Web
{
    public class LeftMenuContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static String GetContent(String Suffix, Int32 CategoryID)
        {
            Channel channel = Channel.FindBySuffix(Suffix);
            Dictionary<String, String> dic = new Dictionary<String, String>();
            String classname = channel.Model.ClassName;
            String id = "0";

            IEntityOperate ieo = EntityFactory.CreateOperate(classname);

            if (CategoryID != 0)
            {
                var i = ieo.Find("ID", CategoryID);
                IEntityTree entity = ieo.Find("ID", CategoryID) as IEntityTree;

                if (entity.Parent != null)
                    id = entity.Parent["ID"].ToString();
            }

            IEntityList list = ieo.FindAll("ParentID", 0);
            //var jj = list.ToList().OrderBy(e => e["ID"]).ToList();

            dic.Add("Suffix", Suffix);
            dic.Add("ModelAddress", channel.ListTemplate);
            dic.Add("SelectedCategory", id);
            dic.Add("MenuTitle", channel.Name);

            CMXEngine engine = new CMXEngine(TemplateConfig.Current);
            engine.ListCategory = list.ToList().OrderBy(e => e["ID"]).ToList().ConvertAll<IEntityTree>(e => e as IEntityTree);
            engine.ArgDic = dic;

            String content = engine.Render(TemplateConfig.Current.LeftAddress);

            return content;
        }
    }
}
