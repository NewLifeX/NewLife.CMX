using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
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
            var modelid = RouteData.Values["model"].ToInt();
            var catid = RouteData.Values["category"].ToInt();

            var key = p["q"];

            return Info.Search(modelid, catid, key, p);
        }

        public override ActionResult Add()
        {
            var entity = Factory.Create() as Info;
            entity.CategoryID = RouteData.Values["category"].ToInt();

            // 记下添加前的来源页，待会添加成功以后跳转
            Session["Cube_Add_Referrer"] = Request.UrlReferrer.ToString();

            return FormView(entity);
        }

        /// <summary>表单页视图。子控制器可以重载，以传递更多信息给视图，比如修改要显示的列</summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override ActionResult FormView(Info entity)
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
            if (mod == null && entity.Category != null)
            {
                mod = entity.Category.Model;
            }

            // 根据模型加载专属表单页
            if (mod != null) tmp = "~/Areas/CMS/Views/{0}/Form.cshtml".F(mod.Name ?? "Info");

            return View(tmp, entity);
        }

        public ActionResult Mod(Int32 id, Pager p = null)
        {
            if (p == null) p = new Pager();

            ViewBag.Page = p;

            // 用于显示的列
            ViewBag.Fields = base.GetFields(false);

            var list = Info.Search(id, 0, null, p);

            return View("List", list);
        }

        public ActionResult Cat(Int32 id, Pager p = null)
        {
            ViewBag.Page = p ?? new Pager();

            // 用于显示的列
            ViewBag.Fields = base.GetFields(false);

            var list = Info.Search(0, id, null, p);

            return View("List", list);
        }

        protected override Int32 OnUpdate(Info entity)
        {
            //using (var tran = Info.Meta.CreateTrans())
            //{
            var pager = ViewBag.Page as Pager;

            // 填充扩展属性
            var ext = Info.FindByID(entity.ID)?.Ext;
            if (ext is IEntity eet && pager != null)
            {
                var fact = ext.GetType().AsFactory();
                foreach (var item in fact.Fields)
                {
                    if (!pager[item.Name].IsNullOrEmpty())
                    {
                        var value = pager[item.Name];
                        // 布尔型可能传两份
                        if (item.Type == typeof(Boolean))
                        {
                            var ss = value.Split(",");
                            value = ss.FirstOrDefault();
                        }

                        eet.SetItem(item.Name, value.ChangeType(item.Type));
                    }
                }
                //eet.Update();
            }

            var rs = base.OnUpdate(entity);

            //tran.Commit();

            return rs;
            //}
        }
    }
}