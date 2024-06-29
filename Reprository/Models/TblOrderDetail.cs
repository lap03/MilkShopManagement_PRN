using System;
using System.Collections.Generic;

namespace Reprository.Models;

public partial class TblOrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public double Price { get; set; }

    public int? Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
