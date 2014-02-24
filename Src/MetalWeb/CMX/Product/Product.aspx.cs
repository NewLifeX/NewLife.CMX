using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;

public partial class CMX_Product : MyModelEntityList<Product>
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ods_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["CategoryID"] = WebHelper.RequestInt("CategoryID");
    }
}