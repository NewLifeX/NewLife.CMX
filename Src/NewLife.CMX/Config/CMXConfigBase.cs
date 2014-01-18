using System;
using System.ComponentModel;
using System.Web;
using System.Xml.Serialization;
using NewLife.Xml;

namespace NewLife.CMX.Config
{
    [XmlConfigFile("config/CMX.config", 15000)]
    /// <summary>基础配置信息</summary>
    [Description("基础配置信息")]
    [Serializable]
    public class CMXConfigBase : CMXmlConfig<CMXConfigBase>
    {
        #region 属性
        [Description("根目录")]
        [XmlIgnore]
        public String CurrentRootPath { get { return _CurrentParentPath; } }
        #endregion

        #region 构造方法
        static String _CurrentParentPath;
        static CMXConfigBase()
        {
            //_CurrentParentPath = HttpRuntime.AppDomainAppVirtualPath.EnsureEnd("/");
            _CurrentParentPath = HttpRuntime.AppDomainAppVirtualPath;
            if (_CurrentParentPath == "/") _CurrentParentPath = "";
        }
        #endregion
    }
}