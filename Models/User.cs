using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual ICollection<ItemUserView> ItemUserViews { get; } = new List<ItemUserView>();

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
