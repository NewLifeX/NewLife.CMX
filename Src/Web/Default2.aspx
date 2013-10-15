<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jQueryFormPatch/jQueryFormPatch.js"></script>
    <script src="Scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btnajax').click(function () {

                $('#form1').ajaxSubmit({
                    url: "UpdateImageLoad.ashx?CustomImagePath=",
                    type: 'post',
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("error:" + errorThrown);
                    },
                    target: ".fb",
                    success: function (responseText, statusText, xhr, $form) {

                        var jsonarray = $.parseJSON(responseText);
                        var targetvalue = '';
                        var length = $('.fb').val().lastIndexOf('\\');

                        if (length > 0) {
                            targetvalue = $('.fb').val().substr(length + 1);
                        }
                        else {
                            targetvalue = $('.fb').val();
                        }

                        $.each(jsonarray, function (i, n) {
                            if (n.key == targetvalue) {
                                $('.img').attr('src', n.value);
                            }
                        });
                    }
                });
            });
        });
    </script>
</head>
<body>

    <form runat="server" id="form1" class="ffo">
        <asp:TextBox ID="tb" runat="server"></asp:TextBox>
        <asp:FileUpload ID="fb" runat="server" CssClass="fb" />
        <asp:Image ID="img" runat="server" CssClass="img"  Width="150px" ImageUrl="#" />
        <%--<asp:Button ID="bt" runat="server" Text="点击" />--%>
        <input type="button" value="测试" id="btnajax" />
    </form>
</body>
</html>
