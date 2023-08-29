namespace NotificationAPI.Producers;

public interface INotifyEventProducer
{
    Task ProduceAsync(string trigger, string message);
}