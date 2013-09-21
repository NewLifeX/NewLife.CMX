<%@ Page Title="文章分类管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ArticleCategory.aspx.cs" Inherits="CMX_ArticleCategory" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>文章分类管理</title>
    <script type="text/javascript">
        //$(function () {
        //    $('.displayurl').each(function () {
        //        var url = $(this).text();
        //        var str = "javascript:parent.f_addTab('SysModel','系统模型管理','<url>')";
        //        var param = location;
        //        var href = str.replace("<url>", url + "&" + param);
        //        $($($('.displayurl')[0]).parent().find("a")).attr('href', href);
        //    });
        //});
    </script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div class="tools_box">
        <div class="tools_bar">
            <a href="ArticleCategoryForm.aspx" class="tools_btn listpage"><span><b class="add">添加文章分类</b></span></a>
            <div class="search_box">
                关键字：<asp:TextBox ID="txtKey" runat="server"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="查询" />
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
            <asp:BoundField DataField="TreeNodeName" HeaderText="名称" />
            <%-- <asp:TemplateField HeaderText="名称" SortExpression="Name">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperNodeName" runat="server" Text='<%# Eval("TreeNodeName") %>'
                        NavigateUrl='<%# "MenuForm.aspx?ID="+Eval("ID") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="ParentName" HeaderText="父类">
                <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="子分类">
                <ItemTemplate>
                    <asp:HyperLink ID="child" runat="server" Text='<%# (Boolean)Eval("IsEnd")?"":"添加子类" %>' NavigateUrl='<%# "ArticleCategoryForm.aspx?ParentID="+Eval("ID")%>' CssClass="formUrl"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="文章列表">
                <ItemTemplate>
                    <asp:HyperLink ID="articlelist" runat="server" Text='<%# (Boolean)Eval("IsEnd")?"添加/查看文章":"" %>' NavigateUrl='<%# "Article.aspx?CategoryID="+Eval("ID")%>' CssClass="formUrl"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑分类">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperManager" runat="server" Text='编辑分类' NavigateUrl='<%# "ArticleCategoryForm.aspx?ID="+Eval("ID")%>' CssClass="formUrl"></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="删除">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick='return confirm("确定删除吗？")' Text="删除"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
            </asp:TemplateField>
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
    <XCL:GridViewExtender ID="gvExt" runat="server" DblClickRowFieldText="编辑分类">
    </XCL:GridViewExtender>
    <div class="line10"></div>
</asp:Content>
