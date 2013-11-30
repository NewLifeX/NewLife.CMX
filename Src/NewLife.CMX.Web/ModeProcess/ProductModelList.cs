using System;
using System.Collections.Generic;
using System.Text;
using XCode;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Config;

namespace NewLife.CMX.Web
{
    public class ProductModelList : ModelListBase
    {
        override public String Process()
        {
            try
            {
                Product.Meta.TableName = "";
                ProductCategory.Meta.TableName = "";
                Product.Meta.TableName += Suffix;
                ProductCategory.Meta.TableName += Suffix;

                EntityList<Product> Products;
                Int32 CountNum = 0;
                EntityList<ProductCategory> Categories;

                //Channel channel = Channel.FindBySuffix(Suffix);
                ProductCategory pc = ProductCategory.FindByID(CategoryID);
                if (pc != null && pc.IsEnd)
                {
                    Products = Product.Search(null, CategoryID, null, Pageindex, RecordNum);
                    Categories = ProductCategory.FindAllChildsNoParent(pc.ParentID);
                    CountNum = Article.SearchCount(new int[] { CategoryID }, null, 0, 0);
                }
                else
                {
                    Categories = ProductCategory.FindAllChildsNoParent(CategoryID).FindAll(delegate(ProductCategory art)
                    {
                        return art.IsEnd == true;
                    });
                    ProductCategory first = Categories[0];
                    Products = Product.Search(null, first.ID, null, Pageindex, RecordNum);
                    CountNum = Article.SearchCount(new int[] { first.ID }, null, 0, 0);
                }

                CountNum = CountNum / 10 + 1;

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                //dic.Add("Suffix", Suffix);
                dic.Add("Pageindex", Pageindex.ToString());
                dic.Add("RecordNum", RecordNum.ToString());
                dic.Add("ContentAddress", channel.FormTemplate);
                dic.Add("ChannelName", ChannelName);
                dic.Add("CountNum", CountNum.ToString());

                CMXEngine engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                engine.Header = Header;
                engine.LeftMenu = LeftMenu;
                engine.Foot = Foot;
                engine.Suffix = Suffix;
                //engine.ListEntity = Products.ConvertAll<IEntity>(e => e as IEntity);
                engine.ListEntity = Products as IEntityList;
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
                Product.Meta.TableName = "";
                ProductCategory.Meta.TableName = "";
            }
        }
    }
}
