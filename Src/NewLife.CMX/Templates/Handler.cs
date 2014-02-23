using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace NewLife.CMX.Templates
{
    /// <summary>模版处理器</summary>
    public class Handler : IHttpHandler
    {
        #region 属性
        private String _TemplateName;
        /// <summary>模版名</summary>
        public String TemplateName { get { return _TemplateName; } set { _TemplateName = value; } }
        #endregion

        #region IHttpHandler
        /// <summary>是否重用。不可以</summary>
        public bool IsReusable { get { return false; } }

        /// <summary>处理请求</summary>
        /// <param name="context"></param>
        public virtual void ProcessRequest(HttpContext context)
        {
        }
        #endregion
    }
}