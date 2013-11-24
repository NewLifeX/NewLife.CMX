using System;
using System.Collections.Generic;
using System.Text;
using XCode;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Config;

namespace NewLife.CMX.Web
{
    public class ProductModelList : IModeList
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

        public String Process()
        {
            try
            {
                Product.Meta.TableName += Suffix;
                ProductCategory.Meta.TableName += Suffix;

                List<Product> Products;
                List<ProductCategory> Categories;

                ProductCategory pc = ProductCategory.FindByID(CategoryID);
                if (pc.IsEnd)
                {
                    Products = Product.Search(null, CategoryID, null, Pageindex, RecordNum);
                    Categories = ProductCategory.FindAllChildsNoParent(pc.ParentID);
                }
                else
                {
                    Categories = ProductCategory.FindAllChildsNoParent(CategoryID).FindAll(delegate(ProductCategory art)
                    {
                        return art.IsEnd == true;
                    });
                    ProductCategory first = Categories[0];
                    Products = Product.Search(null, first.ID, null, Pageindex, RecordNum);
                }

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("Suffix", Suffix);
                dic.Add("Pageindex", Pageindex.ToString());
                dic.Add("RecordNum", RecordNum.ToString());

                CMXEngine engine = new CMXEngine(TemplateConfig.Current);
                engine.ArgDic = dic;
                engine.ListEntity = Products.ConvertAll<IEntity>(e => e as IEntity);
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
                Product.Meta.TableName = "";
                ProductCategory.Meta.TableName = "";
            }
        }
    }
}
