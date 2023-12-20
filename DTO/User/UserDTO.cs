using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using marketplace_backend.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using marketplace_backend.DTO.Address;

namespace marketplace_backend.DTO.User
{
    public class UserDTO
    {
        public int IdUser { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Role { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public AddressDTO Address { get; set; } = null!;
    }
}
