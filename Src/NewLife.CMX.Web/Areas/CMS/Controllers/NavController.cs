using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;
using XCode;

namespace NewLife.CMX.Web.Controllers
{
    public class NavController : EntityTreeController<Nav>
    {
        static NavController()
        {
            // 过滤掉一些字段
            //var list = ListFields;
            //list.RemoveCreateField()
            //    .RemoveUpdateField()
            //    .RemoveRemarkField();
            //list.RemoveAll(e => e.Name.EqualIgnoreCase("ModelID", "CategoryID", "ExtendID", "Version", "Code", "SourceID", "SourceUrl", "StatisticsID", "Summary"));

            //list = FormFields;
            //list.RemoveAll(e => e.Name.EqualIgnoreCase("CategoryName", "StatisticsID"));
            //var fi = Entity<TEntity>.Meta.AllFields.FirstOrDefault(e => e.Name == "StatisticsText");
            //if (fi != null) list.Add(fi);
        }
    }
}