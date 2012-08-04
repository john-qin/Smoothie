using System;

namespace Smoothie.Domain.Enums
{
    [Flags]
    public enum AccountType
    {
        Smoothie = 1,
        Facebook = 2,
        Twitter = 4
    }
}
