using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var webHost = CreateHostBuilder(args).Build();
        
        await ApplyMigrations(webHost.Services);
        await webHost.RunAsync();
    }
    
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(web => web.UseStartup<Startup>());
    }
    
    private static async Task ApplyMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        await context.Database.MigrateAsync();
    }
}
