using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using NewLife.Web;

namespace NewLife.CMX.Web.Handlers
{
    public class FormRouting : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (Admin.Current == null) context.Response.Redirect("Default.aspx");

            var cid = WebHelper.RequestInt("ChannelID");
            var chn = Channel.FindByID(cid);
            //Channel chn = Channel.FindBySuffixOrModel(context.Request["Channel"], WebHelper.RequestInt("ModelID"));

            if (chn != null)
            {
                ChannelRole cr = ChannelRole.FindChannelIDAndRoleID(chn.ID, Admin.Current.RoleID);
                if (cr == null) context.Response.Redirect("Default.aspx");

                String url = chn.Model.TitleTemplatePath;
                if (!String.IsNullOrEmpty(url))
                {
                    url += "?" + context.Request.QueryString.ToString();
                    context.Response.Redirect(url);
                }
                else
                {
                    context.Response.Write("路径地址无法解析");
                }
            }
            context.Response.StatusCode = 404;
            context.Response.Write("未知地址！");
            context.Response.End();
        }

        public bool IsReusable { get { return false; } }
    }
}