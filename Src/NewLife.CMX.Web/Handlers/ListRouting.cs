using System;
using System.Web;
using NewLife.Web;

namespace NewLife.CMX.Web.Handlers
{
    public class ListRouting : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (Admin.Current == null) context.Response.Redirect("Login.aspx");

            //参数频道的扩展名（Suffix）
            //Channel c = Channel.FindBySuffix(context.Request["Channel"]);
            //由于默认频道的存在，默认频道是没有后缀扩展名。所以需要先根据频道扩展名查询，如果没有扩展名在使用模型编号查询
            Channel c = Channel.FindBySuffixOrModel(context.Request["Channel"], WebHelper.RequestInt("ModelID"));

            Admin admin = Admin.Current;

            ChannelRole cr = ChannelRole.FindChannelIDAndRoleID(c.ID, admin.RoleID);

            if (cr == null) context.Response.Redirect("Default.aspx");

            if (c != null)
            {
                String url = c.Model.CategoryTemplatePath;

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
            var chn = Channel.FindByID(channelid);

            return chn == null ? "" : chn.Model.CategoryTemplatePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelname"></param>
        public static String GetUrl(String channelname)
        {
            var chn = Channel.FindByName(channelname);

            return chn == null ? "" : chn.Model.CategoryTemplatePath;
        }

        public bool IsReusable { get { return false; } }
    }
}