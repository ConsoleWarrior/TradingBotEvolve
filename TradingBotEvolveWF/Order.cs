using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBotEvolveWF
{
    class Order
    {
        public static int Id = 0;
        public int OrderId { get; set; }
        public double OpenPrice { get; set; }
        //double ClosePrice { get; set; }
        public int Volume { get; set; }
        public bool BuyOrSell { get; set; }
        public double DecreaseCache { get; set; }
        public Order(double openPrice, int volume, bool buyOrSell)
        {
            OpenPrice = openPrice;
            Volume = volume;
            BuyOrSell = buyOrSell;
            Id++;
            OrderId = Id;
            DecreaseCache = OpenPrice * Volume;
        }
    }
}
//CREATE TABLE QuotesBD.Brent2018 LIKE desktop-glvt5qn\sqlexpress01.tempdb.Brent2018