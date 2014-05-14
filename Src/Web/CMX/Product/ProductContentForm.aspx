<%@ Page Title="产品内容管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ProductContentForm.aspx.cs" Inherits="CMX_ProductContentForm"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <table border="0" class="m_table" cellspacing="1" cellpadding="0" align="Center">
        <tr>
            <th colspan="2">产品内容</th>
        </tr>
        <tr>
            <td align="right">规格参数：</td>
            <td><asp:TextBox ID="frmSpecification" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">功能特点：</td>
            <td><asp:TextBox ID="frmFeature" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">推荐应用：</td>
            <td><asp:TextBox ID="frmApp" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">相关配件：</td>
            <td><asp:TextBox ID="frmFitting" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">产品视频：</td>
            <td><asp:TextBox ID="frmVideo" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox></td>
        </tr>
    </table>
    <table border="0" align="Center" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新产品内容' />
                &nbsp;<asp:Button ID="btnReturn" runat="server" OnClientClick="parent.Dialog.CloseSelfDialog(frameElement);return false;" Text="返回" />
            </td>
        </tr>
    </table>
</asp:Content>