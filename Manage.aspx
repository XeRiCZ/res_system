<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Chata_IS.Account.Manage" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    
    <br />
    Vítejte v sekci pro členy svazu. Zde si můžete prohlédnout výpisy brigád a zapsat se na některou<br />
    za pomoci komentáře.<br />
    <br />
    <div class="auto-style1">

    <asp:Button ID="addNews" runat="server" Text="Přidat novinku" OnClick="AddNewsClick"  />
    
         <asp:Button ID="Button1" runat="server" Text="Přidat nový výpis brigády" OnClick="AddBrigClick"  />
    
        <br />

    </div>
    
    <div style="background-color:#fbe27a">
        <h1>&nbsp;</h1>
        <h1>Výpisy brigád</h1>
        <p>&nbsp;</p>
        <asp:PlaceHolder ID="mainDiv" runat="server" >
            

        </asp:PlaceHolder>
    </div>

</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</asp:Content>

