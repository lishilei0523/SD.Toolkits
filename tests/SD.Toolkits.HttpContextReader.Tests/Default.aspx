<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SD.Toolkits.HttpContextReader.Tests.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" Text="正常异步读取" OnClick="NormallyRead" />
        <asp:Button runat="server" Text="封装异步读取" OnClick="CorrectlyRead" />
        <hr />
        <asp:TextBox ID="Txt_Result" runat="server"></asp:TextBox>
    </form>
</body>
</html>
