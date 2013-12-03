using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;
using System.IO;

public partial class CMX_ProductForm : MyModelEntityForm<Product>
{
    //public String ContentTxt { get { return Entity.ProductContent.Content ?? ""; } }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);

        frmPhotoPathimg.ImageUrl = Request.ApplicationPath + Entity.PhotoPath;
        Entity.CategoryName = Entity.CategoryName ?? Request["Name"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);

        EntityForm.OnGetForm += EntityForm_OnGetForm;
    }

    void EntityForm_OnGetForm(object sender, NewLife.CommonEntity.EntityFormEventArgs e)
    {
        Entity.ConentTxt = Request["MyContent"];
        Entity.ProductGG = Request["MyContent2"];
        Entity.ProductTD = Request["MyContent3"];
        Entity.ProductYY = Request["MyContent4"];
        Entity.ProductPJ = Request["MyContent5"];
        Entity.ProductSP = Request["MyContent6"];

    }
}