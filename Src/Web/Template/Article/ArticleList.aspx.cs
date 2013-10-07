using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;
using NewLife.Linq;

public partial class Template_Article_ArticleList : NewLife.CMX.WebBase.WebPageBase
{
    public String Suffix { get { return Request["Suffix"]; } }
    public Int32 CategoryID { get { return WebHelper.RequestInt("CategoryID"); } }
    public ArticleCategory category;
    public List<Article> ListArticle = new List<Article>();

    protected override void OnInit(EventArgs e)
    {
        ArticleCategory.Meta.TableName += Suffix;
        Article.Meta.TableName += Suffix;

        category = ArticleCategory.FindByID(CategoryID);

        if (category.IsEnd)
        {
            ListArticle.AddRange(GetArticleList(CategoryID));
        }
        else
        {
            //List<Int32> categoryids = category.Childs.GetItem<Int32>(ArticleCategory._.ID);
            //List<ArticleCategory> categories = category.Childs.Where(e => e.IsEnd == true).ToList<ArticleCategory>;
            ////TODO
            //foreach (ArticleCategory item in categories)
            //{
            //    if (item.IsEnd)
            //    {

            //    }
            //    ListArticle.AddRange(GetArticleList(categoryid));
            //}
        }

        base.OnInit(e);
    }

    private List<Article> GetArticleList(int categoryid)
    {
        List<Article> articles = new List<Article>();

        foreach (Article item in Article.FindAllByCategoryID(categoryid))
        {
            articles.Add(item);
        }

        return articles;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}