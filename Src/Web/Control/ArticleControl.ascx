<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleControl.ascx.cs" Inherits="Control_ArticleControl" %>
<ul>
    <asp:Repeater ID="Content" runat="server">
        <ItemTemplate>
            <li><a target="_blank" href="<%# "Info/"+ChannelSuffix+"/"+Eval("ID")+".aspx" %>"><%# Eval("Title") %></a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
