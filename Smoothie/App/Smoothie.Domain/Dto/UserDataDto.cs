using System;

namespace Smoothie.Domain.Dto
{
    /// <summary>
    /// Extra user custom data, it's used for FormsAuthenticationTicket
    /// </summary>
    public class UserDataDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsAdministrator { get; set; }

    }
}
