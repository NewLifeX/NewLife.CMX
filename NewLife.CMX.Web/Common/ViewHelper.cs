using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NewLife.CMX.Web
{
    /// <summary>视图助手</summary>
    public static class ViewHelper
    {
        public static String GenerateUrl(this UrlHelper url, String routeName)
        {
            return UrlHelper.GenerateUrl(routeName, null, null, null, url.RouteCollection, url.RequestContext, false);
        }

        /// <summary>获取IInfo的Url</summary>
        /// <param name="page"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public static String GetUrl(this WebViewPage page, IInfo inf)
        {
            //var cat = inf.Category;
            //if (cat == null) return null;

            var values = page.Url.RequestContext.RouteData.Values;
            //values["modelName"] = cat.Model.Name;
            //values["categoryCode"] = cat.Code;
            values["id"] = inf.ID;
            values["infoCode"] = inf.Code;

            if (inf.Code.IsNullOrEmpty())
                return page.Url.GenerateUrl("CMX_Info");
            else
                return page.Url.GenerateUrl("CMX_Info2");
        }

        /// <summary>获取到分类页面的链接</summary>
        /// <param name="page"></param>
        /// <param name="cat"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static String GetCategoryUrl(this WebViewPage page, ICategory cat, Int32 pageIndex = 1)
        {
            if (cat == null || cat.Model == null) return null;

            var values = page.Url.RequestContext.RouteData.Values;
            //values["modelName"] = cat.Model.Name;
            values["categoryCode"] = cat.Code;
            values["categoryid"] = cat.ID;
            values["pageIndex"] = pageIndex;

            var url = page.Url;
            if (cat.Code.IsNullOrEmpty())
            {
                if (pageIndex <= 1)
                    return page.Url.GenerateUrl("CMX_Category");
                else
                    return page.Url.GenerateUrl("CMX_Category_Page");
            }
            else
            {
                if (pageIndex <= 1)
                    return page.Url.GenerateUrl("CMX_Category2");
                else
                    return page.Url.GenerateUrl("CMX_Category_Page2");
            }
        }

        /// <summary>获取分类的Url</summary>
        /// <param name="page"></param>
        /// <param name="categoryName"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static String GetCategoryUrl(this WebViewPage page, String categoryName, Int32 pageIndex = 1)
        {
            var cat = Category.FindByName(categoryName);
            if (cat == null) return null;

            return page.GetCategoryUrl(cat, pageIndex);
        }

        public static void PushTitle(this WebViewPage page, String title)
        {
            var list = page.ViewData["PageTitle"] as List<String> ?? new List<String>();

            list.Add(title);
            page.ViewData["PageTitle"] = list;
        }

        public static String GetPageTitle(this WebViewPage page)
        {
            var list = page.ViewData["PageTitle"] as List<String> ?? new List<String> { };

            list.Reverse();
            list.Add(SiteConfig.Current.Title);
            return String.Join(" - ", list);
        }
    }
}