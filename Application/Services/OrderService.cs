using Application.Contracts.Services;
using Application.DTOs.Order;
using Domain.Entities;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IAuditService _auditService;

        // In Real project we should make Repository and unit of work ...
        public OrderService(AppDbContext context, IAuditService auditService)
        {
             _context = context;
            _auditService = auditService;   
        }
        // In real project we will add standard response and using Try Catch ...
        public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
        {
            // check user exists 

            var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId);
            if (!userExists)
                throw new Exception("User not found");

            var order = new Order()
            {
                UserId = request.UserId,    

            };

            foreach (var itemDto in request.Items)
            {
                var items = new OrderItem()
                {
                    ProductName = itemDto.ProductName,
                    UnitPrice = itemDto.UnitPrice,
                    Quantity = itemDto.Quantity
                };
                order.Items.Add(items); 
            }

            order.TotalPrice = order.Items.Sum(i => i.UnitPrice * i.Quantity);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await _auditService.LogAsync("CreateOrder", "Order", order.Id,
          $"Order created for UserId={order.UserId} with Total={order.TotalPrice}");

            return new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };


        }

        public async Task<OrderResponse?> GetOrderAsync(int id)
        {
            var order =  _context.Orders.Include(i=>i.Items).FirstOrDefault(i=>i.Id == id);
            if (order == null) return null;


            return new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
