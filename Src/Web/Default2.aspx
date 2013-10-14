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
                    url: "updateload.ashx?updatePath=''",
                    type: 'post',
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("error:" + errorThrown);
                    },
                    target: ".fb",
                    success: function (responseText, statusText, xhr, $form) {
                        var i = responseText;
                        alert(responseText);
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
        <%--<asp:Button ID="bt" runat="server" Text="点击" />--%>
        <input type="button" value="测试" id="btnajax" />
    </form>
</body>
</html>
