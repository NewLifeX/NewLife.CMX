<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="Template_Article_ArticleList" %>

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
                <div id="articlelist">
                    <div class="articlekind">
                        <h1>分类名称</h1>
                        <div id="kindlist">
                            <Custom:KindList runat="server" ID="kind" />
                        </div>
                    </div>
                    <div class="articletitle">
                        <div class="title">
                            <Custom:ListControl runat="server" ID="NewCenter" />
                        </div>
                    </div>
                    <div class="Paging">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" PagingButtonSpacing="8px"
                            OnPageChanged="AspNetPager1_PageChanged" UrlPaging="True" Width="100%" ShowNavigationToolTip="true"
                            UrlPageIndexName="pageindex" EnableUrlRewriting="true" UrlRewritePattern="">
                        </webdiyer:AspNetPager>
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
