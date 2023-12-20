using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class ItemUserView
{
    public int IdUser { get; set; }

    public int IdItem { get; set; }

    public DateTime DateView { get; set; }

    public virtual Item IdItemNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
