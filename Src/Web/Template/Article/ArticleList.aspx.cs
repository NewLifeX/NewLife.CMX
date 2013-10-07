using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Web;
using NewLife.Linq;
using XCode;
using NewLife.Log;

public partial class Template_Article_ArticleList : NewLife.CMX.WebBase.WebPageBase
{
    public String Suffix { get { return Request["Suffix"]; } }

    public Int32 CategoryID { get { return WebHelper.RequestInt("CategoryID"); } }

    public ArticleCategory Category;

    public EntityList<Article> ListArticle = new EntityList<Article>();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            ArticleCategory.Meta.TableName += Suffix;
            Article.Meta.TableName += Suffix;

            Category = ArticleCategory.FindByID(CategoryID);

            if (Category.IsEnd)
            {
                ListArticle.AddRange(GetArticleList(CategoryID));
            }
            else
            {
                List<ArticleCategory> listcategory = Category.AllChilds;

                foreach (ArticleCategory item in listcategory)
                {
                    if (item.IsEnd)
                    {
                        ListArticle.AddRange(GetArticleList(item.ID));
                    }
                }
            }
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
            ArticleCategory.Meta.TableName = "";
            Article.Meta.TableName = "";
        }
    }

    private EntityList<Article> GetArticleList(int categoryid)
    {
        EntityList<Article> articles = new EntityList<Article>();

        foreach (Article item in Article.FindAllByCategoryID(categoryid))
        {
            articles.Add(item);
        }

        return articles;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        frmarticle.DataSource = ListArticle;
        frmarticle.DataBind();
    }
}