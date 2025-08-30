using ECommerce.Api.DTOs;
using ECommerce.Api.Models;

namespace ECommerce.Api.Services;

public interface IOrderService
{
    Task<Order> CreateAsync(CreateOrderDto dto);
    Task<List<Order>> GetByCustomerAsync(int customerId);
    Task<Order?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id, bool restock = true);
}