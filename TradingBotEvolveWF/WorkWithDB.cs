using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public void UpdateTableFromCSVFile(SqlConnection connection, DataTable dataTable, string table)// для копирования из одной бд в другую
        {
            SqlCommand command = new SqlCommand($"TRUNCATE TABLE {table}", connection);
            command.ExecuteNonQuery();
            foreach (DataRow dRow in dataTable.Rows)
            {
                command = new SqlCommand($"Insert into {table} ([<DATE>],[<TIME>],[<OPEN>],[<HIGH>],[<LOW>],[<CLOSE>],[<VOL>]) values ('{dRow.ItemArray[0]}','{dRow.ItemArray[1]}','{dRow.ItemArray[2]}','{dRow.ItemArray[3]}','{dRow.ItemArray[4]}','{dRow.ItemArray[5]}',{dRow.ItemArray[6]})", connection);
                command.ExecuteNonQuery();
            }
        }

        public DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath, System.Text.Encoding.Default))

            {
                string[] headers = sr.ReadLine().Split(';');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(';');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
