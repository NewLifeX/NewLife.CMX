using System;
using NewLife.CMX.Config;
using NewLife.CMX.Interface;
using NewLife.Reflection;

namespace NewLife.CMX.TemplateEngine
{
    public class CMXExtend
    {
        /// <summary>模板扩展方法</summary>
        /// <param name="address"></param>
        /// <param name="kind"></param>
        /// <param name="suffix"></param>
        /// <param name="modelShortName"></param>
        /// <param name="categoryID"></param>
        /// <param name="EntityID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordNum"></param>
        /// <returns></returns>
        public static String Extend(String address, String kind, String suffix = "", String modelShortName = null, Int32 categoryID = 0, Int32 EntityID = 0, Int32 PageIndex = 0, Int32 RecordNum = 0, Int32 RootDeepth = 0)
        {
            if (kind.IsNullOrEmpty()) kind = "X";
            if (suffix == null || suffix == "$") suffix = "";

            kind = kind.ToLower();
            if (kind == "C")
            {
                var chn = Channel.FindBySuffixOrModelShortName(suffix, modelShortName);
                if (chn != null)
                {
                    var list = TypeX.GetType("NewLife.CMX.Web." + chn.ListTemplate);
                    var iml = list.CreateInstance() as IModeList;

                    iml.ChannelID = chn.ID;
                    iml.ModelShortName = modelShortName;
                    iml.Address = address;
                    iml.CategoryID = categoryID;
                    iml.Pageindex = PageIndex == 0 ? 1 : PageIndex;
                    //iml.RecordNum = RecordNum == 0 ? TemplateConfig.Current.RecordNum : RecordNum;

                    return iml.Process();
                }
            }
            else if (kind == "F")
            {
                var chn = Channel.FindBySuffixOrModelShortName(suffix, modelShortName);
                if (chn != null)
                {
                    var mt = TypeX.GetType("NewLife.CMX.Web." + chn.FormTemplate);
                    var im = mt.CreateInstance() as IModelContent;

                    im.ChannelID = chn.ID;
                    im.ModelShortName = modelShortName;
                    im.Address = address;
                    im.ID = EntityID;

                    return im.Process();
                }
            }
            else if (kind == "L")
            {
                var chn = Channel.FindBySuffixOrModelShortName(suffix, modelShortName);
                if (chn != null)
                {
                    var lt = TypeX.GetType("NewLife.CMX.Web.LeftMenuModel");
                    var lf = lt.CreateInstance() as ILeftMenuModel;

                    lf.Address = address;
                    lf.ChannelID = chn.ID;
                    lf.CategoryID = categoryID;
                    lf.ModelShortName = modelShortName;
                    //lf.DisDeepth = DisDeepth;
                    lf.RootDeepth = RootDeepth;
                    //lf.IsContainParent = IsContainParent;

                    return lf.Process();
                }
            }

            var type = TypeX.GetType("NewLife.CMX.Web.Common");
            var ic = type.CreateInstance() as ICommon;
            ic.Address = address;
            return ic.Process();
        }
    }
}