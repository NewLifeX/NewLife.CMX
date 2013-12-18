<%@ Page Title="模型列表" Language="C#" AutoEventWireup="true" CodeFile="SysModel.aspx.cs" Inherits="Admin_Manager_SysModel" MasterPageFile="~/Admin/ManagerPage.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="H">
    <title>模型列表</title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="C">
       <%-- <div class="navigation">首页 &gt; 系统管理 &gt; 模型列表</div>--%>
        <div class="tools_box">
            <div class="tools_bar">
                <a href="SysModelForm.aspx" class="tools_btn"><span><b class="add">添加模型</b></span></a>
                <div class="search_box">
                    <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜 索" CssClass="btnSearch" />
                </div>
            </div>
        </div>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            CssClass="msgtable" CellPadding="0" GridLines="None" PageSize="15" EnableModelValidation="True"
            DataSourceID="ods" EnableViewState="False">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="编号" InsertVisible="False" ReadOnly="True"
                    SortExpression="ID">
                    <HeaderStyle Width="40px" />
                    <ItemStyle CssClass="key" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="名称" SortExpression="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperNodeName" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# "SysModelForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ListUrl" HeaderText="列表页面" SortExpression="ListUrl">
                  <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ViewUrl" HeaderText="编辑页面" SortExpression="ViewUrl">
                  <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Sort" HeaderText="序号" SortExpression="Sort">
                  <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="编辑" SortExpression="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperManager" runat="server" Text='编辑' NavigateUrl='<%# "SysModelForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="60px"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick="return confirm('确定删除？');" Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="60px"/>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                没有符合条件的数据！
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="Search" SelectCountMethod="SearchCount"
            EnableViewState="False" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" UpdateMethod="Update">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtKey" Name="key" Type="String" PropertyName="Text" />
                <asp:Parameter Name="orderClause" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <XCL:GridViewExtender ID="gvExt" runat="server">
        </XCL:GridViewExtender>
        <div class="line10"></div>
</asp:Content>