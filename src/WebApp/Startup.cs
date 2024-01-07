using WebApp.Extensions;

namespace WebApp;

public class Startup
{
    private IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .ConfigureControllers()
            .ConfigureSwagger()
            .ConfigureRepositories()
            .ConfigureServices()
            .ConfigureMapper()
            .ConfigureValidation()
            .ConfigureDatabase(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseDeveloperExceptionPage();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Office API"));
        }
        
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
