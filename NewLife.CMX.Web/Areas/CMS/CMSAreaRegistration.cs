using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NewLife.Cube;

namespace NewLife.CMX.Web
{
    [DisplayName("内容管理")]
    public class CMSArea : AreaBase
    {
        /// <summary>区域名</summary>
        public static String AreaName => nameof(CMSArea).TrimEnd("Area");

        /// <inheritdoc />
        public CMSArea() : base(AreaName) { }

        public static void RegisterArea(IEndpointRouteBuilder endpoints)
        {
            // 用于左边菜单的两条路由
            endpoints.MapControllerRoute(
               "CMS_Manage",
               "CMS/{controller=Index}/{action=Index}/{id?}"
               );

            #region 类别
            endpoints.MapControllerRoute(
                name: "CMX_Category",
                pattern: "cat-{categoryid}",
                defaults: new { controller = "Content", action = "List" },
                constraints: new { categoryid = "[\\d]+" }
            );

            endpoints.MapControllerRoute(
                name: "CMX_Category_Page",
                pattern: "cat-{categoryid}-{pageIndex}",
                defaults: new { controller = "Content", action = "List" },
                constraints: new { categoryid = "[\\d]+", pageIndex = "[\\d]+" }
            );

            endpoints.MapControllerRoute(
                name: "CMX_Category2",
                pattern: "{categoryCode}",
                defaults: new { controller = "Content", action = "List2" },
                constraints: new { categoryCode = new CategoryUrlConstraint() }
            );

            endpoints.MapControllerRoute(
                name: "CMX_Category_Page2",
                pattern: "{categoryCode}-{pageIndex}",
                defaults: new { controller = "Content", action = "List2" },
                constraints: new { categoryCode = new CategoryUrlConstraint(), pageIndex = "[\\d]+" }
            );
            #endregion

            #region 信息
            endpoints.MapControllerRoute(
                name: "CMX_Info",
                pattern: "info-{id}",
                defaults: new { controller = "Content", action = "Detail" },
                constraints: new { id = "[\\d]+" }
            );

            endpoints.MapControllerRoute(
                name: "CMX_Info2",
                pattern: "{categoryCode}-{infoCode}",
                defaults: new { controller = "Content", action = "Detail2" },
                constraints: new { categoryCode = new CategoryUrlConstraint(), infoCode = new InfoUrlConstraint() }
            );
            #endregion

            #region  搜索
            endpoints.MapControllerRoute(
                name: "CMX_Search",
                pattern: "search-{key}-{pageIndex}",
                defaults: new { controller = "Content", action = "Search" },
                constraints: null
            );

            //routes.MapControllerRoute(
            //    name: "CMX_Search2",
            //    url: "{modelName}/Search/{key}/{pageIndex}",
            //    defaults: new { controller = "Content", action = "Search" },
            //    constraints: new { modelName = new ModelUrlConstraint() }
            //);

            endpoints.MapControllerRoute(
                name: "CMX_Search3",
                pattern: "{categoryCode}-{key}-{pageIndex}",
                defaults: new { controller = "Content", action = "Search" },
                constraints: new { categoryCode = new CategoryUrlConstraint() }
            );
            #endregion
        }
    }

    class ModelUrlConstraint : IRouteConstraint
    {
        public Boolean Match(HttpContext httpContext, IRouter route, String parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var name = values[parameterName] + "";
            if (name.IsNullOrEmpty()) return false;

            if (Model.FindByName(name) != null) return true;

            return false;
        }
    }

    class CategoryUrlConstraint : IRouteConstraint
    {
        public Boolean Match(HttpContext httpContext, IRouter route, String parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var name = values[parameterName] + "";
            if (name.IsNullOrEmpty()) return false;

            if (Category.FindByCode(name) != null) return true;

            return false;
        }
    }

    /// <summary>信息路径适配</summary>
    class InfoUrlConstraint : IRouteConstraint
    {
        public Boolean Match(HttpContext httpContext, IRouter route, String parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var cat = Category.FindByCode(values["categoryCode"] + "");
            if (cat == null) return false;

            var infoCode = values[parameterName] + "";
            if (infoCode.IsNullOrEmpty() || infoCode.ToInt() > 0) return false;

            if (Info.FindByCategoryAndCode(cat.ID, infoCode) != null) return true;

            return false;
        }
    }
}