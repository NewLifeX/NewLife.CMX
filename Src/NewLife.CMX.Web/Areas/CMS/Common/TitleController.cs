using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using NewLife.Cube;
using XCode;
using XCode.Membership;

namespace NewLife.CMX.Web
{
    public class TitleController<TEntity> : EntityControllerBase<TEntity> where TEntity : EntityTitle<TEntity>, new()
    {
        static TitleController()
        {
            // 过滤掉一些字段
            var list = ListFields ?? Entity<TEntity>.Meta.Fields.ToList();
            list.RemoveAll(e => e.Name.EqualIgnoreCase("CategoryID", "StatisticsID", "Remark"));
            ListFields = list;

            list = FormFields ?? Entity<TEntity>.Meta.Fields.ToList();
            list.RemoveAll(e => e.Name.EqualIgnoreCase("CategoryName", "StatisticsID"));
            var fi = Entity<TEntity>.Meta.AllFields.FirstOrDefault(e => e.Name == "StatisticsText");
            if (fi != null) list.Add(fi);
            FormFields = list;
        }

        protected override IDictionary<MethodInfo, int> ScanActionMenu(IMenu menu)
        {
            menu.Visible = false;

            return base.ScanActionMenu(menu);
        }
    }
}