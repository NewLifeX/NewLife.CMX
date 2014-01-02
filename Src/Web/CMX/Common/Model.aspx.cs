using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;

public partial class CMX_Model : MyEntityList<Model>
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request["act"] == "scan")
        {
            Int32 count = Model.Scan();
            Js.Alert("扫描到{0}个模型！", count);
        }
    }
}