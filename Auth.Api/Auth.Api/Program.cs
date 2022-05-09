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
                        var portHttp = Environment.GetEnvironmentVariable("PORT") ?? "5003";
                        var portHttps = int.TryParse(portHttp, out var port) ? port + 1 : 5004;
                        webBuilder.UseUrls($"https://localhost:{portHttps}", $"http://localhost:{portHttp}");
                    });
        }

        public static void Main(string[] args)
        {
            Program.CreateHostBuilder(args).Build().Run();
        }
    }
}
