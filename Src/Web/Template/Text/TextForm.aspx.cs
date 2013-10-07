using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Log;
using NewLife.Web;

public partial class Template_Text_TextForm : System.Web.UI.Page
{
    public String Suffix { get { return Request["Suffix"]; } }
    public Int32 TextContentID { get { return WebHelper.RequestInt("ID"); } }
    public TextContent Text;

    protected override void OnInit(EventArgs e)
    {
        try
        {
            TextContent.Meta.TableName += Suffix;
            Text = TextContent.FindByParentIDAndNewVersion(TextContentID);

            base.OnInit(e);
        }
        catch (Exception ex)
        {
            WebHelper.Alert("信息异常，请联系管理员！");
            XTrace.WriteLine(ex.Message);
            return;
        }
        finally
        {
            TextContent.Meta.TableName = "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}