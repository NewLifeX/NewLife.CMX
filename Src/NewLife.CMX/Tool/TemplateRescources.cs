using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NewLife.CMX.Tool
{
    public class TemplateRescources
    {
        public static String GetContent(String FilePath)
        {
            return null;
        }

        public static Dictionary<String, String> Templates(String TemplatesPath)
        {
            Dictionary<String, String> dic = new Dictionary<string, string>();

            if (!Directory.Exists(TemplatesPath)) return dic;

            DirectoryInfo di = new DirectoryInfo(TemplatesPath);
            FileInfo[] fiarray = di.GetFiles();
            String content;

            foreach (FileInfo item in fiarray)
            {
                content = GetContent(item.FullName);
                dic.Add(item.Name, content);
            }

            return dic;
        }


    }
}
