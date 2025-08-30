using ECommerce.Api.Data;
using ECommerce.Api.DTOs;
using ECommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db) => _db = db;

    public async Task<Order> CreateAsync(CreateOrderDto dto)
    {
        if (dto.Items == null || dto.Items.Count == 0)
            throw new InvalidOperationException("Sipariş kalemi yok.");

        var ids = dto.Items.Select(i => i.ProductId).Distinct().ToList();
        var products = await _db.Products.Where(p => ids.Contains(p.Id)).ToListAsync();

        if (products.Count != ids.Count)
            throw new KeyNotFoundException("Bazı ürünler bulunamadı.");

        using var tx = await _db.Database.BeginTransactionAsync();

        foreach (var item in dto.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);

            if (item.Quantity <= 0)
                throw new InvalidOperationException("Adet 1 veya daha büyük olmalı.");

            if (product.Stock < item.Quantity)
                throw new InvalidOperationException($"Stok yetersiz: {product.Name}");

            product.Stock -= item.Quantity;
        }

        var order = new Order
        {
            CustomerId = dto.CustomerId,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var item in dto.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);
            var unit = product.UnitPrice;
            var line = unit * item.Quantity;

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = unit,
                LineTotal = line
            });

            order.TotalAmount += line;
        }

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        await tx.CommitAsync();

        return order;
    }

    public Task<List<Order>> GetByCustomerAsync(int customerId)
    {
        return _db.Orders
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public Task<Order?> GetByIdAsync(int id)
    {
        return _db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<bool> DeleteAsync(int id, bool restock = true)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return false;

        if (restock)
        {
            var productIds = order.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _db.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            foreach (var item in order.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);
                product.Stock += item.Quantity; // basit iade/iptal senaryosu
            }
        }

        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
        return true;
    }
}