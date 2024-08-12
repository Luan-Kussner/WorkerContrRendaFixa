using WorkerContrRendaFixa.Data;
using Microsoft.EntityFrameworkCore;
using WorkerContrRendaFixa;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
