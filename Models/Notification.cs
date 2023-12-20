using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Notification
{
    public int IdNotification { get; set; }

    public int IdUser { get; set; }

    public int? IdCart { get; set; }

    public string NotificationText { get; set; } = null!;

    public bool WasSeen { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Cart? IdCartNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
