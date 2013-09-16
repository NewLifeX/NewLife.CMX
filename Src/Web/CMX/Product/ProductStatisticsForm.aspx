<%@ Page Title="产品统计管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ProductStatisticsForm.aspx.cs" Inherits="CMX_ProductStatisticsForm"%>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>产品统计管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑产品统计</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px" />
                <col />
                <tbody>
            
        <tr>
            <th></th>
            <td>
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新产品统计' />
            </td>
        </tr>
        </tbody>
    </table>
            </div>
    </div>
</asp:Content>