using NewLife.Web;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX.CMS;

public partial class Admin_Manager_SysModel : MyEntityList<SysModel>
{
    protected override void Page_Error(object sender, EventArgs e)
    {
        //base.Page_Error(sender, e);
        WebHelper.AlertAndEnd("系统模型不能删除！");
        Server.ClearError();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}