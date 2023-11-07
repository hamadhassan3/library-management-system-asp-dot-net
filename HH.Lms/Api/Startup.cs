using HH.Lms.Data.Interceptors;
using HH.Lms.Data.Library;
using Microsoft.EntityFrameworkCore;

namespace HH.Lms.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddControllers();
        if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<LibraryDBContext>(options => {
                options.UseMySQL(connectionString);
                options.AddInterceptors(new DatabaseInterceptor());
            });
        }

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
        

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
