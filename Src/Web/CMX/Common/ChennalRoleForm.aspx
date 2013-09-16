<%@ Page Title="频道权限管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ChennalRoleForm.aspx.cs" Inherits="CMX_ChennalRoleForm"%>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>频道权限管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑频道权限</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px" />
                <col />
                <tbody>
        <tr>
            <th>角色ID：</th>
            <td><XCL:NumberBox ID="frmRoleID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <th>频道ID：</th>
            <td><XCL:NumberBox ID="frmChannelID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
    
        <tr>
            <th></th>
            <td>
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新频道权限' />
            </td>
        </tr>
        </tbody>
    </table>
            </div>
    </div>
</asp:Content>