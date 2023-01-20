﻿using System;
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
        public Order(double openPrice, int volume, bool buyOrSell)
        {
            OpenPrice = openPrice;
            Volume = volume;
            BuyOrSell = buyOrSell;
            Id++;
            OrderId = Id;
        }
    }
}