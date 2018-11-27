<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewit.aspx.cs" Inherits="Weeks_9_11.viewit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div>
        <asp:Label ID="Label1" runat="server" Font-Size="50" Text="This is the viewit page"></asp:Label>
        <br /><br /><br />
        <asp:Label ID="lbl2" runat="server" Font-Size="30" Text="HouseID retrieved from the session variable:  "></asp:Label>
        <asp:Label ID="lblHcode" runat="server" Font-Size="30" Text="This is the viewit  page"></asp:Label>
    </div>
    </form>
</body>
</html>
