using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using NewLife.Log;

namespace NewLife.CMX
{
    public class HttpRomting : IHttpModule
    {
        public void Dispose()
        {
            //throw new NotImplementedException();

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;

            context.Error += context_Error;
        }

        void context_Error(object sender, EventArgs e)
        {
            HttpApplication httpapplication = (HttpApplication)sender;
            HttpContext context = httpapplication.Context;

            XTrace.WriteLine("导航异常：" + context.Error.Message);
            context.Server.ClearError();
            HttpContext.Current.Response.Redirect("~/Default.aspx");

        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpContext hc = ((HttpApplication)sender).Context;
            String url = hc.Request.Url.ToString();

            var i = url.LastIndexOf('/');
            url = url.Substring(i + 1);

            if (String.Equals(url, "Default.aspx", StringComparison.OrdinalIgnoreCase))
            {
                //HttpContext.Current.Response.Redirect("~/Default2.aspx");
                HttpContext.Current.RewritePath("~/Default3.aspx");
                

            }
            //throw new NotImplementedException();
        }
    }
}
