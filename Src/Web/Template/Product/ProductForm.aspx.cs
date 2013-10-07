using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Log;
using NewLife.Web;

public partial class Template_Product_ProductForm : System.Web.UI.Page
{
    public String Suffix { get { return Request["Suffix"]; } }
    public Int32 ProductContentID { get { return WebHelper.RequestInt("ID"); } }
    public ProductContent Product;

    protected override void OnInit(EventArgs e)
    {
        try
        {
            ProductContent.Meta.TableName += Suffix;
            Product = ProductContent.FindByParentIDAndNewVersion(ProductContentID);
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
            ProductContent.Meta.TableName = "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}