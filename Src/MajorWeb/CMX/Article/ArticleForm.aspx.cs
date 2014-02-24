using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;
using NewLife.CMX.Editor;
using XCode;

public partial class CMX_ArticleForm : MyModelEntityForm<Article>
{
    //public String ContentTxt { get { return Entity.ArticleContent.Content ?? ""; } }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);

        Entity.CategoryName = Entity.CategoryName ?? Request["Name"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);

        EntityForm.OnGetForm += EntityForm_OnGetForm;
    }

    void EntityForm_OnGetForm(object sender, NewLife.CommonEntity.EntityFormEventArgs e)
    {
        //ArticleContent ac = Entity.Content;
        //Entity.ConentTxt = Request["MyContent"];
        Entity.Content.Content = Request["MyContent"];
    }
}