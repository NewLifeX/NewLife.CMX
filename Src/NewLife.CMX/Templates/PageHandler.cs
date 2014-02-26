using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using XTemplate.Templating;

namespace NewLife.CMX.Templates
{
    /// <summary>模版处理器</summary>
    public class PageHandler : IHttpHandler
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
            //var tmp = Template.Create();

            var name = TemplateName;
            if (name.IsNullOrWhiteSpace()) name = "Index";

            var html = Engine.Current.Process(name, null);
            context.Response.Write(html);
        }
        #endregion
    }
}