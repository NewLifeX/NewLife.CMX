using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_ProductForm : MyModelEntityForm<Product>
{
    String channel;

    protected override void OnInit(EventArgs e)
    {

        channel = Request["Channel"];

        Product.Meta.TableName += channel;

        base.OnInit(e);
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);

        Product.Meta.TableName = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);
    }
}