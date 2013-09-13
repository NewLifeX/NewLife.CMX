<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysModelForm.aspx.cs" Inherits="Admin_Manager_SysModelForm" MasterPageFile="~/Admin/ManagerPage.master"%>

<asp:content id="Content1" runat="server" contentplaceholderid="H">
</asp:content>
<asp:content id="Content2" runat="server" contentplaceholderid="C">
        <%--<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 系统管理 &gt; 系统模型管理</div>--%>
        <div id="contentTab">
            <ul class="tab_nav">
                <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑系统模型</a></li>
            </ul>
            <div class="tab_con" style="display: block;">
                <table class="form_table">
                    <col width="180px" />
                    <col />
                    <tbody>
                        <tr>
                            <th>模型标题：</th>
                            <td>
                                <asp:TextBox ID="frmName" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>排 序：</th>
                            <td>
                                <asp:TextBox ID="frmSoft" runat="server" CssClass="txtInput normal small required digits"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>列表页：</th>
                            <td>
                                 <asp:TextBox ID="frmListUrl" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>详细页：</th>
                            <td>
                                 <asp:TextBox ID="frmViewUrl" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th></th>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="提交保存" CssClass="btnSubmit" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="FindAllWithCache"></asp:ObjectDataSource>
        </div>
</asp:content>


