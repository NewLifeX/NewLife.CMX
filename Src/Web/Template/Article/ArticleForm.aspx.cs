using System;
using NewLife.CMX;
using NewLife.Log;
using NewLife.Web;


public partial class Template_Article_ArticleForm : NewLife.CMX.WebBase.WebPageBase
{
    public String Suffix { get { return Request["Suffix"]; } }
    public Int32 ArticleContentID { get { return WebHelper.RequestInt("ID"); } }
    public ArticleContent ArticleContent;

    protected override void OnInit(EventArgs e)
    {
        try
        {
            ArticleContent.Meta.TableName += Suffix;

            ArticleContent = ArticleContent.FindByParentIDAndNewVersion(ArticleContentID);
       
            ArticleContent.Suffix = Suffix;
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
        if (!IsPostBack)
        {
            Article.UpdateClickHit(Suffix, ArticleContent.ParentID);
        }
    }
}