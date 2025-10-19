﻿namespace VinhMicroservices.Model;

public record OrderMessage
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
