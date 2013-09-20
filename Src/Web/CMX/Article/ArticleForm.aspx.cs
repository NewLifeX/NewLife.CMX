using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_ArticleForm : MyModelEntityForm<Article>
{
    String channel;

    protected override void OnInit(EventArgs e)
    {

        channel = Request["Channel"];

        Article.Meta.TableName += channel;

        base.OnInit(e);
    }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);

        Article.Meta.TableName = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);

        EntityForm.OnGetForm += EntityForm_OnGetForm;
        EntityForm.OnSaveSuccess += EntityForm_OnSaveSuccess;
    }

    void EntityForm_OnSaveSuccess(object sender, NewLife.CommonEntity.EntityFormEventArgs e)
    {
        Article.Meta.TableName = "";
    }

    void EntityForm_OnGetForm(object sender, NewLife.CommonEntity.EntityFormEventArgs e)
    {
        Article.Meta.TableName += channel;
    }
}