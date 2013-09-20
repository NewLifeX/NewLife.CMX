using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;

public partial class CMX_Product : MyModelEntityList<Product>
{
    String channel;

    protected override void OnInit(EventArgs e)
    {
        channel = Request["Channel"];

        Product.Meta.TableName += channel;

        base.OnInit(e);
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);

        Product.Meta.TableName = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}