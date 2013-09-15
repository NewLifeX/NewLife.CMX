<%@ Page Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ModelForm.aspx.cs" Inherits="CMX_ModelForm"%>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>模型管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 </div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑模型</a></li>
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
            <th>启用：</th>
            <td><asp:CheckBox ID="frmEnable" runat="server" Text="启用" /></td>
        </tr>
<tr>
            <th>创建人：</th>
            <td><XCL:NumberBox ID="frmCreateUserID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <th>创建人：</th>
            <td><asp:TextBox ID="frmCreateUserName" runat="server" Width="150px"></asp:TextBox></td>
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
            <th>更新人：</th>
            <td><asp:TextBox ID="frmUpdateUserName" runat="server" Width="150px"></asp:TextBox></td>
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
            <th>表单页：</th>
            <td><asp:TextBox ID="frmFormTemplatePath" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
<tr>
            <th>列表页：</th>
            <td><asp:TextBox ID="frmListTemplatePath" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
<tr>
            <th>类名：</th>
            <td><asp:TextBox ID="frmClassName" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
    
        <tr>
            <th></th>
            <td>
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新模型' />
            </td>
        </tr>
        </tbody>
    </table>
            </div>
    </div>
</asp:Content>