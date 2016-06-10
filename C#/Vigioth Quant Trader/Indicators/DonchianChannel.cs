﻿

using VigiothCapital.QuantTrader.Data.Market;

namespace VigiothCapital.QuantTrader.Indicators
{
    /// <summary>
    /// This indicator computes the upper and lower band of the Donchian Channel.
    /// The upper band is computed by finding the highest high over the given period.
    /// The lower band is computed by finding the lowest low over the given period.
    /// The primary output value of the indicator is the mean of the upper and lower band for 
    /// the given timeframe.
    /// </summary>
    public class DonchianChannel : TradeBarIndicator
    {
        private TradeBar _previousInput;
        /// <summary>
        /// Gets the upper band of the Donchian Channel.
        /// </summary>
        public IndicatorBase<IndicatorDataPoint> UpperBand { get; private set; }

        /// <summary>
        /// Gets the lower band of the Donchian Channel.
        /// </summary>
        public IndicatorBase<IndicatorDataPoint> LowerBand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DonchianChannel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="period">The period for both the upper and lower channels.</param>
        public DonchianChannel(string name, int period)
            : this(name, period, period)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DonchianChannel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="upperPeriod">The period for the upper channel.</param>
        /// <param name="lowerPeriod">The period for the lower channel</param>
        public DonchianChannel(string name, int upperPeriod, int lowerPeriod)
            : base(name)
        {
            UpperBand = new Maximum(name + "_UpperBand", upperPeriod);
            LowerBand = new Minimum(name + "_LowerBand", lowerPeriod);
        }

        /// <summary>
        /// Gets a flag indicating when this indicator is ready and fully initialized
        /// </summary>
        public override bool IsReady
        {
            get { return UpperBand.IsReady && LowerBand.IsReady; }
        }

        /// <summary>
        /// Computes the next value of this indicator from the given state
        /// </summary>
        /// <param name="input">The input given to the indicator</param>
        /// <returns>A new value for this indicator, which by convention is the mean value of the upper band and lower band.</returns>
        protected override decimal ComputeNextValue(TradeBar input)
        {
            if (_previousInput != null)
            {
                UpperBand.Update(new IndicatorDataPoint(_previousInput.Time, _previousInput.High));
                LowerBand.Update(new IndicatorDataPoint(_previousInput.Time, _previousInput.Low));
            }

            _previousInput = input;
            return (UpperBand.Current.Value + LowerBand.Current.Value) / 2;
        }

        /// <summary>
        /// Resets this indicator to its initial state
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            UpperBand.Reset();
            LowerBand.Reset();
        }
    }
}
