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
    
    #line 8 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    
    #line 9 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using System.Web.Mvc.Ajax;
    
    #line default
    #line hidden
    
    #line 10 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using System.Web.Mvc.Html;
    
    #line default
    #line hidden
    
    #line 11 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using System.Web.Routing;
    
    #line default
    #line hidden
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 2 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using NewLife;
    
    #line default
    #line hidden
    
    #line 12 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using NewLife.CMX.Web;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using NewLife.Cube;
    
    #line default
    #line hidden
    using NewLife.Reflection;
    
    #line 3 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using NewLife.Web;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using XCode;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using XCode.Configuration;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
    using XCode.Membership;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/CMS/Views/Info/_List_Data.cshtml")]
    public partial class _Areas_CMS_Views_Info__List_Data_cshtml : System.Web.Mvc.WebViewPage<IList<NewLife.CMX.Info>>
    {
        public _Areas_CMS_Views_Info__List_Data_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 13 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
  
    var fact = ViewBag.Factory as IEntityFactory;
    var page = ViewBag.Page as Pager;
    var fields = ViewBag.Fields as IList<FieldItem>;
    var set = ViewBag.PageSetting as PageSetting;
    //var provider = ManageProvider.Provider;

            
            #line default
            #line hidden
WriteLiteral("\r\n<table");

WriteLiteral(" class=\"table table-bordered table-hover table-striped table-condensed\"");

WriteLiteral(">\r\n    <thead>\r\n        <tr>\r\n");

            
            #line 23 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
            
            
            #line default
            #line hidden
            
            #line 23 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
             if (set.EnableSelect)
            {

            
            #line default
            #line hidden
WriteLiteral("                <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" style=\"width:10px;\"");

WriteLiteral("><input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"chkAll\"");

WriteLiteral(" title=\"全选\"");

WriteLiteral(" /></th>\r\n");

            
            #line 26 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            <th");

WriteLiteral(" class=\"text-center hidden-md hidden-sm hidden-xs\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 902), Tuple.Create("\"", 941)
            
            #line 27 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 909), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("ID"))
            
            #line default
            #line hidden
, 909), false)
);

WriteLiteral(">编号</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 994), Tuple.Create("\"", 1041)
            
            #line 28 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1001), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("CategoryID"))
            
            #line default
            #line hidden
, 1001), false)
);

WriteLiteral(">分类</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1094), Tuple.Create("\"", 1136)
            
            #line 29 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1101), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("Title"))
            
            #line default
            #line hidden
, 1101), false)
);

WriteLiteral(">标题</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" title=\"访问量。由统计表同步过来\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1210), Tuple.Create("\"", 1252)
            
            #line 30 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1217), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("Views"))
            
            #line default
            #line hidden
, 1217), false)
);

WriteLiteral(">访问量</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" title=\"排序。较大值在前\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1323), Tuple.Create("\"", 1364)
            
            #line 31 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1330), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("Sort"))
            
            #line default
            #line hidden
, 1330), false)
);

WriteLiteral(">排序</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1417), Tuple.Create("\"", 1463)
            
            #line 32 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1424), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("Publisher"))
            
            #line default
            #line hidden
, 1424), false)
);

WriteLiteral(">发布人</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" style=\"min-width:134px;\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1542), Tuple.Create("\"", 1590)
            
            #line 33 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1549), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("PublishTime"))
            
            #line default
            #line hidden
, 1549), false)
);

WriteLiteral(">发布时间</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1645), Tuple.Create("\"", 1692)
            
            #line 34 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1652), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("CreateUser"))
            
            #line default
            #line hidden
, 1652), false)
);

WriteLiteral(">创建人</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" style=\"min-width:134px;\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1771), Tuple.Create("\"", 1818)
            
            #line 35 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1778), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("CreateTime"))
            
            #line default
            #line hidden
, 1778), false)
);

WriteLiteral(">创建时间</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1873), Tuple.Create("\"", 1918)
            
            #line 36 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1880), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("CreateIP"))
            
            #line default
            #line hidden
, 1880), false)
);

WriteLiteral(">创建地址</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1973), Tuple.Create("\"", 2020)
            
            #line 37 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 1980), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("UpdateUser"))
            
            #line default
            #line hidden
, 1980), false)
);

WriteLiteral(">更新人</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" style=\"min-width:134px;\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 2099), Tuple.Create("\"", 2146)
            
            #line 38 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 2106), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("UpdateTime"))
            
            #line default
            #line hidden
, 2106), false)
);

