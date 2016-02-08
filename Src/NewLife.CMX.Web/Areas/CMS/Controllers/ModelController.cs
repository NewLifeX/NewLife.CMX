using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;
using NewLife.Web;
using XCode.Configuration;

namespace NewLife.CMX.Web.Controllers
{
    public class ModelController : EntityControllerBase<ModelX>
    {  
        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ActionResult IndexView(Pager p)
        {
            var fields = ViewBag.Fields as List<FieldItem>;
            fields.RemoveAll(f => f.Name.EndsWithIgnoreCase("Template") || f.Name.EqualIgnoreCase("Remark"));

            return base.IndexView(p);
        }
    }
}