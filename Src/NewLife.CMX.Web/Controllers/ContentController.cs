using System;
using System.IO;
using System.Web.Mvc;
using NewLife.Collections;
using NewLife.Web;

namespace NewLife.CMX.Web.Controllers
{
    [AllowAnonymous]
    public class ContentController : Controller
    {
        //private Model _model;
        //public Model Model { get { return _model; } }

        protected virtual Int32 PageSize { get { return 10; } }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    //拦截频道名称
        //    if (filterContext.RouteData.Values.ContainsKey("modelName"))
        //    {
        //        var name = filterContext.RouteData.Values["modelName"] + "";
        //        SetModel(name);
        //    }
        //    if (_model == null && filterContext.RouteData.Values.ContainsKey("categoryCode"))
        //    {
        //        var code = filterContext.RouteData.Values["categoryCode"] + "";
        //        var cat = Category.FindByCode(code);
        //        if (cat != null) _model = cat.Model;
        //    }
        //    if (_model == null && filterContext.RouteData.Values.ContainsKey("infoCode"))
        //    {
        //        var code = filterContext.RouteData.Values["infoCode"] + "";
        //        var inf = Info.FindByCode(code);
        //        if (inf != null) _model = inf.Model;
        //    }
        //    if (_model == null)
        //    {
        //        filterContext.Result = new HttpNotFoundResult();
        //    }
        //    base.OnActionExecuting(filterContext);
        //}

        //protected void SetModel(String name)
        //{
        //    _model = Model.FindByName(name);
        //}

        static Boolean ViewExists(String vpath)
        {
            return System.IO.File.Exists(vpath.GetFullPath());
        }

        static DictionaryCache<String, String> _cache = new DictionaryCache<String, String>(StringComparer.OrdinalIgnoreCase);
        String GetView(String name, IModel model)
        {
            var viewName = "../{0}/{1}".F(model.Name, name);

            // 如果频道模版不存在，则采用模型模版
            return _cache.GetItem(viewName, name, model, (kview, kname, kmodel) =>
            {
                // 模型目录的模版
                var view = kview;
                var vpath = "Views/{0}/{1}.cshtml".F(kmodel.Name, kname);
                if (ViewExists(vpath)) return view;

                // 内容目录的模板
                view = "../{0}/{1}".F("Content", kname);
                vpath = "Views/{0}/{1}.cshtml".F("Content", kname);
                if (ViewExists(vpath)) return view;

                // 共享目录的模板
                view = "../{0}/{1}".F("Shared", kname);
                vpath = "Views/{0}/{1}.cshtml".F("Shared", kname);
                if (ViewExists(vpath)) return view;

                return null;
            });
        }

        public ActionResult Index()
        {
            var name = RouteData.Values["modelName"] + "";
            var model = ModelX.FindByName(name);

            // 选择模版
            var tmp = model.IndexTemplate;
            if (tmp.IsNullOrEmpty() || !ViewExists(tmp)) tmp = GetView("Index", model);

            return View(tmp, model);
        }

        public ActionResult List(Int32 categoryid, Int32? pageIndex)
        {
            var cat = Category.FindByID(categoryid);
            if (cat == null) return HttpNotFound();

            return List(cat, pageIndex ?? 1);
        }

        public ActionResult List2(String categoryCode, Int32? pageIndex)
        {
            var cat = Category.FindByCode(categoryCode);
            if (cat == null) return HttpNotFound();

            return List(cat, pageIndex ?? 1);
        }

        private ActionResult List(ICategory cat, Int32 pageIndex)
        {
            // 选择模版
            var tmp = cat.GetCategoryTemplate();
            if (tmp.IsNullOrEmpty() || !ViewExists(tmp)) tmp = GetView("Category", cat.Model);

            var pager = new Pager { PageIndex = pageIndex, PageSize = PageSize };
            var list = cat.GetInfos(pager);

            ViewBag.Category = cat;
            ViewBag.Pager = pager;
            ViewBag.Infos = list;

            return View(tmp, cat);
        }

        /// <summary>信息详细页</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(Int32 id)
        {
            var inf = Info.FindByID(id);
            if (inf == null) return HttpNotFound();

            return Detail(inf);
        }

        public ActionResult Detail2(String code)
        {
            var inf = Info.FindByCode(code);
            if (inf == null) return HttpNotFound();

            return Detail(inf);
        }

        private ActionResult Detail(IInfo inf)
        {
            var cat = inf.Category;

            // 选择模版
            var tmp = cat.GetInfoTemplate();
            if (tmp.IsNullOrEmpty() || !ViewExists(tmp)) tmp = GetView("Info", inf.Model);

            // 增加浏览数
            inf.Views++;
            inf.Statistics.Increment(null);

            ViewBag.Category = cat;

            return View(tmp, inf);
        }

        public ActionResult Search(String key, Int32? pageIndex)
        {
            var name = RouteData.Values["modelName"] + "";
            var model = ModelX.FindByName(name);

            var code = RouteData.Values["categoryCode"] + "";
            var cat = Category.FindByCode(code);

            var pager = new Pager { PageIndex = pageIndex ?? 1, PageSize = PageSize };
            var list = Info.Search(model != null ? model.ID : 0, cat != null ? cat.ID : 0, key, pager);

            return View(list);
        }
    }
}