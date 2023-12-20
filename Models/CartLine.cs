using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class CartLine
{
    public int IdCart { get; set; }

    public int IdItem { get; set; }

    public int Quantity { get; set; }

    public bool IsPackageSent { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Cart IdCartNavigation { get; } = null!;

    public virtual Item IdItemNavigation { get; } = null!;
}
