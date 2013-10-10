using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Common;
using NewLife.CommonEntity;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Security;
using NewLife.Web;
using XCode;

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

        /// <summary>
        /// 构造方法
        /// </summary>
        public ListMenu() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Url"></param>
        public ListMenu(String Name, String Url)
        {
            this.Name = Name;
            this.Url = Url;
        }

        /// <summary>是否包含子菜单</summary>
        public Boolean IsChild { get { return (Children == null || Children.Count <= 0) ? false : true; } }

        private List<ListMenu> _Children;
        /// <summary>子菜单列表</summary>
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
            if (menus != null)
            {
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
            }
            #endregion

            #region CMX菜单
            List<ChannelRole> crlist = ChannelRole.FindAllByRoleID((icmp.Current as Admin).RoleID);
            Random r = new Random();
            foreach (ChannelRole channel in crlist)
            {
                ListMenu crlm = ConvertToMenu(null, channel.ChannelName, channel.ChannelName, "#", null);

                //crlm.Children.Add(ConvertToMenu(null, channel.ChannelName, channel.ChannelName + channel.Channel.Model.Name, channel.Channel.Model.ListTemplatePath + "?Channel=" + channel.Channel.Suffix, null));
                //crlm.Children.Add(ConvertToMenu(null, channel.ChannelName, channel.ChannelName + r.Next(), "../ListRouting.ashx?Channel=" + channel.Channel.Suffix, null));
                crlm.Children.Add(ConvertToMenu(null, "分类管理", channel.ChannelName + r.Next(), "../ListRouting.ashx?Channel=" + channel.Channel.Suffix, null));

                List<ListMenu> list = GetModelCategory2(channel.Channel.Suffix, channel.Channel.Model.ClassName);

                crlm.Children.AddRange(list);

                lm.Add(crlm);
            }
            #endregion

            return lm;
        }

        /// <summary>
        /// 临时解决方式，暂时没想到什么好的方法
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns></returns>
        private static List<ListMenu> GetModelCategory(String Suffix, String ClassName)
        {
            try
            {
                Random r = new Random();
                EntityFactory.CreateOperate(ClassName).TableName += Suffix;

                Type t = EntityFactory.Create(ClassName).GetType();

                List<ListMenu> list = new List<ListMenu>();

                Dictionary<String, String> CategoryDic = t.InvokeMember("FindChildsByNoParent", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new Object[] { 0 }) as Dictionary<String, String>;

                foreach (KeyValuePair<String, String> item in CategoryDic)
                {
                    ListMenu lm = new ListMenu(item.Value, "../FormRouting.ashx?Channel=" + Suffix + "&CategoryID=" + item.Key + "&Name=" + item.Value.Trim());

                    lm.Title = (item.Value.Trim()) + r.Next();

                    list.Add(lm);
                }
                return list;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine(ex.Message);
                WebHelper.Alert("请联系管理员！");
                return null;
            }
            finally
            {
                EntityFactory.CreateOperate(ClassName).TableName = "";

                Type t = EntityFactory.Create(ClassName).GetType();

                PropertyInfoX pix = PropertyInfoX.Create(t, "Root");

                pix.SetValue(null);
            }
        }

        /// <summary>
        /// 临时解决方式，暂时没想到什么好的方法
        /// </summary>
        /// <param name="Suffix"></param>
        /// <returns></returns>
        private static List<ListMenu> GetModelCategory2(String Suffix, String ClassName)
        {
            try
            {
                Random r = new Random();
                EntityFactory.CreateOperate(ClassName).TableName += Suffix;

                Type t = EntityFactory.Create(ClassName).GetType();

                List<ListMenu> list = new List<ListMenu>();

                Dictionary<String, String> CategoryDic = t.BaseType.InvokeMember("FindChildNameAndIDByNoParent", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new Object[] { 0 }) as Dictionary<String, String>;

                foreach (KeyValuePair<String, String> item in CategoryDic)
                {
                    ListMenu lm = new ListMenu();
                    lm.Name = item.Value;
                    lm.Title = (item.Value + r.Next()).Trim();

                    lm.Url = Convert.ToInt32(item.Key) > 0 ? "../FormRouting.ashx?Channel=" + Suffix + "&CategoryID=" + item.Key + "&Name=" + item.Value.Trim() : "#";

                    list.Add(lm);
                }
                return list;
            }
            catch (Exception ex)
            {
                XTrace.WriteLine(ex.Message);
                WebHelper.Alert("请联系管理员！");
                return null;
            }
            finally
            {
                EntityFactory.CreateOperate(ClassName).TableName = "";

                Type t = EntityFactory.Create(ClassName).GetType();

                PropertyInfoX pix = PropertyInfoX.Create(t, "Root");

                pix.SetValue(null);
            }
        }

        /// <summary>
        /// 转换为Menu
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="CustomName"></param>
        /// <param name="CustomKindTitle"></param>
        /// <param name="CustomUrl"></param>
        /// <param name="Icon"></param>
        /// <returns></returns>
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