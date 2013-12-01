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
                Text.Meta.TableName = "";
                TextCategory.Meta.TableName = "";
                Text.Meta.TableName += Suffix;
                TextCategory.Meta.TableName += Suffix;

                EntityList<Text> texts;
                Int32 CountNum = 0;
                EntityList<TextCategory> Categories;

                //Channel channel = Channel.FindBySuffix(Suffix);
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
                    TextCategory first = Categories[0];
                    texts = Text.Search(null, first.ID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                    CountNum = Article.SearchCount(new int[] { first.ID }, null, 0, 0);
                }

                Int32 PageCount = CountNum / 10 + CountNum % 10 > 0 ? 1 : 0;

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("Pageindex", Pageindex.ToString());
                dic.Add("RecordNum", RecordNum.ToString());
                dic.Add("ContentAddress", channel.FormTemplate);
                dic.Add("ChannelName", ChannelName);
                dic.Add("PageCount", PageCount.ToString());
                dic.Add("CurrentPage", Pageindex + 1 + "");
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
                //engine.ListEntity = texts.ConvertAll<IEntity>(e => e as IEntity);
                engine.ListEntity = texts as IEntityList;
                engine.ListCategory = Categories.ConvertAll<IEntityTree>(e => e as IEntityTree);
                //engine.ListCategory = Categories;
                String content = engine.Render(Address + ".html");

                return content;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Text.Meta.TableName = "";
                TextCategory.Meta.TableName = "";
            }
        }
    }
}
