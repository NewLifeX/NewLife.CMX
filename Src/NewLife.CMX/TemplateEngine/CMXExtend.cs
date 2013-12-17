using System;
using System.Collections.Generic;
using System.Text;
using NewLife.Reflection;

namespace NewLife.CMX.TemplateEngine
{
    public class CMXExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <param name="Suffix"></param>
        /// <param name="CategoryID"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        public static String Extend(String TemplateName, String Suffix, Int32 CategoryID, Int32 RecordCount)
        {
            Channel c = Channel.FindBySuffix(Suffix);

            TypeX type = TypeX.GetType("NewLife.CMX.Web." + c.ListTemplate.Substring(0, c.ListTemplate.IndexOf('.')));

            IModeList iml = type.CreateInstance() as IModeList;

            iml.Suffix = Suffix;
            iml.Address = TemplateName;
            iml.CategoryID = CategoryID;
            iml.RecordNum = RecordCount;
            return iml.Process();
        }
    }
}
