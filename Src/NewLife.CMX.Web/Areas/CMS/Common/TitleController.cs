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
        public TitleController()
        {
            var names = ListFields.ToList();
            names.RemoveAll(e => e.EqualIgnoreCase("CategoryID", "StatisticsID", "Remark"));
            ListFields = names.ToArray();

            names = FormFields.ToList();
            names.RemoveAll(e => e.EqualIgnoreCase("CategoryName", "StatisticsID"));
            names.Add("StatisticsText");
            FormFields = names.ToArray();
        }

        protected override IDictionary<MethodInfo, int> ScanActionMenu(IMenu menu)
        {
            menu.Visible = false;

            return base.ScanActionMenu(menu);
        }
    }
}