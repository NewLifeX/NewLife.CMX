using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class TextController : EntityController<Text> { }

    public class TextCategoryController : EntityController<TextCategory> { }

    public class TextContentController : EntityController<TextContent> { }

    public class TextStatisticsController : EntityController<TextStatistics> { }

}