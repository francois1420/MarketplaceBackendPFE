using System;
using System.Collections.Generic;

namespace marketplace_backend.Models;

public partial class Address
{
    public int IdAddress { get; set; }

    public string StreetNumber { get; set; } = null!;

    public string? AppartmentNumber { get; set; }

    public string StreetName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
