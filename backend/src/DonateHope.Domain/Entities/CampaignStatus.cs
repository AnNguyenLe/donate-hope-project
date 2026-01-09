namespace DonateHope.Domain.Entities;

public class CampaignStatus
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<Campaign>? Campaigns { get; set; }
}
