﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using NewLife;
    using NewLife.CMX;
    using NewLife.CMX.Web;
    using NewLife.Cube;
    using NewLife.Reflection;
    using NewLife.Web;
    using XCode;
    using XCode.Membership;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_Category_Pager.cshtml")]
    public partial class _Views_Shared__Category_Pager_cshtml : System.Web.Mvc.WebViewPage<NewLife.CMX.ICategory>
    {
        public _Views_Shared__Category_Pager_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Shared\_Category_Pager.cshtml"
  
    var Pager = ViewBag.Pager as Pager;

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 5 "..\..\Views\Shared\_Category_Pager.cshtml"
 if (Pager.PageCount > 1)
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"page\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" class=\"cor01\"");

WriteLiteral(">页数：");

            
            #line 8 "..\..\Views\Shared\_Category_Pager.cshtml"
                          Write(Pager.PageIndex);

            
            #line default
            #line hidden
WriteLiteral("/");

            
            #line 8 "..\..\Views\Shared\_Category_Pager.cshtml"
                                           Write(Pager.PageCount);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n        <a");

WriteLiteral(" class=\"first\"");

WriteAttribute("href", Tuple.Create(" href=\"", 229), Tuple.Create("\"", 265)
            
            #line 9 "..\..\Views\Shared\_Category_Pager.cshtml"
, Tuple.Create(Tuple.Create("", 236), Tuple.Create<System.Object, System.Int32>(this.GetCategoryUrl(Model)
            
            #line default
            #line hidden
, 236), false)
);

WriteLiteral(" title=\"首页\"");

WriteLiteral(">首页</a>\r\n");

            
            #line 10 "..\..\Views\Shared\_Category_Pager.cshtml"
        
            
            #line default
            #line hidden
            
            #line 10 "..\..\Views\Shared\_Category_Pager.cshtml"
         if (Pager.PageIndex > 1)
        {

            
            #line default
            #line hidden
WriteLiteral("            <a");

WriteLiteral(" class=\"prev\"");

WriteLiteral(" title=\"上一页\"");

WriteAttribute("href", Tuple.Create(" href=\"", 371), Tuple.Create("\"", 428)
            
            #line 12 "..\..\Views\Shared\_Category_Pager.cshtml"
, Tuple.Create(Tuple.Create("", 378), Tuple.Create<System.Object, System.Int32>(this.GetCategoryUrl(Model, Pager.PageIndex - 1)
            
            #line default
            #line hidden
, 378), false)
);

WriteLiteral(">&lt;</a>\r\n");

            
            #line 13 "..\..\Views\Shared\_Category_Pager.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 14 "..\..\Views\Shared\_Category_Pager.cshtml"
         for (int i = 1; i <= Pager.PageCount; i++)
        {
            if (i == Pager.PageIndex)
            {

            
            #line default
            #line hidden
WriteLiteral("                <a");

WriteLiteral(" class=\"on\"");

WriteLiteral(">");

            
            #line 18 "..\..\Views\Shared\_Category_Pager.cshtml"
                         Write(i);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 19 "..\..\Views\Shared\_Category_Pager.cshtml"
            }
            else
            {

            
            #line default
            #line hidden
WriteLiteral("                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 673), Tuple.Create("\"", 712)
            
            #line 22 "..\..\Views\Shared\_Category_Pager.cshtml"
, Tuple.Create(Tuple.Create("", 680), Tuple.Create<System.Object, System.Int32>(this.GetCategoryUrl(Model, i)
            
            #line default
            #line hidden
, 680), false)
);

WriteLiteral(">");

            
            #line 22 "..\..\Views\Shared\_Category_Pager.cshtml"
                                                      Write(i);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 23 "..\..\Views\Shared\_Category_Pager.cshtml"
            }
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 25 "..\..\Views\Shared\_Category_Pager.cshtml"
         if (Pager.PageIndex < Pager.PageCount)
        {

            
            #line default
            #line hidden
WriteLiteral("            <a");

WriteLiteral(" class=\"next\"");

WriteLiteral(" title=\"下一页\"");

WriteAttribute("href", Tuple.Create(" href=\"", 847), Tuple.Create("\"", 904)
            
            #line 27 "..\..\Views\Shared\_Category_Pager.cshtml"
, Tuple.Create(Tuple.Create("", 854), Tuple.Create<System.Object, System.Int32>(this.GetCategoryUrl(Model, Pager.PageIndex + 1)
            
            #line default
            #line hidden
, 854), false)
);

WriteLiteral(">&gt;</a>\r\n");

            
            #line 28 "..\..\Views\Shared\_Category_Pager.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        <a");

WriteLiteral(" class=\"first\"");

WriteLiteral(" title=\"末页\"");

WriteAttribute("href", Tuple.Create(" href=\"", 962), Tuple.Create("\"", 1022)
            
            #line 29 "..\..\Views\Shared\_Category_Pager.cshtml"
, Tuple.Create(Tuple.Create("", 969), Tuple.Create<System.Object, System.Int32>(this.GetCategoryUrl(Model, (Int32)Pager.PageCount)
            
            #line default
            #line hidden
, 969), false)
);

WriteLiteral(">末页</a>\r\n    </div>\r\n");

            
            #line 31 "..\..\Views\Shared\_Category_Pager.cshtml"
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
