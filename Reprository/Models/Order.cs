using System;
using System.Collections.Generic;

namespace Reprository.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public double? Totalmoney { get; set; }

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; } = new List<TblOrderDetail>();

    public virtual User User { get; set; } = null!;
}
