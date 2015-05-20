using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using NewLife.Cube;
using NewLife.Web;
using XCode;
using XCode.Membership;

namespace NewLife.CMX.Web
{
    public class TitleController<TEntity> : EntityControllerBase<TEntity> where TEntity : EntityTitle<TEntity>, new()
    {
        static TitleController()
        {
            // 过滤掉一些字段
            var list = ListFields;
            list.RemoveAll(e => e.Name.EqualIgnoreCase("CategoryID", "StatisticsID", "Remark"));

            list = FormFields;
            list.RemoveAll(e => e.Name.EqualIgnoreCase("CategoryName", "StatisticsID"));
            var fi = Entity<TEntity>.Meta.AllFields.FirstOrDefault(e => e.Name == "StatisticsText");
            if (fi != null) list.Add(fi);
        }

        protected override IDictionary<MethodInfo, int> ScanActionMenu(IMenu menu)
        {
            menu.Visible = false;

            return base.ScanActionMenu(menu);
        }

        private void LoadChannel()
        {
            if (RouteData.Values.ContainsKey("channel"))
            {
                var chn = Channel.FindByID(RouteData.Values["channel"].ToInt());
                ViewBag.Channel = chn;

                var catid = RouteData.Values["category"].ToInt();
                var cat = chn.AllCategories.FirstOrDefault(e => e.ID == catid);
                ViewBag.Category = cat;
            }
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ActionResult IndexView(Pager p)
        {
            // 加载频道和分类
            LoadChannel();

            return base.IndexView(p);
        }

        /// <summary>表单页视图。子控制器可以重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override System.Web.Mvc.ActionResult FormView(TEntity entity)
        {
            // 加载频道和分类
            LoadChannel();

            return base.FormView(entity);
        }
    }
}