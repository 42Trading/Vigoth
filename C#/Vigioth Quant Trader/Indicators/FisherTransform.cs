﻿
using VigiothCapital.QuantTrader.Data.Market;
using System;

namespace VigiothCapital.QuantTrader.Indicators
{
    /// <summary>
    /// The Fisher transform is a mathematical process which is used to convert any data set to a modified
    /// data set whose Probabilty Distrbution Function is approximately Gaussian.  Once the Fisher transform
    /// is computed, the transformed data can then be analyzed in terms of it's deviation from the mean.
    /// 
    /// The equation is y = .5 * ln [ 1 + x / 1 - x ] where
    /// x is the input
    /// y is the output
    /// ln is the natural logarithm
    /// 
    /// The Fisher transform has much sharper turning points than other indicators such as MACD
    /// 
    /// For more info, read chapter 1 of Cybernetic Analysis for Stocks and Futures by John F. Ehlers
    /// 
    /// We are implementing the lastest version of this indicator found at Fig. 4 of
    /// http://www.mesasoftware.com/papers/UsingTheFisherTransform.pdf
    /// 
    /// </summary>
    public class FisherTransform : TradeBarIndicator
    {
        private double _alpha;
        private double _previous;
        private readonly Minimum _medianMin;
        private readonly Maximum _medianMax;

        /// <summary>
        ///     Initializes a new instance of the FisherTransform class with the default name and period
        /// </summary>
        /// <param name="period">The period of the WMA</param>
        public FisherTransform(int period)
            : this("FISH_" + period, period)
        {
        }

        /// <summary>
        /// A Fisher Transform of Prices
        /// </summary>
        /// <param name="name">string - the name of the indicator</param>
        /// <param name="period">The number of periods for the indicator</param>
        public FisherTransform(string name, int period)
            : base(name)
        {
            _alpha = .33;

            // Initialize the local variables
            _medianMax = new Maximum("MedianMax", period);
            _medianMin = new Minimum("MedianMin", period);
        }

        /// <summary>
        /// Gets a flag indicating when this indicator is ready and fully initialized
        /// </summary>
        public override bool IsReady
        {
            get { return _medianMax.IsReady && _medianMax.IsReady; }
        }

        /// <summary>
        /// Computes the next value in the transform. 
        /// value1 is a function used to normalize price withing the last _period day range.
        /// value1 is centered on its midpoint and then doubled so that value1 wil swing between -1 and +1.  
        /// value1 is also smoothed with an exponential moving average whose alpha is 0.33.  
        /// 
        /// Since the smoothing may allow value1 to exceed the _period day price range, limits are introduced to 
        /// preclude the transform from blowing up by having an input larger than unity.
        /// </summary>
        /// <param name="input">IndicatorDataPoint - the time and value of the next price</param>
        /// <returns></returns>
        protected override decimal ComputeNextValue(TradeBar input)
        {
            var x = 0.0;
            var y = 0.0;
            var price = (input.Low + input.High) / 2m;
            _medianMin.Update(input.Time, price);
            _medianMax.Update(input.Time, price);

            if (!IsReady) return 0;

            var minL = _medianMin.Current.Value;
            var maxH = _medianMax.Current.Value;
            
            if (minL != maxH)
            {
                x = _alpha * 2 * ((double)((price - minL) / (maxH - minL)) - .5) + (1 - _alpha) * _previous;
                y = FisherTransformFunction(x);
            }
            _previous = x;

            return Convert.ToDecimal(y) + .5m * Current.Value;
        }

        /// <summary>
        /// The Fisher transform is a mathematical process which is used to convert any data set to a modified
        /// data set whose Probabilty Distrbution Function is approximately Gaussian.  Once the Fisher transform
        /// is computed, the transformed data can then be analyzed in terms of it's deviation from the mean.
        /// 
        /// The equation is y = .5 * ln [ 1 + x / 1 - x ] where
        /// x is the input
        /// y is the output
        /// ln is the natural logarithm
        /// 
        /// The Fisher transform has much sharper turning points than other indicators such as MACD
        /// 
        /// For more info, read chapter 1 of Cybernetic Analysis for Stocks and Futures by John F. Ehlers
        /// </summary>
        /// <param name="x">Input</param>
        /// <returns>Output</returns>
        private double FisherTransformFunction(double x)
        {
            if (x > .99)
            {
                x = .999;
            }
            if (x < -.99)
            {
                x = -.999;
            }

            return .5 * Math.Log((1.0 + x) / (1.0 - x));
        }
    }
}
