<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleControl.ascx.cs" Inherits="Control_ArticleControl" %>
<ul>
    <asp:Repeater ID="Content" runat="server">
        <ItemTemplate>
            <li><a target="_blank" href="<%# ResolveUrl("~/Info/"+ChannelSuffix+"/"+Eval("ID")+".aspx") %>"><%# Eval("Title") %></a><input class="date" >2013年10月11日17:59:31</input></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
