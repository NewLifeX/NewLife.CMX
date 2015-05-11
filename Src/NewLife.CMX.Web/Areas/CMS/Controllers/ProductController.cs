using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class ProductController : EntityController<Product> { }

    public class ProductCategoryController : EntityController<ProductCategory> { }

    public class ProductContentController : EntityController<ProductContent> { }

    public class ProductStatisticsController : EntityController<ProductStatistics> { }

}