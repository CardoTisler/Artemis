using System.Text.Json;
using Confluent.Kafka;

namespace ArtemisApi.Services;

public class KafkaProducerService : IDisposable
{
    private readonly IProducer<string, string> _producer;
    private const string Topic = "todos";

    public KafkaProducerService(IConfiguration configuration)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task SendMessageAsync<T>(T message)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var kafkamessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = jsonMessage
        };
        
        await _producer.ProduceAsync(Topic, kafkamessage);
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}