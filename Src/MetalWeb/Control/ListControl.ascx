<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListControl.ascx.cs" Inherits="Control_ArticleControl" %>
<ul>
    <asp:Repeater ID="Content" runat="server">
        <ItemTemplate>
            <li><a target="_blank" href="<%# ResolveUrl("~/Info/"+ChannelSuffix+"/"+Eval("ID")+".aspx") %>"><%# Eval("Title") %></a><label class="date" ><%# ((DateTime)Eval("UpdateTime")).ToShortDateString() %></label></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
