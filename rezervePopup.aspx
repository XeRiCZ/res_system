<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rezervePopup.aspx.cs" Inherits="Chata_IS.RezerPopup" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript">
            

            function captchaTest(sender, args) {
                var correctCaptcha = function (response) {
                    alert(response);
                };

                if (!correctCaptcha) {

                    args.IsValid = false; //this will display your validation error msg and stops at clent side itself
                }
                else {
                    args.IsValid = true;// this will allow server side run
                }
            }

            function pageLoad() {
                var mpe = window.parent.$find("MPE");
                mpe.add_shown(onShown);
                $addHandler(document, "keydown", onKeyDown);

                ScriptManager.RegisterStartupScript(this, GetType(), "js" + UserName.ClientID,
  "ValidatorHookupEvent(document.getElementById(\"" + Jmeno.ClientID +
  "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "js" + Password.ClientID,
  "ValidatorHookupEvent(document.getElementById(\"" + Prijmeni.ClientID +
  "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "js" + Password.ClientID,
 "ValidatorHookupEvent(document.getElementById(\"" + Email.ClientID +
 "\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "js" + Password.ClientID,
"ValidatorHookupEvent(document.getElementById(\"" + Telefon.ClientID +
"\"), \"onblur\", \"ValidatorOnChange(event);\");", true);
            }
            function onShown() {
                var background = window.parent.$find("MPE")._backgroundElement;
                background.onclick = function () { window.parent.$find("MPE").hide(); }
            }

            function onKeyDown(e) {
                if (e && e.keyCode == Sys.UI.Key.esc) {
                    window.parent.$find("MPE").hide();
                }
            }

            function manualClose()
            {
                window.parent.$find("MPE").hide();
                setTimeout(function () { window.top.location.href = window.top.location.href }
, 50);
                setTimeout(function () { window.parent.$find("MPE").hide(); }
, 100);
            }
            function refClose()
            {
                setTimeout(function () { window.top.location.href = window.top.location.href }
, 50);
                setTimeout(function () { window.parent.$find("MPE").hide(); }
, 100);
            }


    </script>
    <script type="text/javascript" 

    src='https://www.google.com/recaptcha/api.js'></script>

    <style type="text/css">
        .auto-style4 {
            width: 445px;
            height: 252px;
        }
        .auto-style14 {
            width: 125px;
            text-align: left;
        }
        .auto-style22 {
            font-size: medium;
        }
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
        .auto-style30 {
            text-align: center;
        }
        .auto-style35 {
            font-size: x-large;
        }
              .auto-style36 {
            font-weight: bold;
            color: white;
            height: 60px;
            width: 452px;
        }
        .auto-style38 {
            height: 301px;
            width: 452px;
            text-align: center;
        }
        .auto-style40 {
            height: 50px;
        }
        .auto-style45 {
            width: 452px;
        }
        .auto-style46 {
            text-align: right;
        }
        closebtn {
            position: absolute;
            left: 0px;
            top: 25px;
            z-index: 5;
            padding-top:65px;
        }
        .auto-style51 {
            width: 125px;
            height: 24px;
        }
        .auto-style53 {
            width: 283px;
            height: 24px;
        }
        .auto-style55 {
            font-family: sans-serif;
            font-size: 14px;
            color: #222222;
            letter-spacing: normal;
            background-color: #FFFFFF;
        }
        .auto-style58 {
            height: 24px;
        }
        .auto-style65 {
            width: 283px;
        }
        .auto-style66 {
            width: 283px;
            height: 33px;
        }
        .auto-style68 {
            height: 33px;
        }
        .auto-style69 {
            width: 125px;
        }
        .auto-style70 {
            width: 125px;
            height: 33px;
        }
        .invis
        {
               background: transparent;
    border: none !important;
    font-size:0;
    width:1px;
    height:1px;
        }
        </style>
