using System;
using System.Collections.Generic;
using System.Text;
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
            Channel c = Channel.FindBySuffix(Suffix);

            if (c == null)
            {
                TypeX type = TypeX.GetType("NewLife.CMX.Web." + c.ListTemplate.Substring(0, c.ListTemplate.IndexOf('.')));

                IModeList iml = type.CreateInstance() as IModeList;

                iml.Suffix = Suffix;
                iml.Address = TemplateName;
                iml.CategoryID = CategoryID;
                iml.RecordNum = RecordCount;
                return iml.Process();
            }
            switch (Suffix)
            {
                case "Common":
                    TypeX type = TypeX.GetType("NewLife.CMX.Web.Common");
                    ICommon ic = type.CreateInstance() as ICommon;
                    return ic.Process();
                default:
                    return null;
            }
        }
    }
}
