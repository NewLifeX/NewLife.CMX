using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using XTemplate.Templating;

namespace NewLife.CMX.Templates
{
    /// <summary>页面模版基类。所有页面模版生成类继承于此类</summary>
    public class PageBase : TemplateBase
    {
        #region 属性
        private String _TemplatePath;
        /// <summary>模版目录</summary>
        public String TemplatePath { get { return _TemplatePath; } set { _TemplatePath = value; } }

        private String _ResourcePath;
        /// <summary>资源路径</summary>
        public String ResourcePath
        {
            get
            {
                return _ResourcePath;
            }
        }
        #endregion

        #region 业务处理
        #endregion
    }
}