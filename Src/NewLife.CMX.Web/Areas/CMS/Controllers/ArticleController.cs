using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class ArticleController : EntityController<Article> { }

    public class ArticleCategoryController : EntityController<ArticleCategory> { }

    public class ArticleContentController : EntityController<ArticleContent> { }

    public class ArticleStatisticsController : EntityController<ArticleStatistics> { }

}