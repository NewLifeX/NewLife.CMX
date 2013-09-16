<%@ Page Title="文章统计管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ArticleStatisticsForm.aspx.cs" Inherits="CMX_ArticleStatisticsForm"%>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>文章统计管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑文章统计</a></li>
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
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新文章统计' />
            </td>
        </tr>
        </tbody>
    </table>
            </div>
    </div>
</asp:Content>