using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NewLife.CMX.Web;
//using NewLife.CMX.Web;

public partial class Admin_LeftMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("LeftMenu.ashx");
    }

    String listart = "<li>";
    String liend = "</li>";
    String endStr = "</ul></div>";
    String uiclass = "class=nlist";

    public String LoadMenu()
    {
        List<ListMenu> lm = ListMenu.GetMenus();

        StringBuilder sb = new StringBuilder();

        sb = FormatMenu(lm);

        return FormatMenu(ListMenu.GetMenus()).ToString();
    }

    private StringBuilder FormatMenu(List<ListMenu> lm)
    {
        StringBuilder sb = new StringBuilder();

        foreach (ListMenu menu in lm)
        {
            if (sb.Length == 0)
            {
                sb.AppendLine("<div title=\"" + menu.Name + "\" class=\"l-scroll\">");
                sb.AppendLine("<ul style=\"margin-top: 3px;\" class=\"nlist\">");
            }
            else
            {
                sb.AppendLine("<div title=\"" + menu.Name + "\">");
                sb.AppendLine("<ul class=\"nlist\">");
            }
            if (menu.IsChild)
            {
                foreach (ListMenu child in menu.Children)
                {
                    if (!String.IsNullOrEmpty(child.Url))
                    {
                        Regex r = new Regex(@"((\.\./)*)Admin/(.*)|((\.\./)*)CMX/(.*)");
                        Match m = r.Match(child.Url);
                        if (m.Groups.Count > 1)
                        {
                            if (!String.IsNullOrEmpty(m.Groups[1].ToString()))
                                child.Url = child.Url.Replace(m.Groups[1].ToString(), "../");
                            if (!String.IsNullOrEmpty(m.Groups[4].ToString()))
                                child.Url = child.Url.Replace(m.Groups[4].ToString(), "../");
                        }
                    }

                    sb.AppendLine(listart + "<a class=\"l-link\" href=\"javascript:f_addTab('" + child.Title + "','" + child.Name + "','" + child.Url + "')\">" + child.Name + "</a>" + liend);
                }
            }
            sb.AppendLine(endStr);
        }
        return sb;
    }
}