using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TradingBotEvolveWF
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //SqlConnection connectionDB = new SqlConnection(ConfigurationManager.ConnectionStrings["QuotesTemp"].ConnectionString);////name ="QuotesTemp"
            //connectionDB.Open();
            //if (connectionDB.State == ConnectionState.Open)
            //    Form1.PrintLog("бд подключен", form1);
            //else Form1.PrintLog("бд НЕ подключен", form1);
            //form1.PrintLog("бд НЕ подключен");
            //form1.textBox1.Text += "gopa";

            //Bot bot1= new Bot("Bot1", 1000, form1);
            //bot1.Start();



        }
    }
}
