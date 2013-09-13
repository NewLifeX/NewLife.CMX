<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagerForm.aspx.cs" Inherits="Admin_Manager_ManagerForm" MasterPageFile="~/Admin/ManagerPage.master" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <%--<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 系统管理 &gt; 管理员管理</div>--%>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑管理员信息</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px" />
                <col />
                <tbody>
                    <tr>
                        <th>管理角色：</th>
                        <td>
                            <XCL:DropDownList AppendDataBoundItems="true" ID="frmRoleID" runat="server" DataTextField="Name" DataValueField="ID" DataSourceID="ods" CssClass="select2 required">
                                <asp:ListItem Value="0">请选择</asp:ListItem>
                            </XCL:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>账户状态：</th>
                        <td>
                            <asp:RadioButtonList ID="frmIsEnable" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="True">启用 </asp:ListItem>
                                <asp:ListItem Value="False">禁用 </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <th>用户名：</th>
                        <td>
                            <asp:TextBox ID="frmName" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100"></asp:TextBox><label>*</label></td>
                    </tr>
                    <tr>
                        <th>登录密码：</th>
                        <td>
                            <asp:TextBox ID="frmPassword" runat="server" CssClass="txtInput normal "
                                minlength="2" MaxLength="100" TextMode="Password"></asp:TextBox><label>*</label></td>
                    </tr>
                    <tr>
                        <th>确认密码：</th>
                        <td>
                            <asp:TextBox ID="frmPasswordRe" runat="server" CssClass="txtInput normal "
                                minlength="6" MaxLength="100" TextMode="Password"></asp:TextBox><label>*</label></td>
                    </tr>
                    <tr>
                        <th>姓名：</th>
                        <td>
                            <asp:TextBox ID="frmDisplayName" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="30"></asp:TextBox><label>*</label></td>
                    </tr>
                    <tr>
                        <th>电话：</th>
                        <td>
                            <asp:TextBox ID="frmTelephone" runat="server" CssClass="txtInput normal" minlength="2" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>邮箱：</th>
                        <td>
                            <asp:TextBox ID="frmMail" runat="server" CssClass="txtInput normal email" minlength="2" MaxLength="50"></asp:TextBox></td>
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
</asp:Content>


