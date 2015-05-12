using NewLife.CommonEntity;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCode;

public partial class Admin_Manager_Menu : MyEntityList
{
    /// <summary>实体类型</summary>
    public override Type EntityType { get { return ManageProvider.Provider.MenuType; } set { base.EntityType = value; } }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Int32 n = (Int32)MethodInfoX.Create(EntityType, "ScanAndAdd").Invoke(null);

            WebHelper.Alert("扫描完成，共添加菜单" + n + "个！");
        }
        catch (Exception ex)
        {
            WebHelper.Alert("出错！" + ex.Message);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String xml = MethodInfoX.Create(EntityType, "Export").Invoke(null, ManageProvider.Provider.MenuRoot.Childs) as String;

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "utf8";
        Response.ContentEncoding = Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode("菜单.xml") + "");
        Response.ContentType = "xml/text";
        Response.Output.Write(xml);
        Response.Flush();
        Response.End();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (!FileUpload1.HasFile) return;

        String xml = Encoding.UTF8.GetString(FileUpload1.FileBytes);

        try
        {
            MethodInfoX.Create(EntityType, "Import").Invoke(null, xml);

            gv.DataBind();
        }
        catch (Exception ex)
        {
            WebHelper.Alert(ex.Message);

            XTrace.WriteLine(ex.ToString());
        }

    }

    protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Up")
        {
            IMenu entity = ManageProvider.Provider.FindByMenuID(Convert.ToInt32(e.CommandArgument));
            if (entity != null)
            {
                entity.Up();
                gv.DataBind();
            }
        }
        else if (e.CommandName == "Down")
        {
            IMenu entity = ManageProvider.Provider.FindByMenuID(Convert.ToInt32(e.CommandArgument));
            if (entity != null)
            {
                entity.Down();
                gv.DataBind();
            }
        }
    }

    public Boolean IsFirst(Object dataItem)
    {
        IMenu menu = dataItem as IMenu;
        if (menu == null || menu.Parent == null) return true;
        return menu.ID == menu.Parent.Childs[0].ID;
    }

    public Boolean IsLast(Object dataItem)
    {
        IMenu menu = dataItem as IMenu;
        if (menu == null || menu.Parent == null) return false;
        return menu.ID == menu.Parent.Childs[menu.Parent.Childs.Count - 1].ID;
    }
}