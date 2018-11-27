<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Houses.aspx.cs" Inherits="Weeks_9_11.Houses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Select a category"></asp:Label>
            <asp:DropDownList
                ID="ddlArea"
                runat="server"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlArea_OnSelectedIndexChanged">
            </asp:DropDownList>

            <br />
            <br />
            <br />
            <asp:GridView
                ID="gvHouses"
                runat="server"
                OnSelectedIndexChanged="gvHouses_OnSelectedIndexChanged"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:ButtonField CommandName="Select" Text="Select" />
                    <asp:BoundField DataField="HouseID" HeaderText="House code" SortExpression="" />
                    <asp:BoundField DataField="hadd1" HeaderText="Address" SortExpression="" />
                    <asp:BoundField DataField="hadd2" HeaderText="Town" SortExpression="" />
                    <asp:BoundField DataField="hadd3" HeaderText="County" SortExpression="" />
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
