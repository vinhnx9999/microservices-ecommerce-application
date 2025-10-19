namespace VinhMicroservices.Model;

public record OrderModel
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
}
