using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Web;
using NewLife.Web;
using XCode;

namespace NewLife.CMX.Web
{
    /// <summary></summary>
    public class ArticleModelList : ModelListBase
    {
        /// <summary>
        /// 
        /// </summary>
        override public String Process()
        {
            try
            {
                Article.Meta.TableName = "";
                ArticleCategory.Meta.TableName = "";
                Article.Meta.TableName += Suffix;
                ArticleCategory.Meta.TableName += Suffix;

                Int32 CountNum = 0;
                EntityList<Article> Articles = new EntityList<Article>();
                EntityList<ArticleCategory> Categories = new EntityList<ArticleCategory>();

                //Channel channel = Channel.FindBySuffix(Suffix);
                ArticleCategory ac = ArticleCategory.FindByID(CategoryID);
                if (ac != null && ac.IsEnd)
                {
                    Articles = Article.Search(null, CategoryID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                    Categories = ArticleCategory.FindAllChildsNoParent(ac.ParentID);
                    CountNum = Article.SearchCount(new int[] { CategoryID }, null, 0, 0);
                }
                else
                {
                    Categories = ArticleCategory.FindAllChildsNoParent(CategoryID).FindAll(delegate(ArticleCategory art)
                    {
                        return art.IsEnd == true;
                    });

                    if (Categories != null && Categories.Count > 0)
                    {
                        ArticleCategory first = Categories[0];
                        Articles = Article.Search(null, first.ID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                        CountNum = Article.SearchCount(new int[] { first.ID }, null, 0, 0);
                    }
                }

                PageCount = CountNum / 10 + (CountNum % 10 > 0 ? 1 : 0);

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("Pageindex", Pageindex.ToString());
                dic.Add("RecordNum", RecordNum.ToString());
                dic.Add("ContentAddress", channel.FormTemplate);
                dic.Add("ChannelName", ChannelName);
                dic.Add("PageCount", PageCount > 0 ? PageCount + "" : "1");
                dic.Add("CurrentPage", Pageindex > 0 ? Pageindex + "" : "1");
                dic.Add("BeforeUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Suffix + "_" + BeforePage + "/" + CategoryID + "/" + channel.ListTemplate);
                dic.Add("NextUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Suffix + "_" + NextPage + "/" + CategoryID + "/" + channel.ListTemplate);
                dic.Add("FirstUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Suffix + "/" + CategoryID + "/" + channel.ListTemplate);
                dic.Add("LastUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Suffix + "_" + PageCount + "/" + CategoryID + "/" + channel.ListTemplate);

                CMXEngine engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                engine.Header = Header;
                engine.Foot = Foot;
                engine.LeftMenu = LeftMenu;
                engine.Suffix = Suffix;
                
                engine.ListEntity = Articles as IEntityList;
                engine.ListCategory = Categories.ConvertAll<IEntityTree>(e => e as IEntityTree);
                String content = engine.Render(Address + ".html");

                return content;
            }
            finally
            {
                Article.Meta.TableName = "";
                ArticleCategory.Meta.TableName = "";
            }
        }
    }
}
