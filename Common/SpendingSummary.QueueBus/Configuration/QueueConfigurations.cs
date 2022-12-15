namespace SpendingSummary.QueueBus.Configuration
{
    public sealed record QueueConfigurations (string Host, int Port, string Password, string Username);
}
