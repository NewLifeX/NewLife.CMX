using System;
using System.Collections.Generic;
using System.Reflection;
using NewLife.Common;
using NewLife.CommonEntity;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Web;
using XCode;

namespace NewLife.CMX.Web
{
    /// <summary>列表菜单</summary>
    public class ListMenu
    {
        private String _Name;
        /// <summary>名称</summary>
        public String Name { get { return _Name; } set { _Name = value; } }

        private String _Url;
        /// <summary>地址</summary>
        public String Url { get { return _Url; } set { _Url = value; } }

        private String _Title;
        /// <summary>标题</summary>
        public String Title { get { return _Title; } set { _Title = value; } }

        private String _Icon;
        /// <summary>图标路径</summary>
        public String Icon { get { return _Icon; } set { _Icon = value; } }

        /// <summary>构造方法</summary>
        public ListMenu() { }

        /// <summary>构造方法</summary>
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

        /// <summary>获取菜单</summary>
        /// <returns></returns>
        public static List<ListMenu> GetMenus()
        {
            var lm = new List<ListMenu>();
            var icmp = CommonManageProvider.Provider;
            Int32 mid = WebHelper.RequestInt("ID");

            var menus = icmp.GetMySubMenus(mid);

            #region 系统菜单
            if (menus != null)
            {
                foreach (IMenu menu in menus)
                {
                    if (!menu.IsShow) continue;

                    var lmsingle = ConvertToMenu(menu, null, "Sys", null, null);

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
            var crlist = ChannelRole.FindAllByRoleID((icmp.Current as Admin).RoleID);
            Random r = new Random();
            foreach (var cr in crlist)
            {
                var chn = cr.Channel;
                //隐藏不启用的频道
                if (!chn.Enable) continue;

                var crlm = ConvertToMenu(null, chn.Name, chn.Name, "#", null);

                crlm.Children.Add(ConvertToMenu(null, "分类管理", chn.Name + r.Next(), "../ListRouting.ashx?Channel=" + chn.ID + "&ModelID=" + chn.ModelID, null));

                var list = GetModelCategory3(chn, 2);
                if (list != null) crlm.Children.AddRange(list);

                lm.Add(crlm);
            }
            #endregion

            return lm;
        }

        /// <summary>
        /// 查询指定深度的所有子菜单
        /// </summary>
        /// <param name="chn"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        private static List<ListMenu> GetModelCategory3(Channel chn, Int32 Deepth)
        {
            IModelProvider provider = chn.Model.Provider;
            var eop = EntityFactory.CreateOperate(provider.CategoryType);
            var t = eop.Default.GetType();
            try
            {
                Random r = new Random();
                eop.TableName = null;
                eop.TableName += chn.Suffix;

                var list = new List<ListMenu>();

                var CategoryDic = t.BaseType.InvokeMember("FindChildNameAndIDByNoParent", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new Object[] { 0, 2 }) as Dictionary<Int32, String>;

                foreach (var item in CategoryDic)
                {
                    var lm = new ListMenu();
                    lm.Name = item.Value;
                    lm.Title = (item.Value + r.Next()).Trim();

                    lm.Url = item.Key > 0
                        ? "../FormRouting.ashx?Channel=" + chn.ID + "&CategoryID=" + item.Key + "&Name=" + item.Value.Trim() + "&ModelID=" + chn.ModelID
                        : "../ListRouting.ashx?Channel=" + chn.ID + "&CID=" + -item.Key + "&ModelID=" + chn.ModelID;

                    list.Add(lm);
                }

                return list;
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                WebHelper.Alert("请联系管理员！");
                return null;
            }
            finally
            {
                eop.TableName = null;
                t.SetValue("Root", null);
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
            var lm = new ListMenu();

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