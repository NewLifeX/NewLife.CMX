using System;
using NewLife.Reflection;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using NewLife.CMX;
using NewLife.Log;

namespace NewLife.CMX.Web
{
    /// <summary>视图助手</summary>
    public static class ViewHelper
    {
        public static string GenerateUrl(this UrlHelper url, string routeName)
        {
            //var values = url.RequestContext.RouteData.Values;
            ////values["controller"] = controllerName;
            ////values["action"] = actionName;
            //var virtualPathForArea = url.RouteCollection.GetVirtualPathForArea(url.RequestContext, null, values);

            //var routeCollection = new RouteCollection();
            //foreach (Route item in url.RouteCollection)
            //{
            //    XTrace.WriteLine("{0} {1}", item.GetType().Name, item.Url);
            //    //if (item.DataTokens != null) XTrace.WriteLine(item.DataTokens.Join(",", e => e.Key + "=" + e.Value));
            //    if (item.DataTokens != null)
            //    {
            //        foreach (var dt in item.DataTokens)
            //        {
            //            XTrace.WriteLine("{0}={1}", dt.Key, dt.Value);
            //        }
            //    }
            //    //string text = (item as Route).DataTokens["area"] as String;
            //    ////usingAreas |= (text.Length > 0);
            //    //if (string.Equals(text, null, StringComparison.OrdinalIgnoreCase))
            //    //{
            //    //    routeCollection.Add(item);
            //    //}
            //}
            //var vp = url.RouteCollection.GetVirtualPath(url.RequestContext, values);

            //return url.Action("show-" + entity.ID + ".html", cat.Channel.Name);

            return UrlHelper.GenerateUrl(routeName, null, null, null, url.RouteCollection, url.RequestContext, false);
        }

        /// <summary>获取IEntityTitle的Url</summary>
        /// <param name="page"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static String GetUrl(this WebViewPage page, IEntityTitle entity)
        {
            var cat = entity.Category;
            if (cat == null) return null;

            var values = page.Url.RequestContext.RouteData.Values;
            values["channelName"] = cat.Channel.Name;
            values["id"] = entity.ID;

            return page.Url.GenerateUrl("CMX_Title");
            //var url = page.Url;
            //return UrlHelper.GenerateUrl("CMX_Title", null, null, null, url.RouteCollection, url.RequestContext, true);
        }

        /// <summary>获取到分类页面的链接</summary>
        /// <param name="page"></param>
        /// <param name="cat"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static String GetCategoryUrl(this WebViewPage page, IEntityCategory cat, Int32 pageIndex = 1)
        {
            if (cat == null || cat.Channel == null) return null;

            //if (pageIndex <= 1)
            //    return page.Url.Action(cat.ID + ".html", cat.Channel.Name);
            //else
            //    return page.Url.Action(cat.ID + "-" + pageIndex + ".html", cat.Channel.Name);

            var values = page.Url.RequestContext.RouteData.Values;
            values["channelName"] = cat.Channel.Name;
            values["categoryid"] = cat.ID;
            values["pageIndex"] = pageIndex;

            var url = page.Url;
            if (pageIndex <= 1)
                return page.Url.GenerateUrl("CMX_Category");
            else
                return page.Url.GenerateUrl("CMX_Category_Page");
        }

        /// <summary>获取分类的Url</summary>
        /// <param name="page"></param>
        /// <param name="channelName"></param>
        /// <param name="categoryName"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static String GetCategoryUrl(this WebViewPage page, String channelName, String categoryName, Int32 pageIndex = 1)
        {
            if (channelName.IsNullOrEmpty()) channelName = "Article";

            var chn = Channel.FindByName(channelName);
            if (chn == null) return null;
            var cat = chn.FindCategory(categoryName);
            if (cat == null) return null;

            return page.GetCategoryUrl(cat, pageIndex);
        }

        public static void PushTitle(this WebViewPage page, String title)
        {
            var list = page.ViewData["PageTitle"] as List<String> ?? new List<string>();

            list.Add(title);
            page.ViewData["PageTitle"] = list;
        }

        public static String GetPageTitle(this WebViewPage page)
        {
            var list = page.ViewData["PageTitle"] as List<String> ?? new List<string> { };

            list.Reverse();
            list.Add(SiteConfig.Current.Title);
            return String.Join(" - ", list);
        }
    }
}