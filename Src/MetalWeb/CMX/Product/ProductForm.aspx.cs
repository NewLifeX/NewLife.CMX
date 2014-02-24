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
        Entity.Content.Content = Request["MyContent"];
        Entity.Content.Specification = Request["MyContent2"];
        Entity.Content.Feature = Request["MyContent3"];
        Entity.Content.App = Request["MyContent4"];
        Entity.Content.Fitting = Request["MyContent5"];
        Entity.Content.Video = Request["MyContent6"];
    }
}