namespace SpendingSummary.Queue
{
    public class QueueConfigurations
    {
        public string BrokerName { get; set; }

        public string Host { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public string VirtualHost { get; set; }

        public bool DispatchConsumersAsync { get; set; }
    }
}
