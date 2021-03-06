﻿
using System;
using VigiothCapital.QuantTrader.Data;
using VigiothCapital.QuantTrader.Data.Market;
namespace VigiothCapital.QuantTrader
{
    /// <summary>
    /// Provides static properties to be used as selectors with the indicator system
    /// </summary>
    public static partial class Field
    {
        /// <summary>
        /// Gets a selector that selects the Open value
        /// </summary>
        public static Func<BaseData, decimal> Open
        {
            get { return TradeBarPropertyOrValue(x => x.Open); }
        }
        /// <summary>
        /// Gets a selector that selects the High value
        /// </summary>
        public static Func<BaseData, decimal> High
        {
            get { return TradeBarPropertyOrValue(x => x.High); }
        }
        /// <summary>
        /// Gets a selector that selects the Low value
        /// </summary>
        public static Func<BaseData, decimal> Low
        {
            get { return TradeBarPropertyOrValue(x => x.Low); }
        }
        /// <summary>
        /// Gets a selector that selects the Close value
        /// </summary>
        public static Func<BaseData, decimal> Close
        {
            get { return x => x.Value; }
        }
        /// <summary>
        /// Defines an average price that is equal to (O + H + L + C) / 4
        /// </summary>
        public static Func<BaseData, decimal> Average
        {
            get { return TradeBarPropertyOrValue(x => (x.Open + x.High + x.Low + x.Close) / 4m); }
        }
        /// <summary>
        /// Defines an average price that is equal to (H + L) / 2
        /// </summary>
        public static Func<BaseData, decimal> Median
        {
            get { return TradeBarPropertyOrValue(x => (x.High + x.Low) / 2m); }
        }
        /// <summary>
        /// Defines an average price that is equal to (H + L + C) / 3
        /// </summary>
        public static Func<BaseData, decimal> Typical
        {
            get { return TradeBarPropertyOrValue(x => (x.High + x.Low + x.Close) / 3m); }
        }
        /// <summary>
        /// Defines an average price that is equal to (H + L + 2*C) / 4
        /// </summary>
        public static Func<BaseData, decimal> Weighted
        {
            get { return TradeBarPropertyOrValue(x => (x.High + x.Low + 2 * x.Close) / 4m); }
        }
        /// <summary>
        /// Defines an average price that is equal to (2*O + H + L + 3*C)/7
        /// </summary>
        public static Func<BaseData, decimal> SevenBar
        {
            get { return TradeBarPropertyOrValue(x => (2*x.Open + x.High + x.Low + 3*x.Close)/7m); }
        }
        /// <summary>
        /// Gets a selector that selectors the Volume value
        /// </summary>
        public static Func<BaseData, decimal> Volume
        {
            get { return TradeBarPropertyOrValue(x => x.Volume, x => 0m); }
        }
        private static Func<BaseData, decimal> TradeBarPropertyOrValue(Func<TradeBar, decimal> selector, Func<BaseData, decimal> defaultSelector = null)
        {
            return x =>
            {
                var bar = x as TradeBar;
                if (bar != null)
                {
                    return selector(bar);
                }
                defaultSelector = defaultSelector ?? (data => data.Value);
                return defaultSelector(x);
            };
        }
    }
}
