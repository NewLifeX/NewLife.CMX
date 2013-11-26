using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Web;
using XCode;

namespace NewLife.CMX.Web
{
    public class ArticleModelContent : IModelContent
    {
        private string _Suffix;
        /// <summary></summary>
        public string Suffix { get { return _Suffix; } set { _Suffix = value; } }

        private int _ID;
        /// <summary></summary>
        public int ID { get { return _ID; } set { _ID = value; } }

        private string _Address;
        /// <summary></summary>
        public string Address { get { return _Address; } set { _Address = value; } }

        public string Process()
        {
            //Article.Meta.TableName += Suffix;
            try
            {
                Article.Meta.TableName += Suffix;
                Article article = Article.FindByKey(ID);
                Article.ChannelSuffix = Suffix;
                //ArticleContent.Meta.TableName += Suffix;
                //ArticleContent Ac = ArticleContent.FindByParentIDAndNewVersion(ID);

                if (article == null) return "不存在该记录！";
                //article.Suffix = Suffix;

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("ID", ID.ToString());
                dic.Add("Suffix", Suffix);
                dic.Add("foot",)

                CMXEngine engine = new CMXEngine(TemplateConfig.Current);
                engine.ArgDic = dic;
                engine.Entity = article as IEntity;

                String content = engine.Render(Address + ".html");
                return content;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //ArticleContent.Meta.TableName = "";
                Article.Meta.TableName = "";
            }
        }
    }
}
