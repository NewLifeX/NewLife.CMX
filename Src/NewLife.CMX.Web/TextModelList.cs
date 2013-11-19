using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;

namespace NewLife.CMX.Web
{
    public class TextModelList : IModeList
    {
        private string _Suffix;
        /// <summary></summary>
        public string Suffix { get { return _Suffix; } set { _Suffix = value; } }

        private int _CategoryID;
        /// <summary></summary>
        public int CategoryID { get { return _CategoryID; } set { _CategoryID = value; } }

        private string _Address;
        /// <summary></summary>
        public string Address { get { return _Address; } set { _Address = value; } }

        private int _Pageindex;
        /// <summary></summary>
        public int Pageindex { get { return _Pageindex; } set { _Pageindex = value; } }

        private int _RecordNum;
        /// <summary></summary>
        public int RecordNum { get { return _RecordNum; } set { _RecordNum = value; } }

        public string Process()
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

                CMXEngine engine = new CMXEngine(TemplateConfig.Current);
                engine.ArgDic = dic;
                engine.ListEntity = texts.ConvertAll<IEntity>(e => e as IEntity);
                engine.ListCategory = Categories.ConvertAll<IEntityTree>(e => e as IEntityTree);
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
