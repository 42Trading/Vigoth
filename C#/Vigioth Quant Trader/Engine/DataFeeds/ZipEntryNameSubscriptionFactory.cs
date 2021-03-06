﻿

using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using VigiothCapital.QuantTrader.Data;

namespace VigiothCapital.QuantTrader.Engine.DataFeeds
{
    /// <summary>
    /// Provides an implementation of <see cref="ISubscriptionFactory"/> that reads zip entry names
    /// </summary>
    public class ZipEntryNameSubscriptionFactory : ISubscriptionFactory
    {
        private readonly SubscriptionDataConfig _config;
        private readonly DateTime _dateTime;
        private readonly bool _isLiveMode;
        private readonly BaseData _factory;

        /// <summary>
        /// Event fired when the specified source is considered invalid, this may
        /// be from a missing file or failure to download a remote source
        /// </summary>
        public event EventHandler<InvalidSourceEventArgs> InvalidSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipEntryNameSubscriptionFactory"/> class
        /// </summary>
        /// <param name="config">The subscription's configuration</param>
        /// <param name="dateTime">The date this factory was produced to read data for</param>
        /// <param name="isLiveMode">True if we're in live mode, false for backtesting</param>
        public ZipEntryNameSubscriptionFactory(SubscriptionDataConfig config, DateTime dateTime, bool isLiveMode)
        {
            _config = config;
            _dateTime = dateTime;
            _isLiveMode = isLiveMode;
            _factory = (BaseData) Activator.CreateInstance(config.Type);
        }

        /// <summary>
        /// Reads the specified <paramref name="source"/>
        /// </summary>
        /// <param name="source">The source to be read</param>
        /// <returns>An <see cref="IEnumerable{BaseData}"/> that contains the data in the source</returns>
        public IEnumerable<BaseData> Read(SubscriptionDataSource source)
        {
            if (!File.Exists(source.Source))
            {
                OnInvalidSource(source, new FileNotFoundException("The specified file was not found", source.Source));
            }

            ZipFile zip;
            try
            {
                zip = new ZipFile(source.Source);
            }
            catch (ZipException err)
            {
                OnInvalidSource(source, err);
                yield break;
            }

            foreach (var entryFileName in zip.EntryFileNames)
            {
                yield return _factory.Reader(_config, entryFileName, _dateTime, _isLiveMode);
            }
        }

        /// <summary>
        /// Event invocator for the <see cref="InvalidSource"/> event
        /// </summary>
        /// <param name="source">The <see cref="SubscriptionDataSource"/> that was invalid</param>
        /// <param name="exception">The exception if one was raised, otherwise null</param>
        private void OnInvalidSource(SubscriptionDataSource source, Exception exception)
        {
            var handler = InvalidSource;
            if (handler != null) handler(this, new InvalidSourceEventArgs(source, exception));
        }
    }
}
