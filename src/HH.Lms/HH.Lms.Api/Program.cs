using HH.Lms.Common.Config;

namespace HH.Lms.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .ConfigureAppConfiguration((context, builder) =>
                        builder.CombineSettings(
                            context.HostingEnvironment.EnvironmentName,
                            context.HostingEnvironment.ContentRootPath,
                            @".."))
                    .UseStartup<Startup>();
            });
}
