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

                var RootCategory = imp.CategoryType.BaseType.GetMethod("FindParentByIDAndDeepth").Invoke("FindParentByIDAndDeepth", new object[] { CategoryID, RootDeepth });

                var dic = new Dictionary<String, String>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("RootDeepth", RootDeepth.ToString());
                dic.Add("ChannelName", c.Name);
                //dic.Add("RootCategory", RootCategory);
                //dic.Add("DisDeepth", DisDeepth.ToString());
                //dic.Add("IsContainParent", IsContainParent.ToString());

                var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                engine.Suffix = String.IsNullOrEmpty(c.Suffix) ? "$" : c.Suffix;
                //engine.ListCategory = entities;
                engine.RootEntityTree = RootCategory as IEntityTree;
                engine.ModelShortName = c.Model.ShortName;

                return engine.Render(Address.EnsureEnd(".html"));
            }
            finally
            {
                //重置ROOT清除缓存
                imp.CategoryType.SetValue("Root", null);
                imp.CurrentChannel = 0;
            }
        }
    }
}
