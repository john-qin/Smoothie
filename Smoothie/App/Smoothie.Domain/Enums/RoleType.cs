using System;

namespace Smoothie.Domain.Enums
{
    [Flags]
    public enum RoleType
    {
        // Notice all powers of 2 so we can OR them to combine role permissions.
        // Link: http://stackoverflow.com/questions/4837103/asp-net-mvc-alternative-to-role-provider

        Administrator = 1,
        Member = 2,
        Staff = 4
    }
}