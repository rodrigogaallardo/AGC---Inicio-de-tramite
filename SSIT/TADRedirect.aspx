<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TADRedirect.aspx.cs" Inherits="SSIT.TADRedirect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" action="" method="post" target="">
        <asp:HiddenField ID="sign" runat="server" Value="" />
        <asp:HiddenField ID="token" runat="server" Value="" />
        <%--<input type="submit" name="post" />--%>
    </form>
</body>
</html>
