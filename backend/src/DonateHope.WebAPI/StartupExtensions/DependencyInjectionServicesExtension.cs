using DonateHope.Core.Mappers;
using DonateHope.Core.ServiceContracts.Authentication;
using DonateHope.Core.ServiceContracts.CampaignCommentServiceContracts;
using DonateHope.Core.ServiceContracts.CampaignContributionsServiceContracts;
using DonateHope.Core.ServiceContracts.CampaignRatingsServiceContracts;
using DonateHope.Core.ServiceContracts.CampaignReportsServiceContracts;
using DonateHope.Core.ServiceContracts.CampaignsServiceContracts;
using DonateHope.Core.ServiceContracts.Email;
using DonateHope.Core.ServiceContracts.HtmlTemplate;
using DonateHope.Core.Services.Authentication;
using DonateHope.Core.Services.CampaignCommentServices;
using DonateHope.Core.Services.CampaignCommentsServices;
using DonateHope.Core.Services.CampaignContributionServices;
using DonateHope.Core.Services.CampaignRatingServices;
using DonateHope.Core.Services.CampaignReportServices;
using DonateHope.Core.Services.CampaignsServices;
using DonateHope.Core.Services.EmailService;
using DonateHope.Core.Services.HtmlTemplate;
using DonateHope.Core.Validators.Authentication;
using DonateHope.Core.Validators.CampaignContribution;
using DonateHope.Core.Validators.CampaignRating;
using DonateHope.Core.Validators.CampaignReport;
using DonateHope.Domain.RepositoryContracts;
using DonateHope.Infrastructure.Data;
using DonateHope.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DonateHope.WebAPI.StartupExtensions;

public static class DependencyInjectionServicesExtension
{
    public static IServiceCollection DependencyInjectionServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.TryAddScoped<IJwtService, JwtService>();
        services.TryAddScoped<IAuthenticationService, AuthenticationService>();
        services.TryAddScoped<IEmailHtmlTemplateService, EmailHtmlTemplateService>();
        services.TryAddScoped<IEmailSenderService, EmailSenderService>();
        services.TryAddScoped<ISmtpClientProvider, SmtpClientProvider>();

        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<RegistrationAsCharityRequestValidator>();

        services.TryAddScoped<IDbConnectionFactory>(_ =>
        {
            // Debug: Print every environment variable to the console
            foreach (System.Collections.DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                Console.WriteLine($"ENV: {de.Key} = {de.Value}");
            }

            var dbHostDebug = Environment.GetEnvironmentVariable("DB_HOST");
            Console.WriteLine($"DB_HOST from Environment: {dbHostDebug}");

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            if (string.IsNullOrWhiteSpace(dbHost)) 
            {
                dbHost = "db"; // Match your docker-compose service name
            }
            var connectionString = $"Host={dbHost};Port=5432;Database=donate_hope;Username=dba;Password=admin123;";
            return new NpgsqlDbConnectionFactory(connectionString);
        });

        services.TryAddScoped<ICampaignsRepository, CampaignsRepository>();
        services.TryAddSingleton<CampaignMapper>();
        services.TryAddScoped<ICampaignCreateService, CampaignCreateService>();
        services.TryAddScoped<ICampaignRetrieveService, CampaignRetrieveService>();
        services.TryAddScoped<ICampaignUpdateService, CampaignUpdateService>();

        services.TryAddScoped<ICampaignContributionsRepository, CampaignContributionsRepository>();
        services.TryAddSingleton<CampaignContributionMapper>();
        services.TryAddScoped<
            ICampaignContributionCreateService,
            CampaignContributionCreateService
        >();
        services.TryAddScoped<
            ICampaignContributionRetrieveService,
            CampaignContributionRetrieveService
        >();
        services.TryAddScoped<
            ICampaignContributionUpdateService,
            CampaignContributionUpdateService
        >();
        services.TryAddScoped<
            ICampaignContributionDeleteService,
            CampaignContributionDeleteService
        >();
        services.AddValidatorsFromAssemblyContaining<CampaignContributionCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CampaignContributionUpdateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CampaignContributionDeleteRequestValidator>();

        services.TryAddScoped<ICampaignRatingsRepository, CampaignRatingsRepository>();
        services.TryAddSingleton<CampaignRatingMapper>();
        services.TryAddScoped<ICampaignRatingCreateService, CampaignRatingCreateService>();
        services.TryAddScoped<ICampaignRatingRetrieveService, CampaignRatingRetrieveService>();
        services.TryAddScoped<ICampaignRatingUpdateService, CampaignRatingUpdateService>();
        services.TryAddScoped<ICampaignRatingDeleteService, CampaignRatingDeleteService>();
        services.AddValidatorsFromAssemblyContaining<CampaignRatingCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CampaignRatingUpdateRequestValidator>();

        services.TryAddScoped<ICampaignReportsRepository, CampaignReportsRepository>();
        services.TryAddSingleton<CampaignReportMapper>();
        services.TryAddScoped<ICampaignReportCreateService, CampaignReportCreateService>();
        services.TryAddScoped<ICampaignReportRetrieveService, CampaignReportRetrieveService>();
        services.TryAddScoped<ICampaignReportUpdateService, CampaignReportUpdateService>();
        services.TryAddScoped<ICampaignReportDeleteService, CampaignReportDeleteService>();
        services.AddValidatorsFromAssemblyContaining<CampaignReportDeleteRequestValidator>();

        services.TryAddScoped<ICampaignCommentsRepository, CampaignCommentsRepository>();
        services.TryAddSingleton<CampaignCommentMapper>();
        services.TryAddScoped<ICampaignCommentCreateService, CampaignCommentCreateService>();
        services.TryAddScoped<ICampaignCommentRetrieveService, CampaignCommentRetrieveService>();
        services.TryAddScoped<ICampaignCommentUpdateService, CampaignCommentUpdateService>();
        services.TryAddScoped<ICampaignCommentDeleteService, CampaignCommentDeleteService>();

        return services;
    }
}
