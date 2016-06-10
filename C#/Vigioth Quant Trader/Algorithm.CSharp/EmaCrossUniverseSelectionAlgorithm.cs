﻿

using System;
using System.Collections.Concurrent;
using System.Linq;
using VigiothCapital.QuantTrader.Data.Market;
using VigiothCapital.QuantTrader.Data.UniverseSelection;
using VigiothCapital.QuantTrader.Indicators;

namespace VigiothCapital.QuantTrader.Algorithm.CSharp
{
    /// <summary>
    /// In this algorithm we demonstrate how to perform some technical analysis as
    /// part of your coarse fundamental universe selection
    /// </summary>
    public class EmaCrossUniverseSelectionAlgorithm : QCAlgorithm
    {
        // tolerance to prevent bouncing
        const decimal Tolerance = 0.01m;
        private const int Count = 10;
        // use Buffer+Count to leave a little in cash
        private const decimal TargetPercent = 0.1m;
        private SecurityChanges _changes = SecurityChanges.None;
        // holds our coarse fundamental indicators by symbol
        private readonly ConcurrentDictionary<Symbol, SelectionData> _averages = new ConcurrentDictionary<Symbol, SelectionData>();


        // class used to improve readability of the coarse selection function
        private class SelectionData
        {
            public readonly ExponentialMovingAverage Fast;
            public readonly ExponentialMovingAverage Slow;

            public SelectionData()
            {
                Fast = new ExponentialMovingAverage(100);
                Slow = new ExponentialMovingAverage(300);
            }

            // computes an object score of how much large the fast is than the slow
            public decimal ScaledDelta
            {
                get { return (Fast - Slow)/((Fast + Slow)/2m); }
            }

            // updates the EMA50 and EMA100 indicators, returning true when they're both ready
            public bool Update(DateTime time, decimal value)
            {
                return Fast.Update(time, value) && Slow.Update(time, value);
            }
        }

        /// <summary>
        /// Initialise the data and resolution required, as well as the cash and start-end dates for your algorithm. All algorithms must initialized.
        /// </summary>
        public override void Initialize()
        {
            UniverseSettings.Leverage = 2.0m;
            UniverseSettings.Resolution = Resolution.Daily;

            SetStartDate(2010, 01, 01);
            SetEndDate(2015, 01, 01);
            SetCash(100*1000);

            AddUniverse(coarse =>
            {
                return (from cf in coarse
                        // grab th SelectionData instance for this symbol
                        let avg = _averages.GetOrAdd(cf.Symbol, sym => new SelectionData())
                        // Update returns true when the indicators are ready, so don't accept until they are
                        where avg.Update(cf.EndTime, cf.Price)
                        // only pick symbols who have their 50 day ema over their 100 day ema
                        where avg.Fast > avg.Slow*(1 + Tolerance)
                        // prefer symbols with a larger delta by percentage between the two averages
                        orderby avg.ScaledDelta descending 
                        // we only need to return the symbol and return 'Count' symbols
                        select cf.Symbol).Take(Count);
            });
        }

        /// <summary>
        /// OnData event is the primary entry point for your algorithm. Each new data point will be pumped in here.
        /// </summary>
        /// <param name="data">TradeBars dictionary object keyed by symbol containing the stock data</param>
        public void OnData(TradeBars data)
        {
            if (_changes == SecurityChanges.None) return;

            // liquidate securities removed from our universe
            foreach (var security in _changes.RemovedSecurities)
            {
                if (security.Invested)
                {
                    Liquidate(security.Symbol);
                }
            }

            // we'll simply go long each security we added to the universe
            foreach (var security in _changes.AddedSecurities)
            {
                SetHoldings(security.Symbol, TargetPercent);
            }
        }

        /// <summary>
        /// Event fired each time the we add/remove securities from the data feed
        /// </summary>
        /// <param name="changes">Object containing AddedSecurities and RemovedSecurities</param>
        public override void OnSecuritiesChanged(SecurityChanges changes)
        {
            _changes = changes;
        }
    }
}