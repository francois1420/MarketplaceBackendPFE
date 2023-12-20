using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Item
{
    public int IdItem { get; set; }

    public int IdBaseItem { get; set; }

    public string? Size { get; set; }

    public string? Color { get; set; }

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<CartLine> CartLines { get; } = new List<CartLine>();

    public virtual BaseItem IdBaseItemNavigation { get; set; } = null!;

    public virtual ICollection<ItemImage> ItemImages { get; } = new List<ItemImage>();

    public virtual ICollection<ItemUserView> ItemUserViews { get; } = new List<ItemUserView>();
}