</head>
<body>

                      <form id="form1" runat="server" class="auto-style29">

                          <div class="auto-style46">
                             <asp:Button ID="Button1" runat="server" CssClass="closebtn" OnClick="Button1_Click" Text="X" Height="19px" OnClientClick="manualClose();" />
                      <asp:CreateUserWizard ID="CreateUserWizard1" OnContinueButtonClick="testCaptcha" OnInit="init" OnFinishButtonClick="Finish" OnCreatedUser="registrace" DisableCreatedUser="True"  runat="server" OnCreatingUser="registrace"  FinishPreviousButtonText="Zpět" ContinueButtonText="Zavřít" CancelButtonText="Ukončit" CreateUserButtonText="Pokračovat" StartNextButtonText="Pokračovat" FinishCompleteButtonText="Zaregistrovat" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" Height="366px" CssClass="regStyle" ActiveStepIndex="2">
                            <ContinueButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
                            <CreateUserButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
                            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#FFFFFF" />
                            <WizardSteps>
<asp:CreateUserWizardStep runat="server">
    <ContentTemplate>
        <table style="font-family:Verdana;font-size:100%;height:300px;">
            <tr>
                <td align="center" colspan="2" style="color:White;background-color:#5D7B9D;font-weight:bold;" class="auto-style40">Sign Up for Your New Account</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Visible="false">User Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="UserName" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Visible="false">Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Password" runat="server" Text="abcdefght1213" TextMode="Password" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="ConfirmPasswordLabel" Visible="false" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ConfirmPassword" Visible="false" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="EmailLabel" runat="server" Visible="false" AssociatedControlID="Email">E-mail:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Email" Visible="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="QuestionLabel" runat="server" Visible="false" AssociatedControlID="Question">Security Question:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Question"  Visible="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="AnswerLabel" Visible="false" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Answer" Visible="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="color:Red;">
                    <asp:Literal ID="ErrorMessage" runat="server" Visible="false" EnableViewState="False"></asp:Literal>
                    
                </td>
            </tr>
        </table>
    </ContentTemplate>
                                </asp:CreateUserWizardStep>
                                <asp:WizardStep runat="server" Title="Uvod" >
                                    <div style="height:400px; width:460px;" class="auto-style30">

                                        <br />
                                        <span class="auto-style35">Na den
                                        <asp:Label ID="curDateLabel" runat="server" Text="Label"></asp:Label>
                                        &nbsp;je možné<br />chatu zarezervovat.<br />
                                        <br />
                                        <i style="color: rgb(34, 34, 34); font-family: sans-serif; font-size: 14px; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">At vero eos et accusamus et iusto odio dignissimos ducimus, qui blanditiis praesentium voluptatum deleniti atque corrupti, quos dolores<br />&nbsp;et quas molestias<span>&nbsp;</span><b>exceptur</b>i<span>&nbsp;</span><b>sint, obcaecat</b>i<span>&nbsp;</span><b>cupiditat</b>e<span>&nbsp;</span><b>non
                                        <br />
                                        pro</b>v<b>ident</b>, similique<span>&nbsp;</span><b>sunt in culpa</b>,<span>&nbsp;</span><b>qui officia deserunt mollit</b>ia<span>&nbsp;</span><b>anim</b>i,<span>&nbsp;</span><b>id est laborum</b><span>&nbsp;</span>et dolorum fuga. Et harum quidem<br />&nbsp;rerum facilis est et expedita distinctio. Nam libero tempore,
                                        <br />
                                        cum soluta nobis est eligendi optio, cumque nihil impedit</i><span style="color: rgb(34, 34, 34); font-family: sans-serif; font-size: 14px; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;">.</span></span><span style="color: rgb(34, 34, 34); font-family: sans-serif; font-size: 14px; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial;"></span>
                                    </div>
                                </asp:WizardStep>
                                <asp:WizardStep runat="server" Title="Rezervace" OnActivate="refresh" >

                                                                            <div style="height:400px; width:460px;">

                                    
                                                                                <div class="auto-style30">
                                                                                    <strong style="text-align: right"><span class="auto-style22">Rezervace na den </span>
                                                                                    <asp:Label ID="curDate2" runat="server" CssClass="auto-style22" Text="Label"></asp:Label>
                                                                                    </strong>
                                                                                    <br />
                                                                                    (od 11:00 dopoledne)</div>
                                                                                <table class="auto-style4">
                                            <tr>
                                                <td  class="auto-style14"></td>
                                            </tr>
                                           
                                            
                                            <tr>
                                                <td align="right" class="auto-style69">
                                                    <asp:Label ID="jmenoLabel" runat="server" AssociatedControlID="Jmeno" CssClass="auto-style15">Jméno:</asp:Label>
                                                </td>
                                                <td></td>
                                                <td class="auto-style65">
                                                    <asp:TextBox ID="Jmeno" runat="server" ValidationGroup="UserNameSlot" style="margin-left: 0px; " CssClass="longStyle" Height="20px" Width="220px"></asp:TextBox>
                                                    </td><td>
                                                    <asp:RequiredFieldValidator ID="jmenoRequired" runat="server" ControlToValidate="Jmeno" ErrorMessage="Jméno je nutné vyplnit." ToolTip="Jméno je nutné vyplnit." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="auto-style69">
                                                    <asp:Label ID="prijmeniLabel" runat="server" AssociatedControlID="Prijmeni" CssClass="auto-style15">Příjmení:</asp:Label>
                                                </td>
                                                <td></td>
                                                <td class="auto-style65">
                                                    <asp:TextBox ID="Prijmeni" runat="server" style="margin-left: 0px; " CssClass="longStyle" Height="20px" Width="220px"></asp:TextBox>
                                                    </td><td>
                                                    <asp:RequiredFieldValidator ID="prijmeniRequired" runat="server" ControlToValidate="Prijmeni" ErrorMessage="Příjmení je nutné vyplnit." ToolTip="Příjmení je nutné vyplnit." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                                                                        <tr>
                                                <td align="right" class="auto-style51">
                                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="auto-style15">Email:</asp:Label>
                                                </td>
                                                                                            <td class="auto-style58"></td>
                                                <td class="auto-style53">
                                                    
                                                    <asp:TextBox ID="Email" runat="server" style="margin-left: 0px; " CssClass="longStyle" Height="20px" Width="220px"></asp:TextBox>
                                                    </td><td class="auto-style58">
                                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"

                                                        ErrorMessage="Email je nutné vyplnit."  ToolTip="Email je nutné vyplnit." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                                   <tr><td class="auto-style69"><td>
                                                       
                                                       <td class="auto-style65">
                                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email"

                                                        ErrorMessage="Neplatný formát emailové adresy." ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

                                                        ValidationGroup="CreateUserWizard1"></asp:RegularExpressionValidator>&nbsp;

                                                        
                                                           <td></td></td></td></td>                                     </tr>                                                                                       <tr>
                                                <td align="right" class="auto-style69">
                                                    Telefonní číslo:</td>
                                                                                            <td></td>
                                                <td class="auto-style65">
                                                    <asp:TextBox ID="Telefon" runat="server" style="margin-left: 0px; " Width="220px" CssClass="longStyle" Height="20px"></asp:TextBox>
                                                    </td><td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Telefon" ErrorMessage="Telefonní č. je nutno vyplnit." ToolTip="Telefonní č. je nutno vyplnit." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                                    

                                                </td>
                                            </tr>
                                                                                    <tr><td class="auto-style69"><td><td class="auto-style65">
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Telefon"
    ErrorMessage="Zadejte prosím jen číselný formát tel.čísla." ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="CreateUserWizard1">        </asp:RegularExpressionValidator>
                                                                                        <td></td></td></td></td></tr>
                                            <tr>
                                                <td align="right" class="auto-style70">
                                                    Rezervace do dne :<br />(do 10:00 dopoledne)</td>
                                                <td class="auto-style68"></td>
                                                <td class="auto-style66">
                                                    <asp:DropDownList ID="Dropdown" runat="server" AutoPostBack="True" CssClass="longStyle" OnSelectedIndexChanged="Dropdown_SelectedIndexChanged" style="margin-left: 0px" Width="220px" Height="20px">
                                                    </asp:DropDownList>
                                                   </td><td> 
                                                </td>
                                            </tr>
                                            <tr>
                                                                                                <td align="right" class="auto-style70">
                                                    Přidat poznámku</td>
                                                <td class="auto-style68"></td>
                                                <td class="auto-style66">
                                                    <asp:TextBox ID="PoznamkaBox" runat="server" TextMode="MultiLine" style="margin-left: 0px; " CssClass="longStyle" Height="50px" Width="220px"></asp:TextBox>
                                                    </td><td>
                                                </td>
                                            </tr>
                                        </table>
                                                                                
                                                                                <div style="text-align:center; position:relative; width:100%; padding-left:50px;">
                                                                                    <div class="g-recaptcha" data-sitekey="6LfBhioUAAAAAOVlB_ymYBfItK-lDFK42N5Twivw" data-callback="correctCaptcha"></div>
                                                                                    
                                                 
                                                                                    
                                                                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                                                                    
                                                                                    </div>
                                                                            </div>
                                                                               
                                </asp:WizardStep>
                                <asp:WizardStep runat="server" Title="Souhrn" >
                                     <div style="height:380px; width:460px;" class="auto-style30">
                                    <span class="auto-style35">Souhrn:<br /></span> <br /> 
                                         <asp:TextBox ID="SouhrnTextbox" runat="server" CssClass="auto-style54" Font-Names="Consolas" TextMode="MultiLine" ReadOnly="true" Height="159px" Width="386px"></asp:TextBox>
                                         <br />
                                    <br />
                                    <span class="auto-style22"><strong>Zkontrolujte si prosím platnost údajů.</strong></span><br />
                                    <br />
                                    Po stlačení tlačítka &quot;Zaregistrovat&quot; bude Vaše rezervace zaregistrovaná.<br /> 
                                    <br />
                                    Pro plnou platnost Vaší rezervace bude nutno zaplatit do 5 ti dnů před<br />samotným termínem <strong>zálohu ve výši xxx kč</strong>.<br /></div>
                                </asp:WizardStep>
                                <asp:CompleteWizardStep runat="server" Title="Konec" >
                                    <ContentTemplate>
                                         <div style="height:400px; width:460px;">
                                        <table style="font-family:Verdana;font-size:100%;">
                                            <tr>
                                                <td align="center" style="background-color:#6B696B;" class="auto-style36">Registrace rezervace dokončena.</td>
                                            </tr>
                                            <tr >
                                                <td class="auto-style38">Nyní je nutno zaplatit zálohu ve výši blablabla<br />
                                                    <br />
                                                    na účet xxxx<br />&nbsp;<br />s variabilním symbolem xxxx<br />&nbsp;<br /> <br />Jakmile bude platba obdržena, tak se Vám bude zaslán<br />email a Váš rezervovaný termín bude zapsán v kalendáři.<br />
                                                    <br />
                                                    Rovněž Vám byl na adresu <a href="mailto:xx@xx.xx">xx@xx.xx</a> zaslán email<br />s podrobnostmi vaší rezervace a způsobu platby zálohy.<br />
                                                    <br />
                                                    <strong>Pozor! Pokud nezaplatíte zálohu do pěti dnů od registrace<br />bude vaše rezervace stornována.</strong></td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="auto-style45">
                                                    <asp:Button ID="ContinueButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="Continue" Font-Names="Verdana" ForeColor="#284775" Text="Zavřít" ValidationGroup="CreateUserWizard1" OnClientClick="refClose();" />
                                                </td>
                                            </tr>
                                        </table>
                                             </div>
                                    </ContentTemplate>
                                </asp:CompleteWizardStep>
                            </WizardSteps>
                            <CancelButtonStyle Font-Overline="False" Height="0px" Width="0px" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="#FFFFFF" HorizontalAlign="Center" BorderStyle="Solid" Font-Size="0.9em" />
                            <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" ForeColor="#284775" />
                            <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="#FFFFFF" />
                            <SideBarStyle BackColor="#5D7B9D" BorderWidth="0px" Font-Size="0.9em" VerticalAlign="Top" />
                            <StepNavigationTemplate>
                              
                                <asp:Button ID="StepNextButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CommandName="MoveNext" Font-Names="Verdana" ForeColor="#284775" Text="Pokračovat" OnClick="registrace"  ValidationGroup="CreateUserWizard1"/>
                            </StepNavigationTemplate>
                            <StepStyle BorderWidth="0px" />
                        </asp:CreateUserWizard>
                          </div>

                      </form>
</body>
</html>