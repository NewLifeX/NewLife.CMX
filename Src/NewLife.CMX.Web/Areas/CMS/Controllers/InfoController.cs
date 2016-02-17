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
            list.RemoveAll(e => e.Name.EqualIgnoreCase("ModelID", "CategoryID", "ExtendID", "Version", "Code", "SourceID", "SourceUrl", "StatisticsID", "Summary"));

            list = FormFields;
            list.RemoveAll(e => e.Name.EqualIgnoreCase("ModelID", "CategoryID", "CategoryName", "StatisticsID", "Title", "Code", "Version", "Views"));
            var fi = Entity<TEntity>.Meta.AllFields.FirstOrDefault(e => e.Name == "StatisticsText");
            if (fi != null) list.Add(fi);
        }

        protected override IDictionary<MethodInfo, int> ScanActionMenu(IMenu menu)
        {
            menu.Visible = false;

            return base.ScanActionMenu(menu);
        }

        //private void LoadChannel()
        //{
        //    if (RouteData.Values.ContainsKey("channel"))
        //    {
        //        var chn = Channel.FindByID(RouteData.Values["channel"].ToInt());

        //        // 设置当前频道，改变分类表、主题表、内容表等扩展表的链接定向
        //        chn.Model.Provider.CurrentChannel = chn.ID;

        //        ViewBag.Channel = chn;

        //        var catid = RouteData.Values["category"].ToInt();
        //        var cat = Category.FindByID(catid);
        //        ViewBag.Category = cat;
        //    }
        //}

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
            //// 加载频道和分类
            //LoadChannel();

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

            // 根据模型加载专属表单页
            var tmp = "../{0}/Form".F(entity.ModelName);
            var tf = "~/Areas/CMS/Views/{0}/Form.cshtml".F(entity.ModelName);
            if (!System.IO.File.Exists(tf.GetFullPath())) tmp = "../Info/Form";

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

            p.Sort = "CreateTime";
            p.Desc = true;

            var list = Info<TEntity>.Search(0, id, null, p);

            return View("List", list);
        }
    }
}