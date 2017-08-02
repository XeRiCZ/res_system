using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Chata_IS
{

    // Třída reprezentující jednu určitou buňku v kalendáři 
    public class DayButton
    {
        public ClickableTableCell tc;
        public int day, month, year;
        public int reservationID = -1;
        public int state = 0;       // 0 - nezaplacena zaloha, 1 - zarezervovane, -1 prosle/zrusene
        private IPopupPage page;

        public Label bunkaLabel;
        public Label cisloLabel;
        public DayButton(ClickableTableCell _tc, int d, int m, int y, IPopupPage p,About pg)
        {
            // Přiřazení proměnných
            day = d;
            month = m;
            year = y;
            page = p;
            tc = _tc;


            // Event kliknutí
            tc.Click += lb_Click;

            tc.VerticalAlign = VerticalAlign.Top;
            tc.HorizontalAlign = HorizontalAlign.Right;
            tc.RowSpan = 1;
            tc.Wrap = false;

            cisloLabel = new Label();
            cisloLabel.CssClass = "cisloLabel";
            cisloLabel.Text = day.ToString();



            bunkaLabel = new Label();
            bunkaLabel.CssClass = "bunkaLabel";


            tc.Controls.Add(cisloLabel);
            tc.Controls.Add(new LiteralControl("<br /><br />"));
            tc.Controls.Add(bunkaLabel);
            tc.ID = d + m + y + "cb";

            
            
            

            tc.ID = d + m + y + "cb";
            AsyncPostBackTrigger trg = new AsyncPostBackTrigger();
            trg.ControlID = tc.ID;
            trg.EventName = "Click";
            pg.updPan.Triggers.Add(trg);
        }



        private void lb_Click(object sender, EventArgs e)
        {
            GlobalData.Instance.clickedDay = this;
            page.ShowPopup(this);
        }
    }


    // Třída reprezentující interaktivní kalendář
    public class Schedule
    {
        Table ScheduleTable;
        List<DayButton> dayButtons = new List<DayButton>(); // 
        IPopupPage usedPage;
        About mPage;
        public int savedDeviaton = -999;

        public Schedule(Table usedTable, IPopupPage up, About mpg)
        {
            ScheduleTable = usedTable;
            usedPage = up;
            mPage = mpg;
        }

        // Zjisti status dne
        protected void SetDayStatus(DayButton db, int cDay, int cMonth, int cYear, List<takenDate> takenDates)
        {

            db.tc.CssClass = "free";       // nejprve nastavím že je tento den volný
            db.bunkaLabel.Text = "Volný termín";
            db.state = 0;
            // Kontrola zda tento den byl zabrán

            string currentDateString =
                GlobalData.Instance.addZero(cDay) +
                "/" + GlobalData.Instance.addZero(cMonth) +
                "/" + GlobalData.Instance.addZero(cYear);
                        DateTime oDate = Convert.ToDateTime(currentDateString);
                        DateTime nDate = oDate.AddDays(1);
            bool accesible = true;
            if (cDay == DateTime.DaysInMonth(cYear, cMonth))
            {
                System.Diagnostics.Debug.WriteLine("   - Checking days accessibility  " + nDate.Day + "/" + nDate.Month + "");
                System.Diagnostics.Debug.WriteLine("    - from previous day " + cDay + "/" + cMonth + "/" + cYear);
                accesible =
                    DateChecker.Instance.checkDateAccessibility(nDate.Day, nDate.Month, nDate.Year);
                if (accesible) System.Diagnostics.Debug.WriteLine("    ACCESSIBLE ");
                else System.Diagnostics.Debug.WriteLine("    NOT ACCESSIBLE ");
            }
            if (!accesible)
            {
                db.tc.CssClass = "bor";
                db.bunkaLabel.Text = "Koliduje<br/> s následujícím dnem";
                db.state = 3;
                if (SQLBase.Instance.isAdminLoggedIn())
                {
                    db.tc.Attributes.Add("onmouseover", "this.style.cursor='pointer'");

                }
            }


            foreach (takenDate td in takenDates)
            {
                
                 if (cDay >= td.od_d && (cDay < td.do_d || (td.crossMonth && cMonth == td.od_m)) && td.state==0) // nezaplacena záloha
                {
                    db.tc.CssClass = "rez";
                    db.bunkaLabel.Text = "Zarezervováno<br/>Čeká na zálohu";
                    db.state = 2;
                    db.reservationID = td.id_reservation;
                    db.tc.takenDat = td;
                    if (SQLBase.Instance.isAdminLoggedIn())
                    {
                        db.tc.Attributes.Add("onmouseover", "this.style.cursor='pointer'");
                        return;
                    }
                }
                 else if (cDay >= td.od_d && (cDay < td.do_d || (td.crossMonth && cMonth == td.od_m)) && td.state == 1)  //rezervace + záloha
                {
                    db.tc.CssClass = "tak";
                    db.bunkaLabel.Text = "Zarezervováno<br/>Záloha zaplacena";
                    db.state = 2;
                    db.reservationID = td.id_reservation;
                    db.tc.takenDat = td;
                    if (SQLBase.Instance.isAdminLoggedIn())
                    {
                        db.tc.Attributes.Add("onmouseover", "this.style.cursor='pointer'");
                        return;
                    }
                }
                 else if (((cDay == td.od_d - 1 && accesible)) && td.state != 3 && db.state!=2
                     && !((cDay >= td.od_d && (cDay < td.do_d || (td.crossMonth && cMonth == td.od_m)))&&(td.state==1 || td.state==0) ))
                 {
                     db.tc.CssClass = "bor";
                     db.bunkaLabel.Text = "Koliduje<br/> s následujícím dnem";
                     db.state = 3;
                     db.reservationID = td.id_reservation;
                     db.tc.takenDat = td;
                     if (SQLBase.Instance.isAdminLoggedIn())
                     {
                         db.tc.Attributes.Add("onmouseover", "this.style.cursor='pointer'");

                     }
                 }
                
            }
            db.tc.takenDat = null;
            // Tento den už je minulostí
            int actualDay = DateTime.Now.Day;
            if ((cMonth == DateTime.Now.Month && cYear == DateTime.Now.Year &&
                cDay <= actualDay + GlobalData.Instance.daysLimit) || (cMonth < DateTime.Now.Month && cYear == DateTime.Now.Year))
            {
                db.tc.CssClass = "old";
                db.bunkaLabel.Text = "";
                db.state = 1;
                db.cisloLabel.CssClass = "cisloLabelWhite";
            }
            else if(db.state==0)   // nastav "klikající" kurzor nad volným dnem
                db.tc.Attributes.Add("onmouseover", "this.style.cursor='pointer'");
        }

        protected void ResetTable()
        {
            dayButtons.Clear();
            // Vyčistí tabulku z předchozího měsíce
            while (ScheduleTable.Rows.Count > 1)
            {
                TableRow rw = ScheduleTable.Rows[ScheduleTable.Rows.Count - 1];
                ScheduleTable.Rows.Remove(rw);
            }
            dayButtons = new List<DayButton>();

        }
        public void FillTable(int deviationMonth,Label MonthLabel,Label YearLabel)
        {
            System.Diagnostics.Debug.WriteLine("FILLTABLE INPUT DEVIATION " + deviationMonth);
           // mPage.updPan.Update();
            // Vyplní obsah tabulky
            ResetTable();
            int currentDay = 1;     // proměnná určující "kolikátého" je
            int currentMonth = DateTime.Now.Month;
            int deviationYear = 0;
            savedDeviaton = deviationMonth;
            // Zjištění roku na základě aktuálního měsíce + zvolené odchylky uživatelem
            for (int i = 0; i < Math.Abs(deviationMonth); i++)
            {
                // Kladna odchylka
                if (deviationMonth > 0)
                {
                    currentMonth++;
                    if (currentMonth > 12)  // přičtení roku
                    {
                        currentMonth = 1;
                        deviationYear++;
                    }
                }
                else
                {
                    currentMonth--;
                    if (currentMonth < 1)  // odečtení roku
                    {
                        currentMonth = 12;
                        deviationYear--;
                    }
                }
            }

            int currentYear = DateTime.Now.Year + deviationYear;
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            // Nastavení textu labelů měsíce a roku
            string monthText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentMonth);
            StringBuilder strB = new StringBuilder(monthText);
            strB[0] = Char.ToUpper(monthText[0]);
            monthText = strB.ToString();

            MonthLabel.Text = monthText;
            YearLabel.Text = currentYear.ToString();

            int dayCount = DateTime.DaysInMonth(currentYear, currentMonth);
            int actualDay = DateTime.Now.Day;

            // Načtení rezervací z DB
            List<takenDate> takenDates =
                DateChecker.Instance.TakenDates(currentMonth, currentYear);
           


            // Naplnění jednoho řádku tabulky
            for (int y = 0; y < 6; y++)
            {
                TableRow tr = new TableRow();
                tr.Height = 65;

                int w = 0;
                if (y == 0)  // první den v měsíci nemusí být pondělí, zjisti tedy který den to je
                {
                    DateTime testedDate = new DateTime(currentYear, currentMonth, currentDay);
                    DayOfWeek testedDay = testedDate.DayOfWeek;
                    switch (testedDay)
                    {
                        case DayOfWeek.Monday: w = 1; break;
                        case DayOfWeek.Tuesday: w = 2; break;
                        case DayOfWeek.Wednesday: w = 3; break;
                        case DayOfWeek.Thursday: w = 4; break;
                        case DayOfWeek.Friday: w = 5; break;
                        case DayOfWeek.Saturday: w = 6; break;
                        case DayOfWeek.Sunday: w = 7; break;
                        default: break;
                    }
                }

                // Naplnění sloupců daného řádku
                for (int x = 1; x < 8; x++)
                {
                    // Kontrola zda číslo dne existuje v měsící a zda je daný den (po,ut,str..) v týdnu

                    // Vytvoření nové buňky
                    ClickableTableCell tc = new ClickableTableCell();


                    tc.Text = "";
                    tc.CssClass = "none";
                   
                    tr.Cells.Add(tc);                       // přidání do řádku

                    if (x < w) continue;                    // tato buňka nepatří do tohoto měsíce
                    if (currentDay > dayCount) continue;    // tento měsíc už nemá více dnů


                    // Vypsaní čísla dne do tabulky
                    //  tc.HorizontalAlign = HorizontalAlign.Right;
                    // tc.VerticalAlign = VerticalAlign.Top;
                    // tc.Text = currentDay.ToString();


                    DayButton dayBut = new DayButton(
                        tc, currentDay, currentMonth, currentYear, usedPage,mPage);
                    tc.dayButton = dayBut;

                    // Nastavení stavu dne (volný, zabraný, kolize)
                    SetDayStatus(dayBut, currentDay, currentMonth, currentYear, takenDates);

                    dayButtons.Add(dayBut);
                    currentDay++;
                }
                ScheduleTable.Rows.Add(tr);
            }
            
        }

    }
}