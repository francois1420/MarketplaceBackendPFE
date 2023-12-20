using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Cart
{
    public int IdCart { get; set; }

    public int IdUser { get; set; }

    public bool IsBought { get; set; }

    public DateTime? BoughtDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<CartLine> CartLines { get; } = new List<CartLine>();

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
