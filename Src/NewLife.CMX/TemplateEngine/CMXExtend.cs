using System;
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
        public static String Extend(String TemplateName, String Suffix, Int32 CategoryID, Int32 RecordCount)
        {
            var chn = Channel.FindBySuffix(Suffix);

            if (chn != null)
            {
                var type = TypeX.GetType("NewLife.CMX.Web." + chn.ListTemplate.Substring(0, chn.ListTemplate.IndexOf('.')));

                var iml = type.CreateInstance() as IModeList;

                iml.ChannelID = chn.ID;
                //iml.Suffix = Suffix;
                iml.Address = TemplateName;
                iml.CategoryID = CategoryID;
                iml.RecordNum = RecordCount;

                return iml.Process();
            }
            switch (Suffix)
            {
                case "Common":
                    var type = TypeX.GetType("NewLife.CMX.Web.Common");
                    var ic = type.CreateInstance() as ICommon;
                    return ic.Process();
                default:
                    return null;
            }
        }
    }
}