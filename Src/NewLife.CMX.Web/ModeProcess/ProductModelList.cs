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
                    Products = Product.Search(null, CategoryID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
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
                    Products = Product.Search(null, first.ID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                    CountNum = Article.SearchCount(new int[] { first.ID }, null, 0, 0);
                }

                Int32 PageCount = CountNum / 10 + CountNum % 10 > 0 ? 1 : 0;

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                //dic.Add("Suffix", Suffix);
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
