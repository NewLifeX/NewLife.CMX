using NewLife.CommonEntity;
using NewLife.CommonEntity.Exceptions;
using NewLife.Log;
using NewLife.Threading;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCode;

public partial class Admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 引发反向工程
            ThreadPoolX.QueueUserWorkItem(delegate() { EntityFactory.CreateOperate(ManageProvider.Provider.ManageUserType).FindCount(); });

            IAdministrator user = ManageProvider.Provider.Current as IAdministrator;
            if (user != null)
            {
                if (String.Equals("logout", Request["action"], StringComparison.OrdinalIgnoreCase))
                {
                    IAdministrator admin = user as IAdministrator;
                    if (admin == null) admin.Logout();
                }
                else
                {
                    if (user.RoleID != 2)
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ManageProvider.Provider.Login(UserName.Text, Password.Text);
            if (ManageProvider.Provider.Current != null) Response.Redirect("Default.aspx");
        }
        catch (Exception ex)
        {
            String msg = "登录失败";
            if (ex is EntityException)
                msg += "," + ex.Message;
            else
                XTrace.WriteException(ex);
            WebHelper.Alert(msg);

        }
    }
}