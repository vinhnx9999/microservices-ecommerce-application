var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.VinhMicroservices_ProductService>("vinhmicroservices-productservice");

builder.AddProject<Projects.VinhMicroservices_OrderService>("vinhmicroservices-orderservice");

builder.AddProject<Projects.VinhMicroservices_Client>("vinhmicroservices-client");

builder.Build().Run();
