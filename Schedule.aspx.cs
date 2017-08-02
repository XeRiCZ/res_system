using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;



namespace Chata_IS
{
    

    public interface IPopupPage
    {
        void ShowPopup(DayButton db);
    }

    public partial class About : Page, IPopupPage
    {
        // Kontrolní proměnné

        
        // Pomocné proměnné
        int deviationMonth = 0; // odchylka od aktuálního měsíce ( 0=tento měsíc, 1=další měsíc, -1=předchozí měsíc)
        Schedule schedule;
        public UpdatePanel updPan;

        protected void Page_PreInit(object sender, EventArgs e)
        {

            string str = "neco";
            string str_r = "";
            for (int i = str.Count()-1; i > 0; i--)
                str_r = str_r + str[i];
                

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            updPan = upPan;
            GlobalData.Instance.mp1 = mp1;
            GlobalData.Instance.schedule = this;

            schedule = new Schedule(ScheduleTable, this, this);
            if (!this.IsPostBack)
            {
                
               
                // načtení currentMonnth ze Sessiony
               
                

            }
            if (Session["month"] != null)
                deviationMonth = (int)Session["month"];
            schedule.FillTable(deviationMonth, MonthLabel, YearLabel);
            GlobalData.Instance.deviationMonthMemory = deviationMonth;
            schedule.savedDeviaton = deviationMonth;
            
 
        }

        public void refreshSchedule()
        {
            schedule.FillTable(deviationMonth, MonthLabel, YearLabel);
          //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "refreshThis()", true);
        }
        // Zobrazeni Popup okna
        void IPopupPage.ShowPopup(DayButton sourceButton)
        {
            System.Diagnostics.Debug.WriteLine("   - Logged ID " + GlobalData.Instance.loggedID+ "");
            GlobalData.Instance.chosenDay = sourceButton.tc.takenDat;

            if(SQLBase.Instance.isAdminLoggedIn())
            {
                mp2.Show();
            }
            else if (sourceButton.state == 0)
            {
               // GlobalData.Instance.rezervPopup.reload();
                mp1.Show();
            }
           // updPan.Update();
        }



        // Tlačítko další měsíc
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("   - clickevent");
            if (deviationMonth == GlobalData.Instance.maxDeviation && !SQLBase.Instance.isAdminLoggedIn()) return;
            System.Diagnostics.Debug.WriteLine("   - deviation++");
            deviationMonth++;
            schedule.FillTable(deviationMonth, MonthLabel, YearLabel);

            Session["month"] = deviationMonth;
            GlobalData.Instance.deviationMonthMemory = deviationMonth;
            schedule.savedDeviaton = deviationMonth;

        }

        // Tlačítko předchozí měsíc
        protected void BackBtn_Click(object sender, EventArgs e)
        {
            if (deviationMonth == GlobalData.Instance.minDeviation && !SQLBase.Instance.isAdminLoggedIn()) return;
            deviationMonth--;
            schedule.FillTable(deviationMonth, MonthLabel, YearLabel);

            Session["month"] = deviationMonth;
            GlobalData.Instance.deviationMonthMemory = deviationMonth;
            schedule.savedDeviaton = deviationMonth;
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MailSender.SendMail("Rezervace myslivecké chaty XX.XX.XXX - XX.XX.XXXX", "<h2>Dobr&yacute; den,</h2><h3><br /> Va&scaron;e rezervace na dny XX.XX.XXX - XX.XX.XXXX byla &uacute;spě&scaron;ně zaregistrovan&aacute;.</h3><hr />" +
"<p><span style=\"color: #000080;\">Nyn&iacute; je nutno zaslat z&aacute;lohu ve v&yacute;&scaron;i</span><br /><span style=\"color: #000080;\"> <strong>XXX Kč</strong> na &uacute;čet <strong>XXXXXX/XXXX</strong> s variabiln&iacute;m symbolem <strong>X</strong></span><br /><span style=\"color: #000080;\"> nejpozději do 5ti dnů od rezervace.</span></p>"+
"<hr /><p><br />V&aacute;mi&nbsp;zadan&eacute; informace:</p><table style=\"height: 31px; margin-left: 10px;\" border=\"1\" width=\"97;\">"+
"<tbody><tr><td><blockquote>Souhrn</blockquote><br /><br /></td></tr></tbody></table><p><br />Pokud m&aacute;te jak&yacute;koliv dotaz či si přejete rezervaci zru&scaron;it, můžete napsat email na adresu info@msveselahrachovec.cz , nebo zavolat na telefon&iacute; č&iacute;slo XXXXX.<br /><br /></p>"+
"<p>Děkujeme a přejeme pěkn&yacute; den!</p><hr /><p><em>Tento email je automaticky vygenerovan&yacute;, pros&iacute;m neodpov&iacute;dejte na něj.</em></p>,",
                                "postmaster@msveselahrachovec.cz", "urubek.jan@email.cz", 465, "smtp.forpsi.com", true, "postmaster@msveselahrachovec.cz", "7q!rAf93er");
        }





    }
}