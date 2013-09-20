<%@ WebHandler Language="C#" Class="FormRouting" %>

using System;
using System.Web;
using NewLife.Web;
using NewLife.CMX;

public class FormRouting : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        //参数可以频道的ID也可以频道名称也可以扩展名（Suffix）
        Channel channel = Channel.GetModel(context.Request["Channel"]);

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