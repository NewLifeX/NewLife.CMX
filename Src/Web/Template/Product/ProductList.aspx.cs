using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.CMX;
using NewLife.Log;
using NewLife.Web;
using XCode;

public partial class Template_Product_ProductList : System.Web.UI.Page
{
    public String Suffix { get { return Request["Suffix"]; } }

    public Int32 CategoryID { get { return WebHelper.RequestInt("CategoryID"); } }

    public ProductCategory Category;

    public EntityList<Product> ListProduct = new EntityList<Product>();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            ProductCategory.Meta.TableName += Suffix;
            Product.Meta.TableName += Suffix;

            Category = ProductCategory.FindByID(CategoryID);

            if (Category.IsEnd)
            {
                ListProduct.AddRange(GetProductList(CategoryID));
            }
            else
            {
                List<ProductCategory> listcategory = Category.AllChilds;

                foreach (ProductCategory item in listcategory)
                {
                    if (item.IsEnd)
                    {
                        ListProduct.AddRange(GetProductList(item.ID));
                    }
                }
            }
            base.OnInit(e);
        }
        catch (Exception ex)
        {
            WebHelper.Alert("信息异常，请联系管理员！");
            XTrace.WriteLine(ex.Message);
            return;
        }
        finally
        {
            ProductCategory.Meta.TableName = "";
            Product.Meta.TableName = "";
        }
    }

    private EntityList<Product> GetProductList(int categoryid)
    {
        EntityList<Product> Products = new EntityList<Product>();

        foreach (Product item in Product.FindAllByCategoryID(categoryid))
        {
            Products.Add(item);
        }

        return Products;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        frmproduct.DataSource = ListProduct;
        frmproduct.DataBind();
    }
}