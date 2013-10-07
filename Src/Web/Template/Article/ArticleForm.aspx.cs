using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;

public partial class Template_Article_ArticleForm : NewLife.CMX.WebBase.WebPageBase
{
    public String Suffix { get { return Request["Suffix"]; } }
    public Int32 ArticleContentID { get { return WebHelper.RequestInt("ID"); } }
    public ArticleContent article;

    protected override void OnInit(EventArgs e)
    {
        ArticleContent.Meta.TableName += Suffix;
        article = ArticleContent.FindByKey(ArticleContentID);
        ArticleContent.Meta.TableName = "";

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}