<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managePopup.aspx.cs" Inherits="Chata_IS.manPopup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript">


            function pageLoad() {
                var mpe = window.parent.$find("MPE2");
                mpe.add_shown(onShown);
                $addHandler(document, "keydown", onKeyDown);

            }
            function onShown() {
                var background = window.parent.$find("MPE2")._backgroundElement;
                background.onclick = function () { window.parent.$find("MPE2").hide(); }
            }

            function onKeyDown(e) {
                if (e && e.keyCode == Sys.UI.Key.esc) {
                    window.parent.$find("MPE2").hide();
                }
            }

            function manualClose() {
                window.parent.$find("MPE2").hide();
            }

            function refreshThis()
            {
                $find("mpex").show();
                setTimeout(function () { window.top.location.href = window.top.location.href }
                , 300);
            }

    </script>

    <style type="text/css">
        .auto-style29 {
            width: 456px;
            height: 412px;
            margin-right: 31px;
        }
        regStyle{
            align-self:center;
            justify-content:center;
        }
        longStyle{
            width:95%;
        }
        .auto-style46 {
            text-align: right;
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
        closebtn {
            position: absolute;
            left: 0px;
            top: 25px;
            z-index: 5;
            padding-top:65px;
        }
        .auto-style47 {
            height: 400px;
            width: 460px;
            margin-left: 7px;
            }
        .auto-style48 {
            width: 472px;
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
        .auto-style50 {
            width: 415px;
            margin-left: 12px;
        }
        .auto-style51 {
            width: 363px;
            text-align: right;
        }
        .auto-style52 {
            width: 10px;
        }
        .auto-style55 {
            margin-left: 21px;
        }
        .auto-style56 {
            text-align: center;
        }
        .auto-style57 {
            cursor: pointer;
            display: inline-box;
            position: relative;
            height: 1px;
            width: inherit;
            box-sizing: border-box;
            z-index: 10;
            display: block;
            overflow: hidden;
            padding-left: 1px;
            visibility: hidden;
            left: 0px;
            top: 0px;
        }
        .auto-style58 {
            margin-left: 44px;
        }
        </style>
</head>
<body>
    
                      <form id="form2" runat="server" class="auto-style29">
                          <asp:updatepanel  ID="upPan" runat="server" UpdateMode="Always">
     <ContentTemplate>
                              <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
                          <div class="auto-style46">
                              <asp:Button ID="Button1" runat="server" CssClass="closebtn" OnClick="Button1_Click" Text="X" Height="19px" OnClientClick="manualClose();" />
                          </div>
                          <div class="auto-style47">
                              <div class="auto-style56">
                                  <h2>
                              <asp:Label ID="curDate" runat="server" Text="Label"></asp:Label>
                                  </h2>
                                  </div>
                              Informace o rezervaci:<br />
                              <table class="auto-style50">
                                  <tr>
                                      <td class="auto-style51">Jméno:</td>
                                      <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="jmenolab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                                       <tr>
                                      <td class="auto-style51">Příjmení:</td>
                                           <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="prijmenilab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                                      <tr>
                                      <td class="auto-style51">Tel.číslo:</td>
                                          <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="tellab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                                    <tr>
                                      <td class="auto-style51">Email:</td>
                                        <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="emaillab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                    
                                                                      <tr>
                                      <td class="auto-style51">ID rezervace (var.symbol):</td>
                                                                          <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="idlab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                                                                           </tr>
                    
                                                                      <tr>
                                      <td class="auto-style51">Stav rezervace:</td>
                                                                          <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="stavlab" runat="server" Text="VOLNO"></asp:Label>
                                      </td>
                                  </tr>                 
                                                                                                        <tr>
                                      <td class="auto-style51">Datum vytvoreni rezervace:</td>
                                                                          <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="datvytvorlab" runat="server" Text="-"></asp:Label>
                                      </td>
                                                                                                                                                                                  <tr>
                                      <td class="auto-style51">Zarezervovane dny:</td>
                                                                          <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="zarezerlab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>       
                                                                                                            <tr>
                                      <td class="auto-style51">Poznamka:</td>
                                                                          <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="poznlab" runat="server" Text="-"></asp:Label>
                                      </td>

                                  </tr>   
                                                                                                            <tr>
                                      <td class="auto-style51">IP adresa:</td>
                                                                          <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="IPlab" runat="server" Text="-"></asp:Label>
                                      </td>

                                  </tr>   
                                  </tr>        
                              </table>
                              <br />
                              <asp:Button ID="but_zaplaceno" runat="server" ToolTip="Pokud je v tento den nějaká objednávka, tak ji nastaví na stav ZAPLACENO." CssClass="auto-style55" Text="Nastavit stav ZAPLACENO" Width="180px" OnClick="but_zaplaceno_Click" OnClientClick="refreshThis();" />
                              <asp:Button ID="but_nezaplaceno" runat="server" CssClass="auto-style55" ToolTip="Pokud je v tento den nějaká objednávka a již má stav ZAPLACENO, tak ji lze vrátit na stav NEZAPLACENO" Text="Nastavit stav NEZAPLACENO" Width="191px" OnClientClick="refreshThis();" OnClick="but_nezaplaceno_Click" />
                              <br />
                              <br />
                              <asp:Button ID="but_zamknout" runat="server" CssClass="auto-style55" Text="Zamknout den" ToolTip="Zamkne den. (Vytvoří prázdnou, zaplacenou objednávku na tento den. Pokud již v tento den existuje nějaká rezervace tak ji ZRUŠÍ)." Width="186px" OnClick="but_zamknout_Click" OnClientClick="refreshThis();" />
                              <asp:Button ID="but_odemknout" runat="server" CssClass="auto-style55" Text="Odemknout den" ToolTip="Odemkne den (zruší prázdnou objednávku)" Width="165px" OnClick="but_odemknout_Click" OnClientClick="refreshThis();" />
                              <br />
                              <br />
                              <asp:Button ID="but_zruseni" runat="server" CssClass="auto-style55" Text="ZRUŠIT REZERVACI" ToolTip="Smaže rezervaci v tento den" Width="165px" OnClick="but_zruseni_Click" OnClientClick="refreshThis();" />
                              <asp:Button ID="but_ipban" runat="server" CssClass="auto-style58"  OnClientClick="refreshThis();" Text="ZAKAZAT IP" ToolTip="Zakáže dané IP adrese vytváření nových rezervací." Width="165px" OnClick="but_ipban_Click" />
                              <asp:Button ID="opBut" runat="server" CssClass="auto-style57" OnClick="opBut_Click" />

    <cc1:ModalPopupExtender ID="mp" runat="server" BehaviorID="mpex" PopupControlID="Panel1" TargetControlID="opBut"
             BackgroundCssClass="Background" CancelControlID="clBut">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" style = "display:none">
            <div>Zpracovávám...</div>
           <br/>

        </asp:Panel>
                          </div>
                          </ContentTemplate>
     </asp:updatepanel> 
                      </form>
         
     

</body>
</html>