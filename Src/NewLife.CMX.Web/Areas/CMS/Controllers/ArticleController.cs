using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class ArticleController : TitleController<Article> { }

    public class ArticleCategoryController : CategoryController<ArticleCategory> { }

    //public class ArticleContentController : ContentController<ArticleContent> { }

    //public class ArticleStatisticsController : StatisticsController<ArticleStatistics> { }

}