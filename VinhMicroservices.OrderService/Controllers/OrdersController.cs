using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VinhMicroservices.Model;
using VinhMicroservices.OrderService.Data;
using VinhMicroservices.OrderService.Kafka;

namespace VinhMicroservices.OrderService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(VinhMicroOrderDbContext context, IKafkaProducer producer, IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<ActionResult<OrderModel>> PostOrder(OrderModel order)
    {
        order.OrderDate = DateTime.UtcNow;
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        var orderMessage = new OrderMessage
        {
            OrderId = order.Id,
            ProductId = order.ProductId,
            Quantity = order.Quantity
        };

        string topic = _configuration["Kafka:Topic"] ?? "order-topic";
        await producer.ProduceAsync(topic, new Message<string, string>
        {
            Key = order.Id.ToString(),
            Value = JsonSerializer.Serialize(orderMessage)
        });

        return order;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderModel>>> GetOrder()
    {
        return await context.Orders.ToListAsync();
    }
}
