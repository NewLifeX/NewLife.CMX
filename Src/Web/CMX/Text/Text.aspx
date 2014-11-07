<%@ Page Title="文本管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="Text.aspx.cs" Inherits="CMX_Text" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>文本管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div class="tools_box">
        <div class="tools_bar">
            <%--<a href="TextForm.aspx" class="tools_btn"><span><b class="add">添加文本</b></span></a>--%>
            <a href="TextForm.aspx" class="tools_btn listpage"><span><b class="add">添加单文本</b></span></a>
            <div class="search_box">
                关键字：<asp:TextBox ID="txtKey" runat="server" CssClass="form-control"></asp:TextBox>
                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnSearch"/>
            </div>
        </div>
    </div>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ods" AllowPaging="True" AllowSorting="True" CssClass="msgtable" PageSize="10" CellPadding="0" GridLines="None" EnableModelValidation="True">
        <Columns>
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cb" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="ID" HeaderText="编号" SortExpression="ID" InsertVisible="False" ReadOnly="True">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Ikey" />
            </asp:BoundField>
            <asp:BoundField DataField="CategoryName" HeaderText="分类" SortExpression="CategoryName">
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
            </asp:BoundField>
            <asp:BoundField DataField="Title" HeaderText="标题" SortExpression="Title" />
            <asp:BoundField DataField="Version" HeaderText="最新版本" SortExpression="Version" DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Right" Font-Bold="True" />
            </asp:BoundField>
            <asp:BoundField DataField="Views" HeaderText="访问量" SortExpression="Hits" DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CreateUserName" HeaderText="创建人" SortExpression="CreateUserName" />
            <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="UpdateUserName" HeaderText="更新人" SortExpression="UpdateUserName" />
            <asp:BoundField DataField="UpdateTime" HeaderText="更新时间" SortExpression="UpdateTime" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="编辑" SortExpression="Name">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperManager" runat="server" Text='编辑' NavigateUrl='<%# "TextForm.aspx?ID="+Eval("ID")%>' CssClass="formUrl"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="删除">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick='return confirm("确定删除吗？")' Text="删除"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            没有符合条件的数据！
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="SearchCount" SelectMethod="Search" SortParameterName="orderClause" EnableViewState="false" OnSelecting="ods_Selecting">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtKey" Name="key" PropertyName="Text" Type="String" />
            <asp:Parameter Name="CategoryID" Type="Int32" />
            <asp:Parameter Name="orderClause" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <XCL:GridViewExtender ID="gvExt" runat="server">
    </XCL:GridViewExtender>
    <div class="line10"></div>
</asp:Content>
