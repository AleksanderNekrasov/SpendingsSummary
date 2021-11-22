using SpendingSummary.Common.Models;
using System;
using System.Collections.Generic;

namespace SpendingSummary.Queue
{
    public static class EventDefinitions
    {
        public static string DataParsedQueue = "data_parsed_queue";
        public static string DataParcedExchange = "data_parsed_exchange";

        public static Dictionary<Type, (string queue, string exchange)> ByEventType = new()
        {
            [typeof(DataParsedEvent)] = (DataParsedQueue, DataParcedExchange)
        };
    }
}
