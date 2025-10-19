using Confluent.Kafka;
namespace VinhMicroservices.OrderService.Kafka;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;
    public KafkaProducer(IConfiguration configuration)
    {
        string groupId = configuration["Kafka:groupId"] ?? "order-group";
        string bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";

        var config = new ConsumerConfig
        {
            GroupId = groupId,
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync(string topic, Message<string, string> message)
    {
        await _producer.ProduceAsync(topic, message);
    }    
}
