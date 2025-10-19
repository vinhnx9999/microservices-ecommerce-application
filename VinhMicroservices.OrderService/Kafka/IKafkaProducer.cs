using Confluent.Kafka;
namespace VinhMicroservices.OrderService.Kafka;
public interface IKafkaProducer
{
    Task ProduceAsync(string topic, Message<string, string> message);
}
