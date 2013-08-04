<%@ Page Title="文章内容管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="TextContentForm.aspx.cs" Inherits="CMX_TextContentForm"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <table border="0" class="m_table" cellspacing="1" cellpadding="0" align="Center">
        <tr>
            <th colspan="2">文章内容</th>
        </tr>
        <tr>
            <td align="right">主题：</td>
            <td><XCL:NumberBox ID="frmParentID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <td align="right">标题：</td>
            <td><asp:TextBox ID="frmTitle" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">版本：</td>
            <td><XCL:NumberBox ID="frmVersion" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <td align="right">创建人：</td>
            <td><XCL:NumberBox ID="frmCreateUser" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <td align="right">创建人：</td>
            <td><asp:TextBox ID="frmCreateName" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">创建时间：</td>
            <td><XCL:DateTimePicker ID="frmCreateTime" runat="server"></XCL:DateTimePicker></td>
        </tr>
<tr>
            <td align="right">内容：</td>
            <td><asp:TextBox ID="frmContent" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox></td>
        </tr>
    </table>
    <table border="0" align="Center" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新文章内容' />
                &nbsp;<asp:Button ID="btnReturn" runat="server" OnClientClick="parent.Dialog.CloseSelfDialog(frameElement);return false;" Text="返回" />
            </td>
        </tr>
    </table>
</asp:Content>