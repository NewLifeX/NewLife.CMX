<%@ Page Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="<#=Table.Name#>Form.aspx.cs" Inherits="<#=Config.EntityConnName+"_"+Table.Name#>Form"%>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title><#=Table.DisplayName#>管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑<#=Table.DisplayName#></a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px" />
                <col />
                <tbody>
        <# 
        foreach(IDataColumn Field in Table.Columns) { 
            String pname = Field.Name;
            if(Field.PrimaryKey) continue;
            String frmName = "frm" + pname;
            TypeCode code = Type.GetTypeCode(Field.DataType);
        #><tr>
            <th><#=Field.DisplayName#>：</th>
            <td><#
                if(code == TypeCode.String){
                    if(pname.Equals("Password", StringComparison.OrdinalIgnoreCase) || pname.Equals("Pass", StringComparison.OrdinalIgnoreCase)){
                #><asp:TextBox ID="<#=frmName#>" runat="server" TextMode="Password"></asp:TextBox><#
                    }else if(Field.Length>300 || Field.Length<0){
                #><asp:TextBox ID="<#=frmName#>" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox><#
                    }else{
                #><asp:TextBox ID="<#=frmName#>" runat="server" Width="<#=Field.Length+100#>px"></asp:TextBox><#
                    }
                }else if(code == TypeCode.Int32){
                #><XCL:NumberBox ID="<#=frmName#>" runat="server" Width="80px"></XCL:NumberBox><#
                }else if(code == TypeCode.Double){
                #><XCL:RealBox ID="<#=frmName#>" runat="server" Width="80px"></XCL:RealBox><#
                }else if(code == TypeCode.DateTime){
                #><XCL:DateTimePicker ID="<#=frmName#>" runat="server"></XCL:DateTimePicker><#
                }else if(code == TypeCode.Decimal){
                #><XCL:DecimalBox ID="<#=frmName#>" runat="server" Width="80px"></XCL:DecimalBox><#
                }else if(code == TypeCode.Boolean){
                #><asp:CheckBox ID="<#=frmName#>" runat="server" Text="<#=Field.DisplayName#>" /><#}
            #></td>
        </tr>
<#}#>    
        <tr>
            <th></th>
            <td>
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新<#=Table.DisplayName#>' />
            </td>
        </tr>
        </tbody>
    </table>
            </div>
    </div>
</asp:Content>