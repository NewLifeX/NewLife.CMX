<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>东莞市月无声实业</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/style/css.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/style/base.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/style/bootstrap.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/style/bootstrap-theme.css") %>" />
    <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/jquery/jquery-1.9.1.min.js") %>" />
    <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/bootstrap/bootstrap.js") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrap">
            <div id="wrpper">
                <div class="header">
                    <Custom:HeadControl runat="server" ID="head" />
                </div>
                <div class="w1134_1">
                    <div class="mod1">
                        <div class="box">
                            <a href="#" target="_blank">
                                <img src="images/1.jpg" width="223" height="123" /></a>
                        </div>
                        <div class="int">简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介</div>
                    </div>
                    <div class="flash"></div>
                </div>
                <div class="w1134_2">
                    <div class="mod2">
                        <div class="box">
                            <a href="#" target="_blank">
                                <img src="images/1.jpg" width="223" height="123" /></a>
                        </div>
                        <div class="int">简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介</div>
                    </div>
                    <div class="mod3">
                        <div class="box">
                            <a href="#" target="_blank">
                                <img src="images/1.jpg" width="223" height="123" /></a>
                        </div>
                        <div class="int">简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介</div>
                    </div>
                    <div class="mod4">
                        <div class="box">
                            <a href="#" target="_blank">
                                <img src="images/1.jpg" width="223" height="123" /></a>
                        </div>
                        <div class="int">简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介简介</div>
                    </div>
                </div>
                <div class="ArticleContentList">
                    <div class="text">
                        <div class="more"><a target="_blank" href="<%= ResolveUrl("~/List/SX/2/ArticleModelList.aspx") %>" title="更多新闻"></a></div>
                        <Custom:ListControl runat="server" ID="NewCenter" ChannelSuffix="SX" Count="6" CategoryName="迎泽区" />
                    </div>
                    <div class="text2">
                        <Custom:ListControl runat="server" ID="ArticleControl1" ChannelSuffix="SX" Count="6" CategoryName="迎泽区" />
                    </div>
                </div>
                <div class="foot">
                    <Custom:FootControl runat="server" ID="foot" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
