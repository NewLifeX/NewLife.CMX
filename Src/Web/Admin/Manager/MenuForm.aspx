<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuForm.aspx.cs" Inherits="Admin_Manager_MenuForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统菜单管理</title>
    <link href="../../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../../scripts/ui/js/ligerBuild.min.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
        //表单验证
        $(function () {
            $("#form1").validate({
                errorPlacement: function (lable, element) {
                    element.ligerTip({ content: lable.html(), appendIdTo: lable });
                },
                success: function (lable) {
                    lable.ligerHideTip();
                }
            });
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="navigation"><a href="javascript:history.go(-1);" class="back">后退</a>首页 &gt; 系统管理 &gt; 菜单列表</div>
        <div id="contentTab">
            <ul class="tab_nav">
                <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑管理员信息</a></li>
            </ul>
            <div class="tab_con" style="display: block;">
                <table class="form_table">
                    <col width="180px" />
                    <col />
                    <tbody>
                        <tr>
                            <th>名称：</th>
                            <td>
                                <asp:TextBox ID="frmName" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100"></asp:TextBox><label>*</label></td>
                        </tr>
                        <tr>
                            <th>所属父菜单：</th>
                            <td>
                                <XCL:DropDownList ID="frmParentID" runat="server" DataTextField="TreeNodeName2" DataValueField="ID"   CssClass="select2"
                                    AppendDataBoundItems="True" DataSourceID="ods">
                                </XCL:DropDownList>
                                <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="FindAllChildsByParent">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="parentKey" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <th>链接：
                            </th>
                            <td>
                                <asp:TextBox ID="frmUrl" runat="server" CssClass="txtInput normal required" minlength="1" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <th>序号：</th>
                            <td>
                                <XCL:NumberBox ID="frmSort" runat="server" Width="80px"></XCL:NumberBox></td>
                        </tr>
                        <tr>
                            <th>权限：</th>
                            <td>
                                <asp:TextBox ID="frmPermission" runat="server" CssClass="txtInput normal required" minlength="2" MaxLength="100"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>显示：</th>
                            <td>
                                <asp:RadioButtonList ID="frmIsEnable" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="True">显示 </asp:ListItem>
                                    <asp:ListItem Value="False">隐藏 </asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <th>备注：</th>
                            <td>
                               <asp:TextBox ID="frmRemark" runat="server" CssClass="txtInput normal" minlength="2" MaxLength="100"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <th></th>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="提交保存" CssClass="btnSubmit" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

