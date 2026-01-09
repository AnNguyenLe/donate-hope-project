namespace DonateHope.Domain.Traceables;

public interface IActiveStatus
{
    public bool IsActive { get; set; }
    public string? ActiveStatusNote { get; set; }
}
