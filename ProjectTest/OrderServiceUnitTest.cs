using Application.DTOs.Order;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ProjectTest
{
    public class OrderServiceUnitTest
    {
        [Fact]
        public async Task CreateOrder_ShouldCalculateTotalCorrectly()
        {
            
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb1")
                .Options;

            using var context = new AppDbContext(options);
            context.Users.Add(new User { Id = 1, Name = "Test", Email = "t@test.com", PasswordHash = "x" });
            await context.SaveChangesAsync();

            var auditService = new AuditService(context);
            var service = new OrderService(context, auditService);

            var request = new CreateOrderRequest
            {
                UserId = 1,
                Items =
            {
                new OrderItemDto { ProductName = "A", UnitPrice = 10, Quantity = 2 }, // 20
                new OrderItemDto { ProductName = "B", UnitPrice = 5, Quantity = 3 }   // 15
            }
            };

            // Act
            var result = await service.CreateOrderAsync(request);

            // Assert: total should be 35
            Assert.Equal(35, result.TotalPrice);
        } 

        [Fact]
        public async Task GetOrder_ShouldReturnOrder_WhenExists()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb2")
                .Options;

            using var context = new AppDbContext(options);
            context.Users.Add(new User { Id = 1, Name = "Test", Email = "t@test.com", PasswordHash = "x" });
            await context.SaveChangesAsync();

            var auditService = new AuditService(context);
            var service = new OrderService(context, auditService);

            var createRequest = new CreateOrderRequest
            {
                UserId = 1,
                Items = { new OrderItemDto { ProductName = "A", UnitPrice = 10, Quantity = 1 } }
            };

            var created = await service.CreateOrderAsync(createRequest);

            var fetched = await service.GetOrderAsync(created.Id);

            Assert.NotNull(fetched);
            Assert.Equal(created.Id, fetched!.Id);
        }

    }
}