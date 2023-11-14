using HH.Lms.Data.Interceptors;
using HH.Lms.Data.Library;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.AutoMapper;
using HH.Lms.Service.Library;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

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
        services.AddLogging();

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

        services.AddAutoMapper(typeof(Startup), typeof(AutoMapperProfile));
        services.AddScoped<BookRepository>();
        services.AddScoped<UserRepository>();
        services.AddScoped<BookService>();
        services.AddScoped<UserService>();

        string secret = Configuration.GetValue<string>("JWTSecret")!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration.GetValue<string>("JWTIssuer"),
                    ValidAudience = Configuration.GetValue<string>("JWTAudience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });
        services.AddAuthorization();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        // Authentication and Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
