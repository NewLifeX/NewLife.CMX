using System;
using System.Collections.Generic;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;

namespace NewLife.CMX.Web
{
    /// <summary></summary>
    public class ArticleModelList : ModelListBase
    {
        /// <summary>处理</summary>
        public override String Process()
        {
            try
            {
                ArticleProvider.CurrentChannel = ChannelID;

                Int32 CountNum = 0;
                var artList = new EntityList<Article>();
                var catList = new EntityList<ArticleCategory>();

                var cat = ArticleCategory.FindByID(CategoryID);
                if (cat != null && cat.IsEnd)
                {
                    artList = Article.Search(null, CategoryID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                    catList = ArticleCategory.FindAllChildsNoParent(cat.ParentID);
                    CountNum = Article.SearchCount(new int[] { CategoryID }, null, 0, 0);
                }
                else
                {
                    catList = ArticleCategory.FindAllChildsNoParent(CategoryID).FindAll(art => art.IsEnd);

                    if (catList != null && catList.Count > 0)
                    {
                        var first = catList[0];
                        artList = Article.Search(null, first.ID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                        CountNum = Article.SearchCount(new int[] { first.ID }, null, 0, 0);
                    }
                }

                PageCount = CountNum / 10 + (CountNum % 10 > 0 ? 1 : 0);

                var dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("Pageindex", Pageindex.ToString());
                dic.Add("RecordNum", RecordNum.ToString());
                dic.Add("ContentAddress", Channel.FormTemplate);
                dic.Add("ChannelName", ChannelName);
                dic.Add("PageCount", PageCount > 0 ? PageCount + "" : "1");
                dic.Add("CurrentPage", Pageindex > 0 ? Pageindex + "" : "1");
                dic.Add("BeforeUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Channel.Suffix + "_" + BeforePage + "/" + CategoryID + "/" + Channel.ListTemplate);
                dic.Add("NextUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Channel.Suffix + "_" + NextPage + "/" + CategoryID + "/" + Channel.ListTemplate);
                dic.Add("FirstUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Channel.Suffix + "/" + CategoryID + "/" + Channel.ListTemplate);
                dic.Add("LastUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Channel.Suffix + "_" + PageCount + "/" + CategoryID + "/" + Channel.ListTemplate);

                var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                //engine.Header = Header;
                //engine.Foot = Foot;
                //engine.LeftMenu = LeftMenu;
                engine.Suffix = String.IsNullOrEmpty(Channel.Suffix) ? "$" : Channel.Suffix;
                engine.ModelShortName = Channel.Model.ShortName;

                engine.ListEntity = artList as IEntityList;
                engine.ListCategory = catList.ConvertAll<IEntityTree>(e => e as IEntityTree);

                //return engine.Render(Address + ".html");
                return engine.Render(Address.EnsureEnd(".html"));

                //return content;
            }
            finally
            {
                ArticleProvider.CurrentChannel = 0;
            }
        }
    }
}