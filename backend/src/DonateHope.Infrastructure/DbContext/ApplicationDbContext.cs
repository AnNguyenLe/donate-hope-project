using DonateHope.Domain.Entities;
using DonateHope.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DonateHope.Infrastructure.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<CampaignRating> CampaignRatings { get; set; }
    public DbSet<CampaignComment> CampaignComments { get; set; }
    public DbSet<CampaignContribution> CampaignContributions { get; set; }
    public DbSet<CampaignReport> CampaignReports { get; set; }
    public DbSet<CampaignLog> CampaignLogs { get; set; }
    public DbSet<CampaignStatus> CampaignStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>(b =>
        {
            b.ToTable("app_users");
        });

        modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
        {
            b.ToTable("app_user_claims");
        });

        modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.ToTable("app_user_logins");
        });

        modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.ToTable("app_user_tokens");
        });

        modelBuilder.Entity<AppRole>(b =>
        {
            b.ToTable("app_roles");
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(b =>
        {
            b.ToTable("app_role_claims");
        });

        modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.ToTable("app_user_roles");
        });

        // app_users table
        modelBuilder.Entity<AppUser>().HasKey(user => user.Id);
        modelBuilder
            .Entity<AppUser>()
            .Ignore(user => user.CampaignRatings)
            .Ignore(user => user.Campaigns)
            .Ignore(user => user.CampaignComments)
            .Ignore(user => user.CampaignContributions);

        // campaigns table
        modelBuilder.Entity<Campaign>().HasKey(c => c.Id);
        modelBuilder
            .Entity<Campaign>()
            .Ignore(c => c.User)
            .Ignore(c => c.CampaignRatings)
            .Ignore(c => c.CampaignComments)
            .Ignore(c => c.CampaignContributions)
            .Ignore(c => c.CampaignReports)
            .Ignore(c => c.CampaignLogs)
            .Ignore(c => c.CampaignStatus);

        modelBuilder
            .Entity<Campaign>()
            .HasOne<AppUser>()
            .WithMany(u => u.Campaigns)
            .HasForeignKey(c => c.UserId);

        modelBuilder
            .Entity<Campaign>()
            .HasOne<CampaignStatus>()
            .WithMany(cs => cs.Campaigns)
            .HasForeignKey(c => c.CampaignStatusId);

        // campaign_ratings table
        modelBuilder.Entity<CampaignRating>().HasKey(r => r.Id);
        modelBuilder.Entity<CampaignRating>().Ignore(cr => cr.Campaign).Ignore(cr => cr.User);
        modelBuilder
            .Entity<CampaignRating>()
            .HasOne<AppUser>()
            .WithMany(user => user.CampaignRatings)
            .HasForeignKey(cr => cr.UserId);
        modelBuilder
            .Entity<CampaignRating>()
            .HasOne<Campaign>()
            .WithMany(c => c.CampaignRatings)
            .HasForeignKey(cr => cr.CampaignId);

        // campaign_comments table
        modelBuilder.Entity<CampaignComment>().HasKey(cc => cc.Id);
        modelBuilder.Entity<CampaignComment>().Ignore(cc => cc.User).Ignore(cc => cc.Campaign);
        modelBuilder
            .Entity<CampaignComment>()
            .HasOne<AppUser>()
            .WithMany(user => user.CampaignComments)
            .HasForeignKey(cc => cc.UserId);
        modelBuilder
            .Entity<CampaignComment>()
            .HasOne<Campaign>()
            .WithMany(c => c.CampaignComments)
            .HasForeignKey(cc => cc.CampaignId);

        // campaign_contributions table
        modelBuilder.Entity<CampaignContribution>().HasKey(cc => cc.Id);
        modelBuilder.Entity<CampaignContribution>().Ignore(cc => cc.User).Ignore(cc => cc.Campaign);
        modelBuilder
            .Entity<CampaignContribution>()
            .HasOne<AppUser>()
            .WithMany(user => user.CampaignContributions)
            .HasForeignKey(cc => cc.UserId);
        modelBuilder
            .Entity<CampaignContribution>()
            .HasOne<Campaign>()
            .WithMany(c => c.CampaignContributions)
            .HasForeignKey(cc => cc.CampaignId);

        // campaign_reports table
        modelBuilder.Entity<CampaignReport>().HasKey(cr => cr.Id);
        modelBuilder.Entity<CampaignReport>().Ignore(cr => cr.Campaign);
        modelBuilder
            .Entity<CampaignReport>()
            .HasOne<Campaign>()
            .WithMany(c => c.CampaignReports)
            .HasForeignKey(cr => cr.CampaignId);

        // campaign_logs table
        modelBuilder.Entity<CampaignLog>().HasKey(cl => cl.Id);
        modelBuilder.Entity<CampaignLog>().Ignore(cl => cl.Campaign);
        modelBuilder
            .Entity<CampaignLog>()
            .HasOne<Campaign>()
            .WithMany(c => c.CampaignLogs)
            .HasForeignKey(cl => cl.CampaignId);

        // campaign_statuses table
        modelBuilder.Entity<CampaignStatus>().HasKey(cs => cs.Id);
    }
}
