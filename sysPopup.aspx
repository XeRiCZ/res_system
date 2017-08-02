<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sysPopup.aspx.cs" Inherits="Chata_IS.sysPopup" %>
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
                setTimeout(function () { window.top.location.reload(); }
                , 1200);
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
        .auto-style56 {
            text-align: center;
        }
        .auto-style59 {
            width: 269px;
            text-align: right;
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
                              <asp:Label ID="curDate" runat="server" Text="Systémové proměnné"></asp:Label>
                                  </h2>
                                  </div>
                              Informace o rezervaci:<br />
                              <table class="auto-style50">
                                  <tr>
                                      <td class="auto-style59">Jméno:</td>
                                      <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="jmenolab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                                       <tr>
                                      <td class="auto-style59">Příjmení:</td>
                                           <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="prijmenilab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                                      <tr>
                                      <td class="auto-style59">Tel.číslo:</td>
                                          <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="tellab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                                    <tr>
                                      <td class="auto-style59">Email:</td>
                                        <td class="auto-style52"></td>
                                      <td class="auto-style48">
                                          <asp:Label ID="emaillab" runat="server" Text="-"></asp:Label>
                                      </td>
                                  </tr>
                     
                                      
                              </table>
                           
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