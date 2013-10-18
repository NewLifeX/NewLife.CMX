using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using NewLife.CMX.Config;
using NewLife.CMX.Tool;
using System.IO;

namespace NewLife.CMX.UrlRewrite
{
    public class CMXHttpModel : IHttpModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);

            context.Error += new EventHandler(Application_OnError);
        }

        private void Application_OnError(object sender, EventArgs e)
        {
            String CurrentUrl = HelperTool.GetReques();
            HttpApplication httpApplication = (HttpApplication)sender;

            httpApplication.Context.Server.ClearError();

            HttpContext.Current.Response.Redirect(CMXConfigBase.Current.CurrentParentPath.CombinePath("Index.aspx"));
        }

        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
