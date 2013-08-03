using NewLife.CommonEntity;
using NewLife.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCode;

public partial class Admin_Manager_Log : MyEntityList
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Type type = CommonManageProvider.Provider.LogType;
        ods.TypeName = type.FullName;
        ods.DataObjectTypeName = type.FullName;
        odsCategory.TypeName = type.FullName;
        odsCategory.DataObjectTypeName = type.FullName;
        
        if (!IsPostBack)
        {
            IEntityOperate eop = EntityFactory.CreateOperate(CommonManageProvider.Provider.AdminstratorType);
            if (eop != null)
            {
                // 管理员选项最多只要50个
                ddlAdmin.DataSource = eop.FindAll(null, null, null, 0, 50);
                ddlAdmin.DataBind();
            }
        }
    }

    protected void Export_Click(object sender, EventArgs e)
    {
    
        String xml = EntityFactory.CreateOperate(CommonManageProvider.Provider.LogType).FindAll().ToXml();
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "utf8";
        Response.ContentEncoding = Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode("日志信息.xml") + "");
        Response.ContentType = "xml/text";
        Response.Output.Write(xml);
        Response.Flush();
        Response.End();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}