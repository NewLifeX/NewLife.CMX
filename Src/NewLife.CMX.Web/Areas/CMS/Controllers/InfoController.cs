using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using NewLife.Log;
using NewLife.Web;
using XCode;
using XCode.Membership;

namespace NewLife.CMX.Web.Controllers
{
    public class InfoController : InfoController<Info> { }

    public class InfoController<TEntity> : EntityControllerBase<TEntity> where TEntity : Info<TEntity>, new()
    {
        static InfoController()
        {
            // 过滤掉一些字段
            var list = ListFields;
            list.RemoveAll(e => e.Name.EqualIgnoreCase("ModelID", "ModelName", "CategoryID", "ExtendID", "Version", "Code", "SourceID", "SourceUrl", "StatisticsID", "Summary"));

            list = FormFields;
            list.RemoveAll(e => e.Name.EqualIgnoreCase("ModelID", "CategoryID", "CategoryName", "StatisticsID", "Title", "Code", "Version", "Views"));
            var fi = Entity<TEntity>.Meta.AllFields.FirstOrDefault(e => e.Name == "StatisticsText");
            if (fi != null) list.Add(fi);
        }

        protected override IDictionary<MethodInfo, Int32> ScanActionMenu(IMenu menu)
        {
            menu.Visible = false;

            return base.ScanActionMenu(menu);
        }

        /// <summary>列表页视图。子控制器可重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected override ActionResult IndexView(Pager p)
        {
            p.Sort = "CreateTime";
            p.Desc = true;

            //LoadChannel();

            var catid = RouteData.Values["category"].ToInt();
            var list = Info<TEntity>.Search(0, catid, null, p);

            return View("List", list);
        }

        public override ActionResult Add()
        {
            var entity = Factory.Create() as TEntity;
            entity.CategoryID = RouteData.Values["category"].ToInt();

            // 记下添加前的来源页，待会添加成功以后跳转
            Session["Cube_Add_Referrer"] = Request.UrlReferrer.ToString();

            return FormView(entity);
        }

        /// <summary>表单页视图。子控制器可以重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override ActionResult FormView(TEntity entity)
        {
            // 用于显示的列
            if (ViewBag.Fields == null) ViewBag.Fields = GetFields(true);
            if (Session["cid"] != null)
            {
                entity.CategoryID = Session["cid"].ToInt();
            }
            if (Session["mid"] != null)
            {
                entity.ModelID = Session["mid"].ToInt();
            }
            var mod = entity.Model;
            var tmp = "~/Areas/CMS/Views/Info/Form.cshtml";
            if (entity.Category != null)
            {
                mod = entity.Category.Model;
                // 根据模型加载专属表单页
                tmp = "~/Areas/CMS/Views/{0}/Form.cshtml".F(mod.Name ?? "Info");
            }
            return View(tmp, entity);
        }

        public ActionResult Mod(Int32 id, Pager p = null)
        {
            if (p == null) p = new Pager();

            ViewBag.Page = p;

            // 用于显示的列
            var fields = GetFields(false);
            ViewBag.Fields = fields;

            p.Sort = "CreateTime";
            p.Desc = true;
            Session["mid"] = id;
            var list = Info<TEntity>.Search(id, 0, null, p);

            return View("List", list);
        }

        public ActionResult Cat(Int32 id, Pager p = null)
        {
            if (p == null) p = new Pager();

            ViewBag.Page = p;

            // 用于显示的列
            var fields = GetFields(false);
            ViewBag.Fields = fields;
            Session["cid"] = id;
            p.Sort = "CreateTime";
            p.Desc = true;

            var list = Info<TEntity>.Search(0, id, null, p);

            return View("List", list);
        }
    }
}