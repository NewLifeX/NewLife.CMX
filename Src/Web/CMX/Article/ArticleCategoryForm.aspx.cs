using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_ArticleCategoryForm : MyModelEntityForm<ArticleCategory>
{
    private String _suffix;

    public String Suffix
    {
        get
        {
            if (_suffix == null)
                _suffix = Request["Channel"];
            return _suffix;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        ArticleCategory.Meta.TableName += Suffix;

        base.OnInit(e);
    }

    protected override void OnUnload(EventArgs e)
    {
        ArticleCategory.Meta.TableName = "";

        base.OnUnload(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);

        //EntityForm.OnValid += EntityForm_OnValid;

        //EntityForm.OnSaving += EntityForm_OnSaving;

    }

    void EntityForm_OnSaving(object sender, NewLife.CommonEntity.EntityFormEventArgs e)
    {
        ArticleCategory.Meta.TableName += Suffix;
    }

    void EntityForm_OnValid(object sender, NewLife.CommonEntity.EntityFormEventArgs e)
    {
        ArticleCategory.Meta.TableName += Suffix;
    }
}