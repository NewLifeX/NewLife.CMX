using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NewLife.CMX.Tool;
using NewLife.Xml;

namespace NewLife.CMX.Config
{
    public class CMXmlConfig<T> : XmlConfig<T> where T : CMXmlConfig<T>, new()
    {
        static CMXmlConfig()
        {
            // 修正配置文件路径，加上论坛目录，因为论坛可能不是在顶级
            var file = _.ConfigFile;
            if (!file.IsNullOrWhiteSpace())
            {
                if (typeof(T) != typeof(CMXConfigBase)) file = HelperTool.GetFullPath(CMXConfigBase.Current.CurrentRootPath + "/"+_.ConfigFile);
                if (!File.Exists(file)) file = _.ConfigFile.GetFullPath();
                _.ConfigFile = file;
            }
        }
    }
}
