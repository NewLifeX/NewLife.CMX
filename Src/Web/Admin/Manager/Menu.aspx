<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="Admin_Manager_Menu" MasterPageFile="~/Admin/ManagerPage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="H">
    <title>菜单管理</title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="C">
    <div class="tools_box">
        <div class="tools_bar">
            <a href="MenuForm.aspx" class="tools_btn"><span><b class="add">添加菜单</b></span></a>
            <div class="search_box">
                <asp:Label ID="Label_Info" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="导出" CssClass="btnSearch"
                    OnClick="Button2_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" CssClass="btnSearch" runat="server" />
                &nbsp;<asp:Button ID="Button3" runat="server" Text="导入" CssClass="btnSearch" OnClick="Button3_Click" />
                &nbsp;<asp:Button ID="Button1" runat="server" Text="扫描目录" CssClass="btnSearch" OnClick="Button1_Click" />
            </div>
        </div>
    </div>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        CssClass="msgtable" CellPadding="0" GridLines="None" PageSize="15" EnableModelValidation="True"
        DataSourceID="ods" OnRowCommand="GridView1_RowCommand" EnableViewState="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="编号" InsertVisible="False" ReadOnly="True"
                SortExpression="ID">
                <HeaderStyle Width="40px" />
                <ItemStyle CssClass="key" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="名称" SortExpression="Name">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperNodeName" runat="server" Text='<%# Eval("TreeNodeName") %>'
                        NavigateUrl='<%# "MenuForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="Url" HeaderText="链接" SortExpression="Url">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ParentMenuName" HeaderText="父菜单" SortExpression="ParentID">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Sort" HeaderText="序号" SortExpression="Sort">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Permission" HeaderText="权限" SortExpression="Permission">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="显示">
                <ItemTemplate>
                    <asp:CheckBox ID="checkebox1" runat="server" Enabled="false" Checked='<%# Bind("IsShow") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="升" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                        CommandName="Up" Text="↑" Font-Size="12pt" ForeColor="Red" Visible='<%# !IsFirst(Container.DataItem) %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Font-Size="12pt" ForeColor="Red" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="降" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                        CommandName="Down" Text="↓" Font-Size="12pt" ForeColor="Green" Visible='<%# !IsLast(Container.DataItem) %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Font-Size="12pt" ForeColor="Green" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="添加子菜单" SortExpression="Name">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperSub" runat="server" Text='添加子菜单' NavigateUrl='<%# "MenuForm.aspx?ParentID="+Eval("ID") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" SortExpression="Name">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperManager" runat="server" Text='编辑' NavigateUrl='<%# "MenuForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        OnClientClick="return confirm('确定删除？');" Text="删除"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:CheckBoxField />
        </Columns>
        <EmptyDataTemplate>
            没有符合条件的数据！
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="ods" runat="server" DeleteMethod="Delete" SelectMethod="FindAllChildsNoParent"
        EnableViewState="False">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="parentKey" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <XCL:GridViewExtender ID="gvExt" runat="server"></XCL:GridViewExtender>
    <div class="line10"></div>
</asp:Content>
