using NewLife.CommonEntity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Main : System.Web.UI.Page
{
    protected IAdministrator admin { get { return CommonManageProvider.Provider.Current as IAdministrator; } }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}