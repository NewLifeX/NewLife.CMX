using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.UI;
using NewLife.CMX;
using NewLife.CMX.Config;
using NewLife.CMX.Web;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Web;

public partial class Template_Info : Page
{

    /// <summary>内容ID</summary>
    public Int32 ID
    {

        get { return WebHelper.RequestInt("ID"); }
    }

    /// <summary>类编码</summary>
    private String Suffix
    {
        get { return Request["Suffix"]; }
    }

    /// <summary>请求地址</summary>
    private String Address
    {
        get { return Request["Address"]; }
    }

    private Channel _C;
    /// <summary>频道</summary>
    private Channel C
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
        else if (ID <= 0)
        {
            Err("未知的参数！");
        }

        String content = "";
        try
        {
            TypeX type = TypeX.GetType("NewLife.CMX.Web." + Address);
            IModelContent iml = type.CreateInstance() as IModelContent;

            iml.Suffix = Suffix;
            iml.Address = Address;
            iml.ID = ID;
            content = iml.Process();
        }
        catch (ThreadAbortException)
        {
            Response.Redirect(CMXConfigBase.Current.CurrentRootPath + "/Index.html");
        }
        catch (Exception err)
        {
            XTrace.WriteException(err);

            Err("编译出错！");
        }

        Response.Write(content);
    }

    private void Err(String Msg)
    {
        Response.Clear();
        Response.Write(Msg);
        Response.End();
    }
}