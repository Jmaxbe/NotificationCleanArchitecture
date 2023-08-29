using KafkaFlow;
using KafkaFlow.TypedHandler;
using NotificationAPI.Models.Kafka;

namespace NotificationAPI.Handlers;

public class PrintConsoleHandler : IMessageHandler<TestMessage>
{
    private readonly ILogger<PrintConsoleHandler> _logger;

    public PrintConsoleHandler(ILogger<PrintConsoleHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(IMessageContext context, TestMessage message)
    {
        Console.WriteLine(
            "Partition: {0} | Offset: {1} | Message: {2}",
            context.ConsumerContext.Partition,
            context.ConsumerContext.Offset,
            message.Text);
        
        _logger.LogInformation("Sended message: {0}", message.Text);

        return Task.CompletedTask;
    }
}