using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CommonEntity;
using NewLife.Reflection;
using NewLife.Web;

public partial class ManagerPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IManagePage manager = Reflect.GetValue(Page, "Manager", false) as IManagePage;
        if (manager != null) Navigation.Text = manager.Navigation;

    }

    public static void SetFormScript(Boolean IsForm)
    {
        Page p = (Page)HttpContext.Current.Handler;
        if (p == null || p.Master == null) return;

        if (IsForm)
        {
            //向根据isfrom参数决定是否需要向页面注册Js脚本
            p.ClientScript.RegisterClientScriptInclude("validate", p.ResolveUrl("~/admin/scripts/jquery/jquery.validate.min.js"));
            p.ClientScript.RegisterClientScriptInclude("fun", p.ResolveUrl("~/admin/scripts/jquery/messages_cn.js"));
            p.ClientScript.RegisterClientScriptBlock(p.GetType(), "alert", @"$(function(){$('.form1').validate({errorPlacement: function (lable, element) {element.ligerTip({ content: lable.html(), appendIdTo: lable });},success: function (lable) {lable.ligerHideTip();}});$('.back').css('display','block')});", true);
        }
    }
}