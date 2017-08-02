<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Chata_IS._Default" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <br />
    
    <div style="margin-left:8px; margin-right:8px;">
        <asp:PlaceHolder ID="AktualityDiv" runat="server" >


        </asp:PlaceHolder>
    </div>
   
</asp:Content>

<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
 
    <style type="text/css">

        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-left: 10px;
            width:100%;
        }
 

    </style>
</asp:Content>
