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

        public SqlConnection ConnectToDB(string connect, Form1 form1)
        {
            SqlConnection connectionDB = new SqlConnection(connect);////name ="QuotesTemp"
            connectionDB.Open();
            if (connectionDB.State == ConnectionState.Open)
            {
                form1.PrintLog2("DB connected");
                return connectionDB;
            }
            else
            {
                form1.PrintLog2("DB not connected"); return null;
            }
        }
        public void InsertIntoDB(SqlConnection connection, DataSet dataSet, string table)// для копирования из одной бд в другую
        {
            foreach (DataRow dRow in dataSet.Tables[0].Rows)
            {
                SqlCommand command = new SqlCommand($"Insert into {table} ([<DATE>],[<TIME>],[<OPEN>],[<HIGH>],[<LOW>],[<CLOSE>],[<VOL>]) values ('{dRow.ItemArray[1]}','{dRow.ItemArray[2]}','{dRow.ItemArray[3]}','{dRow.ItemArray[4]}','{dRow.ItemArray[5]}','{dRow.ItemArray[6]}',{dRow.ItemArray[7]})", connection);
                command.ExecuteNonQuery();

                //'{dRow.ItemArray[1]}','{dRow.ItemArray[2]}','{dRow.ItemArray[3]}','{dRow.ItemArray[4]}','{dRow.ItemArray[5]}','{dRow.ItemArray[6]}','{dRow.ItemArray[7]}'
                //[<TIME>],[<OPEN>],[<HIGH>],[<LOW>],[<CLOSE>],[<VOL>]
            }
        }
    }
}
