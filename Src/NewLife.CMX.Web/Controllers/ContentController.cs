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
        private Channel _channel;
        public Channel Channel { get { return _channel; } }

        protected virtual Int32 PageSize { get { return 10; } }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //拦截频道名称
            if (filterContext.RouteData.Values.ContainsKey("channelName"))
            {
                var name = filterContext.RouteData.Values["channelName"] + "";
                SetChannel(name);
            }
            if (_channel == null && filterContext.RouteData.Values.ContainsKey("categoryCode"))
            {
                var code = filterContext.RouteData.Values["categoryCode"] + "";
                var cat = Channel.FindCategoryByCode(code);
                if (cat != null) _channel = cat.Channel;
            }
            if (_channel == null)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetChannel(String name)
        {
            _channel = Channel.FindByName(name);
        }

        static DictionaryCache<String, String> _cache = new DictionaryCache<String, String>(StringComparer.OrdinalIgnoreCase);
        String GetView(String name)
        {
            var viewName = "../{0}/{1}".F(Channel.Name, name);

            // 如果频道模版不存在，则采用模型模版
            return _cache.GetItem(viewName, name, (k, kn) =>
            {
                var v = k;
                var vp = "Views/{0}/{1}.cshtml".F(Channel.Name, kn);
                if (System.IO.File.Exists(vp.GetFullPath())) return v;

                v = "../{0}/{1}".F(Channel.Model.Name, kn);
                vp = "Views/{0}/{1}.cshtml".F(Channel.Model.Name, kn);
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
            var tmp = Channel.IndexTemplate;
            if (tmp.IsNullOrEmpty()) tmp = "Channel";
            var viewName = GetView(tmp);

            ViewBag.Channel = Channel;

            return View(viewName, Channel);
        }

        public ActionResult List(Int32 categoryid, Int32? pageindex)
        {
            var cat = Channel.FindCategory(categoryid);
            if (cat == null) return HttpNotFound();

            // 选择模版
            var tmp = cat.GetCategoryTemplate();
            if (tmp.IsNullOrEmpty()) tmp = "Category";
            var viewName = GetView(tmp);

            ViewBag.Channel = Channel;
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
            var title = Channel.FindTitle(id);
            if (title == null) return HttpNotFound();
            var cat = title.Category;

            // 选择模版
            var tmp = cat.GetTitleTemplate();
            if (tmp.IsNullOrEmpty()) tmp = "Title";
            var viewName = GetView(tmp);

            // 增加浏览数
            title.Views++;
            title.Statistics.Increment(null);

            ViewBag.Channel = Channel;
            ViewBag.Category = cat;

            return View(viewName, title);
        }

        public ActionResult List2(String categoryCode, Int32? pageindex)
        {
            var cat = Channel.FindCategoryByCode(categoryCode);
            if (cat == null) return HttpNotFound();

            // 选择模版
            var tmp = cat.GetCategoryTemplate();
            if (tmp.IsNullOrEmpty()) tmp = "Category";
            var viewName = GetView(tmp);

            ViewBag.Channel = Channel;
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