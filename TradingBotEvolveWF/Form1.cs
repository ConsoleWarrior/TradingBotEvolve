using System;
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
        WorkWithDB workWithDB = new WorkWithDB();
        SqlConnection sqlexpress01;
        List<double> botArray = new List<double>();
        public StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        int botNumber = 0;
        double cache = 1000;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            sqlexpress01 = workWithDB.ConnectToDB("Data Source=DESKTOP-GLVT5QN\\SQLEXPRESS01;Initial Catalog=tempdb;Integrated Security=True", this);
            //SqlConnection quotesBD = workWithDB.ConnectToDB("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Oleg PK SSD\\Documents\\GitHub\\TradingBotEvolve\\TradingBotEvolveWF\\QuotesBD.mdf\";Integrated Security=True", this);
            //workWithDB.InsertIntoDB(quotesBD, dataSet, "SP500");
            DataSet dataSet2 = new DataSet();
            SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT name FROM sys.objects WHERE type in (N'U')", sqlexpress01);//заполняем комбобокс списком таблиц
            adapter2.Fill(dataSet2);
            foreach (DataRow item in dataSet2.Tables[0].Rows)
            {
                comboBox1.Items.Add(item.ItemArray[0]);
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
            if(comboBox1.Text != null)
            {
                BotStartAsync(new Bot($"BOT{botNumber}", cache, this, botArray, textBox5.Text == "" ? 0 : Convert.ToInt32(textBox5.Text), textBox6.Text == "" ? 0 : Convert.ToInt32(textBox6.Text)));
                botNumber++;
            }

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            botArray.Clear();
            DataSet dataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {comboBox1.Text}", sqlexpress01);
            adapter.Fill(dataSet);
            foreach (DataRow dRow in dataSet.Tables[0].Rows)
            {
                botArray.Add(Convert.ToDouble(dRow.ItemArray[6]));
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            cache = Convert.ToDouble(textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sb2.Clear(); textBox2.Clear();
        }
    }
}
