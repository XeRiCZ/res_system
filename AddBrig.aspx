<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBrig.aspx.cs" Inherits="Chata_IS.Account.AddBrig" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Přídání nového výpisu brigády</h1>
    </hgroup>

    <br />


	<CKEditor:CKEditorControl ID="CKEditor1" runat="server" Height="200">
	&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. You are using &lt;a href="http://ckeditor.com/"&gt;CKEditor&lt;/a&gt;.&lt;/p&gt;
	</CKEditor:CKEditorControl>
	<br />
	
	<div id="footer">
		<hr />
	</div>
	


    <div>

 
    <asp:Button ID="Button1" runat="server" CssClass="auto-style2" Text="Uložit výpis" OnClick="SaveNews" />
        </div>
<br />
    <br />
    <br />

  
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style2 {
            margin-left: 216px;
        }
    </style>
    </asp:Content>

