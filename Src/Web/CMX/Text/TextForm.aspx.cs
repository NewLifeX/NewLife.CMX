using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_TextForm : MyModelEntityForm<Text>
{
    String channel;

    protected override void OnInit(EventArgs e)
    {

        channel = Request["Channel"];

        Text.Meta.TableName += channel;

        base.OnInit(e);
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);

        //Text.Meta.TableName = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);
    }
}