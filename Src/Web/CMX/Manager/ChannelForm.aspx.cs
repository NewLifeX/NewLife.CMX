using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_ChannelForm : MyEntityForm<Channel>
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // 创建完成以后，后缀不许修改
        if (!EntityForm.IsNew) frmSuffix.Enabled = false;
        
        frmModelID.DataSource = Model.FindAll(Model._.Enable, true);
        frmModelID.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);
    }
}