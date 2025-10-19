using Confluent.Kafka;
using Newtonsoft.Json;
using VinhMicroservices.Model;
using VinhMicroservices.ProductService.Data;

namespace VinhMicroservices.ProductService.Kafka;

public class KafkaConsumer(IServiceScopeFactory scopeFactory, IConfiguration configuration) : BackgroundService
{
    private readonly IConfiguration _configuration = configuration;
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string topic = _configuration["Kafka:Topic"] ?? "order-topic";
        return Task.Run(() => ConsumeAsync(topic, stoppingToken), stoppingToken);
    }

    public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
    {
        string groupId = _configuration["Kafka:groupId"] ?? "order-group";
        string bootstrapServers = _configuration["Kafka:BootstrapServers"] ?? "localhost:9092";

        var config = new ConsumerConfig
        {
            GroupId = groupId,
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        using var consumer = new ConsumerBuilder<string, string>(config).Build();

        consumer.Subscribe(topic);
        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);

            var order = JsonConvert.DeserializeObject<OrderMessage>(consumeResult.Message.Value);
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<VinhMicroProductDbContext>();

            var product = await dbContext.Products.FindAsync([order?.ProductId], cancellationToken: stoppingToken);
            if (product != null)
            {
                product.Quantity -= order?.Quantity ?? 0;
                await dbContext.SaveChangesAsync(stoppingToken);
            }
        }
        consumer.Close();
    }
}