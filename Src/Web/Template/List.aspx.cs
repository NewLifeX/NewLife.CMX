using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.UI;
using NewLife.CMX;
using NewLife.Log;
using NewLife.Web;

public partial class Template_List : NewLife.CMX.WebBase.WebPageBase
{
    /// <summary>ID</summary>
    public Int32 CategoryID
    {
        get { return WebHelper.RequestInt("CategoryID"); }
    }

    /// <summary>类编码</summary>
    public String Suffix
    {
        get { return Request["Suffix"]; }
    }

    private Channel _C;
    /// <summary>频道</summary>
    public Channel C
    {
        get
        {
            if (_C == null & !String.IsNullOrEmpty(Suffix))
            {
                _C = Channel.FindBySuffix(Suffix);
            }
            return _C;
        }
        set { _C = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (C == null)
        {
            Err("未确定的频道！");
        }
        else if (String.IsNullOrEmpty(C.ListTemplate))
        {
            Err("未绑定模版！");
        }

        StringWriter strWriterHTML = new StringWriter();
        Page aspxPage = new Page();

        String path = C.ListTemplate + GetRQ();

        try
        {
            //aspxPage.Server.Execute(C.Template + "?Code=" + Code, strWriterHTML);//将aspx页执行产生的html输出到StringWriter中
            aspxPage.Server.Execute(path, strWriterHTML);//将aspx页执行产生的html输出到StringWriter中
        }
        catch (ThreadAbortException ex)
        {
            Response.Redirect(path);
        }
        catch (Exception ex)
        {
            // 不要随便拦截异常而无所作为，会害死人的
            XTrace.WriteException(ex);

            Err("编译出错！");
        }

        Response.Write(strWriterHTML);
    }

    /// <summary>
    /// 获取QueryString
    /// </summary>
    /// <returns></returns>
    private String GetRQ()
    {
        StringBuilder sb = new StringBuilder();
        if (Request.QueryString.Count > 0)
        {
            sb.Append("?");

            foreach (String item in Request.QueryString.AllKeys)
            {
                if (sb.Length > 1)
                    sb.Append("&");
                sb.AppendFormat("{0}={1}", item, Request.QueryString[item]);
            }
        }

        return sb.ToString();
    }

    private void Err(String Msg)
    {
        Response.Clear();
        Response.Write(Msg);
        Response.End();
    }
}