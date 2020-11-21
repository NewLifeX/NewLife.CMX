using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace NewLife.CMX.Web
{
    /// <summary>视图助手</summary>
    public static class ViewHelper
    {
        /// <summary>获取IInfo的Url</summary>
        /// <param name="url"></param>
        /// <param name="inf"></param>
        /// <returns></returns>
        public static String GetUrl(this IUrlHelper url, Info inf)
        {
            if (inf.Code.IsNullOrEmpty())
                return url.Link("CMX_Info", new { id = inf.ID });
            else
                return url.Link("CMX_Info2", new { categoryCode = inf.Category?.Code, infoCode = inf.Code });
        }

        /// <summary>获取到分类页面的链接</summary>
        /// <param name="url"></param>
        /// <param name="cat"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static String GetCategoryUrl(this IUrlHelper url, Category cat, Int32 pageIndex = 1)
        {
            if (cat == null || cat.Model == null) return null;

            if (cat.Code.IsNullOrEmpty())
            {
                if (pageIndex <= 1)
                    return url.Link("CMX_Category", new { categoryid = cat.ID });
                else
                    return url.Link("CMX_Category_Page", new { categoryid = cat.ID, pageIndex });
            }
            else
            {
                if (pageIndex <= 1)
                    return url.Link("CMX_Category2", new { categoryCode = cat.Code });
                else
                    return url.Link("CMX_Category_Page2", new { categoryCode = cat.Code, pageIndex });
            }
        }

        /// <summary>获取分类的Url</summary>
        /// <param name="page"></param>
        /// <param name="categoryName"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static String GetCategoryUrl(this IUrlHelper page, String categoryName, Int32 pageIndex = 1)
        {
            var cat = Category.FindByName(categoryName);
            if (cat == null) return null;

            return page.GetCategoryUrl(cat, pageIndex);
        }

        public static void PushTitle(this RazorPage page, String title)
        {
            var list = page.TempData["PageTitle"] as List<String> ?? new List<String>();

            list.Add(title);
            page.TempData["PageTitle"] = list;
        }

        public static String GetPageTitle(this RazorPage page)
        {
            var list = page.TempData["PageTitle"] as List<String> ?? new List<String> { };

            list.Reverse();
            list.Add(SiteConfig.Current.Title);
            return String.Join(" - ", list);
        }
    }
}