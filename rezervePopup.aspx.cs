using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Net;
using System.Text.RegularExpressions;
using System.Data;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace Chata_IS
{
    public partial class RezerPopup : System.Web.UI.Page
    {

        string userIP = "";

        public static string getExternalIp()
    {
        try
        {
            string externalIP;
            externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
            externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                         .Matches(externalIP)[0].ToString();
            return externalIP;
        }
        catch { return null; }
    }

        protected void refresh(object sender, EventArgs e)
        {
            userIP = getExternalIp();
            System.Diagnostics.Debug.WriteLine("IP = " + userIP);
            if (GlobalData.Instance.clickedDay != null)
            {
                curDateLabel.Text = "" + GlobalData.Instance.clickedDay.day + "." +
                GlobalData.Instance.clickedDay.month + "." +
                GlobalData.Instance.clickedDay.year + "";

                curDate2.Text = curDateLabel.Text;
                string currentDateString =
                    GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.day) +
                    "/" + GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.month) +
                    "/" + GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.year);
                Dropdown.Items.Clear();

                for (int i = 2; i < GlobalData.Instance.maxDays + 1; i++)
                {

                    DateTime oDate = Convert.ToDateTime(currentDateString);
                    oDate = oDate.AddDays(i);


                    DateTime nDate = oDate.AddDays(-1);

                    // System.Diagnostics.Debug.WriteLine("   - Checking day " + nDate.Day + "/" + nDate.Month + "");

                    if (!DateChecker.Instance.checkDateAccessibility(
                        nDate.Day, nDate.Month, nDate.Year))
                    {
                        System.Diagnostics.Debug.WriteLine("   - COLISION!");
                        break;
                    }

                    Dropdown.Items.Add(oDate.Date.ToString(
                        "dd/MM/yyyy"));


                }
                if (Session["DP_ID"] != null)
                {
                    Dropdown.SelectedValue = Session["DP_ID"].ToString();
                }
                else
                {
                    if (Dropdown.Items.Count == 0) return;
                    Dropdown.Items[0].Selected = true;
                    Session["DP_ID"] = Dropdown.SelectedValue;
                }
                System.Diagnostics.Debug.WriteLine("Sel value = " + Session["DP_ID"]);
            }
        }

        
        

        protected void Page_Load(object sender, EventArgs e)
        {
 
        }

        public void init(object sender, EventArgs e)
        {
            GlobalData.Instance.rezervPopup = this;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
 
        }


        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {

        }

        public bool Validate()
        {

            string Response = Request["g-recaptcha-response"];//Getting Response String Appned to Post Method

            bool Valid = false;
            //Request to Google Server
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(" https://www.google.com/recaptcha/api/siteverify?secret=6LfBhioUAAAAAHmbd2sPzSLJrYxYhQ4IVwSL61rO&response=" + Response);

            try
            {
                //Google recaptcha Responce 
                using (WebResponse wResponse = req.GetResponse())
                {

                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json 

                        Valid = Convert.ToBoolean(data.success);


                    }
                }

                return Valid;

            }
            catch (WebException ex)
            {
                throw ex;
            }


        }

        public class MyObject
        {
            public string success { get; set; }
        }

        protected void registrace(object sender, EventArgs e)
        {
            var encodedResponse = Request.Form["g-Recaptcha-Response"];
            var isCaptchaValid = ReCaptcha.Validate(encodedResponse);

            if (!isCaptchaValid)
            {
                CreateUserWizard1.ActiveStepIndex = 1;
                ErrorMessage.Text = "Problem s ověřením.";
                ErrorMessage.Visible = true;
                ErrorMessage.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
                return;
            }
            
            else
            {
                ErrorMessage.Visible = false;
                ErrorMessage.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            }
             List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@ip", userIP));

            List<string>[] queryResult = SQLBase.Instance.Select(
                "SELECT ip_adresa FROM Zakazane_ip where ip_adresa=@ip", 1, parameters);


            if (SQLBase.Instance.foundSomeResult)
            {
                // Kontrola IP
                CreateUserWizard1.ActiveStepIndex = 1;
                ErrorMessage.Text = "Registrace rezervací byla zablokována pro tento počítač..";
                ErrorMessage.Visible = true;
                ErrorMessage.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
                System.Diagnostics.Debug.WriteLine("Registrace rezervací byla zablokována pro tento počítač." + "')</script>");
                CreateUserWizard1.ActiveStepIndex = 1;
                return;
            }
            else
            {
                // Kontrola IP
                System.Diagnostics.Debug.WriteLine("Zadane data jsou OK. IP je taky OK." + "')</script>");
                SouhrnTextbox.Text =
                    "-----------------------------------------\n"+
                    "Zarezervování chaty na termín\n   od:\t\t\t" + curDateLabel.Text + " 11:00\n" +
                    "   do:\t\t\t" + Session["DP_ID"].ToString() + " 10:00\n" +
                    "   Jméno a příjmení:\t" + Jmeno.Text + " " + Prijmeni.Text + "\n" +
                    "   Emailová adresa :\t" + Email.Text + "\n" +
                    "   Telefoní číslo  :\t" + Telefon.Text + "\n" +
                    "   Vlastní poznámka pro správce:" +
                    "\n"+
                         PoznamkaBox.Text +
                        "\n-----------------------------------------";
            }
        }

        protected void testCaptcha(object sender, EventArgs e)
        {
            if (!Validate())
            {
                ErrorMessage.Visible = true;
                ErrorMessage.Text = "Špatně zadaný kód!";
                ErrorMessage.EnableViewState = true;
                System.Diagnostics.Debug.WriteLine("Špatně zadaný kód!");
                return;
            }
            else { System.Diagnostics.Debug.WriteLine("Dobrý kód!");  ErrorMessage.Visible = false;  }
        }

        protected void StepPreviousButton_Click(object sender, EventArgs e)
        {

        }

        protected void Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["DP_ID"] = Dropdown.SelectedValue;
            System.Diagnostics.Debug.WriteLine("Sel value = " + Session["DP_ID"]);
        }

        protected void Finish(object sender, WizardNavigationEventArgs e)
        {
            string currentDateString =
                    GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.day) +
                    "/" + GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.month) +
                    "/" + GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.year);
            DateTime oDate = Convert.ToDateTime(currentDateString);
            DateTime nDate = Convert.ToDateTime(Session["DP_ID"].ToString());


            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@jmeno", Jmeno.Text));
            parameters.Add(new MySqlParameter("@prijmeni", Prijmeni.Text));
            parameters.Add(new MySqlParameter("@tel_cislo", Telefon.Text));
            parameters.Add(new MySqlParameter("@email", Email.Text));
            parameters.Add(new MySqlParameter("@od_cas", oDate.Date.ToString("yyyy-MM-dd")));
            parameters.Add(new MySqlParameter("@do_cas", nDate.Date.ToString("yyyy-MM-dd")));
            parameters.Add(new MySqlParameter("@poznamka", PoznamkaBox.Text));
            parameters.Add(new MySqlParameter("@ip_adresa", getExternalIp()));
            parameters.Add(new MySqlParameter("@datum_vytvoreni", DateTime.Now.ToString("yyyy-MM-dd")));
            parameters.Add(new MySqlParameter("@stav_rezervace", Byte.Parse("0")));

            SQLBase.Instance.Insert("insert into Rezervace (`jmeno`, `prijmeni`, `tel_cislo`, `email`, `od_cas`, `do_cas`,`poznamka`, `ip_adresa`,`datum_vytvoreni`,`stav_rezervace`)" +
                " values (@jmeno,@prijmeni,@tel_cislo,@email,@od_cas,@do_cas,@poznamka,@ip_adresa,@datum_vytvoreni,@stav_rezervace);", parameters);

            MailSender.SendMail("Rezervace myslivecké chaty " + oDate.Date.ToString("yyyy-MM-dd") + " - " + nDate.Date.ToString("yyyy-MM-dd"),
                                "<h2>Dobrý den,</h2><br/> Vaše rezervace na dny " + oDate.Date.ToString("yyyy-MM-dd") + " - " + nDate.Date.ToString("yyyy-MM-dd") + " byla úspěšně zaregistrovaná. " +
                                "<br/><hr>Nyní je nutno zaslat zálohu ve výši<br/> <b>XXX Kč</b> na účet <b>XXXXXX/XXXX</b> s variabilním symbolem <b>X</b><br/> nejpozději do 5ti dnů od rezervace.<hr><br/>" +
                                "<br/>Vaše zadané informace<br/><table style=\"border-style:solid;border-width:1px;\"><tr><td>" + SouhrnTextbox.Text + " </td></tr></table><br/>" +
                                "Pokud máte jakýkoliv dotaz, můžete napsat email na adresu info@msveselahrachovec.cz , nebo zavolat na telefoní číslo XXXXX<br/><br/><hr>" +
                                "<i>Tento email je automaticky vygenerovaný, prosím neodpovídejte na něj.</i>",
                                "chata@msveselahrachovec.cz", Email.Text, 465, "smtp.forpsi.com", true, "chata@msveselahrachovec.cz", "V55BQP5");
        }

    }
}