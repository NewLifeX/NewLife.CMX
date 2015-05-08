using NewLife.CommonEntity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCode.Membership;

public partial class Admin_Main : System.Web.UI.Page
{
    protected IUser admin { get { return ManageProvider.Provider.Current as IUser; } }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}