using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBotEvolveWF
{
    internal class WorkWithDB
    {
        public void CopyTable()
        {

        }
        public SqlConnection ConnectToDB(string connect, Form1 form1)
        {
            SqlConnection connectionDB = new SqlConnection("Data Source=DESKTOP-GLVT5QN\\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");////name ="QuotesTemp"
            connectionDB.Open();
            if (connectionDB.State == ConnectionState.Open)
            {
                form1.PrintLog("бд подключен");
                return connectionDB;
            }
            else
            {
                form1.PrintLog("бд НЕ подключен"); return null;
            }
        }
        //public void NewQuaryToDB()
    }
}
