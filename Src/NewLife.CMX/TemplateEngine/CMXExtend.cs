using System;
using NewLife.CMX.Config;
using NewLife.CMX.Interface;
using NewLife.Reflection;

namespace NewLife.CMX.TemplateEngine
{
    public class CMXExtend
    {
        /// <summary>
        /// 模板扩展方法
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <param name="Suffix"></param>
        /// <param name="CategoryID"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        //public static String Extend(String TemplateName, String Suffix, Int32 CategoryID, Int32 RecordCount)
        //{
        //    var chn = Channel.FindBySuffix(Suffix);

        //    if (chn != null)
        //    {
        //        var type = TypeX.GetType("NewLife.CMX.Web." + chn.ListTemplate.Substring(0, chn.ListTemplate.IndexOf('.')));

        //        var iml = type.CreateInstance() as IModeList;

        //        iml.ChannelID = chn.ID;
        //        //iml.Suffix = Suffix;
        //        iml.Address = TemplateName;
        //        iml.CategoryID = CategoryID;
        //        iml.RecordNum = RecordCount;

        //        return iml.Process();
        //    }
        //    switch (Suffix)
        //    {
        //        case "Common":
        //            var type = TypeX.GetType("NewLife.CMX.Web.Common");
        //            var ic = type.CreateInstance() as ICommon;
        //            return ic.Process();
        //        default:
        //            return null;
        //    }
        //}

        /// <summary>
        /// 模板扩展方法
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="ModelKind"></param>
        /// <param name="Suffix"></param>
        /// <param name="ModelShortName"></param>
        /// <param name="CategoryID"></param>
        /// <param name="EntityID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="RecordNum"></param>
        /// <returns></returns>
        public static String Extend(String Address, String ModelKind, String Suffix = "", String ModelShortName = null, Int32 CategoryID = 0, Int32 EntityID = 0, Int32 PageIndex = 0, Int32 RecordNum = 0, Int32 RootDeepth = 0)
        {
            if (ModelKind.IsNullOrEmpty()) ModelKind = "X";
            if (Suffix == null || Suffix == "$") Suffix = "";
            
            switch (ModelKind)
            {

                case "c":
                case "C":
                    Channel cc = Channel.FindBySuffixOrModelShortName(Suffix, ModelShortName);
                    var list = TypeX.GetType("NewLife.CMX.Web." + cc.ListTemplate);
                    var iml = list.CreateInstance() as IModeList;

                    iml.ChannelID = cc.ID;

                    iml.ModelShortName = ModelShortName;
                    iml.Address = Address;
                    iml.CategoryID = CategoryID;
                    iml.Pageindex = PageIndex == 0 ? 1 : PageIndex;
                    iml.RecordNum = RecordNum == 0 ? TemplateConfig.Current.RecordNum : RecordNum;

                    return iml.Process();
                case "f":
                case "F":
                    Channel cf = Channel.FindBySuffixOrModelShortName(Suffix, ModelShortName);
                    var mt = TypeX.GetType("NewLife.CMX.Web." + cf.FormTemplate);
                    var im = mt.CreateInstance() as IModelContent;

                    im.ChannelID = cf.ID;
                    im.ModelShortName = ModelShortName;
                    im.Address = Address;
                    im.ID = EntityID;

                    return im.Process();
                case "l":
                case "L":
                    Channel cl = Channel.FindBySuffixOrModelShortName(Suffix, ModelShortName);
                    var lt = TypeX.GetType("NewLife.CMX.Web.LeftMenuModel");
                    var lf = lt.CreateInstance() as ILeftMenuModel;

                    lf.Address = Address;
                    lf.ChannelID = cl.ID;
                    lf.CategoryID = CategoryID;
                    lf.ModelShortName = ModelShortName;
                    //lf.DisDeepth = DisDeepth;
                    lf.RootDeepth = RootDeepth;
                    //lf.IsContainParent = IsContainParent;

                    return lf.Process();
                default:
                    TypeX type = TypeX.GetType("NewLife.CMX.Web.Common");
                    ICommon ic = type.CreateInstance() as ICommon;
                    ic.Address = Address;
                    return ic.Process();
            }
        }
    }
}