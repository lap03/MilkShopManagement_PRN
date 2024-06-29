using System;
using System.Collections.Generic;

namespace Reprository.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double Price { get; set; }

    public int QuantityInStock { get; set; }

    public bool IsActive { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; } = new List<TblOrderDetail>();
}
