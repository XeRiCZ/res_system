using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chata_IS
{
    public class GlobalData
    {
                // Jedináček
        private static GlobalData instance;
        public GlobalData() { }
        public static GlobalData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalData();
                }
                return instance;
            }
        }

        public string addZero(int input)
        {
            if (input < 10) return "0" + input;
            return input.ToString();
        }

        // Kontrolní proměnné
        public int daysLimit = 1;     // chata se musí zarezervovat min. 1 den předem  
        public int maxDeviation = 12; // maximalně na rok dopředu se může uživatel podívat
        public int minDeviation = 0;  // uživatel se nemůže dívat na minulý měsíc (rezervace v minulosti nejde :)   
        public int maxDays = 5;       // 1 rezervace je na max. 5 dnů

        // Pomocné proměnné
        public AjaxControlToolkit.ModalPopupExtender mp1;
        public int loggedID = -1;
        public int deviationMonthMemory = 0;
        public takenDate chosenDay;
        public DayButton clickedDay;
        public RezerPopup rezervPopup;
        public About schedule;
    }
}