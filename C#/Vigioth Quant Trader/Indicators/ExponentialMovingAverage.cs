﻿

namespace VigiothCapital.QuantTrader.Indicators
{

    /// <summary>
    ///     Represents the traditional exponential moving average indicator (EMA)
    /// </summary>
    public class ExponentialMovingAverage : Indicator
    {
        private readonly decimal _k;
        private readonly int _period;

        /// <summary>Initializes a new instance of the ExponentialMovingAverage class with the specified name and period
        /// </summary>
        /// <param name="name">The name of this indicator</param>
        /// <param name="period">The period of the EMA</param>
        public ExponentialMovingAverage(string name, int period)
            : base(name)
        {
            _period = period;
            _k = ExponentialMovingAverage.SmoothingFactorDefault(period);
        }

        /// <summary>Initializes a new instance of the ExponentialMovingAverage class with the specified name and period
        /// </summary>
        /// <param name="name">The name of this indicator</param>
        /// <param name="period">The period of the EMA</param>
        /// <param name="smoothingFactor">The percentage of data from the previous value to be carried into the next value</param>
        public ExponentialMovingAverage(string name, int period, decimal smoothingFactor)
            : base(name)
        {
            _period = period;
            _k = smoothingFactor;
        }

        /// <summary>
        ///     Initializes a new instance of the ExponentialMovingAverage class with the default name and period
        /// </summary>
        /// <param name="period">The period of the EMA</param>
        public ExponentialMovingAverage(int period)
            : this("EMA" + period, period)
        {
        }

        /// <summary>Initializes a new instance of the ExponentialMovingAverage class with the default name and period
        /// </summary>
        /// <param name="period">The period of the EMA</param>
        /// <param name="smoothingFactor">The percentage of data from the previous value to be carried into the next value</param>
        public ExponentialMovingAverage(int period, decimal smoothingFactor)
            : this("EMA" + period, period, smoothingFactor)
        {
        }

        /// <summary>Calculates the default smoothing factor for an ExponentialMovingAverage indicator
        /// </summary>
        /// <param name="period">The period of the EMA</param>
        /// <returns>The default smoothing factor</returns>
        public static decimal SmoothingFactorDefault(int period)
        {
            return 2.0m / ((decimal) period + 1.0m);
        }

        /// <summary>
        ///     Gets a flag indicating when this indicator is ready and fully initialized
        /// </summary>
        public override bool IsReady
        {
            get { return Samples >= _period; }
        }

        /// <summary>
        ///     Computes the next value of this indicator from the given state
        /// </summary>
        /// <param name="input">The input given to the indicator</param>
        /// <returns>A new value for this indicator</returns>
        protected override decimal ComputeNextValue(IndicatorDataPoint input)
        {
            // our first data point just return identity
            if (Samples == 1)
            {
                return input;
            }
            return input*_k + Current*(1 - _k);
        }
    }
}