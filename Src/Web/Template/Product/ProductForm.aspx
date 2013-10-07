<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductForm.aspx.cs" Inherits="Template_Product_ProductForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <div>
            <h2><%=Product.Title %></h2>
            <div>
                <%=Product.Content %>
            </div>
        </div>
    </form>
</body>
</html>
