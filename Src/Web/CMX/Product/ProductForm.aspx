﻿<%@ Page Title="产品管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ProductForm.aspx.cs" Inherits="CMX_ProductForm" ValidateRequest="false" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>产品管理</title>
    <script type="text/javascript" charset="utf-8" src="../../UEditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../UEditor/ueditor.all.js"></script>
    <script src="../../Scripts/jQueryFormPatch/jQueryFormPatch.js"></script>
    <script src="../../Scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.imglabel').css('display', 'none');

            $('#upload').click(function () {

                if (!$('.fu').val()) {
                    alert('请选择图片！');
                    return;
                }

                $('form').ajaxSubmit({
                    url: "../../UpdateImageLoad.ashx",
                    type: 'post',
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("error:" + errorThrown);
                    },
                    target: ".fu",
                    success: function (responseText, statusText, xhr, $form) {

                        var jsonarray = $.parseJSON(responseText);
                        var targetvalue = '';
                        var length = $('.fu').val().lastIndexOf('\\');

                        if (length > 0) {
                            targetvalue = $('.fu').val().substr(length + 1);
                        }
                        else {
                            targetvalue = $('.fu').val();
                        }

                        $.each(jsonarray, function (i, n) {
                            if (n.key == targetvalue) {
                                $('.img').attr('src', n.value);
                                $('.imglabel').val(n.value);
                                $('.fu').text('');
                            }
                        });
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑产品</a></li>
            <li><a onclick="tabs('#contentTab',1);" href="javascript:void(0);">编辑产品内容</a></li>
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
                            <asp:TextBox ID="frmTitle" runat="server" Width="300px"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <th>价格：</th>
                        <td>
                            <XCL:DecimalBox ID="frmPrice" runat="server" Width="80px"></XCL:DecimalBox></td>
                    </tr>
                    <tr>
                        <th>上传图片：</th>
                        <td>
                            <asp:Image ID="frmPhotoPathimg" runat="server" CssClass="img" Width="150px" ImageUrl="#" />
                            <asp:TextBox ID="frmPhotoPath" runat="server" CssClass="imglabel"></asp:TextBox>
                            <asp:FileUpload ID="fu" runat="server" CssClass="fu" />
                            <input type="button" value="上传" id="upload" /></td>
                    </tr>
                    <tr>
                        <th>访问统计：</th>
                        <td>
                            <XCL:NumberBox ID="frmStatisticsID" runat="server" Width="80px"></XCL:NumberBox></td>
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
                            <asp:TextBox ID="frmUpdateUserName" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>更新时间：</th>
                        <td>
                            <XCL:DateTimePicker ID="frmUpdateTime" runat="server"></XCL:DateTimePicker></td>
                    </tr>
                    <tr>
                        <th>最新版本：</th>
                        <td>
                            <XCL:NumberBox ID="frmVersion" runat="server" Width="80px"></XCL:NumberBox></td>
                    </tr>
                    <tr>
                        <th>备注：</th>
                        <td>
                            <asp:TextBox ID="frmRemark" runat="server" Width="300px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                            &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新产品' />
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

