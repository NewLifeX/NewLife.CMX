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
                <div>
                    <table>
                        <asp:Repeater runat="server" ID="frmarticle" EnableViewState="false">
                            <ItemTemplate>
                                <tr>
                                    <td><a href="<%# ResolveUrl("~/Info/"+Suffix+"/"+Eval("ID")+".aspx")%>"><%# Eval("Title") %></a></td>
                                    <td></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="foot">
                    <Custom:FootControl runat="server" ID="foot" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
