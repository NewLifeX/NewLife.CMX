using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class ProductController : TitleController<Product> { }

    public class ProductCategoryController : CategoryController<ProductCategory> { }

    //public class ProductContentController : ContentController<ProductContent> { }

    //public class ProductStatisticsController : StatisticsController<ProductStatistics> { }

}