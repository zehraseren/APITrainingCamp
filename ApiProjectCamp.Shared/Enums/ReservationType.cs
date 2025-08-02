using System.ComponentModel;

namespace ApiProjectCamp.Shared.Enums;

public enum ReservationType
{
    [Description("Onaylandı")]
    Approved = 1,
    [Description("Beklemede")]
    Pending = 2,
    [Description("Reddedildi")]
    Rejected = 3
}
