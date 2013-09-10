using NewLife.CommonEntity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Default : Page
{
    protected IAdministrator admin { get { return CommonManageProvider.Provider.Current as IAdministrator; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (admin == null) Response.Redirect("Login.aspx");
    }

    public String SysMenu
    {
        get
        {
            //<div title="控制面板" iconcss="menu-icon-setting">
            StringBuilder menuSB = new StringBuilder();
            List<IMenu> list2 = admin.Role.GetMySubMenus(0);
            foreach (IMenu item in list2)
            {
                menuSB.AppendLine(String.Format("<div title=\"{0}\" iconcss=\"menu-icon-setting\"><ul class=\"nlist\">", item.Name));
                menuSB.AppendLine("</ul></div>");
            }
            return menuSB.ToString();
        }
    }
    protected void lbtnExit_Click(object sender, EventArgs e)
    {
        admin.Logout();
        Response.Redirect("Login.aspx");
    }
}