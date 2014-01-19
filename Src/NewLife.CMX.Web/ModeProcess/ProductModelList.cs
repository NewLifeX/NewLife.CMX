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
                ProductProvider.CurrentChannel = ChannelID;

                EntityList<Product> Products = new EntityList<Product>(); ;
                EntityList<ProductCategory> Categories = new EntityList<ProductCategory>(); ;
                Int32 CountNum = 0;

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

                    if (Categories != null && Categories.Count > 0)
                    {
                        ProductCategory first = Categories[0];
                        Products = Product.Search(null, first.ID, null, (Pageindex > 0 ? Pageindex - 1 : 0) * RecordNum, RecordNum);
                        CountNum = Article.SearchCount(new int[] { first.ID }, null, 0, 0);
                    }
                }

                Int32 PageCount = CountNum / 10 + CountNum % 10 > 0 ? 1 : 0;

                Dictionary<String, String> dic = new Dictionary<string, string>();
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

                CMXEngine engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                engine.Header = Header;
                engine.LeftMenu = LeftMenu;
                engine.Foot = Foot;
                engine.Suffix = Channel.Suffix;
                engine.ModelShortName = ModelShortName;
                engine.ListEntity = Products as IEntityList;
                engine.ListCategory = Categories.ConvertAll<IEntityTree>(e => e as IEntityTree);
               
                return engine.Render(Address.EnsureEnd(".html"));
            }
            finally
            {
                ProductProvider.CurrentChannel = 0;
            }
        }
    }
}
