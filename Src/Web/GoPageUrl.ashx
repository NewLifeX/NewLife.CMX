<%@ WebHandler Language="C#" Class="GoPageUrl" %>

using System;
using System.Web;
using NewLife.Web;
using System.Text.RegularExpressions;

public class GoPageUrl : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Int32 PageNum = WebHelper.RequestInt("PageNum");
        String urldata = context.Request["urldata"];

        urldata = urldata.Replace("pagenum", PageNum.ToString());

        Int32 i = urldata.IndexOf("/List/");
        String lasturl = urldata.Substring(i + 6);
        String firsturl = urldata.Substring(0, i + 6);

        Regex r = new Regex(@"([^_\./]*)(_){0,1}(\d+){0,1}");
        Match m = r.Match(lasturl);
        if (m.Groups[2] == null || (m.Groups[2] != null && m.Groups[3] == null))
        {
            lasturl = lasturl.Replace(m.Groups[1].Value, m.Groups[1] + "_" + PageNum);
        }
        else
        {
            lasturl = lasturl.Replace(m.Groups[1].Value + m.Groups[2].Value + m.Groups[3].Value, m.Groups[1] + "_" + PageNum);
        }
        context.Response.Write(firsturl + lasturl);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}