namespace ECommerce.Api.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal UnitPrice { get; set; }  
    public int Stock { get; set; }          
}