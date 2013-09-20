using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;

public partial class CMX_ProductCategory : MyModelEntityList<ProductCategory>
{
    //protected override void OnInit(EventArgs e)
    //{
    //    Channel c = Channel.FindBySuffix(Request["Channel"]);

    //    if (c == null) throw new Exception("未知频道");

    //    ProductCategory.Meta.TableName += c.Suffix;
    //    base.OnInit(e);
    //}

    //protected override void OnSaveStateComplete(EventArgs e)
    //{
    //    base.OnSaveStateComplete(e);
    //    //EntityFactory.CreateOperate(EntityType).TableName = "";
    //    ProductCategory.Meta.TableName = "";
    //}

    //protected override void OnUnload(EventArgs e)
    //{
    //    //EntityFactory.CreateOperate(EntityType).TableName = "";
    //    ProductCategory.Meta.TableName = "";
    //    base.OnUnload(e);
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}