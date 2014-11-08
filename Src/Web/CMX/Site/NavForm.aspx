<%@ Page Title="导航管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="NavForm.aspx.cs" Inherits="CMX_NavForm"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑导航</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px" />
                <col />
                <tbody>
        <tr>
            <th>名称：</th>
            <td><asp:TextBox ID="frmName" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
<tr>
            <th>地址：</th>
            <td><asp:TextBox ID="frmUrl" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
<tr>
            <th>是否空白窗口打开：</th>
            <td><asp:CheckBox ID="frmNewWindow" runat="server" Text="是否空白窗口打开" /></td>
        </tr>
<tr>
            <th>创建人：</th>
            <td><XCL:NumberBox ID="frmCreateUserID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <th>创建时间：</th>
            <td><XCL:DateTimePicker ID="frmCreateTime" runat="server"></XCL:DateTimePicker></td>
        </tr>
<tr>
            <th>更新人：</th>
            <td><XCL:NumberBox ID="frmUpdateUserID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <th>更新时间：</th>
            <td><XCL:DateTimePicker ID="frmUpdateTime" runat="server"></XCL:DateTimePicker></td>
        </tr>
<tr>
            <th>备注：</th>
            <td><asp:TextBox ID="frmRemark" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
    
        <tr>
            <th></th>
            <td>
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新导航' />
            </td>
        </tr>
        </tbody>
    </table>
            </div>
    </div>
</asp:Content>