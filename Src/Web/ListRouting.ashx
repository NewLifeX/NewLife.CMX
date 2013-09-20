<%@ WebHandler Language="C#" Class="ListRouting" %>

using System;
using System.Web;
using NewLife.Web;
using NewLife.CMX;

public class ListRouting : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        //参数可以频道的ID也可以频道名称也可以扩展名（Suffix）
        String channel = context.Request["Channel"];

        Channel c = Channel.FindBySuffix(channel);

        if (c != null)
        {
            String url = c.Model.ListTemplatePath;

            if (String.IsNullOrEmpty(url)) return;

            url += "?" + context.Request.QueryString.ToString();
            context.Response.Redirect(url);
        }

        context.Response.StatusCode = 404;
        context.Response.Write("未知地址！");
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