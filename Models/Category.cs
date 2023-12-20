using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Name { get; set; } = null!;

    public string UrlImage { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<BaseItem> BaseItems { get; } = new List<BaseItem>();
}
