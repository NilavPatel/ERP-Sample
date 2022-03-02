using System.ComponentModel;

namespace ERP.Domain.Enums
{
    public enum UserStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("Blocked")]
        Blocked = 2,
        [Description("Blocked Due To Invalid Login Attempts")]
        BlockedDueToInvalidLoginAttempts = 3
    }
}