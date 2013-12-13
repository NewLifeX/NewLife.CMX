using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace NewLife.CMX.Web.Handlers
{
    public class ListRouting : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (Admin.Current == null) context.Response.Redirect("Default.aspx");

            //参数频道的扩展名（Suffix）
            Channel c = Channel.FindBySuffix(context.Request["Channel"]);

            Admin admin = Admin.Current;

            ChannelRole cr = ChannelRole.FindChannelIDAndRoleID(c.ID, admin.RoleID);

            if (cr == null) context.Response.Redirect("Default.aspx");

            if (c != null)
            {
                String url = c.Model.ListTemplatePath;

                if (String.IsNullOrEmpty(url)) return;

                url += "?" + context.Request.QueryString.ToString();
                context.Response.Redirect(url);
            }

            context.Response.StatusCode = 404;
            context.Response.Write("未知地址！");
            context.Response.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelid"></param>
        public static String GetUrl(Int32 channelid)
        {
            Channel c = Channel.FindByID(channelid);

            return c == null ? "" : c.Model.ListTemplatePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelname"></param>
        public static String GetUrl(String channelname)
        {
            Channel c = Channel.FindBySuffix(channelname);

            return c == null ? "" : c.Model.ListTemplatePath;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
