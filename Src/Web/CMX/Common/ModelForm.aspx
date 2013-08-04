<%@ Page Title="模型管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ModelForm.aspx.cs" Inherits="CMX_ModelForm"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <table border="0" class="m_table" cellspacing="1" cellpadding="0" align="Center">
        <tr>
            <th colspan="2">模型</th>
        </tr>
        <tr>
            <td align="right">名称：</td>
            <td><asp:TextBox ID="frmName" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
    </table>
    <table border="0" align="Center" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新模型' />
                &nbsp;<asp:Button ID="btnReturn" runat="server" OnClientClick="parent.Dialog.CloseSelfDialog(frameElement);return false;" Text="返回" />
            </td>
        </tr>
    </table>
</asp:Content>