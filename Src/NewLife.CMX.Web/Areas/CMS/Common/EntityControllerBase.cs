using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using NewLife.Cube;
using NewLife.Web;
using XCode;
using XCode.Configuration;
using XCode.Membership;

namespace NewLife.CMX.Web
{
    public class EntityControllerBase<TEntity> : EntityController<TEntity> where TEntity : EntityBase<TEntity>, new()
    {
        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ActionResult IndexView(Pager p)
        {
            var fields = ViewBag.Fields as List<FieldItem>;

            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].Name.EqualIgnoreCase("CreateUserID"))
                    fields[i] = Entity<TEntity>.Meta.AllFields.FirstOrDefault(e => e.Name == "CreateUserName");
                if (fields[i].Name.EqualIgnoreCase("UpdateUserID"))
                    fields[i] = Entity<TEntity>.Meta.AllFields.FirstOrDefault(e => e.Name == "UpdateUserName");
            }

            return base.IndexView(p);
        }
    }
}