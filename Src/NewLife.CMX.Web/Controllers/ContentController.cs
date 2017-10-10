using System;
using System.IO;
using System.Web.Mvc;
using NewLife.Collections;
using NewLife.Web;
using XCode;

namespace NewLife.CMX.Web.Controllers
{
    [AllowAnonymous]
    public class ContentController : Controller
    {
        protected virtual Int32 PageSize { get { return 10; } }

        static Boolean ViewExists(String vpath)
        {
            return System.IO.File.Exists(vpath.GetFullPath());
        }

        static DictionaryCache<String, String> _cache = new DictionaryCache<String, String>(StringComparer.OrdinalIgnoreCase);
        String GetView(String name, IModel model)
        {
            var viewName = "../{0}/{1}".F(model.Name, name);

            // 如果频道模版不存在，则采用模型模版
            return _cache.GetItem(viewName, k =>
            {
                // 模型目录的模版
                var view = k;
                var vpath = "Views/{0}/{1}.cshtml".F(model.Name, name);
                if (ViewExists(vpath)) return view;

                // 内容目录的模板
                view = "../{0}/{1}".F("Content", name);
                vpath = "Views/{0}/{1}.cshtml".F("Content", name);
                if (ViewExists(vpath)) return view;

                // 共享目录的模板
                view = "../{0}/{1}".F("Shared", name);
                vpath = "Views/{0}/{1}.cshtml".F("Shared", name);
                if (ViewExists(vpath)) return view;

                return null;
            });
        }

        public ActionResult Index()
        {
            var name = RouteData.Values["modelName"] + "";
            var model = Model.FindByName(name);

            // 选择模版
            var tmp = model.IndexTemplate;
            if (tmp.IsNullOrEmpty() || !ViewExists(tmp)) tmp = GetView("Index", model);

            return View(tmp, model);
        }

        #region 分类列表页
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

            ViewBag.Title = cat.Name;
            ViewBag.Category = cat;
            ViewBag.Pager = pager;
            ViewBag.Infos = list;

            return View(tmp, cat);
        }
        #endregion

        #region 信息详细页
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
            (inf as IEntity).SaveAsync(15);

            ViewBag.Title = inf.Title;
            ViewBag.Category = cat;

            return View(tmp, inf);
        }
        #endregion

        #region 搜索页
        public ActionResult Search(String key, Int32? pageIndex)
        {
            var name = RouteData.Values["modelName"] + "";
            var model = Model.FindByName(name);

            var code = RouteData.Values["categoryCode"] + "";
            var cat = Category.FindByCode(code);

            var pager = new Pager { PageIndex = pageIndex ?? 1, PageSize = PageSize };
            var list = Info.Search(model != null ? model.ID : 0, cat != null ? cat.ID : 0, key, pager);

            ViewBag.Title = "搜索[{0}]".F(key);

            return View(list);
        }
        #endregion
    }
}