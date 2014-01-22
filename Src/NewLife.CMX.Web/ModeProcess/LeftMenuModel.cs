using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Web.Base;
using NewLife.Reflection;
using XCode;

namespace NewLife.CMX.Web
{
    public class LeftMenuModel : LeftMenuModelBase
    {
        public override string Process()
        {
            Channel c = Channel.FindByID(ChannelID);

            IModelProvider imp = c.Model.Provider;
            try
            {
                imp.CurrentChannel = c.ID;

                var entities = imp.CategoryType.BaseType.GetMethod("FindAllByIDAndDeepth").Invoke("FindAllByIDAndDeepth", new object[] { CategoryID, RootDeepth, DisDeepth, IsContainParent });

                entities=

                var dic = new Dictionary<String, String>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("RootDeepth", RootDeepth.ToString());
                dic.Add("DisDeepth", DisDeepth.ToString());
                dic.Add("IsContainParent", IsContainParent.ToString());
                dic.Add("ChannelName", c.Name);

                var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                engine.Suffix = c.Suffix;
                engine.ListCategory = entities;
                engine.ModelShortName = ModelShortName;

                return engine.Render(Address.EnsureEnd(".html"));
            }
            finally
            {
                imp.CurrentChannel = 0;
            }
        }
    }
}
