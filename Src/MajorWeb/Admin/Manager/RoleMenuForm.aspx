<%@ Page Title="角色管理" Language="C#" AutoEventWireup="true" CodeFile="RoleMenuForm.aspx.cs" Inherits="Admin_Manager_RoleMenuForm" MasterPageFile="~/Admin/ManagerPage.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="H">
    <title>权限管理</title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="C">
        <%--<div class="navigation">首页 &gt; 系统管理 &gt; 角色管理</div>--%>
        <div class="tools_box">
            <div class="tools_bar">
                <a href="Manager.aspx" class="tools_btn"><span><b class="return">管理员列表</b></span></a>
                <a href="Role.aspx" class="tools_btn"><span><b class="return">角色列表</b></span></a>
                <div class="search_box">
                    &nbsp;角色：<asp:DropDownList ID="ddlRole" runat="server" DataSourceID="odsRole"  CssClass="select2"
                        DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            CssClass="msgtable" CellPadding="0" GridLines="None" PageSize="15" EnableModelValidation="True"
            DataSourceID="ods" OnRowDataBound="gv_RowDataBound"
            EnableViewState="False">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="编号" InsertVisible="False" ReadOnly="True"
                    SortExpression="ID">
                    <HeaderStyle Width="40px" />
                    <ItemStyle CssClass="key" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="权限名称" SortExpression="Permission">
                    <ItemTemplate>
                        <%# new String('　', (Convert.ToInt32(Eval("Deepth"))-1)*2)%><asp:Label ID="Label1"
                            runat="server" Text='<%# Eval("Permission") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="200px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="授权">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" BorderWidth="0px"
                            OnCheckedChanged="CheckBox1_CheckedChanged" />
                    </ItemTemplate>
                    <ItemStyle Width="40px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作权限">
                    <ItemTemplate>
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                        </asp:CheckBoxList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                没有符合条件的数据！
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="ods" runat="server"
            SelectMethod="FindAllChildsNoParent">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="parentKey" Type="Object" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsRole" runat="server" SelectMethod="FindAllByName">
            <SelectParameters>
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="value" Type="Object" />
                <asp:Parameter Name="orderClause" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <XCL:GridViewExtender ID="gvExt" runat="server">
        </XCL:GridViewExtender>
        <div class="line10"></div>
</asp:Content>
