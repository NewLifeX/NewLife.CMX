<%@ Page Title="频道管理" Language="C#" MasterPageFile="~/Admin/FormPage.master" AutoEventWireup="true" CodeFile="ChannelForm.aspx.cs" Inherits="CMX_ChannelForm"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <table border="0" class="m_table" cellspacing="1" cellpadding="0" align="Center">
        <tr>
            <th colspan="2">频道</th>
        </tr>
        <tr>
            <td align="right">名称：</td>
            <td><asp:TextBox ID="frmName" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">模型：</td>
            <td><XCL:NumberBox ID="frmModelID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <td align="right">后缀：</td>
            <td><asp:TextBox ID="frmSuffix" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">启用：</td>
            <td><asp:CheckBox ID="frmEnable" runat="server" Text="启用" /></td>
        </tr>
<tr>
            <td align="right">创建人：</td>
            <td><XCL:NumberBox ID="frmCreateUserID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <td align="right">创建人：</td>
            <td><asp:TextBox ID="frmCreateUserName" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">创建时间：</td>
            <td><XCL:DateTimePicker ID="frmCreateTime" runat="server"></XCL:DateTimePicker></td>
        </tr>
<tr>
            <td align="right">更新人：</td>
            <td><XCL:NumberBox ID="frmUpdateUserID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <td align="right">更新人：</td>
            <td><asp:TextBox ID="frmUpdateUserName" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
<tr>
            <td align="right">更新时间：</td>
            <td><XCL:DateTimePicker ID="frmUpdateTime" runat="server"></XCL:DateTimePicker></td>
        </tr>
<tr>
            <td align="right">备注：</td>
            <td><asp:TextBox ID="frmRemark" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
    </table>
    <table border="0" align="Center" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新频道' />
                &nbsp;<asp:Button ID="btnReturn" runat="server" OnClientClick="parent.Dialog.CloseSelfDialog(frameElement);return false;" Text="返回" />
            </td>
        </tr>
    </table>
</asp:Content>