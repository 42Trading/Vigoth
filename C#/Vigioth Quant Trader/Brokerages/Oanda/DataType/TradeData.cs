﻿
using VigiothCapital.QuantTrader.Brokerages.Oanda.DataType.Communications;

namespace VigiothCapital.QuantTrader.Brokerages.Oanda.DataType
{
#pragma warning disable 1591
    /// <summary>
    /// Represents a Trade Data object containing the details of a trade.
    /// </summary>
    public class TradeData : Response
    {
        public long id { get; set; }
        public int units { get; set; }
        public string side { get; set; }
        public string instrument { get; set; }
        public string time { get; set; }
        public double price { get; set; }
        public double takeProfit { get; set; }
        public double stopLoss { get; set; }
        public int trailingStop { get; set; }
		public double trailingAmount { get; set; }
    }
#pragma warning restore 1591
}
