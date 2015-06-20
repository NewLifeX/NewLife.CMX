using System;
using System.Web.Mvc;
using NewLife.CMX.Web.Models.Content;

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

        public ActionResult List(Int32 categoryid, Int32? pageindex)
        {
            var cat = Channel.FindCategory(categoryid);
            if (cat == null) return HttpNotFound();

            // 选择模版
            var viewName = cat.GetCategoryTemplate();
            if (viewName.IsNullOrEmpty()) viewName = "Category." + Channel.Name;

            //ViewBag.Category = cat;
            //ViewBag.Channel = Channel;
            //ViewBag.PageIndex = pageindex ?? 1;
            //ViewBag.PageSize = PageSize;

            //return View(viewName, cat);
            var vm = new CategoryModel
            {
                Category = cat,
                Channel = Channel,
                PageIndex = pageindex ?? 1,
                PageSize = PageSize
            };
            return View(viewName, vm);
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
            var viewName = cat.GetCategoryTemplate();
            if (viewName.IsNullOrEmpty()) viewName = "Detail." + Channel.Name;

            // 增加浏览数
            title.Views++;
            title.Statistics.Increment(null);

            //ViewBag.Channel = Channel;
            //ViewBag.Category = cat;

            //return View(viewName, title);
            var vm = new DetailModel
            {
                Channel = Channel,
                Category = cat,
                Detail = title
            };

            return View(viewName, vm);
        }
    }
}