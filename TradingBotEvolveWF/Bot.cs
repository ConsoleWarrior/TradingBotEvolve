using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TradingBotEvolveWF
{
    class Bot
    {
        string Name { get; set; }
        double Caсhe { get; set; } 
        int Genom { get; set; }
        List<double> Chart;
        double CurrentPrice { get; set; }
        List<Order> Orders;


        public List<double> LoadNextBar()
        {
            return Chart;
        }
        public void AnalyzeChart(List<double> chart)
        {
            if (true) AnalyzeOrders(Orders);
            if (true) this.OpenOrder(CalculateVolume(), true);
        }
        public void AnalyzeOrders(List<Order> orders)
        {
            foreach (var item in orders)
            {
                if (true) this.CloseOrder(CurrentPrice, item);
            }
            
        }
        public void OpenOrder(int volume, bool buyOrSell)
        {
            Order newOrder = new Order(CurrentPrice, volume, buyOrSell);
            Orders.Add(newOrder);
            Form1.PrintLog($"{Name} open new order ID={newOrder.OrderId}:{buyOrSell} {volume} for {CurrentPrice}");
            if (buyOrSell) Caсhe -= CurrentPrice;
            else Caсhe += CurrentPrice;
        }
        public void CloseOrder(double currentPrice, Order order)
        {
            if(order.BuyOrSell) Caсhe += CurrentPrice;
            else Caсhe -= CurrentPrice;
            Form1.PrintLog($"{Name} close order ID={order.OrderId}:{order.BuyOrSell} {order.Volume} for {CurrentPrice}. PROFIT pp={CurrentPrice-order.OpenPrice}");
            Orders.Remove(order);
        }
        public int CalculateVolume()
        {
            return 1;
        }
    }
}
