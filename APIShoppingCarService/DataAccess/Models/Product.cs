using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageBase64 { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; } = new List<ShoppingCartItem>();
}
