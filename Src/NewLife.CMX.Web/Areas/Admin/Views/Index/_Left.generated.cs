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
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using NewLife;
    
    #line 2 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
    using NewLife.CMX;
    
    #line default
    #line hidden
    using NewLife.Cube;
    
    #line 1 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
    using NewLife.Model;
    
    #line default
    #line hidden
    using NewLife.Reflection;
    using NewLife.Web;
    using XCode;
    using XCode.Membership;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Index/_Left.cshtml")]
    public partial class _Areas_Admin_Views_Index__Left_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Admin_Views_Index__Left_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
  
    var user = ManageProvider.User;

    var fact = ObjectContainer.Current.Resolve<IMenuFactory>();

    var Menus = fact.Root.Childs;
    if (user != null && user.Role != null)
    {
        Menus = fact.GetMySubMenus(fact.Root.ID);
    }

    // 如果顶级只有一层，并且至少有三级目录，则提升一级
    if (Menus.Count == 1 && Menus[0].Childs.All(m => m.Childs.Count > 0)) { Menus = Menus[0].Childs; }

    String[] icos = new String[] { "fa-tachometer", "fa-desktop", "fa-list", "fa-pencil-square-o", "fa-list-alt", "fa-calendar", "fa-picture-o", "fa-tag", "fa-file-o" };
    Int32 _idx = 0;

            
            #line default
            #line hidden
WriteLiteral("\r\n<ul");

WriteLiteral(" class=\"nav nav-list\"");

WriteLiteral(">\r\n");

            
            #line 21 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
    
            
            #line default
            #line hidden
            
            #line 21 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
     foreach (IMenu menu in Menus.Where(m => m.Visible))
    {
        var childs = fact.GetMySubMenus(menu.ID).Where(m => m.Visible);
        if (_idx >= icos.Length) { _idx = 0; }

            
            #line default
            #line hidden
WriteLiteral("        <li ");

            
            #line 25 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
       Write(Html.Raw(menu == Menus[0] ? "class=\"active open\"" : ""));

            
            #line default
            #line hidden
WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"dropdown-toggle\"");

WriteLiteral(">\r\n                <i");

WriteAttribute("class", Tuple.Create(" class=\"", 988), Tuple.Create("\"", 1022)
, Tuple.Create(Tuple.Create("", 996), Tuple.Create("menu-icon", 996), true)
, Tuple.Create(Tuple.Create(" ", 1005), Tuple.Create("fa", 1006), true)
            
            #line 27 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
, Tuple.Create(Tuple.Create(" ", 1008), Tuple.Create<System.Object, System.Int32>(icos[_idx++]
            
            #line default
            #line hidden
, 1009), false)
);

WriteLiteral("></i>\r\n                <span");

WriteLiteral(" class=\"menu-text\"");

WriteLiteral(">");

            
            #line 28 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                                   Write(menu.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n\r\n                <b");

WriteLiteral(" class=\"arrow fa fa-angle-down\"");

WriteLiteral("></b>\r\n            </a>\r\n");

            
            #line 32 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
            
            
            #line default
            #line hidden
            
            #line 32 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
             if (childs.Any())
            {

            
            #line default
            #line hidden
WriteLiteral("                <b");

WriteLiteral(" class=\"arrow\"");

WriteLiteral("></b>\r\n");

            
            #line 35 "..\..\Areas\Admin\Views\Index\_Left.cshtml"


            
            #line default
            #line hidden
WriteLiteral("                <ul");

WriteLiteral(" class=\"submenu\"");

WriteLiteral(">\r\n");

            
            #line 37 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 37 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                     foreach (IMenu menu2 in childs)
                    {
                        
            
            #line default
            #line hidden
            
            #line 39 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                   Write(Html.Partial("_Left_Item", menu2));

            
            #line default
            #line hidden
            
            #line 39 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                                                          ;
                    }

            
            #line default
            #line hidden
WriteLiteral("                </ul>\r\n");

            
            #line 42 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </li>\r\n");

            
            #line 44 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

WriteLiteral("\r\n");

            
            #line 46 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
    
            
            #line default
            #line hidden
            
            #line 46 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
     foreach (var mod in ModelX.GetAll())
    {
        // 判断是否有权访问该频道
        var res = "CMS\\" + mod.Name;
        //if (!user.Has(res) && !user.Has(res + "Category")) { continue; }
        //if (!user.Has(res)) { continue; }
        if (_idx >= icos.Length) { _idx = 0; }

            
            #line default
            #line hidden
WriteLiteral("        <li>\r\n            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"dropdown-toggle\"");

WriteLiteral(">\r\n                <i");

WriteAttribute("class", Tuple.Create(" class=\"", 1903), Tuple.Create("\"", 1937)
, Tuple.Create(Tuple.Create("", 1911), Tuple.Create("menu-icon", 1911), true)
, Tuple.Create(Tuple.Create(" ", 1920), Tuple.Create("fa", 1921), true)
            
            #line 55 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
, Tuple.Create(Tuple.Create(" ", 1923), Tuple.Create<System.Object, System.Int32>(icos[_idx++]
            
            #line default
            #line hidden
, 1924), false)
);

WriteLiteral("></i>\r\n                <span");

WriteLiteral(" class=\"menu-text\"");

WriteLiteral(">");

            
            #line 56 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                                   Write(mod.ToString());

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n\r\n                <b");

WriteLiteral(" class=\"arrow fa fa-angle-down\"");

WriteLiteral("></b>\r\n            </a>\r\n\r\n            <b");

WriteLiteral(" class=\"arrow\"");

WriteLiteral("></b>\r\n\r\n            <ul");

WriteLiteral(" class=\"submenu\"");

WriteLiteral(">\r\n                <li>\r\n                    <a");

WriteAttribute("href", Tuple.Create(" href=\"", 2202), Tuple.Create("\"", 2224)
, Tuple.Create(Tuple.Create("", 2209), Tuple.Create<System.Object, System.Int32>(Href("~/CMS/")
, 2209), false)
            
            #line 65 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
, Tuple.Create(Tuple.Create("", 2215), Tuple.Create<System.Object, System.Int32>(mod.Name
            
            #line default
            #line hidden
, 2215), false)
);

WriteLiteral(" target=\"main\"");

WriteLiteral(">\r\n                        <i");

WriteLiteral(" class=\"menu-icon fa fa-caret-right\"");

WriteLiteral("></i>\r\n");

WriteLiteral("                        ");

            
            #line 67 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                   Write(mod);

            
            #line default
            #line hidden
WriteLiteral(" 管理\r\n                    </a>\r\n                </li>\r\n");

            
            #line 70 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                
            
            #line default
            #line hidden
            
            #line 70 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                 foreach (ICategory cat2 in mod.GetTopCategories())
                {
                    
            
            #line default
            #line hidden
            
            #line 72 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
               Write(Html.Partial("_Left_Category", cat2));

            
            #line default
            #line hidden
            
            #line 72 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
                                                         ;
                }

            
            #line default
            #line hidden
WriteLiteral("            </ul>\r\n        </li>\r\n");

            
            #line 76 "..\..\Areas\Admin\Views\Index\_Left.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</ul>\r\n<!-- /.nav-list -->\r\n");

        }
    }
}
#pragma warning restore 1591
