using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradingBotEvolveWF
{
    class Bot
    {
        string Name { get; set; }
        double Caсhe { get; set; } 
        int Genom { get; set; }
        List<double> Chart = new List<double>();
        double CurrentPrice { get; set; }
        LinkedList<Order> Orders = new LinkedList<Order>();
        Form1 MyForm { get; set; }
        List<double> MyArray = new List<double>();
        int Tic = 0;
        int OrdersCount = 0;


        public Bot(string name, double cache, Form1 form, List<double> myArray)
        {
            Name = name;
            Caсhe = cache;
            MyForm = form;
            MyArray = myArray;
        }
        public void Start()
        {
            while (Tic<MyArray.Count)
            {
                //MyForm.PrintLog($"Tic: {Tic} ");
                LoadNextBar(MyArray);
                if (Orders.Count > 0)
                {
                    LinkedList<Order> TempOrders = new LinkedList<Order>(Orders);
                    AnalyzeOrders(TempOrders);
                }
                AnalyzeChart(Chart);
                //Thread.Sleep(1000);
            }

            MyForm.PrintLog2($"{Name} Кэш: {Caсhe}; Открытых сделок сейчас:{Orders.Count} на сумму: {Orders.Count*CurrentPrice}; Итого: {Caсhe+ (Orders.Count * CurrentPrice)} Сделок всего: {OrdersCount}");
        }

        public void LoadNextBar(List<double> mychart)
        {
            Chart.Add(mychart[Tic]); CurrentPrice = mychart[Tic]; Tic++;
        }

        public void AnalyzeOrders(LinkedList<Order> orders)
        {
            foreach (var item in orders)
            {
                if (CurrentPrice > item.OpenPrice*1.25) this.CloseOrder(CurrentPrice, item);
            }
            
        }

        public void AnalyzeChart(List<double> chart)
        {
            if (Caсhe > CurrentPrice) this.OpenOrder(CalculateVolume(), true);
        }


        public void OpenOrder(int volume, bool buyOrSell)
        {
            Order newOrder = new Order(CurrentPrice, volume, buyOrSell);
            Orders.AddLast(newOrder);
            MyForm.PrintLog($" {Name} open new order ID {newOrder.OrderId}: {(buyOrSell?"Buy":"Sell")} {volume} for {CurrentPrice}");
            if (buyOrSell) Caсhe -= CurrentPrice*volume;
            else Caсhe += CurrentPrice*volume;
            OrdersCount++;
        }
        public void CloseOrder(double currentPrice, Order order)
        {
            if(order.BuyOrSell) Caсhe += CurrentPrice*order.Volume;
            else Caсhe -= CurrentPrice*order.Volume;
            MyForm.PrintLog($" {Name} close order ID {order.OrderId}: {(order.BuyOrSell ? "Buy" : "Sell")} {order.Volume} for {CurrentPrice}. PROFIT = {CurrentPrice-order.OpenPrice}");
            Orders.Remove(order);
        }
        public int CalculateVolume()
        {

            return 1;//(int)(Caсhe / CurrentPrice);
        }
    }
}
