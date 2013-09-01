using System;
using NewLife.Reflection;
using System.Web.UI.HtmlControls;
using NewLife.CommonEntity;

public partial class FormPage : System.Web.UI.MasterPage
{
    
    //表单的母板页，增加了页面js验证
    
    protected void Page_Load(object sender, EventArgs e)
    {
        FieldInfoX fix = FieldInfoX.Create(Page.GetType(), "Manager");
        if (fix != null)
        {
            IManagePage manager = fix.GetValue(Page) as IManagePage;
            if (manager != null) Navigation.Text = manager.Navigation;
        }
        //Page.ClientScript.RegisterClientScriptInclude("jquery", ResolveUrl("~/Scripts/jquery/jquery-1.9.1.min.js"));
        //Page.ClientScript.RegisterClientScriptInclude("adminstyle", ResolveUrl("~/Scripts/ui/js/ligerBuild.min.js"));
        //Page.ClientScript.RegisterClientScriptInclude("fun", ResolveUrl("~/Scripts/function.js"));       

        //HtmlLink link = new HtmlLink();
        //link.Href = ResolveUrl("~/scripts/ui/skins/Aqua/css/ligerui-all.css");
        //link.Attributes["rel"] = "stylesheet";
        //link.Attributes["type"] = "text/css";
        //Page.Header.Controls.Add(link);

        //HtmlLink link2 = new HtmlLink();
        //link2.Href = ResolveUrl("~/Admin/images/style.css");
        //link2.Attributes["rel"] = "stylesheet";
        //link2.Attributes["type"] = "text/css";
        //Page.Header.Controls.Add(link2);

    }
}