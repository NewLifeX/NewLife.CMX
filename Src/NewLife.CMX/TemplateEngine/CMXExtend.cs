using System;
using NewLife.CMX.Config;
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
        public static String Extend(String Address, String ModelKind = "C", String Suffix = null, String ModelShortName = null, Int32 CategoryID = 0, Int32 EntityID = 0, Int32 PageIndex = 0, Int32 RecordNum = 0)
        {
            switch (ModelKind)
            {

                case "l":
                case "L":
                    Channel cl = Channel.FindBySuffixOrModelShortName(Suffix, ModelShortName);
                    var list = TypeX.GetType("NewLife.CMX.Web." + cl.ListTemplate);
                    var iml = list.CreateInstance() as IModeList;

                    iml.ChannelID = cl.ID;

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
                default:
                    TypeX type = TypeX.GetType("NewLife.CMX.Web.Common");
                    ICommon ic = type.CreateInstance() as ICommon;
                    ic.Address = Address;
                    return ic.Process();
            }
        }
    }
}