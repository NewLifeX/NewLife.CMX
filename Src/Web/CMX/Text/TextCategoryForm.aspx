<%@ Page Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="TextCategoryForm.aspx.cs" Inherits="CMX_TextCategoryForm"%>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>文本分类管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑文本分类</a></li>
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
            <th>父类：</th>
            <td><XCL:NumberBox ID="frmParentID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <th>排序：</th>
            <td><XCL:NumberBox ID="frmSort" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <th>备注：</th>
            <td><asp:TextBox ID="frmRemark" runat="server" Width="300px"></asp:TextBox></td>
        </tr>
    
        <tr>
            <th></th>
            <td>
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新文本分类' />
            </td>
        </tr>
        </tbody>
    </table>
            </div>
    </div>
</asp:Content>