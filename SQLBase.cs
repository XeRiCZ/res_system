using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Chata_IS
{
    public class SQLBase
    {
        // Jedináček
        private static SQLBase instance;
        public SQLBase() { }
        public static SQLBase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SQLBase();
                }
                return instance;
            }
        }



        private string connectionString = "SERVER=127.0.0.1;DATABASE=f92455;UID=400605DB;PASSWORD=V55BQP5;Encrypt=true;";
        
        
        MySqlConnection myConnection;
        public bool sqlStatementCompleted = false;   // indikuje zda se SQL přikaz zdařil (hrač byl připojen na DB)
        public string statusText = "";
        public int numberOfSelectedRows;
        public bool foundSomeResult = false;

        public bool isAdminLoggedIn()
        {
            if(GlobalData.Instance.loggedID == -1) return false;
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id_clena", GlobalData.Instance.loggedID));

            List<string>[] queryResult = Select(
                "SELECT admin_prava FROM Clen where id_clena=@id_clena", 1, parameters);

            if (queryResult[0][0] == "1")
                return true;
            else return false;

        }
        public bool openConnection()
        {
            // Hlavní metoda pro připojení do databáze
            myConnection = new MySqlConnection(connectionString);

            try
            {
                myConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                statusText = "Failed to connect to database.";
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                myConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public bool Insert(string insertStatement, List<MySqlParameter> parameters)
        {
            if (this.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(insertStatement, myConnection);
                foreach (MySqlParameter param in parameters)
                    cmd.Parameters.Add(param);
                // Proveď Insert příkaz
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }
           // this.CloseConnection();
            return false;   // nepřipojeno na DB
        }

        public bool Update(string updateStatement, List<MySqlParameter> parameters)
        {
            if (this.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(updateStatement, myConnection);
                foreach (MySqlParameter param in parameters)
                    cmd.Parameters.Add(param);
                // Proveď Insert příkaz
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }
           // this.CloseConnection();
            return false;   // nepřipojeno na DB
        }

        public bool Delete(string deleteStatement, List<MySqlParameter> parameters)
        {
            if (this.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(deleteStatement, myConnection);
                foreach (MySqlParameter param in parameters)
                    cmd.Parameters.Add(param);
                // Proveď Insert příkaz
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return true;
            }
            // this.CloseConnection();
            return false;   // nepřipojeno na DB
        }

        public DateTime parsDate;
        // Klasicka operace Select  - numRows indikuje počet sloupcu u vychozi tabulky
        // prace s vystupem např.:  queryResult[0][1] =  nulty sloupec prvni radek vysledku
        public List<string>[] Select(string selectStatement, int numCols, List<MySqlParameter> parameters, int datetimecoll = -1)
        {
            string query = selectStatement;
            // Console.WriteLine(" - Creating select statement");

            foundSomeResult = false;
            //Vytvoření listu obsahujícího výsledky operace Select
            // - výsledky jsou listy typu <string>
            List<string>[] list = new List<string>[numCols];
            for (int i = 0; i < numCols; i++)
            {
                list[i] = new List<string>();
            }
            numberOfSelectedRows = 0;
            // Otevři připojení
            if (this.openConnection() == true)
            {
                sqlStatementCompleted = true;
                //Vytvoř přikaz
                MySqlCommand cmd = new MySqlCommand(selectStatement, myConnection);
                foreach (MySqlParameter param in parameters)
                    cmd.Parameters.Add(param);
                //Vytvoř dataReader a spusť příkaz
              //  try
             //   {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
     

                        //Přečti všechny data
                        while (dataReader.Read())
                        {
                            numberOfSelectedRows++;
                            //System.Diagnostics.Debug.WriteLine("NewRow");
                            for (int i = 0; i < numCols; i++)
                            {
                                if(i == datetimecoll) parsDate = (DateTime)dataReader[i];
                                list[i].Add(dataReader[i] + "");

                                foundSomeResult = true;
                                //System.Diagnostics.Debug.WriteLine("   - Adding to result query " + dataReader[i] + "");
                                //Console.WriteLine("   - Adding to result query " + dataReader[i] + "");
                            }
                        }

                        //zavři data reader
                        dataReader.Close();
                        // Console.WriteLine(" - Select statement ended");
                        //zavři připojení
                        this.CloseConnection();

                        //vrať list
                        return list;
                    }
                
              /*  catch(MySqlException e)
                {
                    sqlStatementCompleted = false;
                    this.CloseConnection();
                    return list;
                }
                catch(System.InvalidOperationException e2)
                {
                    sqlStatementCompleted = false;
                    this.CloseConnection();
                    return list;
                }*/
           // }

            else
            {
                sqlStatementCompleted = false;
                this.CloseConnection();
                return list;
            }
        }

    }
}