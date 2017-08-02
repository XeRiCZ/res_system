<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="Chata_IS.Account.Login" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .auto-style1 {
        text-align: left;
        width: 529px;
    }
    .auto-style2 {
        margin-left: 0px;
    }
</style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    

    <br />
    <h1>Přihlášení do sekce pro členy svazu</h1>
<br />
        <hr>
                              <div style="width:100% auto">
                    <div style ="width: 90%; height:220px; margin: 0 auto;">

        
                        <div style ="margin: 0 auto;" class="auto-style1">
    <asp:Login ID="Login1" runat="server" onauthenticate="OnAuthenticate" TitleText="" CssClass="auto-style2" Width="555px" DisplayRememberMe="false" >
        <CheckBoxStyle ForeColor="Black" />
        <InstructionTextStyle ForeColor="Black" />
        <LabelStyle ForeColor="Black" />
        
    </asp:Login>
                            </div>
    </div>
                                  </div>
</asp:Content>
