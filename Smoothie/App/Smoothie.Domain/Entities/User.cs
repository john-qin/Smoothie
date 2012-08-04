using System;
using Smoothie.Domain.Enums;

namespace Smoothie.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastLogin { get; set; }

        public AccountType AccountType { get; set; }
        public RoleType Roles { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string ThirdPartyId { get; set; }
        public Status Status { get; set; }
        public string Ip { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
