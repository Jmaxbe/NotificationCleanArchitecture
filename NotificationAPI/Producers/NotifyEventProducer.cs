using Confluent.Kafka;
using KafkaFlow;

namespace NotificationAPI.Producers;

public class NotifyEventProducer :INotifyEventProducer
{
    private readonly IMessageProducer<NotifyEventProducer> _producer;

    public NotifyEventProducer(IMessageProducer<NotifyEventProducer> producer)
    {
        _producer = producer;
    }


    public async Task ProduceAsync(string trigger, string message)
    {
        await _producer.ProduceAsync((object)Guid.NewGuid().ToString(), message, new MessageHeaders(new Headers()
        {
            {
                "trigger", System.Text.Encoding.UTF8.GetBytes(trigger)
            }
        }));
    }
}