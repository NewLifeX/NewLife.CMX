<%@ Page Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="<#=Table.Name#>.aspx.cs" Inherits="<#=Config.EntityConnName+"_"+Table.Name#>" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title><#=Table.DisplayName#>管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div class="navigation">首页</div>
    <div class="tools_box">
        <div class="tools_bar">
            <a href="<#=Table.Name#>Form.aspx" class="tools_btn"><span><b class="add">添加<#=Table.DisplayName#></b></span></a>
            <div class="search_box">
                关键字：<asp:TextBox ID="txtKey" runat="server"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="查询" />
            </div>
        </div>
    </div><#
StringBuilder sbpk=new StringBuilder();
StringBuilder sbpk2=new StringBuilder();
Int32 pki=0;
foreach(IDataColumn Field in Table.Columns){
    if(Field.PrimaryKey) {
        if(sbpk.Length>0)sbpk.Append(",");
        sbpk.Append(Field.Name);

        if(sbpk2.Length>0)sbpk2.Append("&");
        sbpk2.Append(Field.Name+"={"+pki+++"}");
    } 
}
    #>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="<#=sbpk#>" DataSourceID="ods" AllowPaging="True" AllowSorting="True" CssClass="msgtable" PageSize="10" CellPadding="0" GridLines="None" EnableModelValidation="True">
        <Columns>
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cb" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>--%><#
foreach(IDataColumn Field in Table.Columns){
    String pname = Field.Name;

    // 查找关系，如果对方有名为Name的字符串字段，则加一个扩展属性
    IDataRelation dr=XCode.DataAccessLayer.ModelHelper.GetRelation(Table, Field.ColumnName);
    if(dr!=null&&!dr.Unique){
        IDataTable rtable=FindTable(dr.RelationTable);
        if(rtable!=null){
            IDataColumn rname=rtable.GetColumn("Name");
            if(rname!=null&&rname.DataType==typeof(String)){#>
            <%--<asp:BoundField DataField="<#=pname#>" HeaderText="<#=Field.DisplayName#>" SortExpression="<#=pname#>" <# if(Field.PrimaryKey){#>InsertVisible="False" ReadOnly="True" <#}#>>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="key" />
            </asp:BoundField>--%>
            <asp:BoundField DataField="<#=rtable.Name+"Name"#>" HeaderText="<#=Field.DisplayName#>" SortExpression="<#=pname#>" /><#
                continue;
            }
        }
    }

    if(Field.Identity){#>
            <asp:BoundField DataField="<#=pname#>" HeaderText="<#=Field.DisplayName#>" SortExpression="<#=pname#>" <# if(Field.PrimaryKey){#>InsertVisible="False" ReadOnly="True" <#}#>>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Ikey" />
            </asp:BoundField><#}
    else if(Field.DataType == typeof(DateTime)){#>
            <asp:BoundField DataField="<#=pname#>" HeaderText="<#=Field.DisplayName#>" SortExpression="<#=pname#>" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" <# if(Field.PrimaryKey){#>InsertVisible="False" ReadOnly="True" <#}#>>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
            </asp:BoundField><#}
    else if(Field.DataType == typeof(Decimal)){#>
            <asp:BoundField DataField="<#=pname#>" HeaderText="<#=Field.DisplayName#>" SortExpression="<#=pname#>" DataFormatString="{0:c}">
                <ItemStyle HorizontalAlign="Right" Font-Bold="True" ForeColor="Blue" />
            </asp:BoundField><#}
    else if(Type.GetTypeCode(Field.DataType)>=TypeCode.Int16&&Type.GetTypeCode(Field.DataType)<=TypeCode.UInt64){#>
            <asp:BoundField DataField="<#=pname#>" HeaderText="<#=Field.DisplayName#>" SortExpression="<#=pname#>" DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Right" Font-Bold="True" />
            </asp:BoundField><#}
    else if(Field.DataType == typeof(Boolean)){#>
            <asp:TemplateField HeaderText="<#=Field.DisplayName#>" SortExpression="<#=pname#>">
                <ItemTemplate>
                    <asp:Label ID="<#=pname#>1" runat="server" Text="√" Visible='<%# Eval("<#=pname#>") %>' Font-Bold="True" Font-Size="14pt" ForeColor="Green"></asp:Label>
                    <asp:Label ID="<#=pname#>2" runat="server" Text="×" Visible='<%# !(Boolean)Eval("<#=pname#>") %>' Font-Bold="True" Font-Size="16pt" ForeColor="Red"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField><#}
    // 密码字段和大文本字段不输出
    else if(!pname.Equals("Password", StringComparison.OrdinalIgnoreCase) && 
       !pname.Equals("Pass", StringComparison.OrdinalIgnoreCase) && 
       !pname.Equals("Pwd", StringComparison.OrdinalIgnoreCase) && 
       Field.Length>0 && Field.Length<300){#>
            <asp:BoundField DataField="<#=pname#>" HeaderText="<#=Field.DisplayName#>" SortExpression="<#=pname#>" <# if(Field.PrimaryKey){#>InsertVisible="False" ReadOnly="True" <#}#>/><#
}}#>
                <asp:TemplateField HeaderText="编辑" SortExpression="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperManager" runat="server" Text='编辑<#=Table.DisplayName#>' NavigateUrl='<%# "<#=Table.Name#>Form.aspx?<#=sbpk#>="+Eval("<#=sbpk#>")%>'></asp:HyperLink>
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
    <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="SearchCount" SelectMethod="Search" SortParameterName="orderClause" EnableViewState="false">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtKey" Name="key" PropertyName="Text" Type="String" />
            <asp:Parameter Name="orderClause" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <XCL:GridViewExtender ID="gvExt" runat="server">
    </XCL:GridViewExtender>
    <div class="line10"></div>
</asp:Content>