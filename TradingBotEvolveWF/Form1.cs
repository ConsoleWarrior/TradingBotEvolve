﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace TradingBotEvolveWF
{
    public partial class Form1 : Form
    {

        //SqlDataAdapter adapter;
        DataSet dataSet = new DataSet();
        List<double> myArray = new List<double>();
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        int botNumber = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string temp = ConfigurationManager.ConnectionStrings["QuotesTemp"].ConnectionString;
            //if(temp == null)PrintLog("empty");

            //SqlConnection connectionDB = new SqlConnection("Data Source=DESKTOP-GLVT5QN\\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True");////name ="QuotesTemp"
            //connectionDB.Open();
            //if (connectionDB.State == ConnectionState.Open) PrintLog("бд подключен");
            //else PrintLog("бд НЕ подключен");

            WorkWithDB workWithDB= new WorkWithDB();
            SqlConnection sqlexpress01 = workWithDB.ConnectToDB("Data Source=DESKTOP-GLVT5QN\\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True", this);
            SqlConnection quotesBD = workWithDB.ConnectToDB("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Oleg PK SSD\\Documents\\GitHub\\TradingBotEvolve\\TradingBotEvolveWF\\QuotesBD.mdf\";Integrated Security=True", this);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Brent2018", sqlexpress01);
            adapter.Fill(dataSet);
            SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT * FROM Brent2018", quotesBD);
            //adapter2.Fill(dataSet);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter2);
            adapter2.UpdateCommand = builder.GetUpdateCommand();
            adapter2.Update(dataSet);


            //foreach (DataRow dRow in dataSet.Tables[0].Rows)
            //{
            //    myArray.Add(Convert.ToDouble(dRow.ItemArray[6]));
            //}
            foreach (DataRow dRow in dataSet.Tables[0].Rows)
            {
                myArray.Add(Convert.ToDouble(dRow.ItemArray[6]));
            }

        }
        public void PrintLog(string message)
        {
            sb1.AppendLine(message);
            textBox1.Invoke(new Action(() => textBox1.Text = sb1.ToString()));
        }
        public void PrintLog2(string message)
        {
            sb2.AppendLine(message);
            textBox1.Invoke(new Action(() => textBox2.Text = sb2.ToString()));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BotStartAsync(new Bot($"Bot{botNumber}", 1000, this, myArray));
            botNumber++;
        }
        async Task BotStartAsync(Bot bot)
        {
            //Bot bot1 = new Bot("Bot1", 1000, this, myArray);
            await Task.Run(()=>bot.Start()); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sb1.Clear(); textBox1.Clear();
        }
    }
}
