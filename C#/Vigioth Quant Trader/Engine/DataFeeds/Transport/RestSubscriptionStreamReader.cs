﻿

using System;
using VigiothCapital.QuantTrader.Logging;
using RestSharp;

namespace VigiothCapital.QuantTrader.Engine.DataFeeds.Transport
{
    /// <summary>
    /// Represents a stream reader capable of polling a rest client
    /// </summary>
    public class RestSubscriptionStreamReader : IStreamReader
    {
        private readonly RestClient _client;
        private readonly RestRequest _request;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestSubscriptionStreamReader"/> class.
        /// </summary>
        /// <param name="source">The source url to poll with a GET</param>
        public RestSubscriptionStreamReader(string source)
        {
            _client = new RestClient(source);
            _request = new RestRequest(Method.GET);
        }

        /// <summary>
        /// Gets <see cref="SubscriptionTransportMedium.Rest"/>
        /// </summary>
        public SubscriptionTransportMedium TransportMedium
        {
            get { return SubscriptionTransportMedium.Rest; }
        }

        /// <summary>
        /// Gets whether or not there's more data to be read in the stream
        /// </summary>
        public bool EndOfStream
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the next line/batch of content from the stream 
        /// </summary>
        public string ReadLine()
        {
            try
            {
                var response = _client.Execute(_request);
                if (response != null)
                {
                    return response.Content;
                }
            }
            catch (Exception err)
            {
                Log.Error(err);
            }

            return string.Empty;
        }

        /// <summary>
        /// This stream reader doesn't require disposal
        /// </summary>
        public void Dispose()
        {
        }
    }
}