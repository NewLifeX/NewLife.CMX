using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace NewLife.CMX.Config
{
    public class TemplateConfig : CMXmlConfig<TemplateConfig>
    {
        #region 属性
        private String _TemplateStyle;
        /// <summary>模板样式</summary>
        [Description("模板样式")]
        public String TemplateStyle { get { return _TemplateStyle; } set { _TemplateStyle = value; } }

        private String _TemplateID;
        /// <summary>模板编号</summary>
        [Description("模板编号")]
        public String TemplateID { get { return _TemplateID; } set { _TemplateID = value; } }

        private String _TemplateRootPath = Path.Combine(CMXConfigBase.Current.CurrentParentPath, "templates");
        /// <summary>模板目录</summary>
        [Description("模板根目录")]
        public String TemplateRootPath { get { return _TemplateRootPath; } set { _TemplateRootPath = value; } }

        private String _OutputPath;
        /// <summary>输出文件根目录</summary>
        [Description("输出文件根目录")]
        public String OutputPath { get { return _OutputPath; } set { _OutputPath = value; } }

        private String _HeadTemplate;
        /// <summary>模板头</summary>
        [Description("模板头")]
        public String HeadTemplate { get { return _HeadTemplate; } set { _HeadTemplate = value; } }

        private Boolean _IsCover = true;
        /// <summary>是否覆盖原文件</summary>
        [Description("是否覆盖原文件")]
        public Boolean IsCover { get { return _IsCover; } set { _IsCover = value; } }

        private Boolean _IsCreateTemplate = true;
        /// <summary>是否创建模板</summary>
        [Description("是否创建模板")]
        public Boolean IsCreateTemplate { get { return _IsCreateTemplate; } set { _IsCreateTemplate = value; } }

        private Boolean _IsDebug;
        /// <summary>是否调试模式</summary>
        [Description("是否调试模式")]
        public Boolean IsDebug { get { return _IsDebug; } set { _IsDebug = value; } }

        /// <summary>文件版本</summary>
        [XmlIgnore]
        public static String Version { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }

        private String _CsRootPath = "NewLift.CMX.Web";
        /// <summary>Cs页面路径</summary>
        [Description("Cs页面路径")]
        public String CsRootPath { get { return _CsRootPath; } set { _CsRootPath = value; } }

        private String _IgnoreExtendName;
        /// <summary>忽略扩展名称(多项请用逗号分隔)</summary>
        [Description("忽略扩展名称(多项请用逗号分隔)")]
        public String IgnoreExtendName { get { return _IgnoreExtendName; } set { _IgnoreExtendName = value; } }

        #endregion

        #region 构造方法
        /// <summary>构造方法</summary>
        public TemplateConfig()
        {
            var sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine(" * XCoder v<#=Version#>");
            sb.AppendLine(" * 作者：<#=Environment.UserName + \"/\" + Environment.MachineName#>");
            sb.AppendLine(" * 时间：<#=DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss\")#>");
            sb.AppendLine(" * 版权：版权所有 (C) 新生命开发团队 2002~<#=DateTime.Now.ToString(\"yyyy\")#>");
            sb.AppendLine("*/");
            HeadTemplate = sb.ToString();
        }
        #endregion
    }
}
