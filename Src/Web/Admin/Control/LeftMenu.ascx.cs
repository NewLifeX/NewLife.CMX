using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Common;
using NewLife.CommonEntity;
using NewLife.Security;
using NewLife.Web;

public partial class Admin_LeftMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    String listart = "<li>";
    String liend = "</li>";
    String endStr = "</ul></div>";
    //String uiclass = "class=nlist";

    public String LoadMenu()
    {
        List<ListMenu> lm = ListMenu.GetMenus();

        StringBuilder sb = new StringBuilder();

        sb = FormatMenu(lm);

        return sb.ToString();
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
                    sb.AppendLine(listart + "<a class=\"l-link\" href=\"javascript:f_addTab('" + child.Title + "','" + child.Name + "','" + child.Url + "')\">" + child.Name + "</a>" + liend);
                }
            }
            sb.AppendLine(endStr);
        }
        return sb;
    }

    public class ListMenu
    {
        private String _Name;
        //名称
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private String _Url;
        //地址
        public String Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        private String _Title;
        //标题
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private String _Icon;
        //图标路径
        public String Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        //是否包含子菜单
        public Boolean IsChild { get { return (Children == null || Children.Count <= 0) ? false : true; } }

        private List<ListMenu> _Children;
        //子菜单列表
        public List<ListMenu> Children
        {
            get
            {
                if (_Children == null)
                    _Children = new List<ListMenu>();
                return _Children;
            }
            set { _Children = value; }
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public static List<ListMenu> GetMenus()
        {
            List<ListMenu> lm = new List<ListMenu>();

            ICommonManageProvider icmp = CommonManageProvider.Provider;

            Int32 MenuID = WebHelper.RequestInt("ID");

            IList<IMenu> menus = icmp.GetMySubMenus(MenuID);

            #region 系统菜单
            foreach (IMenu menu in menus)
            {
                if (!menu.IsShow) continue;

                ListMenu lmsingle = ConvertToMenu(menu, null, "Sys", null, null);

                if (menu.Childs.Count > 0)
                {
                    foreach (IMenu child in menu.Childs)
                    {
                        if (!child.IsShow) continue;

                        lmsingle.Children.Add(ConvertToMenu(child, null, "SysChild", null, null));
                    }
                }

                lm.Add(lmsingle);
            }
            #endregion

            #region CMX菜单
            List<ChannelRole> crlist = ChannelRole.FindAllByRoleID((icmp.Current as Admin).RoleID);

            foreach (ChannelRole channel in crlist)
            {
                ListMenu crlm = ConvertToMenu(null, channel.ChannelName, channel.ChannelName, "#", null);

                crlm.Children.Add(ConvertToMenu(null, channel.Channel.Model.Name, channel.Channel.Model.Name, channel.Channel.Model.ListTemplatePath, null));

                lm.Add(crlm);
            }
            #endregion

            return lm;
        }

        //转换为Menu
        public static ListMenu ConvertToMenu(IMenu menu, String CustomName, String CustomKindTitle, String CustomUrl, String Icon)
        {
            ListMenu lm = new ListMenu();

            if (menu == null)
            {
                lm.Name = CustomName;
                lm.Title = PinYin.GetFirst(CustomKindTitle);
                lm.Url = CustomUrl;
            }
            else
            {
                lm.Name = String.IsNullOrEmpty(CustomName) ? menu.Name : CustomName;
                lm.Title = CustomKindTitle + "_" + PinYin.GetFirst(menu.Name);
                lm.Url = menu.Url;
            }
            lm.Icon = Icon;

            return lm;
        }
    }
}