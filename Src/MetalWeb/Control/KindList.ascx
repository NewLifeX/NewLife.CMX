<%@ Control Language="C#" AutoEventWireup="true" CodeFile="KindList.ascx.cs" Inherits="Control_KindList" %>
<ul>
    <asp:Repeater runat="server" ID="kindrepeater">
        <ItemTemplate>
            <li><a href="<%# ResolveUrl("~/List/"+ChannelSuffix+ "/"+Eval("ID")+".aspx") %>"><%# Eval("TreeNodeName") %></a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
