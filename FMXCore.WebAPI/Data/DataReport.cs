using System;
using System.Collections;
using System.Collections.Generic;

namespace FMXCore.WebAPI.Data
{
    class DataReport
    {
        private List<Models.Finance> listFinanceAux = new List<Models.Finance>();        
        public DataReport(List<Models.Finance> listFinance)
        {
            listFinanceAux = listFinance;
        }
        public ArrayList reportFinance()
        {
            Double credit = 0;
            Double debit = 0;
            Double balance = 0;

            ArrayList listCredit = new ArrayList();
            ArrayList listDebit = new ArrayList();
            ArrayList listTotalCredit = new ArrayList();
            ArrayList listTotalDebit = new ArrayList();
            ArrayList listBalance = new ArrayList();
            ArrayList newlistFinance = new ArrayList();            

            listCredit.Insert(0, "Credit");
            listDebit.Insert(0, "Debit");
            listTotalCredit.Insert(0, "Total Credit");
            listTotalDebit.Insert(0, "Total Debit");
            listBalance.Insert(0, "Balance");

            int i = 0;

            foreach (var n in listFinanceAux)
            { 
                if (n.Type == "D")
                {
                    listDebit.Insert(1, listFinanceAux[i]);
                    debit += n.Value;
                }
                else if (n.Type == "R")
                {
                    listCredit.Insert(1, listFinanceAux[i]);
                    credit += n.Value;
                }               

                i++;
            }

            balance = credit - debit;

            listTotalCredit.Insert(1, credit);
            listTotalDebit.Insert(1, debit);
            listBalance.Insert(1, balance);            

            newlistFinance.Insert(0, listCredit);
            newlistFinance.Insert(1, listDebit);
            newlistFinance.Insert(2, listTotalCredit);
            newlistFinance.Insert(3, listTotalDebit);
            newlistFinance.Insert(4, listBalance);

            return (newlistFinance);
        }
    }
}
