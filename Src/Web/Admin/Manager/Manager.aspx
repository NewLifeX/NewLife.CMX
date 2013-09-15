<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manager.aspx.cs" Inherits="Admin_Manager_Manager" MasterPageFile="~/Admin/ManagerPage.master" %>


<asp:content id="Content1" runat="server" contentplaceholderid="H">
    <title>管理员管理</title>
</asp:content>
<asp:content id="Content2" runat="server" contentplaceholderid="C">
        <%--<div class="navigation">首页 &gt; 系统管理 &gt; 管理员管理</div>--%>
        <div class="tools_box">
            <div class="tools_bar">
                <div class="search_box">
                    <asp:TextBox ID="txtKey" runat="server" CssClass="txtInput"></asp:TextBox>
                    角色：<XCL:DropDownList ID="ddlRole" runat="server" DataSourceID="odsRole" AppendDataBoundItems="true"
                        DataTextField="Name" DataValueField="ID" AutoPostBack="True"  CssClass="select2">
                        <asp:ListItem Value="0">请选择</asp:ListItem>
                    </XCL:DropDownList><asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" />
                </div>
                <a href="ManagerForm.aspx" class="tools_btn"><span><b class="add">添加管理员</b></span></a>
                <a href="Role.aspx" class="tools_btn"><span><b class="return">角色管理</b></span></a>
                 <asp:LinkButton ID="btnEnable" runat="server" CssClass="tools_btn"
                    OnClientClick="return confirm('确定批量启用吗？')" OnClick="btnEnable_Click"><span><b class="error">批量启用</b></span></asp:LinkButton>
                 <asp:LinkButton ID="btnDisable" runat="server" CssClass="tools_btn"
                    OnClientClick="return confirm('确定批量禁用吗？')" OnClick="btnDisable_Click"><span><b class="delete">批量禁用</b></span></asp:LinkButton>
                <asp:LinkButton ID="btnDelete" runat="server" CssClass="tools_btn"
                    OnClientClick="return confirm('确定批量删除吗？')" OnClick="btnDelete_Click"><span><b class="delete">批量删除</b></span></asp:LinkButton>
            </div>
        </div>
        <!--列表展示.开始-->
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="ods" CssClass="msgtable" CellPadding="0" GridLines="None"
            EnableModelValidation="True" AllowPaging="True" AllowSorting="True"
            PageSize="10">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cb" CssClass="checkall" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="20px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="编号" SortExpression="ID" InsertVisible="False"
                    ReadOnly="True">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"/>
                    <ItemStyle CssClass="key" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="用户名" SortExpression="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperName" runat="server" Text='<%# Bind("Name") %>' NavigateUrl='<%# "ManagerForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="友好名称" SortExpression="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperFriendName" runat="server" Text='<%# Bind("FriendName") %>' NavigateUrl='<%# "ManagerForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="RoleName" HeaderText="角色" SortExpression="RoleID">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Logins" HeaderText="登录次数" SortExpression="Logins">
                  <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastLogin" HeaderText="最后登录" SortExpression="LastLogin"
                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" >
                      <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastLoginIP" HeaderText="最后登陆IP" SortExpression="LastLoginIP">
                  <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="是否启用" SortExpression="IsEnable">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkbox1" runat="server" Enabled="false" Checked='<%# Bind("IsEnable")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="checkbox2" runat="server" Checked='<%# Bind("IsEnable")%>' />
                    </EditItemTemplate>
                      <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" SortExpression="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperManager" runat="server" Text='编辑管理员' NavigateUrl='<%# "ManagerForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
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
            </Columns>
            <EmptyDataTemplate>
                没有数据!
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="Search" DeleteMethod="Delete"
            EnablePaging="True" SelectCountMethod="SearchCount"
            SortParameterName="orderClause">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtKey" Name="key" Type="String" PropertyName="Text" />
                <asp:ControlParameter ControlID="ddlRole" Name="roleId" Type="Int32" PropertyName="SelectedValue" />
                <asp:Parameter Name="orderClause" Type="String" DefaultValue="ID Desc" />
                <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsRole" runat="server" SelectMethod="FindAllWithCache"></asp:ObjectDataSource>
        <XCL:GridViewExtender ID="gvExt" runat="server">
        </XCL:GridViewExtender>
        <div class="line10"></div>
</asp:content>
