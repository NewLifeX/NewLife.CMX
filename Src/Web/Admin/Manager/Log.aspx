<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Log.aspx.cs" Inherits="Admin_Manager_Log" MasterPageFile="~/Admin/ManagerPage.master"%>

<asp:content id="Content1" runat="server" contentplaceholderid="H">
    <title>系统日志</title>
</asp:content>
<asp:content id="Content2" runat="server" contentplaceholderid="C">
        <%--<div class="navigation">首页 &gt; 系统管理 &gt; 日志列表</div>--%>
        <div class="tools_box">
            <div class="tools_bar">
                <div class="search_box">
                    类别：<asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="True"
                        DataSourceID="odsCategory" DataTextField="Category" DataValueField="Category"   CssClass="select2">
                        <asp:ListItem>全部</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;管理员：<asp:DropDownList ID="ddlAdmin" runat="server" AppendDataBoundItems="True"
                        DataTextField="FriendName" DataValueField="ID"  CssClass="select2">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;关键字：<asp:TextBox ID="key" runat="server" CssClass="textfield" Width="70px"></asp:TextBox>
                    &nbsp;时间：<XCL:DateTimePicker ID="StartDate" runat="server" LongTime="False">
                    </XCL:DateTimePicker>
                    &nbsp;至
                    <XCL:DateTimePicker ID="EndDate" runat="server" LongTime="False">
                    </XCL:DateTimePicker>
                    &nbsp;<asp:Button ID="Button1" runat="server" CssClass="btnSearch" Text="查询" OnClick="Button1_Click" />
                     <asp:Button ID="Button2" runat="server" Text="导出" CssClass="btnSearch"  OnClick="Export_Click"/>
                </div>
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
            DataSourceID="ods" AllowPaging="True" AllowSorting="True" CssClass="msgtable"
            CellPadding="0" CellSpacing="1" GridLines="None" PageSize="10" EnableModelValidation="True">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="序号" InsertVisible="False" ReadOnly="True"
                    SortExpression="ID">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" CssClass="key" />
                </asp:BoundField>
                <asp:BoundField DataField="Category" HeaderText="类别" SortExpression="Category">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Action" HeaderText="操作" SortExpression="Action">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="管理员" SortExpression="UserName">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IP" HeaderText="IP地址" SortExpression="IP">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OccurTime" HeaderText="时间" SortExpression="OccurTime"
                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="Remark" HeaderText="详细信息" SortExpression="Remark">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                没有符合条件的数据！
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
            SelectCountMethod="SearchCount" SelectMethod="Search" SortParameterName="orderClause"
            TypeName="">
            <SelectParameters>
                <asp:ControlParameter ControlID="key" Name="key" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ddlAdmin" Name="adminid" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="ddlCategory" Name="category" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="StartDate" Name="start" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="EndDate" Name="end" PropertyName="Text" Type="DateTime" />
                <asp:Parameter Name="orderClause" Type="String" />
                <asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="maximumRows" Type="Int32" DefaultValue="200000" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsCategory" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="FindAllCategory" TypeName=""></asp:ObjectDataSource>
        <XCL:GridViewExtender ID="gvExt" runat="server">
        </XCL:GridViewExtender>
       
        <div class="line10"></div>
</asp:content>

