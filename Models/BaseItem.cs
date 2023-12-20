using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class BaseItem
{
    public int IdBaseItem { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int IdCategory { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual ICollection<Item> Items { get; } = new List<Item>();
}
