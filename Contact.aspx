<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Chata_IS.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>Kontakt</h1>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Telefon:</h3>
        </header>
        <p>
            <span class="label">Libor Mlýnek:</span>
            <span>425.555.0100</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Dotazy:</span>
            <a href="mailto:Marketing@example.com"><span class="auto-style1">Test@test.cz</span></a>
        </p>
    </section>

    <section class="contact">
        <header>
        </header>
    </section>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
    .auto-style1 {
        text-decoration: underline;
    }
</style>
</asp:Content>
