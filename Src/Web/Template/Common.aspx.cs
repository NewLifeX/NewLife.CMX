using System;
using NewLife.CMX;
using NewLife.Reflection;

public partial class Template_Common : System.Web.UI.Page
{
    public String Address
    {
        get { return Request["Address"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        String content = "";
        //try
        //{
        TypeX type = TypeX.GetType("NewLife.CMX.Web.Common");
        ICommon iml = type.CreateInstance() as ICommon;
        iml.Address = Address;
        content = iml.Process();
        //}
        //catch (ThreadAbortException)
        //{
        //    Response.Redirect(CMXConfigBase.Current.CurrentRootPath + "/Index.html");
        //}
        //catch (Exception err)
        //{
        //    XTrace.WriteException(err);

        //    Err("编译出错！");
        //}
        Response.Write(content);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Msg"></param>
    private void Err(String Msg)
    {
        Response.Clear();
        Response.Write(Msg);
        Response.End();
    }
}