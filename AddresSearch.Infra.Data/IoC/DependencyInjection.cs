using AddressSearch.Infra.Data.Persistence;
using AddressSearch.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AddressSearch.Infra.Data.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<SqlServerConfiguration>(config.GetSection("SqlServer"));

        services.AddDbContext<AppDbContext>((sp, opts) =>
        {
            var cs = config.GetConnectionString("Default")
                     ?? sp.GetRequiredService<IOptions<SqlServerConfiguration>>().Value.ConnectionString
                     ?? Environment.GetEnvironmentVariable("ConnectionStrings__Default")
                     ?? Environment.GetEnvironmentVariable("SqlServer__ConnectionString");

            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException("Connection string não configurada (ConnectionStrings:Default ou SqlServer:ConnectionString).");

            var sqlOpts = sp.GetRequiredService<IOptions<SqlServerConfiguration>>().Value;
            opts.UseSqlServer(cs, sql =>
            {
                sql.EnableRetryOnFailure(sqlOpts.RetryCount, TimeSpan.FromSeconds(sqlOpts.RetryDelaySeconds), null);
            });
        });

        services.AddScoped<ILocalizacaoRepository, Repositories.LocalizacaoRepository>();
        services.AddScoped<IUnitOfWork, Repositories.UnitOfWork>();

        services.AddHttpClient<IViaCepClient, Http.ViaCepClient>(c =>
            c.BaseAddress = new Uri("https://viacep.com.br"));

        return services;
    }
}
