using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using NewLife.Web;
using XCode;

public partial class Template_Article_ArticleModelList : System.Web.UI.Page
{
    public String Suffix { get { return Request["Suffix"]; } }

    public Int32 CategoryID { get { return WebHelper.RequestInt("CategoryID"); } }

    protected override void OnInit(EventArgs e)
    {
        try
        {
            Article.Meta.TableName += Suffix;
            ArticleCategory.Meta.TableName += Suffix;

            List<Article> Articles;
            List<ArticleCategory> Categories;

            Dictionary<String, Object> dic = new Dictionary<string, object>();
            ArticleCategory ac = ArticleCategory.FindByID(CategoryID);
            if (ac.IsEnd)
            {
                Articles = Article.FindAllByCategoryID(CategoryID);
                Categories = ArticleCategory.FindAllChildsNoParent(ac.ParentID);
            }
            else
            {
                Categories = ArticleCategory.FindAllChildsNoParent(CategoryID).FindAll(delegate(ArticleCategory art)
                {
                    return art.IsEnd == true;
                });
                ArticleCategory first = Categories[0];
                Articles = Article.FindAllByCategoryID(first.ID);
            }

            dic.Add("ArticleList", Articles);
            dic.Add("CategoryList", Categories);

            CMXEngine engine = new CMXEngine(TemplateConfig.Current);
            engine.ArgDic = dic;
            String content = engine.Render("ArticleModelList.html");
            Response.HeaderEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }
        catch (Exception ex)
        {

             throw;
        }
        finally
        {
            Article.Meta.TableName = "";
            ArticleCategory.Meta.TableName = "";
        }
    }

    private bool pro(ArticleCategory obj)
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}