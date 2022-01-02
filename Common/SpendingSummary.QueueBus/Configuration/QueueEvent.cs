namespace SpendingSummary.QueueBus.Configuration
{
    public  class QueueEvent
    {
        public string Name { get; set; }

        public string Queue { get; set; }

        public string Exchange { get; set; }
    }
}
