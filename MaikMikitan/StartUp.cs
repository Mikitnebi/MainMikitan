using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

public class Startup
{
    // Constructor to get IConfiguration if needed
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // ConfigureServices method for service registrations
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container...
        // For example, services.AddControllers();
    }

    // Configure method for middleware pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Add production error handling middleware if needed...
            // app.UseExceptionHandler("/Error");
        }

        // Configure middleware and routing...
        // For example, app.UseHttpsRedirection(), app.UseRouting(), etc.

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
