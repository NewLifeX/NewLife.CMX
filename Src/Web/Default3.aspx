<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="style/bootstrap.css" rel="stylesheet" />
    <%--<link href="style/Lessbootcss.css" rel="stylesheet" />--%>
    <style type="text/css">
        .tf:after
        {
            content: "123";
        }
        .tf:before
        {
            content:"321";
        }
    </style>
</head>
<body>
    <%--  <form id="form1" runat="server">
    <div>
    
    </div>
    </form>--%>
    <form role="form">
        <%--<div class="form-group">--%>
        <span>hello</span>
        <input type="text" class="form-control" />
        <%--</div>--%>
        <%--<div class="form-group">--%>
        <span>word</span>
        <input type="text" class="form-control" />
        <%-- </div>--%>
    </form>

    <div class="tf">
        hello
    </div>
</body>
</html>
