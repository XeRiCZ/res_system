using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Web.Services;

namespace Chata_IS
{
    public partial class manPopup : System.Web.UI.Page
    {
        [WebMethod]
        void refresh()
        {
            if (GlobalData.Instance.clickedDay == null)
                curDate.Text = "";
            else curDate.Text = GlobalData.Instance.clickedDay.day + "." +
                GlobalData.Instance.clickedDay.month + "." + GlobalData.Instance.clickedDay.year;

            if (GlobalData.Instance.chosenDay == null)
            {
                but_nezaplaceno.Enabled = false;
                but_zaplaceno.Enabled = false;
                but_zruseni.Enabled = false;
                but_zamknout.Enabled = true;
                but_odemknout.Enabled = false;
                but_ipban.Enabled = false;

                return;
            }



            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", GlobalData.Instance.chosenDay.id_reservation.ToString()));

            System.Diagnostics.Debug.WriteLine("Creating and executing SQL select");
            List<string>[] queryResult = SQLBase.Instance.Select(
                "SELECT jmeno,prijmeni,tel_cislo,email,od_cas,do_cas,poznamka,stav_rezervace,datum_vytvoreni,ip_adresa FROM Rezervace where id_rezervace = @id", 10, parameters);
            if (!SQLBase.Instance.sqlStatementCompleted)
            {
                System.Diagnostics.Debug.WriteLine("SQL select failed.");
                return;
            }

            jmenolab.Text = queryResult[0][0];
            prijmenilab.Text = queryResult[1][0];
            tellab.Text = queryResult[2][0];
            emaillab.Text = queryResult[3][0];
            idlab.Text = GlobalData.Instance.chosenDay.id_reservation.ToString();
            IPlab.Text = queryResult[9][0];
            DateTime from, to;
            DateTime.TryParse(queryResult[4][0], out from);
            DateTime.TryParse(queryResult[5][0], out to);
            from.AddHours(11);
            to.AddHours(10);

            zarezerlab.Text = from.Date.ToString("dd/MM/yy") + " 11:00 - " + to.Date.ToString("dd/MM/yy") + " 10:00";
            poznlab.Text = queryResult[6][0];
            if (queryResult[7][0] == "0")
            {
                stavlab.Text = "NEZAPLACENO";
                but_nezaplaceno.Enabled = false;
                but_zaplaceno.Enabled = true;
                but_zruseni.Enabled = true;

                but_odemknout.Enabled = false;
                but_zamknout.Enabled = true;
                but_ipban.Enabled = true;
                datvytvorlab.Text = queryResult[8][0];
                return;
            }
            else if (queryResult[7][0] == "1")
            {
                stavlab.Text = "ZAPLACENO";
                but_nezaplaceno.Enabled = true;
                but_zaplaceno.Enabled = false;
                but_zruseni.Enabled = true;

                but_odemknout.Enabled = false;
                but_zamknout.Enabled = true;
                but_ipban.Enabled = true;
                datvytvorlab.Text = queryResult[8][0];

                if (emaillab.Text == "ZAMKNUTO")
                {
                    but_nezaplaceno.Enabled = false;
                    but_zaplaceno.Enabled = false;
                    but_zruseni.Enabled = false;
                    stavlab.Text = "ZAMKNUTO";

                    but_odemknout.Enabled = true;
                    but_zamknout.Enabled = false;
                    but_ipban.Enabled = false;
                }
                return;
            }
            else
            {
                stavlab.Text = "ZRUSENO/VYPRSELO";
            }
            datvytvorlab.Text = queryResult[8][0];


            but_nezaplaceno.Enabled = false;
            but_zaplaceno.Enabled = false;
            but_zruseni.Enabled = false;
            but_zamknout.Enabled = false;
            but_odemknout.Enabled = true;
            but_ipban.Enabled = false;

            GlobalData.Instance.schedule.refreshSchedule();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            refresh();
//mp.Hide();
        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {

        }

        protected void registrace(object sender, EventArgs e)
        {

        }

        protected void StepPreviousButton_Click(object sender, EventArgs e)
        {

        }

        protected void Dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void but_zruseni_Click(object sender, EventArgs e)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", GlobalData.Instance.chosenDay.id_reservation.ToString()));
            SQLBase.Instance.Update("update Rezervace set stav_rezervace = 3 where id_rezervace = @id", parameters);

        }

        protected void but_zaplaceno_Click(object sender, EventArgs e)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", GlobalData.Instance.chosenDay.id_reservation.ToString()));
            SQLBase.Instance.Update("update Rezervace set stav_rezervace = 1 where id_rezervace = @id", parameters);

            //refresh();
        }

        protected void but_nezaplaceno_Click(object sender, EventArgs e)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", GlobalData.Instance.chosenDay.id_reservation.ToString()));
            SQLBase.Instance.Update("update Rezervace set stav_rezervace = 0 where id_rezervace = @id", parameters);

           // refresh();
        }

        protected void but_zamknout_Click(object sender, EventArgs e)
        {
            if (GlobalData.Instance.chosenDay != null)
            {
                List<MySqlParameter> parameterss = new List<MySqlParameter>();
                parameterss.Add(new MySqlParameter("@id", GlobalData.Instance.chosenDay.id_reservation.ToString()));
                SQLBase.Instance.Update("update Rezervace set stav_rezervace = 3 where id_rezervace = @id", parameterss);
            }

            string currentDateString=
                    GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.day) +
                    "/"+ GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.month) +
                    "/"+ GlobalData.Instance.addZero(GlobalData.Instance.clickedDay.year);
            DateTime oDate = Convert.ToDateTime(currentDateString);
            DateTime nDate = oDate.AddDays(1);
            

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            SQLBase.Instance.Insert("insert into Rezervace (`jmeno`, `prijmeni`, `tel_cislo`, `email`, `od_cas`, `do_cas`,`poznamka`, `ip_adresa`,`datum_vytvoreni`,`stav_rezervace`)" +
                " values ('ZAMKNUTO','ZAMKNUTO','ZAMKNUTO','ZAMKNUTO',"+
                "'"+ oDate.Date.ToString("yyyy-MM-dd")+"',"+
                "'"+ nDate.Date.ToString("yyyy-MM-dd")+"',"+
                "'ZAMKNUTO','ZAMKNUTO','" + oDate.Date.ToString("yyyy-MM-dd") + "',1)", parameters);

            //refresh();
        }

        protected void but_odemknout_Click(object sender, EventArgs e)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", GlobalData.Instance.chosenDay.id_reservation.ToString()));
            SQLBase.Instance.Update("update Rezervace set stav_rezervace = 3 where id_rezervace = @id", parameters);
           // refresh();
        }

        protected void opBut_Click(object sender, EventArgs e)
        {

        }

        protected void but_ipban_Click(object sender, EventArgs e)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@ip", IPlab.Text));

            SQLBase.Instance.Insert("insert into Zakazane_ip (`ip_adresa`) values (@ip);",parameters);
        }


    }
}