<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeadControl.ascx.cs" Inherits="Control_HeadControl" %>
<div class="mod">
    <div class="link">
        <a href="#" target="_blank"></a>
        <a href="#" target="_blank"></a>
        <a href="#" target="_blank"></a>
        <a href="#" target="_blank"></a>
    </div>
    <div class="search">
        <input name="" type="text" />
        <button name="搜索" class="botton" style="width: 54px;">搜索</button>
    </div>
</div>
<div class="nav">
    <ul>
        <li><a href="<%= ResolveUrl("~/index.aspx") %>" id="nav1" title="首页" class="navtitle">首页</a></li>
        <li><a href="<%= ResolveUrl("~/List/YWS/14.aspx") %>" id="nav2" title="关于月无声" class="navtitle"></a></li>
        <li><a href="<%= ResolveUrl("~/List/YWS/14.aspx") %>" id="nav3" title="产品与服务" class="navtitle">产品与服务</a></li>
        <li><a href="<%= ResolveUrl("~/List/YWS/14.aspx") %>" id="nav4" title="企业文化" class="navtitle">企业文化</a></li>
        <li><a href="<%= ResolveUrl("~/List/YWS/5.aspx") %>" id="nav5" title="人才招聘" class="navtitle">人才招聘</a></li>
        <li><a href="<%= ResolveUrl("~/List/YWS/4.aspx") %>" id="nav6" title="新闻中心" class="navtitle">新闻中心</a></li>
        <li><a href="#" id="nav7" target="_blank" title="技术论坛" class="navtitle">技术论坛</a></li>
        <li><a href="#" id="nav8" target="_blank" title="月无声商城" class="navtitle">月无声商城</a></li>
    </ul>
</div>
