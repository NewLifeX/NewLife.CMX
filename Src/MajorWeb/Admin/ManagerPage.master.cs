using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Web;

public partial class ManagerPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e) { }

    public static void SetFormScript(Boolean IsForm)
    {
        Page p = (Page)HttpContext.Current.Handler;

        if (p == null || p.Master == null) return;

        if (IsForm)
        {
            //向根据isfrom参数决定是否需要向页面注册Js脚本
            p.ClientScript.RegisterClientScriptInclude("validate", p.ResolveUrl("~/Scripts/jquery/jquery.validate.min.js"));
            p.ClientScript.RegisterClientScriptInclude("fun", p.ResolveUrl("~/scripts/jquery/messages_cn.js"));
            p.ClientScript.RegisterClientScriptBlock(p.GetType(), "alert", @"$(function(){$('.form1').validate({errorPlacement: function (lable, element) {element.ligerTip({ content: lable.html(), appendIdTo: lable });},success: function (lable) {lable.ligerHideTip();}});$('.back').css('display','block')});", true);
        }
    }
}