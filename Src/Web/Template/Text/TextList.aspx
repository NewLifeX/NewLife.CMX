<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TextList.aspx.cs" Inherits="Template_Text_TextList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <asp:Repeater runat="server" ID="frmtext" EnableViewState="false">
                    <ItemTemplate>
                        <tr>
                            <td><a href="<%# ResolveUrl("~/Info/"+Suffix+"/"+Eval("ID")+".aspx")%>"><%# Eval("Title") %></a></td>
                            <td></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
