using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using NewLife.Xml;


namespace NewLife.CMX.Config
{
    /// <summary>基础配置信息</summary>
    [XmlConfigFile("config/CMX.config", 15000)]
    [Description("基础配置信息")]
    [Serializable]
    public class CMXConfig : CMXmlConfigBase<CMXConfig>
    {
        #region 属性
        /// <summary></summary>
        [Description("根目录")]
        [XmlIgnore]
        public String CurrentParentPath { get { return _CurrentParentPath; } }
        #endregion

        #region 构造方法
        static String _CurrentParentPath;
        static CMXConfig()
        {
            _CurrentParentPath = HttpRuntime.AppDomainAppVirtualPath.EnsureEnd("/");
        }
        #endregion
    }
}
