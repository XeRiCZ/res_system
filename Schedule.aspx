<%@ Page Title="Kalendář rezervací" EnableEventValidation="false"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="Chata_IS.About" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
     
            </hgroup>
        <hr>
    <asp:updatepanel  ID="upPan" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
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
          


                      
               
         <asp:Button ID="refButt" BehaviorID="refButt" runat="server" CssClass="invisLink" />

                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="SendMail" />

                <asp:Button ID="hidd" runat="server" CssClass="invisLink" />
         <asp:Button ID="hidd2" runat="server" CssClass="invisLink" />
    <asp:Button ID="hiddClose" runat="server" CssClass="invisLink" />
         <asp:Button ID="hiddClose2" runat="server" CssClass="invisLink" />
            <cc1:ModalPopupExtender ID="mp1" runat="server" BehaviorID="MPE" PopupControlID="Panl1" TargetControlID="hidd"
             BackgroundCssClass="Background" CancelControlID="hiddClose">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">
            <iframe style=" width: 480px; height: 480px;" id="irm1" src="rezervePopup.aspx" runat="server"></iframe>
           <br/>

        </asp:Panel>
    <asp:Button ID="Button1" runat="server" CssClass="invisLink" />
            <cc1:ModalPopupExtender ID="mp2" runat="server" BehaviorID="MPE2" PopupControlID="Panel1" TargetControlID="hidd2"
             BackgroundCssClass="Background" CancelControlID="hiddClose2">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" style = "display:none">
            <iframe style=" width: 480px; height: 460px;" id="Iframe1" src="managePopup.aspx" runat="server"></iframe>
           <br/>

        </asp:Panel>

         </ContentTemplate>
        <Triggers>

        </Triggers>
     </asp:updatepanel>



    <hgroup>

    </hgroup>

    </asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">


            function pageLoad() {
                var mpe = $find("MPE");
                mpe.add_shown(onShown);
                $addHandler(document, "keydown", onKeyDown);

            }
            function onShown() {
                var background = $find("MPE")._backgroundElement;
                background.onclick = function () {
                    $find("MPE").hide();
                    $find("MPE2").hide();
                }
            }

            function onKeyDown(e) {
                if (e && e.keyCode == Sys.UI.Key.esc) {
                    $find("MPE").hide();
                    $find("MPE2").hide();
                }
            }





    </script>

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
        min-width: 100px;
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
            min-width: 100px;
    }
    .old {
        background-image: url("Images/cel_old.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
            min-width: 100px;
            color:white;
    }
         .old:hover {
        background-image: url("Images/cel_old_h.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
            min-width: 100px;
            color:white;
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
            min-width: 100px;
            
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
            min-width: 100px;
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
            min-width: 100px;
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
            min-width: 100px;
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
            min-width: 100px;
    }
        .bor {
        background-image: url("Images/cel_bor.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
            min-width: 100px;
    }
    .bor:hover{
        background-image: url("Images/cel_bor_h.png"); background-size:cover;
         -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Safari */
     -khtml-user-select: none; /* Konqueror HTML */
       -moz-user-select: none; /* Firefox */
        -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
            min-width: 100px;
    }

    bunkaLabel{
        font-size:medium;
        font-family:Verdana;
        text-shadow:inherit;
        position:relative;
        text-align:left;
        vertical-align:text-top;
        margin-top:30px;
    }

    cisloLabel{
        font-size:x-large;
        font-family:Verdana;
        text-shadow:inherit;
        position:relative;
        text-align:right;
        vertical-align:text-top;
        
        margin-top:80%;
    }
    cisloLabelWhite{
        font-size:x-large;
        font-family:Verdana;
        text-shadow:inherit;
        position:relative;
        text-align:right;
        vertical-align:text-top;
        color: white;
        margin-top:80%;
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
        visibility:hidden;
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

        }
        .lbl
        {
            font-size:16px;
            font-style:italic;
            font-weight:bold;
        }

    </style>
</asp:Content>