WriteLiteral(">更新时间</a></th>\r\n            <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral("><a");

WriteAttribute("href", Tuple.Create(" href=\"", 2201), Tuple.Create("\"", 2246)
            
            #line 39 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 2208), Tuple.Create<System.Object, System.Int32>(Html.Raw(page.GetSortUrl("UpdateIP"))
            
            #line default
            #line hidden
, 2208), false)
);

WriteLiteral(">更新地址</a></th>\r\n");

            
            #line 40 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
            
            
            #line default
            #line hidden
            
            #line 40 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
             if (this.Has(PermissionFlags.Detail, PermissionFlags.Update, PermissionFlags.Delete))
            {

            
            #line default
            #line hidden
WriteLiteral("                <th");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(" style=\"min-width:100px;\"");

WriteLiteral(">操作</th>\r\n");

            
            #line 43 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </tr>\r\n    </thead>\r\n    <tbody>\r\n");

            
            #line 47 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
        
            
            #line default
            #line hidden
            
            #line 47 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
         foreach (var entity in Model)
        {

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n");

            
            #line 50 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                
            
            #line default
            #line hidden
            
            #line 50 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                 if (set.EnableSelect)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral("><input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"keys\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2715), Tuple.Create("\"", 2733)
            
            #line 52 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
      , Tuple.Create(Tuple.Create("", 2723), Tuple.Create<System.Object, System.Int32>(entity.ID
            
            #line default
            #line hidden
, 2723), false)
);

WriteLiteral(" /></td>\r\n");

            
            #line 53 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral(" class=\"text-center hidden-md hidden-sm hidden-xs\"");

WriteLiteral(">");

            
            #line 54 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                                                                 Write(entity.ID);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");

            
            #line 55 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
               Write(entity.CategoryName);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td><a");

WriteAttribute("href", Tuple.Create(" href=\"", 2919), Tuple.Create("\"", 2946)
            
            #line 56 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 2926), Tuple.Create<System.Object, System.Int32>(this.GetUrl(entity)
            
            #line default
            #line hidden
, 2926), false)
);

WriteLiteral(" target=\"_blank\"");

WriteLiteral(">");

            
            #line 56 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                                                              Write(entity.Title);

            
            #line default
            #line hidden
WriteLiteral("</a></td>\r\n                <td");

WriteLiteral(" class=\"text-right\"");

WriteLiteral(">");

            
            #line 57 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                                  Write(entity.Views.ToString("n0"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td");

WriteLiteral(" class=\"text-right\"");

WriteLiteral(">");

            
            #line 58 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                                  Write(entity.Sort.ToString("n0"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");

            
            #line 59 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
               Write(entity.Publisher);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");

            
            #line 60 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
               Write(entity.PublishTime.ToFullString(""));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");

            
            #line 61 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
               Write(entity.CreateUser);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");

            
            #line 62 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
               Write(entity.CreateTime.ToFullString(""));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td");

WriteAttribute("title", Tuple.Create(" title=\"", 3368), Tuple.Create("\"", 3406)
            
            #line 63 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 3376), Tuple.Create<System.Object, System.Int32>(entity.CreateIP.IPToAddress()
            
            #line default
            #line hidden
, 3376), false)
);

WriteLiteral(">");

            
            #line 63 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                                                      Write(entity.CreateIP);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");

            
            #line 64 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
               Write(entity.UpdateUser);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");

            
            #line 65 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
               Write(entity.UpdateTime.ToFullString(""));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td");

WriteAttribute("title", Tuple.Create(" title=\"", 3557), Tuple.Create("\"", 3595)
            
            #line 66 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
, Tuple.Create(Tuple.Create("", 3565), Tuple.Create<System.Object, System.Int32>(entity.UpdateIP.IPToAddress()
            
            #line default
            #line hidden
, 3565), false)
);

WriteLiteral(">");

            
            #line 66 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                                                      Write(entity.UpdateIP);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 67 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                
            
            #line default
            #line hidden
            
            #line 67 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                 if (this.Has(PermissionFlags.Detail, PermissionFlags.Update, PermissionFlags.Delete))
                {

            
            #line default
            #line hidden
WriteLiteral("                    <td");

WriteLiteral(" class=\"text-center\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 70 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                   Write(Html.Partial("_List_Data_Action", (Object)entity));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </td>\r\n");

            
            #line 72 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </tr>\r\n");

            
            #line 74 "..\..\Areas\CMS\Views\Info\_List_Data.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </tbody>\r\n</table>");

        }
    }
}
#pragma warning restore 1591
