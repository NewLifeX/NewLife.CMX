<%@ Page Title="文章管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ArticleForm.aspx.cs" Inherits="CMX_ArticleForm" ValidateRequest="false" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>文章管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" charset="utf-8">
        //window.UEDITOR_HOME_URL = "/web/ueditor/";
    </script>
    <%--<script type="text/javascript" charset="utf-8" src="ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="ueditor/ueditor.all.js"></script>--%>
    <script type="text/javascript" charset="utf-8" src="../../UEditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../UEditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑文章</a></li>
            <li><a onclick="tabs('#contentTab',1);" href="javascript:void(0);">编辑文章内容</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px" />
                <col />
                <tbody>
                    <tr>
                        <th>分类：</th>
                        <td>
                            <asp:Label runat="server" ID="frmCategoryName"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>标题：</th>
                        <td>
                            <asp:TextBox ID="frmTitle" runat="server" Width="300px" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>最新版本：</th>
                        <td>
                            <XCL:NumberBox ID="frmVersion" runat="server" Width="80px"></XCL:NumberBox></td>
                    </tr>
                    <tr>
                        <th>访问量：</th>
                        <td>
                            <XCL:NumberBox ID="frmHits" runat="server" Width="80px"></XCL:NumberBox></td>
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
                            <asp:TextBox ID="frmCreateUserName" runat="server" Width="150px" CssClass="form-control"></asp:TextBox></td>
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
                            <asp:TextBox ID="frmUpdateUserName" runat="server" Width="150px" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>更新时间：</th>
                        <td>
                            <XCL:DateTimePicker ID="frmUpdateTime" runat="server"></XCL:DateTimePicker></td>
                    </tr>
                    <tr>
                        <th>备注：</th>
                        <td>
                            <asp:TextBox ID="frmRemark" runat="server" Width="292px" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
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
                                    <script id="editor" type="text/plain" style="width: 1076px;" name="myContent"><%=Entity.ContentText%></script>
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
        <div class="foot_btn_box">
            <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' CssClass="btnSubmit" />
            &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新文章' CssClass="btnSubmit" />
        </div>
    </div>
</asp:Content>
