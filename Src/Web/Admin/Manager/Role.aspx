<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Admin_Manager_Role" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统角色管理</title>
    <link type="text/css" rel="stylesheet" href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" />
    <link type="text/css" rel="stylesheet" href="../images/style.css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="navigation">首页 &gt; 系统管理 &gt; 角色管理</div>
        <div class="tools_box">
            <div class="tools_bar">
                <div class="search_box">
                    角色：
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtInput"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="添 加" CssClass="btnSearch" OnClick="btnAdd_Click" />&nbsp;模版角色：<asp:TextBox ID="txtRoleTemplate" runat="server" CssClass="textfield"></asp:TextBox>
                    &nbsp;<asp:Button ID="btnCopyRole" runat="server" Text="批量复制权限"
                        OnClientClick='return confirm("确定批量操作吗？")' CssClass="btnSearch" OnClick="btnCopyRole_Click" />
                </div>
                <a href="Manager.aspx" class="tools_btn"><span><b class="return">管理员列表</b></span></a>
            </div>
        </div>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="ods" AllowPaging="True" AllowSorting="True" CssClass="msgtable"
            CellPadding="0" GridLines="None" PageSize="20" EnableModelValidation="True">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cb" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="20px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="编号" InsertVisible="False" ReadOnly="True"
                    SortExpression="ID">
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="key" />
                </asp:BoundField>

                <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" />
                <asp:TemplateField HeaderText="权限设置" SortExpression="Name">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperManager" runat="server" Text='权限设置' NavigateUrl='<%# "RoleMenuForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="删除" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick="return confirm('确定删除？');" Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                没有符合条件的数据！
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
            SelectCountMethod="FindCountByName" SelectMethod="FindAllByName" SortParameterName="orderClause"
            InsertMethod="Insert" DeleteMethod="Delete" UpdateMethod="Update">
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
    </form>
</body>
</html>
