using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.Security;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

// Třída která načte již zarezervované datumy určitého měsíce, které se použijí pří kontrole

namespace Chata_IS
{
    // Struktura zaznamenávající jednu rezervaci
    public class takenDate
    {
        public int od_d; //den
        public int do_d; //den
        public int id_reservation;
        public bool crossMonth=false;
        public int od_m;
        public byte state;
        public takenDate(int o, int d, int id_ob, byte st, bool cross, int m) { od_d = o; do_d = d; id_reservation = id_ob; state = st; crossMonth = cross; od_m = m; }
    };

    public class DateChecker
    {
        // Jedináček
        private static DateChecker instance;
        public DateChecker() { }
        public static DateChecker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DateChecker();
                }
                return instance;
            }
        }

        // Jednoducha funkce převede např. číslo 1 na 01 a rovnou převede na string
        string addZero (int input)
        {
            if(input < 10) return "0" + input;
            return input.ToString();
        }

        public bool checkDateAccessibility(int day,int month, int year)
        {
            List<takenDate> tds = TakenDates(month, year);
            foreach (takenDate td in tds)
            {

                if (day >= td.od_d && day <= td.do_d && td.state != 3)  // nezaplacena záloha
                    return false;
            }
            return true;
        }

        // Načtení zabraných dat do Listu
        public List<takenDate> TakenDates(int demandedMonth, int demandedYear)
        {
            List<takenDate> output = new List<takenDate>();
          //  System.Diagnostics.Debug.WriteLine("Connecting to database...");

            SQLBase.Instance.openConnection();
            string prv_den = demandedYear + "-" + addZero(demandedMonth) + "-01";
            string pos_den = demandedYear + "-" + addZero(demandedMonth) + "-31";

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@prv_den", prv_den.ToString()));
            parameters.Add(new MySqlParameter("@pos_den", pos_den.ToString()));

         //   System.Diagnostics.Debug.WriteLine("Creating and executing SQL select");
            List<string>[] queryResult = SQLBase.Instance.Select(
                "SELECT od_cas,do_cas,id_rezervace,stav_rezervace FROM Rezervace where ((od_cas >= @prv_den AND od_cas <= @pos_den) OR (do_cas >= @prv_den AND do_cas <= @pos_den))", 4, parameters);
            if (!SQLBase.Instance.sqlStatementCompleted)
            {
          //      System.Diagnostics.Debug.WriteLine("SQL select failed.");
                return output;
            }

         //   System.Diagnostics.Debug.WriteLine("SQL select results: " );
            // Prochazeni vysledku selectu
            for (int i = 0; i < SQLBase.Instance.numberOfSelectedRows; i++)
            {
                 DateTime from_date, to_date;
                 DateTime.TryParse(queryResult[0][i], out from_date);
                 DateTime.TryParse(queryResult[1][i], out to_date);

                 int from_range = from_date.Day;
                 int to_range = to_date.Day;
                 int id = Int32.Parse(queryResult[2][i]);
                 byte state = Byte.Parse(queryResult[3][i]);
                 // Pokud rezervace pouze končí v tento měsíc (nezačíná) tak nastav from_date na prvního
                 bool cross = from_date.Month != to_date.Month;
                 if (from_date.Month < demandedMonth)
                 {
                     from_range = 1;
                     cross = false;
                 }
                 if (state == 2 || state == 3) continue;
           //      System.Diagnostics.Debug.WriteLine("Mesic :" + demandedMonth + " nalezena objednavka : [" + from_range + " - " + to_range + "] id=" + id);
                 output.Add(new takenDate(from_range, to_range, id, state, cross, from_date.Month));
            }


            return output;
            

    
            /*
            //return output;
            // Select příkaz (výběr všech rezervací v určitém měsíci)
            string command;

            // Vytvoření stringu z pozadovaneho data na YYYY-MM-DD format
            Console.WriteLine(demandedYear + "-" + addZero(demandedMonth) + "-01");

            string prv_den = demandedYear + "-" + addZero(demandedMonth) + "-01";
            string pos_den = demandedYear + "-" + addZero(demandedMonth) + "-31";


            //select * from Rezervace where ((od_cas >= '2017-07-01' AND od_cas <= '2017-07-31') OR (do_cas >= '2017-07-01' AND do_cas <= '2017-07-31'))
            command = "SELECT od_cas,do_cas,id_rezervace FROM Rezervace where ((od_cas >= @prv_den AND od_cas <= @pos_den) OR (do_cas >= @prv_den AND do_cas <= @pos_den))";

            SqlDataReader dr;
            using (SqlCommand cmd = new SqlCommand(command, con))
            {
                cmd.Parameters.AddWithValue("@prv_den", prv_den);
                cmd.Parameters.AddWithValue("@pos_den", pos_den);

                using (dr = cmd.ExecuteReader())
                {
                    // Procházení rezervací zasahujících do tohoto měsíce
                    while (dr.Read())
                    {
                        DateTime from_date, to_date;
                        DateTime.TryParse(dr.GetString(0), out from_date);
                        DateTime.TryParse(dr.GetString(1), out to_date);

                        int from_range = from_date.Day;
                        int to_range = to_date.Day;
                        int id = dr.GetInt32(2);
                        // Pokud rezervace pouze končí v tento měsíc (nezačíná) tak nastav from_date na prvního
                        if (from_date.Month < demandedMonth) from_range = 1;

                        Console.WriteLine("Mesic :" + demandedMonth + " nalezena objednavka : [" + from_range + " - " + to_range + "] id=" + id);
                        output.Add(new takenDate(from_range, to_range, id));
                    }
                }

            }*/
            return output;
        }

    }
}