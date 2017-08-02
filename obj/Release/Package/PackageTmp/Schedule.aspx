<%@ Page Title="Kalendář rezervací" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="Chata_IS.About" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2><br />
                    <br />
                    </h2>
            </hgroup>

    <asp:updatepanel runat="server" UpdateMode="Always">
     <ContentTemplate>
                <div class="list-of-floating-elements">
                        <div class="floated">
                                <asp:Button ID="BackBtn" runat="server" OnClick="BackBtn_Click" Text="&lt;" />
                        </div>
                        <div class="floated">        
                                <asp:Label ID="MonthLabel" runat="server" Text="Měsíc" Font-Size="XX-Large"></asp:Label>
                        </div>
                        <div class="floated">
                                <asp:Button ID="NextBtn" runat="server" Text="&gt;" OnClick="NextBtn_Click" />
                        </div>
                </div>
                <div class="list-of-floating-elements">
                        <div class="floated">
                                
                        </div>
                        <div class="floated">        
                                <asp:Label ID="YearLabel" runat="server" Text="Rok" Font-Size="Large" Font-Bold="True" ForeColor="#669900" Height="30px"></asp:Label>
                        </div>
                        <div class="floated">
                                
                        </div>
                </div>

            <asp:Table ID="ScheduleTable" runat="server" BackColor="White" GridLines="Both" Height="460px" HorizontalAlign="Center" Width="90%" BorderWidth="1px">
                    <asp:TableRow BackColor="#e8e8e8" BorderStyle="Solid" BorderColor="#c0c0c0" Height="22">
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" BorderColor="#c0c0c0" BorderWidth="1px">Po</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" BorderColor="#c0c0c0" BorderWidth="1px">Út</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" BorderColor="#c0c0c0" BorderWidth="1px">St</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" BorderColor="#c0c0c0" BorderWidth="1px">Čt</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" BorderColor="#c0c0c0" BorderWidth="1px">Pá</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" BorderColor="#c0c0c0" BorderWidth="1px">So</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="Solid" BorderColor="#c0c0c0" BorderWidth="1px">Ne</asp:TableCell>
                    </asp:TableRow>
            </asp:Table>
            <br />

            <br />
          






         </ContentTemplate>
     </asp:updatepanel>
       
             <asp:Button ID="Button1" runat="server" Text="Press me" onclick="Button1_Click" />

        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Button1"
             BackgroundCssClass="Background">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">
            <iframe style=" width: 350px; height: 300px;" id="irm1" src="rezPae.aspx" runat="server"></iframe>
           <br/>

        </asp:Panel>


    <hgroup>

    </hgroup>

    </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .calendar {
            font-family: Verdana, Geneva, Tahoma, sans-serif;
            font-size: large;
            border: thin solid #999999;
            clip: rect(auto, auto, 0px, auto);
            margin: auto;
            padding: 3px;
            max-width: 100%;
            height: 100%;
            width: 90%; /* ie8 */
        }

    .list-of-floating-elements{
        list-style:none;
        text-align: center;
        margin-top:19px;
        margin-bottom:0px;
            height: 28px;
        }

    .floated{
        float: left;
         width: 33%; 
         height:38px; 
         vertical-align:middle; 
         display: inline-block;
                 margin-top:0px;
        margin-bottom:0px;
    }
    .none {
        background-image: url("Images/cell_none.png"); background-size:cover;
    }
    .free {
        background-image: url("Images/cell_free.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
    }
    .free:hover{
        background-image: url("Images/cell_free_h.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
            
    }
    .tak {
        background-image: url("Images/cell_tak.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
    }
    .tak:hover{
        background-image: url("Images/cell_tak_h.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
    }
    .rez {
        background-image: url("Images/cell_col.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
    }
    .rez:hover{
        background-image: url("Images/cell_col_h.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
    }
    .invisLink{
        cursor: pointer;
        display: inline-box;
        position:relative;
        height:inherit;
        width:inherit;
        box-sizing: border-box;
        z-index: 10;
        display: block;
        overflow:hidden;
        padding-left:1px;
    }


            .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }
        .lbl
        {
            font-size:16px;
            font-style:italic;
            font-weight:bold;
        }

    </style>
</asp:Content>
