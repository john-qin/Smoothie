using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoothie.Domain.Enums
{
    [Flags]
    public enum Status
    {
        Deleted = -2,
        Banned = -1,
        Pending = 0,
        Approved = 1
    }
}
