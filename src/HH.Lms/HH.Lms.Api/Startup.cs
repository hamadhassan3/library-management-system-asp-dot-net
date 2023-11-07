<<<<<<< Updated upstream
﻿using Microsoft.AspNetCore.Builder;
=======
﻿using HH.Lms.Data.Interceptors;
using HH.Lms.Data.Library;
using Microsoft.AspNetCore.Builder;
>>>>>>> Stashed changes
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HH.Lms.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
<<<<<<< Updated upstream
=======

            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<LibraryDBContext>(options => {
                    options.UseMySQL(connectionString);
                    options.AddInterceptors(new DatabaseInterceptor());
                });
            }
>>>>>>> Stashed changes
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
