<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" charset="utf-8">
        //window.UEDITOR_HOME_URL = "/web/ueditor/";
    </script>
    <script type="text/javascript" charset="utf-8" src="ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="ueditor/ueditor.all.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <script id="editor" type="text/plain" style="width: 1076px;">这里可以书写，编辑器的初始内容</script>
            </div>
            <script type="text/javascript">
                //实例化编辑器
                UE.getEditor('editor');
            </script>
        </div>
    </form>
</body>
</html>
