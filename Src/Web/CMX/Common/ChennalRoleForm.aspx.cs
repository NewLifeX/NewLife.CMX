using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;
using NewLife.CommonEntity;
using XCode;

public partial class CMX_ChennalRoleForm : MyEntityForm<ChennalRole>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        frmRoleID.DataSource = Role.FindAll();
        frmRoleID.DataBind();

        frmChannelID.DataSource = Channel.FindAll(Channel._.Enable, true);
        frmChannelID.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);

    }

}