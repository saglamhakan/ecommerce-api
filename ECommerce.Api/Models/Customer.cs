namespace ECommerce.Api.Models;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
}