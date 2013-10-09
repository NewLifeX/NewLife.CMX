<%@ WebHandler Language="C#" Class="FormRouting" %>

using System;
using System.Web;
using NewLife.Web;
using NewLife.CMX;

public class FormRouting : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (Admin.Current == null) context.Response.Redirect("Default.aspx");

        //参数频道扩展名（Suffix）
        Channel channel = Channel.FindBySuffix(context.Request["Channel"]);

        Admin admin = Admin.Current;

        ChannelRole cr = ChannelRole.FindChannelIDAndRoleID(channel.ID, admin.RoleID);

        if (cr == null) context.Response.Redirect("Default.aspx");

        if (channel != null)
        {
            String url = channel.Model.FormTemplatePath;

            if (!String.IsNullOrEmpty(url))
            {
                //context.Request.
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
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}