<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectAlbum.aspx.cs" Inherits="MusicStore.SelectAlbum" %>

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
                ID="ddlArtist"
                runat="server"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddl_OnSelectedIndexChanged">
            </asp:DropDownList>
            <br />
            <asp:RadioButtonList ID="rbGenre"  RepeatDirection="Horizontal" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="rbChanged"></asp:RadioButtonList>
           
            <asp:GridView
                ID="gvAlbums"
                runat="server"
                OnSelectedIndexChanged="gvAlbums_OnSelectedIndexChanged"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:ButtonField CommandName="Select" Text="Add" />
                    <asp:BoundField DataField="AlbumID" HeaderText="Album Code" SortExpression="" />
                     <asp:BoundField DataField="GenreID" HeaderText="Genre Code" SortExpression="" />
                    <asp:BoundField DataField="ArtistID" HeaderText="ArtistID" SortExpression="" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </form>
</body>
</html>
