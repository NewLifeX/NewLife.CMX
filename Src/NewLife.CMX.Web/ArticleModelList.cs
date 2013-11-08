using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Web.Interface;
using NewLife.Web;

namespace NewLife.CMX.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class ArticleModelList : IModeProcess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        public String Process(Dictionary<string, object> paramsdic)
        {
            try
            {
                String Suffix = paramsdic["Suffix"].ToString();
                Int32 CategoryID = 0;
                Int32.TryParse(paramsdic["CategoryID"].ToString(), out CategoryID);

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

                //Byte[] b = Encoding.UTF8.GetBytes(content);

                //MemoryStream ms = new MemoryStream();
                //StreamWriter sw = new StreamWriter(ms);

                //HttpResponse hr = new HttpResponse(sw);
                //hr.Write(content);
                //hr.Flush();
                //hr.End();

                return content;
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
    }
}
