<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuForm.aspx.cs" Inherits="Admin_Manager_MenuForm" MasterPageFile="~/Admin/ManagerPage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <%--<div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 系统管理 &gt; 菜单列表</div>--%>
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
                        <th>名称：</th>
                        <td>
                            <asp:TextBox ID="frmName" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100"></asp:TextBox><label>*</label></td>
                    </tr>
                    <tr>
                        <th>所属父菜单：</th>
                        <td>
                            <XCL:DropDownList ID="frmParentID" runat="server" DataTextField="TreeNodeName2" DataValueField="ID" CssClass="select2"
                                AppendDataBoundItems="True" DataSourceID="ods">
                            </XCL:DropDownList>
                            <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="FindAllChildsByParent">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="parentKey" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <th>链接：
                        </th>
                        <td>
                            <asp:TextBox ID="frmUrl" runat="server" CssClass="txtInput normal required" minlength="1" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <th>序号：</th>
                        <td>
                            <XCL:NumberBox ID="frmSort" runat="server" Width="80px"></XCL:NumberBox></td>
                    </tr>
                    <tr>
                        <th>权限：</th>
                        <td>
                            <asp:TextBox ID="frmPermission" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>显示：</th>
                        <td>
                            <asp:RadioButtonList ID="frmIsEnable" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="True">显示 </asp:ListItem>
                                <asp:ListItem Value="False">隐藏 </asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <th>备注：</th>
                        <td>
                            <asp:TextBox ID="frmRemark" runat="server" CssClass="txtInput normal" minlength="2" MaxLength="100"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="提交保存" CssClass="btnSubmit" /></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

