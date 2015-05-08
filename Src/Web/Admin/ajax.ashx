<%@ WebHandler Language="C#" Class="ajax" %>

using System;
using System.Web;
using System.Text;
using System.Web.SessionState;
using NewLife.Web;
using NewLife.CommonEntity;
using XCode.Membership;

public class ajax : IHttpHandler, IRequiresSessionState
{
    /// <summary>获取整型参数</summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static String RequestStr(String name)
    {
        String str = HttpContext.Current.Request[name];
        return !String.IsNullOrEmpty(str) ? str : String.Empty;
    }
    /// <summary> 删除最后结尾的指定字符后的字符</summary>
    public static string DelLastChar(string str, string strchar)
    {
        if (string.IsNullOrEmpty(str))
            return "";
        if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }
        return str;
    }
    protected IUser admin { get { return ManageProvider.Provider.Current as IUser; } }

    public void ProcessRequest(HttpContext context)
    {
        String Action = RequestStr("action");
        switch (Action)
        {
            case "channel": //
                channel(context);
                break;
            default:
                break;
        }
    }
    #region 加载菜单
    /// <summary>管理系统菜单</summary>
    /// <param name="context"></param>
    public void channel(HttpContext context)
    {
        if (admin == null) context.Response.Write("没有登陆");
        StringBuilder strTxt = new StringBuilder();
        strTxt.Append("[");
        System.Collections.Generic.IList<IMenu> list2 = CommonManageProvider.Provider.GetMySubMenus(0);
        foreach (IMenu item in list2)
        {
            strTxt.Append("{");
            strTxt.Append("\"text\":\"" + item.Name + "\",");
            strTxt.Append("\"isexpand\":\"false\",");
            strTxt.Append("\"children\":[");
            strTxt.Append(SubMenu(item.ID));
            strTxt.Append("]");
            strTxt.Append("}");
            strTxt.Append(",");
        }
        string newTxt = DelLastChar(strTxt.ToString(), ",") + "]";
        context.Response.Write(newTxt);
    }
    public String SubMenu(Int32 ParentID)
    {
        StringBuilder strTxt = new StringBuilder();
        System.Collections.Generic.IList<IMenu> list2 = CommonManageProvider.Provider.GetMySubMenus(ParentID);

        int j = 1;
        if (list2 != null && list2.Count > 0)
        {
            foreach (IMenu nav in list2)
            {
                strTxt.Append("{");
                strTxt.Append("\"text\":\"" + nav.Name + "\",");
                if (String.IsNullOrEmpty(SubMenu(nav.ID)))
                {
                    strTxt.Append("\"url\":\"" + nav.Url + "\""); //此处要优化，加上nav.nav_url网站目录标签替换
                    strTxt.Append("}");
                    if (j < list2.Count) { strTxt.Append(","); }
                }
                else
                {
                    strTxt.Append("\"children\":[");
                    strTxt.Append(SubMenu(nav.ID));
                    strTxt.Append("]");
                    strTxt.Append("}");
                    if (j < list2.Count) { strTxt.Append(","); }
                }
                j++;
            }
        }
        return strTxt.ToString();
    }
    #endregion
    public bool IsReusable { get { return false; } }

}