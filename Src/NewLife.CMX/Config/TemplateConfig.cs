using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NewLife.Xml;

namespace NewLife.CMX.Config
{
    [XmlConfigFile("config/Template.config", 15000)]
    /// <summary>模板配置</summary>
    [Description("模板配置")]
    [Serializable]
    public class TemplateConfig : CMXmlConfig<TemplateConfig>
    {
        private String _TemplateStyle;
        /// <summary>模板样式</summary>
        public String TemplateStyle { get { return _TemplateStyle; } set { _TemplateStyle = value; } }

        private String _TemplateID;
        /// <summary>模板编号</summary>
        public String TemplateID { get { return _TemplateID; } set { _TemplateID = value; } }
    }
}
