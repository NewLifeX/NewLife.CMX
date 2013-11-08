using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.UI;
using NewLife.CMX;
using NewLife.CMX.Config;
using NewLife.Log;
using NewLife.Reflection;
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

        String content = "";
        try
        {
            TypeX type = TypeX.GetType("NewLife.CMX.Web." + C.ListTemplate);
            MethodInfoX mix = type.GetMethod("Process", new Type[] { typeof(Dictionary<String, Object>) });
            Dictionary<String, Object> dic = GetQueryDic();
            content = mix.Invoke(type.CreateInstance(), dic).ToString();
        }
        catch (ThreadAbortException ex)
        {
            Response.Redirect(CMXConfigBase.Current.CurrentRootPath + "/Index.aspx");
        }
        catch (Exception ex)
        {
            // 不要随便拦截异常而无所作为，会害死人的
            XTrace.WriteException(ex);

            Err("编译出错！");
        }

        Response.Write(content);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Dictionary<String, Object> GetQueryDic()
    {
        Dictionary<String, Object> dic = new Dictionary<string, object>();

        foreach (String item in Request.QueryString.AllKeys)
        {
            dic.Add(item, Request.QueryString[item]);
        }

        return dic;
    }

    private void Err(String Msg)
    {
        Response.Clear();
        Response.Write(Msg);
        Response.End();
    }
}