using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingBotEvolveWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static void PrintLog(string message, Form1 f)
        {
            //f.listView1.Text += Environment.NewLine + message;
            f.listView1.Items.Add(message);
            f.listView1.Items.Add(Environment.NewLine);
            //f.listView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintLog("sssss", this);
        }
    }
}
