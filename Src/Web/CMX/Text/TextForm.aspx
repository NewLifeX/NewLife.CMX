<%@ Page Title="文本管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="TextForm.aspx.cs" Inherits="CMX_TextForm" ValidateRequest="false" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>文本管理</title>
    <script type="text/javascript" charset="utf-8" src="../../UEditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../UEditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑文本</a></li>
            <li><a onclick="tabs('#contentTab',1);" href="javascript:void(0);">编辑文本内容</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px" />
                <col />
                <tbody>
                    <tr>
                        <th>分类：</th>
                        <td>
                            <asp:Label ID="frmCategoryName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>标题：</th>
                        <td>
                            <asp:TextBox ID="frmTitle" runat="server" Width="300px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>最新版本：</th>
                        <td>
                            <XCL:NumberBox ID="frmVersion" runat="server" Width="80px"></XCL:NumberBox></td>
                    </tr>
                    <tr>
                        <th>访问统计：</th>
                        <td>
                            <XCL:NumberBox ID="frmStatisticsID" runat="server" Width="80px"></XCL:NumberBox></td>
                    </tr>
                    <tr>
                        <th>创建人：</th>
                        <td>
                            <XCL:NumberBox ID="frmCreateUserID" runat="server" Width="80px"></XCL:NumberBox></td>
                    </tr>
                    <tr>
                        <th>创建人：</th>
                        <td>
                            <asp:TextBox ID="frmCreateUserName" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>创建时间：</th>
                        <td>
                            <XCL:DateTimePicker ID="frmCreateTime" runat="server"></XCL:DateTimePicker></td>
                    </tr>
                    <tr>
                        <th>更新人：</th>
                        <td>
                            <XCL:NumberBox ID="frmUpdateUserID" runat="server" Width="80px"></XCL:NumberBox></td>
                    </tr>
                    <tr>
                        <th>更新人：</th>
                        <td>
                            <asp:TextBox ID="frmUpdateUserName" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>更新时间：</th>
                        <td>
                            <XCL:DateTimePicker ID="frmUpdateTime" runat="server"></XCL:DateTimePicker></td>
                    </tr>
                    <tr>
                        <th>备注：</th>
                        <td>
                            <asp:TextBox ID="frmRemark" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <th></th>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                            &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新文本' />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="tab_con">
            <table class="form_table">
                <col width="180px">
                <col>
                <tbody>
                    <tr>
                        <th>内容</th>
                        <td>
                            <div>
                                <div>
                                    <script id="editor" type="text/plain" style="width: 1076px;" name="myContent"><%=ContentTxt %></script>
                                </div>
                                <script type="text/javascript">
                                    //实例化编辑器
                                    UE.getEditor('editor');
                                </script>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
