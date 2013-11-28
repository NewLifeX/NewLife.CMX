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
                Text.Meta.TableName += Suffix;
                TextCategory.Meta.TableName += Suffix;

                List<Text> texts;
                List<TextCategory> Categories;

                TextCategory tc = TextCategory.FindByID(CategoryID);
                if (tc.IsEnd)
                {
                    texts = Text.Search(null, CategoryID, null, Pageindex, RecordNum);
                    Categories = TextCategory.FindAllChildsNoParent(tc.ParentID);
                }
                else
                {
                    Categories = TextCategory.FindAllChildsNoParent(CategoryID).FindAll(delegate(TextCategory art)
                    {
                        return art.IsEnd == true;
                    });
                    TextCategory first = Categories[0];
                    texts = Text.Search(null, first.ID, null, Pageindex, RecordNum);
                }

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("Suffix", Suffix);
                dic.Add("Pageindex", Pageindex.ToString());
                dic.Add("RecordNum", RecordNum.ToString());
                dic.Add("Header", Header);
                dic.Add("Foot", Foot);

                CMXEngine engine = new CMXEngine(TemplateConfig.Current);
                engine.ArgDic = dic;
                //engine.ListEntity = texts.ConvertAll<IEntity>(e => e as IEntity);
                engine.ListEntity = texts as IEntityList;
                //engine.ListCategory = Categories.ConvertAll<IEntityTree>(e => e as IEntityTree);
                engine.ListCategory = Categories as IEntityList;
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
