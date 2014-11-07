using System;
using System.Web;

namespace NewLife.CMX.Templates
{
    /// <summary>模版处理器</summary>
    public class PageHandler : IHttpHandler
    {
        #region 属性
        private String _Name;
        /// <summary>模版名</summary>
        public String Name { get { return _Name; } set { _Name = value; } }
        #endregion

        #region IHttpHandler
        /// <summary>是否重用。不可以</summary>
        public bool IsReusable { get { return false; } }

        /// <summary>处理请求</summary>
        /// <param name="context"></param>
        public virtual void ProcessRequest(HttpContext context)
        {
            //var tmp = Template.Create();

            var name = Name;
            if (name.IsNullOrWhiteSpace()) name = "index";

            var html = Engine.Current.Process(name, null);
            context.Response.Write(html);
        }
        #endregion
    }
}