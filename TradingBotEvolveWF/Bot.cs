using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        double AllCache;
        double MaxAllCache = 0;
        int TakeProfit { get; set; }
        int StopLoss { get; set; }
        int LossOrders = 0;
        int ProfitOrders = 0;


        public Bot(string name, double cache, Form1 form, List<double> myArray, int takeProfit, int stopLoss)
        {
            Name = name;
            Caсhe = cache;
            MyForm = form;
            MyArray = myArray;
            TakeProfit = takeProfit;
            StopLoss = stopLoss;
        }
        public void Start()
        {
            while (Tic<MyArray.Count)
            {
                LoadNextBar(MyArray);
                AllCache = CalculateAllCache();
                
                if (Orders.Count > 0)
                {
                    LinkedList<Order> TempOrders = new LinkedList<Order>(Orders);
                    AnalyzeOrders(TempOrders);
                }
                if(Tic > 10) AnalyzeChart(Chart);
            }
            MyForm.PrintLog("the end");
            MyForm.PrintLog2($"{Name} Текущая цена: {CurrentPrice}. Открытые сделки:");
            double sum = 0;
            foreach (Order item in Orders)
            {
                sum += (item.BuyOrSell ? CurrentPrice * item.Volume : item.Volume * (2 * item.OpenPrice - CurrentPrice));
                MyForm.PrintLog2($"ID {item.OrderId} тип {(item.BuyOrSell ? "Buy" : "Sell")} {item.Volume} за {item.OpenPrice} стоимостью сейчас: {(item.BuyOrSell ? CurrentPrice*item.Volume : item.Volume*(2*item.OpenPrice - CurrentPrice))}");
            }
            MyForm.PrintLog2($"{Name} Кэш: {Math.Round(Caсhe,2)}; Открытых сделок сейчас:{Orders.Count} на сумму: {sum}; Максимальная стоимость: {MaxAllCache} Сделок всего: {OrdersCount}; прибыльных: {ProfitOrders}; убыточных: {LossOrders} ИТОГО: { Math.Round(AllCache, 2)}");
            MyForm.PrintLog2("------------------------------------------------------------------------------------");
                //Thread.Sleep(1000);
        }

        public void LoadNextBar(List<double> mychart)
        {
            Chart.Add(mychart[Tic]); CurrentPrice = mychart[Tic]; Tic++;
        }

        public void AnalyzeOrders(LinkedList<Order> orders)
        {
            foreach (var item in orders)
            {
                if (item.BuyOrSell)
                {
                    if (TakeProfit != 0 && CurrentPrice > item.OpenPrice * (100+TakeProfit)/100) this.CloseOrder(CurrentPrice, item, "TakeProfit");
                    if (StopLoss != 0 && CurrentPrice < item.OpenPrice * (100-StopLoss)/100) this.CloseOrder(CurrentPrice, item, "StopLoss ");
                }
                else
                {
                    if (TakeProfit != 0 && CurrentPrice < item.OpenPrice * (100-TakeProfit)/100) this.CloseOrder(CurrentPrice, item, "TakeProfit");
                    if (StopLoss != 0 && CurrentPrice > item.OpenPrice * (100+StopLoss)/100) this.CloseOrder(CurrentPrice, item, "StopLoss ");
                }
            }
        }

        public void AnalyzeChart(List<double> chart)
        {
            if (Caсhe > AllCache / 5 && Math.Floor(AllCache / 5 / CurrentPrice)>0)
            {
                if(this.MyForm.checkSell)
                if (chart[chart.Count - 5] < CurrentPrice && chart[chart.Count - 10] < CurrentPrice) this.OpenOrder(CalculateVolume(), false);
                if (chart[chart.Count - 5] > CurrentPrice && chart[chart.Count - 10] > CurrentPrice) this.OpenOrder(CalculateVolume(), true);
            }
        }


        public void OpenOrder(int volume, bool buyOrSell)
        {
            Order newOrder = new Order(CurrentPrice, volume, buyOrSell);
            Orders.AddLast(newOrder);
            //if (buyOrSell) Caсhe -= CurrentPrice*volume;
            //else Caсhe += CurrentPrice*volume;
            Caсhe -= CurrentPrice * volume;
            MyForm.sb1.AppendLine($"Open new ID: {newOrder.OrderId}; {(buyOrSell ? "Buy" : "Sell")} {volume} for {CurrentPrice}");
            OrdersCount++;
        }
        public void CloseOrder(double currentPrice, Order order, string status)
        {
            double result;
            if (order.BuyOrSell) result = CurrentPrice * order.Volume;
            else result = order.Volume * (2 * order.OpenPrice - CurrentPrice);
            Caсhe += result;
            if ((result - order.PaidCache) > 0) ProfitOrders++;
            else LossOrders++;
            MyForm.sb1.AppendLine($"{status} ID: {order.OrderId}; {(order.BuyOrSell ? "Buy" : "Sell")} {order.Volume} for {order.OpenPrice} to {CurrentPrice};{(result - order.PaidCache>0? " PROFIT" : " LOSS")} = {Math.Round((result-order.PaidCache),2)}");
            Orders.Remove(order);
        }
        public int CalculateVolume()
        {
            //if (AllCache / 5 < Caсhe) return (int)Math.Floor(AllCache/5/CurrentPrice);
            //else return 0;
            return (int)Math.Floor(AllCache / 5 / CurrentPrice);
        }
        public double CalculateAllCache()
        {
            double sum = Caсhe;
            foreach(Order order in Orders)
            {
                if (order.BuyOrSell) sum += order.Volume*CurrentPrice;
                else sum += order.Volume*(2*order.OpenPrice - CurrentPrice);
            }
            if(sum > MaxAllCache) MaxAllCache = sum;
            return sum;
        }
    }
}
