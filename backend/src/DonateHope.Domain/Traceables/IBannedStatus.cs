using System;

namespace DonateHope.Domain.Traceables;

public interface IBannedStatus
{
    public bool IsBanned { get; set; }
    public string? BannedStatusNote { get; set; }
}
