using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NewLife.CMX.Tool;
using NewLife.Xml;

namespace NewLife.CMX.Config
{
    /// <summary>
    /// CMX配置文件基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CMXmlConfigBase<T> : XmlConfig<T> where T : CMXmlConfigBase<T>, new()
    {
        static CMXmlConfigBase()
        {
            // 修正配置文件路径，加上论坛目录，因为论坛可能不是在顶级
            var file = _.ConfigFile;
            if (!file.IsNullOrWhiteSpace())
            {
                if (typeof(T) != typeof(CMXConfig)) file = HelperTool.GetFullPath(CMXConfig.Current.CurrentParentPath + _.ConfigFile);
                if (!File.Exists(file)) file = _.ConfigFile.GetFullPath();
                _.ConfigFile = file;
            }
        }
    }
}
