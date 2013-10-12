using System;
using System.Collections.Generic;
using System.Web.UI;
using NewLife.CMX;
using NewLife.CommonEntity;
using NewLife.Log;
using NewLife.Web;
using XCode;

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
                NewCenter.Scoure = GetArticleList(new int[] { CategoryID });
            }
            else
            {
                List<Int32> listcategory = Category.AllChilds.FindAll(ArticleCategory._.IsEnd, true).GetItem<Int32>(ArticleCategory._.ID);

                NewCenter.Scoure = GetArticleList(listcategory.ToArray());

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

    private EntityList<Article> GetArticleList(Int32[] categoryid)
    {
        return Article.Search(categoryid, null, AspNetPager1.StartRecordIndex - 1, AspNetPager1.PageSize);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            kind.ChannelSuffix = Suffix;
            kind.CategoryID = CategoryID;

            Page.EnableViewState = false;

            AspNetPager1.UrlRewritePattern = "/List/" + Suffix + "/" + CategoryID + "_{0}.aspx";

            AspNetPager1.PageSize = 13;

            ArticleCategory.Meta.TableName += Suffix;
            Article.Meta.TableName += Suffix;

            Category = ArticleCategory.FindByID(CategoryID);

            if (Category.IsEnd)
            {
                AspNetPager1.RecordCount = GetArticleList(new int[] { CategoryID }).Count;
            }
            else
            {
                List<Int32> listcategory = Category.AllChilds.FindAll(ArticleCategory._.IsEnd, true).GetItem<Int32>(ArticleCategory._.ID);
                AspNetPager1.RecordCount = GetArticleList(listcategory.ToArray()).Count;
            }
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        try
        {
            ArticleCategory.Meta.TableName += Suffix;
            Article.Meta.TableName += Suffix;

            Category = ArticleCategory.FindByID(CategoryID);

            if (Category.IsEnd)
            {
                NewCenter.Scoure = GetArticleList(new int[] { CategoryID });
            }
            else
            {
                List<Int32> listcategory = Category.AllChilds.FindAll(ArticleCategory._.IsEnd, true).GetItem<Int32>(ArticleCategory._.ID);
                NewCenter.Scoure = GetArticleList(listcategory.ToArray());
            }
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
}