using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Log;
using NewLife.Web;

public partial class Template_Article_ArticleForm : NewLife.CMX.WebBase.WebPageBase
{
    public String Suffix { get { return Request["Suffix"]; } }
    public Int32 ArticleContentID { get { return WebHelper.RequestInt("ID"); } }
    public ArticleContent Article;

    protected override void OnInit(EventArgs e)
    {
        try
        {
            ArticleContent.Meta.TableName += Suffix;
            Article = ArticleContent.FindByParentIDAndNewVersion(ArticleContentID);

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
            ArticleContent.Meta.TableName = "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}