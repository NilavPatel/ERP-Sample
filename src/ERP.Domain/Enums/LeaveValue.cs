using System.ComponentModel;

namespace ERP.Domain.Enums
{
    public enum LeaveValue
    {
        [Description("First Half")]
        FirstHalf = 0,
        [Description("Second Half")]
        SecondHalf = 1,
        [Description("Full Day")]
        FullDay = 2
    }
}