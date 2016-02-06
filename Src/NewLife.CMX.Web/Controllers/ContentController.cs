using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using NewLife.Collections;
using NewLife.Web;

namespace NewLife.CMX.Web.Controllers
{
    [AllowAnonymous]
    public class ContentController : Controller
    {
        private Model _model;
        public Model Model { get { return _model; } }

        protected virtual Int32 PageSize { get { return 10; } }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //拦截频道名称
            if (filterContext.RouteData.Values.ContainsKey("modelName"))
            {
                var name = filterContext.RouteData.Values["modelName"] + "";
                SetModel(name);
            }
            if (_model == null && filterContext.RouteData.Values.ContainsKey("categoryCode"))
            {
                var code = filterContext.RouteData.Values["categoryCode"] + "";
                var cat = Model.FindCategoryByCode(code);
                if (cat != null) _model = cat.Model;
            }
            if (_model == null)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetModel(String name)
        {
            _model = Model.FindByName(name);
        }

        static DictionaryCache<String, String> _cache = new DictionaryCache<String, String>(StringComparer.OrdinalIgnoreCase);
        String GetView(String name)
        {
            var viewName = "../{0}/{1}".F(Model.Name, name);

            // 如果频道模版不存在，则采用模型模版
            return _cache.GetItem(viewName, name, (k, kn) =>
            {
                var v = k;
                var vp = "Views/{0}/{1}.cshtml".F(Model.Name, kn);
                if (System.IO.File.Exists(vp.GetFullPath())) return v;

                v = "../{0}/{1}".F(Model.Name, kn);
                vp = "Views/{0}/{1}.cshtml".F(Model.Name, kn);
                if (System.IO.File.Exists(vp.GetFullPath())) return v;

                v = "../{0}/{1}".F("Content", kn);
                vp = "Views/{0}/{1}.cshtml".F("Content", kn);
                if (System.IO.File.Exists(vp.GetFullPath())) return v;

                v = "../{0}/{1}".F("Shared", kn);
                vp = "Views/{0}/{1}.cshtml".F("Shared", kn);
                if (System.IO.File.Exists(vp.GetFullPath())) return v;

                return null;
            });
        }

        public ActionResult Index()
        {
            // 选择模版
            var tmp = Model.IndexTemplate;
            if (tmp.IsNullOrEmpty()) tmp = "Model";
            var viewName = GetView(tmp);

            ViewBag.Model = Model;

            return View(viewName, Model);
        }

        public ActionResult List(Int32 categoryid, Int32? pageindex)
        {
            var cat = Model.FindCategory(categoryid);
            if (cat == null) return HttpNotFound();

            // 选择模版
            var tmp = cat.GetCategoryTemplate();
            if (tmp.IsNullOrEmpty()) tmp = "Category";
            var viewName = GetView(tmp);

            ViewBag.Model = Model;
            ViewBag.Category = cat;
            //ViewBag.PageIndex = pageindex ?? 1;
            //ViewBag.PageSize = PageSize;

            var pager = new Pager { PageIndex = pageindex ?? 1, PageSize = PageSize };
            var list = cat.GetTitles(pager);

            ViewBag.Titles = list;
            ViewBag.Pager = pager;

            return View(viewName, cat);
        }

        /// <summary>主题详细页</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(Int32 id)
        {
            var title = Model.FindTitle(id);
            if (title == null) return HttpNotFound();
            var cat = title.Category;

            // 选择模版
            var tmp = cat.GetTitleTemplate();
            if (tmp.IsNullOrEmpty()) tmp = "Title";
            var viewName = GetView(tmp);

            // 增加浏览数
            title.Views++;
            title.Statistics.Increment(null);

            ViewBag.Model = Model;
            ViewBag.Category = cat;

            return View(viewName, title);
        }

        public ActionResult List2(String categoryCode, Int32? pageindex)
        {
            var cat = Model.FindCategoryByCode(categoryCode);
            if (cat == null) return HttpNotFound();

            // 选择模版
            var tmp = cat.GetCategoryTemplate();
            if (tmp.IsNullOrEmpty()) tmp = "Category";
            var viewName = GetView(tmp);

            ViewBag.Model = Model;
            ViewBag.Category = cat;
            //ViewBag.PageIndex = pageindex ?? 1;
            //ViewBag.PageSize = PageSize;

            var pager = new Pager { PageIndex = pageindex ?? 1, PageSize = PageSize };
            var list = cat.GetTitles(pager);

            ViewBag.Titles = list;
            ViewBag.Pager = pager;

            return View(viewName, cat);
        }
    }
}