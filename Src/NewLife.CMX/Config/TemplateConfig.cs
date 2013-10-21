using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

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

        private Int32 _IsCover = 1;
        /// <summary>是否覆盖原有模板（0否1是）</summary>
        [Description("是否覆盖原有模板（0否1是）")]
        public Int32 IsCover { get { return _IsCover; } set { _IsCover = value; } }

        private Int32 _IsCreateTemplate = 1;
        /// <summary>是否创建模板（0否1是）</summary>
        [Description("是否创建模板（0否1是）")]
        public Int32 IsCreateTemplate { get { return _IsCreateTemplate; } set { _IsCreateTemplate = value; } }
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
