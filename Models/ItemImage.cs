using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class ItemImage
{
    public int IdImage { get; set; }

    public int IdItem { get; set; }

    public bool IsMain { get; set; }

    public string UrlImage { get; set; } = null!;

    public virtual Item IdItemNavigation { get; set; } = null!;
}
