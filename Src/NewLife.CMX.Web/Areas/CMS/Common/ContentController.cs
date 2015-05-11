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
    public class ContentController<TEntity> : EntityController<TEntity> where TEntity : EntityContent<TEntity>, new()
    {
        protected override IDictionary<MethodInfo, int> ScanActionMenu(IMenu menu)
        {
            menu.Visible = false;

            return base.ScanActionMenu(menu);
        }
    }
}