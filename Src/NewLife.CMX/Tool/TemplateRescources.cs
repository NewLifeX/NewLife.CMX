using System;
using System.Collections.Generic;
using System.IO;

namespace NewLife.CMX.Tool
{
    public class TemplateRescources
    {
        public static String GetContent(String FilePath) { return null; }

        public static Dictionary<String, String> Templates(String TemplatesPath)
        {
            var dic = new Dictionary<String, String>();

            if (!Directory.Exists(TemplatesPath)) return dic;

            var di = new DirectoryInfo(TemplatesPath);
            foreach (var item in di.GetFiles())
            {
                var content = GetContent(item.FullName);
                dic.Add(item.Name, content);
            }

            return dic;
        }
    }
}