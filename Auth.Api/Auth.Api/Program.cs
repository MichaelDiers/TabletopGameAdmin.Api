namespace Auth.Api
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                        var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                        webBuilder.UseUrls($"http://0.0.0.0:{port}");
                    });
        }

        public static void Main(string[] args)
        {
            Program.CreateHostBuilder(args).Build().Run();
        }
    }
}
