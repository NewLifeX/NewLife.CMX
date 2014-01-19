using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;

namespace NewLife.CMX.Web
{
    public class TextModelList : ModelListBase
    {
        override public string Process()
        {
            try
            {
                TextProvider.CurrentChannel = ChannelID;

                Int32 CountNum = 0;
                var texts = new EntityList<Text>();
                var Categories = new EntityList<TextCategory>();

                TextCategory tc = TextCategory.FindByID(CategoryID);
                if (tc != null && tc.IsEnd)
                {
                    texts = Text.Search(null, CategoryID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                    Categories = TextCategory.FindAllChildsNoParent(tc.ParentID);
                    CountNum = Article.SearchCount(new int[] { CategoryID }, null, 0, 0);
                }
                else
                {
                    Categories = TextCategory.FindAllChildsNoParent(CategoryID).FindAll(delegate(TextCategory art)
                    {
                        return art.IsEnd == true;
                    });

                    if (Categories != null && Categories.Count > 0)
                    {
                        TextCategory first = Categories[0];
                        texts = Text.Search(null, first.ID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                        CountNum = Article.SearchCount(new int[] { first.ID }, null, 0, 0);
                    }
                }

                Int32 PageCount = CountNum / 10 + CountNum % 10 > 0 ? 1 : 0;

                var dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("Pageindex", Pageindex.ToString());
                dic.Add("RecordNum", RecordNum.ToString());
                dic.Add("ContentAddress", Channel.FormTemplate);
                dic.Add("ChannelName", ChannelName);
                dic.Add("PageCount", PageCount > 0 ? PageCount + "" : "1");
                dic.Add("CurrentPage", (Pageindex > 0 ? Pageindex : 1) + "");
                dic.Add("BeforeUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Channel.Suffix + "_" + BeforePage + "/" + CategoryID + "/" + Channel.ListTemplate);
                dic.Add("NextUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Channel.Suffix + "_" + NextPage + "/" + CategoryID + "/" + Channel.ListTemplate);
                dic.Add("FirstUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Channel.Suffix + "/" + CategoryID + "/" + Channel.ListTemplate);
                dic.Add("LastUrl", CMXConfigBase.Current.CurrentRootPath + "/List/" + Channel.Suffix + "_" + PageCount + "/" + CategoryID + "/" + Channel.ListTemplate);

                var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                engine.Header = Header;
                engine.Foot = Foot;
                engine.LeftMenu = LeftMenu;
                engine.Suffix = Channel.Suffix;
                engine.ModelShortName = ModelShortName;
                engine.ListEntity = texts as IEntityList;
                engine.ListCategory = Categories.ConvertAll<IEntityTree>(e => e as IEntityTree);

                return engine.Render(Address.EnsureEnd(".html"));
            }
            finally
            {
                TextProvider.CurrentChannel = 0;
            }
        }
    }
}
