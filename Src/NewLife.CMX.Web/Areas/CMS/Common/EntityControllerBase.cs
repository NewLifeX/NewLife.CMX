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

            FieldFilter(fields);

            return base.IndexView(p);
        }

        /// <summary>表单页视图。子控制器可以重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override ActionResult FormView(TEntity entity)
        {
            var fields = ViewBag.Fields as List<FieldItem>;
            if (fields == null) ViewBag.Fields = fields = Entity<TEntity>.Meta.Fields.ToList();

            FieldFilter(fields);

            return base.FormView(entity);
        }

        void FieldFilter(List<FieldItem> fields)
        {
            var fs = Entity<TEntity>.Meta.AllFields;
            
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].Name.EqualIgnoreCase("CreateUserID"))
                {
                    if (!fields.Any(e => e.Name == "CreateUserName"))
                        fields[i] = fs.FirstOrDefault(e => e.Name == "CreateUserName");
                    else
                        fields.RemoveAt(i--);
                }
                if (fields[i].Name.EqualIgnoreCase("UpdateUserID"))
                {
                    if (!fields.Any(e => e.Name == "UpdateUserName"))
                        fields[i] = fs.FirstOrDefault(e => e.Name == "UpdateUserName");
                    else
                        fields.RemoveAt(i--);
                }
            }
        }
    }
}