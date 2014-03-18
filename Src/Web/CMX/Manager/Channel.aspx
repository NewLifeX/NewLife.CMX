<%@ Page Title="频道管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="Channel.aspx.cs" Inherits="CMX_Channel" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>频道管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div class="tools_box">
        <div class="tools_bar">
            <a href="ChannelForm.aspx" class="tools_btn"><span><b class="add">添加频道</b></span></a>
            <div class="search_box">
                关键字：<asp:TextBox ID="txtKey" runat="server" CssClass="form-control"></asp:TextBox>
                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnSearch" />
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
            <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="ModelName" HeaderText="模型" SortExpression="ModelID">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Suffix" HeaderText="后缀" SortExpression="Suffix" />
            <asp:BoundField DataField="ListTemplate" HeaderText="列表模板" SortExpression="ListTemplate" />
            <asp:BoundField DataField="FormTemplate" HeaderText="内容模板" SortExpression="FormTemplate" />
            <asp:TemplateField HeaderText="启用" SortExpression="Enable">
                <ItemTemplate>
                    <asp:Label ID="Enable1" runat="server" Text="√" Visible='<%# Eval("Enable") %>' Font-Bold="True" Font-Size="14pt" ForeColor="Green"></asp:Label>
                    <asp:Label ID="Enable2" runat="server" Text="×" Visible='<%# !(Boolean)Eval("Enable") %>' Font-Bold="True" Font-Size="16pt" ForeColor="Red"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="UpdateUserName" HeaderText="更新人" SortExpression="UpdateUserID">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="UpdateTime" HeaderText="更新时间" SortExpression="UpdateTime" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="编辑频道" SortExpression="Name">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperManager" runat="server" Text='编辑频道' NavigateUrl='<%# "ChannelForm.aspx?ID="+Eval("ID")%>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="删除">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick='return confirm("确定删除吗？")' Text="删除"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            没有符合条件的数据！
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="SearchCount" SelectMethod="Search" SortParameterName="orderClause" EnableViewState="false">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtKey" Name="key" PropertyName="Text" Type="String" />
            <asp:Parameter Name="orderClause" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <XCL:GridViewExtender ID="gvExt" runat="server" DblClickRowFieldText="编辑频道">
    </XCL:GridViewExtender>
    <div class="line10"></div>
</asp:Content>
