<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArticleForm.aspx.cs" Inherits="Template_Article_ArticleForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>东莞市月无声实业</title>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/style/css.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/style/base.css") %>" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrap">
            <div id="wrpper">
                <div class="header">
                    <Custom:HeadControl runat="server" ID="head" />
                </div>
                <div class="contentinfo">
                    <h2><%=ArticleContent.Title %></h2>
                    <div class="otherInfo">浏览：<%= ArticleContent.Article.Hits %>&nbsp;次&nbsp;添加时间：【<%= ArticleContent.CreateTime.ToShortDateString() %>】</div>
                    <div class="contentLine"></div>
                    <div id="article">
                        <div id="articleContent">
                            <%=ArticleContent.Content %>
                        </div>
                    </div>
                </div>
                <div class="foot1">
                    <Custom:FootControl runat="server" ID="foot" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
