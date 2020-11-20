using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewLife.Reflection;
using NewLife.Web;
using XCode;

namespace NewLife.CMX.Web.Controllers
{
    public class InfoController : EntityControllerBase<Info>
    {
        static InfoController()
        {
            // 过滤掉一些字段
            var list = ListFields;
            list.RemoveAll(e => e.Name.EqualIgnoreCase("ModelID", "ModelName", "CategoryID", "ExtendID", "Version", "Code", "SourceID", "SourceUrl", "StatisticsID", "Summary"));

            list = FormFields;
            list.RemoveAll(e => e.Name.EqualIgnoreCase("ModelID", "CategoryID", "CategoryName", "StatisticsID", "Title", "Code", "Version", "Views"));
            //var fi = Info.Meta.AllFields.FirstOrDefault(e => e.Name == "StatisticsText");
            //if (fi != null) list.Add(fi);
        }

        protected override IEnumerable<Info> Search(Pager p)
        {
            var modelid = RouteData.Values["modelId"].ToInt();
            var catid = RouteData.Values["categoryId"].ToInt();

            var key = p["q"];

            return Info.Search(modelid, catid, key, p);
        }

        public override ActionResult Add()
        {
            var entity = Factory.Create(true) as Info;
            entity.CategoryID = Request.Query["categoryId"].ToInt();

            // 记下添加前的来源页，待会添加成功以后跳转
            Session["Cube_Add_Referrer"] = Request.Headers["Referer"].FirstOrDefault() + "";

            return FormView(entity);
        }

        /// <summary>表单页视图。子控制器可以重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override ActionResult FormView(Info entity)
        {
            // 用于显示的列
            if (ViewBag.Fields == null) ViewBag.Fields = GetFields(true);

            var tmp = "~/Areas/CMS/Views/Info/Form.cshtml";

            var mod = entity.Category?.Model ?? entity.Model;

            // 根据模型加载专属表单页
            if (mod != null) tmp = "~/Areas/CMS/Views/{0}/Form.cshtml".F(mod.Name ?? "Info");

            // 没有导航
            PageSetting.EnableNavbar = false;

            return View(tmp, entity);
        }

        public ActionResult Mod(Int32 modelId, Pager p = null)
        {
            if (p == null) p = new Pager();
            ViewBag.Page = p;

            // 用于显示的列
            ViewBag.Fields = base.GetFields(false);

            // 此时不能发布
            PageSetting.EnableAdd = false;
            PageSetting.EnableNavbar = false;

            var list = Info.Search(modelId, 0, null, p);

            return View("List", list);
        }

        public ActionResult Cat(Int32 categoryId, Pager p = null)
        {
            if (p == null) p = new Pager();
            ViewBag.Page = p;

            // 用于显示的列
            ViewBag.Fields = base.GetFields(false);

            // 没有导航
            PageSetting.EnableNavbar = false;

            var list = Info.Search(0, categoryId, null, p);

            return View("List", list);
        }

        protected override Int32 OnUpdate(Info entity)
        {
            var page = ViewBag.Page as Pager;

            // 填充扩展属性
            var ext = Info.FindByID(entity.ID)?.Ext;
            if (ext is IEntity eet && page != null)
            {
                var fact = ext.GetType().AsFactory();
                foreach (var item in fact.Fields)
                {
                    if (!page[item.Name].IsNullOrEmpty())
                    {
                        var value = page[item.Name];
                        // 布尔型可能传两份
                        if (item.Type == typeof(Boolean))
                        {
                            var ss = value.Split(",");
                            if (ss.Any(e => e == "true"))
                                value = "true";
                            else
                                value = ss.FirstOrDefault();
                        }

                        eet.SetItem(item.Name, value.ChangeType(item.Type));
                    }
                }
            }

            return base.OnUpdate(entity);
        }
    }
}